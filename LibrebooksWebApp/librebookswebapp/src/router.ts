import { createBrowserRouter } from 'react-router-dom'
import routes from './routing'

const router = createBrowserRouter(routes, {
	future: {
		v7_relativeSplatPath: true,
	},
})

export default router