import { RouterProvider } from 'react-router-dom'
import router from './router'
import { FluentProvider, tokens } from '@fluentui/react-components'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { useEffect, useState } from 'react'
import { AppThemes, IAppTheme } from './strings/AppThemes'
import { AppSettingsContext, IAppSettingsContext } from './contexts'

const queryClient = new QueryClient()


const AppSessionVars = {
	Theme: "OSKIT_APP_THEME"
}

function App() {
	const savedTheme = localStorage.getItem(AppSessionVars.Theme)
	const [theme, setTheme] = useState(savedTheme && AppThemes.themes.includes(savedTheme) ? savedTheme : "light")

	const appSettingsContext: IAppSettingsContext = {
		theme,
		setTheme: (theme: string) => {
			setTheme(theme)
			localStorage.setItem(AppSessionVars.Theme, theme)
		}
	}

	useEffect(() => {
		console.log(savedTheme)
		if (!savedTheme)
			localStorage.setItem(AppSessionVars.Theme, theme)
	}, [theme, savedTheme])

	return (
		<AppSettingsContext.Provider value={appSettingsContext}>
			<FluentProvider theme={(AppThemes[theme] as IAppTheme).theme}
				style={{
					height: "100vh",
					backgroundColor: tokens.colorNeutralBackground3Hover
				}}>
				<QueryClientProvider client={queryClient}>
					<RouterProvider router={router} />
				</QueryClientProvider>
			</FluentProvider>
		</AppSettingsContext.Provider>
	)
}

export default App
