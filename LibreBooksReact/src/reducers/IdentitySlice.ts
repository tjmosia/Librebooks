import { createSlice } from '@reduxjs/toolkit'
import { IAppUser, IIdentity } from '../types/identity'
import IReducerPayloadAction from '../types/IReducerPayloadAction'

const IdentitySlice = createSlice({
	name: "identity",
	initialState: {} as IIdentity,
	reducers: {
		setUser(state, action: IReducerPayloadAction<IAppUser | undefined>) {
			state.user = action.payload
		},
		removeUser: (state) => {
			state.user = undefined
		}
	},
	selectors: {
		getUser: (state) => state.user
	}
})

export const { setUser, removeUser } = IdentitySlice.actions
export const { getUser } = IdentitySlice.selectors
export default IdentitySlice.reducer

