function UseSessionData<T> ()
{
	const createKey = (inputKey: string) => "SessionData_" + inputKey

	return {
		Get: (key: string) =>
		{
			try
			{
				const value = sessionStorage.getItem(createKey(key))
				if (value)
					return JSON.parse(value) as T
			} catch
			{
				return undefined
			}

		},
		Add: (key: string, value: T) => sessionStorage.setItem(createKey(key), JSON.stringify(value)),
		Remove: (key: string) => sessionStorage.removeItem(createKey(key))
	}
}

export default UseSessionData