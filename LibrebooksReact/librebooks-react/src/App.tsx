import {RouterProvider, createBrowserRouter} from 'react-router'
import './styles/App.css'
import {routes} from "./routes.ts";
import {useState} from "react";
import type {IUser} from "./core/identity";
import {IdentityContext, type IIdentityContext} from "./contexts/identity-context.ts";
import {FluentProvider} from '@fluentui/react-components'
import {lightTheme} from "./strings/theme.ts";

const router = createBrowserRouter(routes)

function App() {
  const [user, setUser] = useState<IUser | undefined>();

  const identityContext: IIdentityContext =  {
    getUser: () => user,
    setUser: (user: IUser) => setUser(user),
    removeUser: () => setUser(undefined)
  }
    return (
      <IdentityContext.Provider value={identityContext}>
        <FluentProvider theme={lightTheme}>
          <RouterProvider router={router} />
        </FluentProvider>
      </IdentityContext.Provider>
  )
}

export default App
