import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import { Provider } from 'react-redux'
import store from './stores/store'
import { FluentProvider, webLightTheme } from '@fluentui/react-components'
import './main.tsx.css'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

const queryClient = new QueryClient()

createRoot(document.getElementById('root')!).render(
	<StrictMode>
		<Provider store={store}>
			<FluentProvider theme={webLightTheme}
				style={{
					height: "100vh"
				}}>
				<QueryClientProvider client={queryClient}>
					<App />
				</QueryClientProvider>
			</FluentProvider>
		</Provider>
	</StrictMode >,
)
