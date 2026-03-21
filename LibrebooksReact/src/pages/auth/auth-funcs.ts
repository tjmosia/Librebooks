import { ajax } from "rxjs/ajax"
import type { ITransactionResult } from "../../core/http"
import { serverData } from "../../strings"
import type { IAuthRootMessage } from "./auth-layout-contexts"

export function sendEmailVerificationCode(

    email: string,
    setRootMessage?: (message?: IAuthRootMessage) => void,
    setResendingCode?: React.Dispatch<React.SetStateAction<boolean>>,
    targetRoute?: string,
    callbackOnSuccess?: () => void,
    callbackOnError?: () => void) {
    if (setResendingCode) setResendingCode(true)

    ajax<ITransactionResult<null>>({
        url: serverData.route("/verification/request"),
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            email: email,
            reason: serverData.verificationReasons.passwordReset
        })
    }).subscribe({
        next(response) {
            if (setResendingCode) setResendingCode(false)
            if (response.response.succeeded) {
                if (setRootMessage) {
                    setRootMessage({
                        message: "Verification code successfully sent! Please check your mailbox.",
                        intent: "success",
                        targetRoute: targetRoute
                    })
                }
                if (callbackOnSuccess)
                    callbackOnSuccess()
            } else {
                if (setRootMessage) {
                    setRootMessage({
                        message: response.response.errors[0]?.description || "Failed to send verification code. Please try again later.",
                        intent: "error",
                        targetRoute: targetRoute
                    })
                }

                if (callbackOnError)
                    callbackOnError()
            }
        },
        error(err) {
            if (setResendingCode) setResendingCode(false)

            if (callbackOnError)
                callbackOnError()

            if (err.status === 0) {
                if (setRootMessage) {
                    setRootMessage({
                        message: "Network error. Please check your connection and try again.",
                        intent: "error",
                        targetRoute: targetRoute
                    })
                }
            } else {
                if (setRootMessage) {
                    setRootMessage({
                        message: "An error occurred while sending the verification code. Please try again later.",
                        intent: "error",
                        targetRoute: targetRoute
                    })
                }
            }
        },
    })
}