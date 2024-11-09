import {IAppUser} from './IAppUser'

export interface IIdentity
{
	user?: IAppUser
	isAuthenticated: boolean
}