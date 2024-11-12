import { useHttp } from "../../../hooks/http.hook";
import { useAppSettings } from "../../../strings/AppSettings";

export const AuthQueries = {

}

export function SendEmailVerificationQuery(username: string) {
    const { headers, mimeTypes } = useHttp()
    const { createApiPath } = useAppSettings()

    return fetch(createApiPath("/auth/verify/send-otp"), {
        method: "POST",
        body: JSON.stringify({ username }),
        headers: {
            [headers.contentType.name]: mimeTypes.Json
        }
    }).then(response => response.json())
}

export function GetUserQuery(username: string) {
    const { headers, mimeTypes } = useHttp()
    const { createApiPath } = useAppSettings()

    return fetch(createApiPath("/auth"), {
        method: "POST",
        body: JSON.stringify({ username }),
        headers: {
            [headers.contentType.name]: mimeTypes.Json
        }
    }).then(response => response.json())
}