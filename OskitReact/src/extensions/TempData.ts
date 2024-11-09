function UseTempData<T> ()
{
	const createKey = (inputKey: string) => "TempData_" + inputKey
	const remove = (inputKey: string) => sessionStorage.removeItem(createKey(inputKey))

	return {
		Get (key: string, persist = false): T | undefined
		{
			try
			{
				const value = sessionStorage.getItem(createKey(key))
				if (value)
				{
					if (!persist) remove(key)
					return JSON.parse(value) as T
				}
			} catch
			{
				return undefined
			}
		},
		Add: (key: string, value: T) => sessionStorage.setItem(createKey(key), JSON.stringify(value))
	}
}

export default UseTempData