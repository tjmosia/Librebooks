import useSesionData from './useSessionData'

export default function useTempData() {
    const { tryGet, trySet, remove } = useSesionData()

    return {
        trySet<T>(key: string, value: T) {
            trySet<T>(key, value)
        },
        tryGet<T>(key: string) {
            return tryGet<T>(key, true)
        },
        remove
    }
}