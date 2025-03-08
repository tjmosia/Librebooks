import { FluentProvider, tokens } from '@fluentui/react-components'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { useEffect, useState } from 'react'
import { AppThemes, IAppTheme } from './strings/AppThemes'
import { AppSettingsContext, IAppSettingsContext } from './contexts'
import Routing from './Routing'

const queryClient = new QueryClient()

const AppSessionVars = {
	Theme: "OSKIT_APP_THEME"
}

function App() {
	const savedTheme = localStorage.getItem(AppSessionVars.Theme)
	const [theme, setTheme] = useState(savedTheme && AppThemes.themes.includes(savedTheme) ? savedTheme : "dark")

	const appSettingsContext: IAppSettingsContext = {
		theme,
		setTheme: (theme: string) => {
			setTheme(theme)
			localStorage.setItem(AppSessionVars.Theme, theme)
		}
	}

	useEffect(() => {
		if (!savedTheme)
			localStorage.setItem(AppSessionVars.Theme, theme)
	}, [theme, savedTheme])

	return (
		<AppSettingsContext.Provider value={appSettingsContext}>
			<FluentProvider theme={(AppThemes[theme] as IAppTheme).theme}
				style={{
					minHeight: "100vh",
					backgroundColor: tokens.colorNeutralBackground1Pressed
				}}>
				<QueryClientProvider client={queryClient}>
					<Routing />
				</QueryClientProvider>
			</FluentProvider>
		</AppSettingsContext.Provider>
	)
}

export default App
