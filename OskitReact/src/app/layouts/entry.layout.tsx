import { useEffect } from 'react'
import { useDispatch } from 'react-redux'
import { createSearchParams, Outlet, useLocation, useNavigate } from 'react-router-dom'
import useSessionData from '../../extensions/SessionData'
import { IAppUser } from '../../types/identity'
import { SessionVariables } from '../../strings/SessionVars'
import { Button } from '@fluentui/react-components'
import { useIdentityManager } from '../../hooks/userManager.hook'

export default function EntryLayout() {
	const identityManager = useIdentityManager()
	const navigate = useNavigate()
	const location = useLocation()
	const session = useSessionData()
	const user = session.get<IAppUser>(SessionVariables.IdentityUser)
	const dispatch = useDispatch()

	useEffect(() => {
		if (!identityManager.isSignedIn()) {
			if (user)
				identityManager.signIn(user)
			else
				navigate({
					pathname: "/auth",
					search: createSearchParams({ returnUrl: location.pathname }).toString()
				})
		}
	}, [identityManager, navigate, location, dispatch, user])

	function handleButtonClick() {
		identityManager.confirmSignIn()
	}

	return <>
		<div>
			<h1>Welcome back, {identityManager.getUser()?.firstName}</h1>
			<Outlet />
			<p>You have been isAuthenticated</p>
			<Button onClick={handleButtonClick} appearance='primary'>Confirm Sign In</Button>
		</div>
	</>
}