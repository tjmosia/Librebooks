import { createContext } from "react"

export const AppSettingsContext = createContext<IAppSettingsContext>({})

export interface IAppSettingsContext {
    theme?: string,
    setTheme?: (theme: string) => void
}

export default AppSettingsContext