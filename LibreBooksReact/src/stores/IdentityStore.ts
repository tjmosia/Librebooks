
import { configureStore } from "@reduxjs/toolkit";
import identitySlice from '../reducers/identitySlice'
import { useDispatch, useSelector } from "react-redux";

export const store = configureStore({

    reducer: {
        identity: identitySlice
    }
})


export type IIdentityState = ReturnType<typeof store.getState>
export type IAppDispatch = typeof store.dispatch

export const useAppDispatch = useDispatch.withTypes<IAppDispatch>()
export const useAppSelector = useSelector.withTypes<IIdentityState>()