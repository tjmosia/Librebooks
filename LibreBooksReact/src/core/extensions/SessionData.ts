function useSessionData() {
	const createKey = (inputKey: string) => "SESSION_DATA__" + inputKey

	return {
		get: <T>(key: string, persist = true): T | null => {
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

			return value as T

		},
		add: (key: string, value: object | string | number | boolean) =>
			sessionStorage.setItem(createKey(key), JSON.stringify(value)),

		addRange: (items: { key: string, value: object | string | number | boolean }[]) => {
			if (items.length > 0)
				items.forEach((item) => {
					sessionStorage.setItem(item.key, JSON.stringify(item.value))
				})
		},

		remove: (key: string) => sessionStorage.removeItem(createKey(key)),

		removeRange: (keys: string[]) => {
			if (keys.length < 1)
				return
			keys.forEach((key) => {
				sessionStorage.removeItem(key)
			})
		}
	}
}

export default useSessionData