
import { useDispatch, useSelector } from 'react-redux'
import { getCompany, setCompany } from './../reducers'
import ICompany from '../types/ICompany'

export default function useCompanyManager() {
    const currentCompany = useSelector(getCompany)
    const dispatch = useDispatch()

    return {
        getCompany: () => currentCompany,
        setCompany: (company: ICompany) => dispatch(setCompany(company)),
        deleteCompany: () => dispatch(setCompany(undefined))
    }
}