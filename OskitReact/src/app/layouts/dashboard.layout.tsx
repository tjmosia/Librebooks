import React, {FC, useEffect} from 'react'
import {GetIdentity} from '../../slices/identity.slice'
import {useSelector} from 'react-redux'
import {useLocation, useNavigate} from 'react-router'
import {createSearchParams} from 'react-router-dom'

const MainLayout: FC<{children: React.ReactElement}> = ({children}) =>
{
	const identity = useSelector(GetIdentity)
	const navigate = useNavigate()
	const location = useLocation()

	useEffect(() =>
	{
		if (!identity.isAuthenticated)
			navigate({
				pathname: "/auth/login",
				search: createSearchParams({returnUrl: location.pathname}).toString()
			})
	}, [identity, navigate, location])

	return <>
		{children}
	</>
}

export default MainLayout