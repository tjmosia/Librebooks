import {Button, Field, Input} from "@fluentui/react-components";
import type {IFormField} from "../../../core/forms";
import {type ChangeEvent, useState, type SubmitEvent, useEffect, useContext} from "react";
import {AuthLayoutContext} from "../auth-layout-contexts.ts";
import {MdEmail} from "react-icons/md";

const initialModel: IFormField<string> = {
}

export function AuthEntryPage() {
    const {setLoading, setFormTitle, isLoading} = useContext(AuthLayoutContext)
    const [email, setEmail] = useState(initialModel);

    function onEmailInputChange(e: ChangeEvent<HTMLInputElement>) {
        setEmail({
            value: e.target.value,
        });
    }

    function onSubmit(e: SubmitEvent<HTMLFormElement>){
        e.preventDefault();
        alert("Submitted")
    }

    useEffect(() => {
        setFormTitle("Sign in or Sign Up")
        return () => {
            setLoading(false)
        }
    },[setFormTitle, setLoading])

    return<>
        <form onSubmit={onSubmit} className="fields authForm">
            <div className="field">
                <Field
                    label="Email"
                    validationState={email.error ? "error" : "none"}
                    validationMessage={email.error}>
                    <Input onChange={onEmailInputChange}
                           disabled={isLoading}
                           contentBefore={<MdEmail size={22} />}
                           value={email.value} />
                </Field>
            </div>
            <div className="field">
                <Button disabled={isLoading}
                        type="submit">Continue</Button>
            </div>
        </form>
        Auth Entry Works
    </>
}