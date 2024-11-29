import { useToastController } from "@fluentui/react-components"
import { createContext, useContext } from "react"


export interface IAppContext {
    toasterId: string
}

export const AppContext = createContext<IAppContext>({
    toasterId: ""
})

export function useAppContext() {
    const context = useContext(AppContext)
    const toaster = useToastController(context.toasterId)
    return {
        toaster
    }
}