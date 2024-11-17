function useTempData() {
	const createKey = (inputKey: string) => "TEMP_DATA__" + inputKey
	const remove = (inputKey: string) => sessionStorage.removeItem(createKey(inputKey))

	return {
		get<T>(key: string, persist = false): T | undefined {
			try {
				const value = sessionStorage.getItem(createKey(key))
				if (value) {
					if (!persist) remove(key)
					return JSON.parse(value) as T
				}
			} catch {
				return undefined
			}
		},
		add: (key: string, value: unknown) => sessionStorage.setItem(createKey(key), JSON.stringify(value))
	}
}

export default useTempData