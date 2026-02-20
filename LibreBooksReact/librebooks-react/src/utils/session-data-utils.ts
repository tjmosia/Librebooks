
export const SessionData = (() => {
    const removeItem = (key: string): void => sessionStorage.removeItem(key)
    const addItem = (key: string, data: string | number | object): void => sessionStorage.setItem(key, JSON.stringify(data))
    const getItem = (key: string) =>  {
        const item = sessionStorage.getItem(key) ?? undefined
        if(item)
        {
            try{
                return JSON.parse(item)
            } catch{
                return null
            }
        }
        else{
            return undefined
        }
    }
    const clear = () => {
        sessionStorage.clear()
    }

    return {
        addItem,
        getItem,
        removeItem,
        clear
    }
})();