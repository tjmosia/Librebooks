import { useEffect } from 'react'
import { GetIdentity } from '../../slices/identity.slice'
import { useSelector } from 'react-redux'
import { createSearchParams, Outlet, useLocation, useNavigate } from 'react-router-dom'

export default function EntryLayout() {
	const identity = useSelector(GetIdentity)
	const navigate = useNavigate()
	const location = useLocation()

	useEffect(() => {
		if (!identity.isAuthenticated)
			navigate({
				pathname: "/auth",
				search: createSearchParams({ returnUrl: location.pathname }).toString()
			})
	}, [identity, navigate, location])

	return <>
		<div>
			<h1>Hellow from Entry</h1>
			<Outlet />
		</div>
	</>
}