import {SessionData} from "./session-data-utils.ts";

export const TempData = (()=>{
    const addItem = (key: string, data: string): void => SessionData.addItem(key,data)

    const getItem = (key: string) => {
        const item= SessionData.getItem(key)
        if(item) SessionData.removeItem(key)
        return item
    }

    return {
        addItem,
        getItem
    }
})()