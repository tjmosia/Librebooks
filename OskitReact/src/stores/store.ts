import { configureStore } from '@reduxjs/toolkit'
import IdentityReducer from '../slices/IdentitySlice'
import companyReducer from '../slices/CompanySlice'
import { useDispatch } from 'react-redux'

const store = configureStore({
	reducer: {
		identity: IdentityReducer,
		company: companyReducer
	}
})

export default store

export type IStoreState = ReturnType<typeof store.getState>
export type IAppDispatch = ReturnType<typeof useDispatch>