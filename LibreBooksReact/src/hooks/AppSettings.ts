import { useContext } from "react"
import { AppSettingsParameters } from "../strings"
import { AppSettingsContext } from "../contexts"

export default function useAppSettings() {
    const context = useContext(AppSettingsContext)

    function createApiPath(pathName: string) {
        return AppSettingsParameters.ApiBasePath + pathName
    }

    return {
        apiBasePath: AppSettingsParameters.ApiBasePath,
        createApiPath,
        theme: context.theme,
        setTheme: context.setTheme
    }
}
