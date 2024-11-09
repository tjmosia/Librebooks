import {createSlice} from '@reduxjs/toolkit'
import {IAppUser, IIdentity} from '../types/identity'
import IReducerPayloadAction from '../types/IReducerPayloadAction'

const IdentitySlice = createSlice({
	name: "identity",
	initialState: {
		isAuthenticated: false,
		user: undefined
	} as IIdentity,

	reducers: {
		SignIn (state, action: IReducerPayloadAction<IAppUser>)
		{
			state.isAuthenticated = true
			state.user = action.payload
		},
		SignOut (state)
		{
			state.user = undefined
			state.isAuthenticated = false
		}
	},

	selectors: {
		GetUser: (state) => state.user,
		GetIdentity: (state) => state
	}
})

export const {SignIn, SignOut} = IdentitySlice.actions
export const {GetUser, GetIdentity} = IdentitySlice.selectors
export default IdentitySlice.reducer

