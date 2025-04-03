
export default function useSessionData() {
    return {
        set(key: string, value: string) {
            sessionStorage.setItem(key, value);
        },
        get(key: string, persist = true) {
            const value = sessionStorage.getItem(key)
            if (value && !persist)
                sessionStorage.removeItem(key)
            return value
        },
        trySet<T>(key: string, value: T) {
            sessionStorage.setItem(key, JSON.stringify(value))
        },
        tryGet<T>(key: string, persist = true) {
            try {
                const value = sessionStorage.getItem(key)
                if (value) {
                    if (!persist)
                        sessionStorage.removeItem(key)
                    return JSON.parse(value) as T
                }
            } catch {
                return null
            }
        },
        remove(key: string) {
            sessionStorage.removeItem(key)
        }
    }
}