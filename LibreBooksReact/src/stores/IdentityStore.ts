import { create } from 'zustand'
import { IAppUser } from '../types/identity'

export interface IIdentityStore {
    user?: IAppUser
    updateUser: (user: IAppUser) => void
    deleteUser: () => void
}

const useIdentityStore = create<IIdentityStore>(set => ({
    user: undefined,
    updateUser: (user: IAppUser) => {
        console.log(user)
        set(({ user }))
    },
    deleteUser: () => set({ user: undefined })
}))

export default useIdentityStore