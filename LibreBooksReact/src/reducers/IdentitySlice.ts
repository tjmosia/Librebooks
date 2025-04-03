import { IUser } from "../core/identity"
import { createSlice, type PayloadAction as IPayloadAction } from "@reduxjs/toolkit"


interface IIdentityStore {
    user: IUser | null
}

const initialState: IIdentityStore = {
    user: null
}

const identitySlice = createSlice({
    name: 'identity',
    initialState,
    reducers: {
        login(state, action: IPayloadAction<IUser>) {
            state.user = action.payload
        },
        logout(state) {
            state.user = null
        },
        updateUser(state, action: IPayloadAction<IUser>) {
            state.user = action.payload
        }
    }
})


export const identityActions = identitySlice.actions

export default identitySlice.reducer