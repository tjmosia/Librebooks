import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query'
import {AppSettings} from '../../strings/AppSettings'
import {IAppUser} from '../../types/identity'


const IdentityApi = createApi({
	reducerPath: "",
	baseQuery: fetchBaseQuery({baseUrl: AppSettings.ApiUrl}),
	endpoints: (builder) => ({
		getUserById: builder.query<IAppUser, string>({
			query: (userId: string) => `user/${userId}`
		})
	})
})

export const {getUserById} = IdentityApi.endpoints


const { } = getUserById("asd")
