import { Theme, webDarkTheme, webLightTheme } from "@fluentui/react-components"


export interface IAppTheme {
    name: string
    theme: Theme
}

interface IAppThemes {
    [key: string]: IAppTheme | string[]
    light: IAppTheme,
    dark: IAppTheme,
    themes: string[]
}

export const AppThemes: IAppThemes = {
    light: {
        name: "light",
        theme: webLightTheme
    },
    dark: {
        name: "dark",
        theme: webDarkTheme
    },
    themes: [
        "light",
        "dark"
    ]
}