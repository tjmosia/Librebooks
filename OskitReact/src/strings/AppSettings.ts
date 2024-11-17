
export function useAppSettings() {
	const apiBasePath = "http://localhost:5262/api"
	function createApiPath(pathName: string) {
		return apiBasePath + pathName
	}

	return {
		apiBaseUrl: apiBasePath,
		createApiPath
	}
}
