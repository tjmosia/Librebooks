import {useDispatch, useSelector} from 'react-redux'
import type {IAppDispatch, IStoreState} from '../stores/store'

export const useAppDispatch = useDispatch<IAppDispatch>
export const useAppSelector = useSelector<IStoreState>

