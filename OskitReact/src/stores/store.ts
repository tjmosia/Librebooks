import { configureStore } from '@reduxjs/toolkit'
import IdentityReducer from '../slices/identity.slice'
import companyReducer from '../slices/company.slice'

const store = configureStore({
	reducer: {
		identity: IdentityReducer,
		company: companyReducer
	}
})

export default store

export type IStoreState = ReturnType<typeof store.getState>
export type IAppDispatch = typeof store.dispatch