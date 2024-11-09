import {configureStore} from '@reduxjs/toolkit'
import IdentityReducer from '../slices/identity.slice'
import CompaniesReducer from '../slices/companies.slice'

const store = configureStore({
	reducer: {
		identity: IdentityReducer,
		companies: CompaniesReducer
	}
})

export default store

export type IStoreState = ReturnType<typeof store.getState>
export type IAppDispatch = typeof store.dispatch