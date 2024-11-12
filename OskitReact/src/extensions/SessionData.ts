function useSessionData() {
	const createKey = (inputKey: string) => "SESSION_DATA__" + inputKey

	return {
		get: <T>(key: string, persist = true): T | string | null => {

			const value = sessionStorage.getItem(createKey(key))

			try {
				if (value) {
					if (!persist)
						sessionStorage.removeItem(createKey(key))
					return JSON.parse(value) as T
				}
				// eslint-disable-next-line no-empty
			} catch {
			}
			return value

		},
		add: <T>(key: string, value: T) => sessionStorage.setItem(createKey(key), JSON.stringify(value)),
		remove: (key: string) => sessionStorage.removeItem(createKey(key))
	}
}

export default useSessionData