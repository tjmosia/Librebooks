import {useContext, useEffect} from "react";
import {AuthLayoutContext} from "../auth-layout-contexts.ts";
import {Field, Input} from "@fluentui/react-components";

export function LoginPage() {
    const {setFormTitle} = useContext(AuthLayoutContext)

    useEffect(() => {
        setFormTitle('Login')
    },[setFormTitle])

    return(
        <form>
            <div>
                <Field>
                    <Input />
                </Field>
            </div>
        </form>
    )
}