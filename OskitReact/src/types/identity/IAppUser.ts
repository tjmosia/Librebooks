import IClaim from './IClaim'

export interface IAppUser
{
	username?: string
	givenName?: string
	isAuthenticated: boolean
	accessToken?: string
	claims?: IClaim[]
}