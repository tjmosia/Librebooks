import { create } from 'zustand'
import { IAppUser } from '../types/identity'

export interface IIdentityStore {
    user?: IAppUser
    setUser: (user: IAppUser) => void
    deleteUser: () => void
}

const useIdentityStore = create<IIdentityStore>(set => ({
    user: undefined,
    setUser: (user: IAppUser) => {
        set(({ user }))
    },
    deleteUser: () => set({ user: undefined })
}))
export default useIdentityStore