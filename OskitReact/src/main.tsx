import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import { Provider } from 'react-redux'
import store from './stores/store'
import { FluentProvider, webLightTheme } from '@fluentui/react-components'
import './main.tsx.css'

createRoot(document.getElementById('root')!).render(
	<StrictMode>
		<Provider store={store}>
			<FluentProvider
				style={{
					height: "100vh"
				}}
				theme={webLightTheme}>
				<App />
			</FluentProvider>
		</Provider>
	</StrictMode >,
)
