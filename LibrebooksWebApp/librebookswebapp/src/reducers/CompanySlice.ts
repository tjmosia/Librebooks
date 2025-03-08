import { createSlice } from '@reduxjs/toolkit'
import IReducerPayloadAction from '../types/IReducerPayloadAction'
import ICompany from '../types/ICompany'

interface ICompaniesState {
	company?: ICompany
}

const CompanySlice = createSlice({
	name: "company",
	initialState: {
		company: undefined
	} as ICompaniesState,
	reducers: {
		setCompany: (state, action: IReducerPayloadAction<ICompany | undefined>) => {
			state.company = action.payload
		}
	},
	selectors: {
		getCompany: state => state.company
	}
})

export const { setCompany } = CompanySlice.actions
export const { getCompany } = CompanySlice.selectors
export default CompanySlice.reducer