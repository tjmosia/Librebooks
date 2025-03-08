import { configureStore } from '@reduxjs/toolkit'
import { useDispatch } from 'react-redux'
import { companyReducer, identityReducer } from '../reducers'

const store = configureStore({
	reducer: {
		identity: identityReducer,
		company: companyReducer
	}
})

export default store

export type IStoreState = ReturnType<typeof store.getState>
export type IAppDispatch = ReturnType<typeof useDispatch>