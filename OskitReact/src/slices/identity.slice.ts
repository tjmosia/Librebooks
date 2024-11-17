import { createSlice } from '@reduxjs/toolkit'
import { IAppUser, IIdentity } from '../types/identity'
import IReducerPayloadAction from '../types/IReducerPayloadAction'



const IdentitySlice = createSlice({
	name: "identity",
	initialState: {
		isAuthenticated: false
	} as IIdentity,

	reducers: {
		signIn(state, action: IReducerPayloadAction<IAppUser>) {
			state.isAuthenticated = true
			state.user = action.payload
		},
		signOut(state) {
			state.user = undefined
			state.isAuthenticated = false
		}
	},
	selectors: {
		getUser: (state) => state.user,
		getIdentity: (state) => state
	}
})

export const { signIn, signOut } = IdentitySlice.actions
export const { getUser, getIdentity } = IdentitySlice.selectors
export default IdentitySlice.reducer

