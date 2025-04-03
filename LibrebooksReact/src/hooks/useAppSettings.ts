
export default function useAppSettings() {
    const apiBaseUrl = "https://localhost:5262"

    function getApiUrl(routeName: string) {
        return apiBaseUrl + routeName
    }

    return {
        apiBaseUrl, getApiUrl
    }
}