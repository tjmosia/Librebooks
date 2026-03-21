import { FluentProvider } from '@fluentui/react-components';
import { useState } from "react";
import { RouterProvider, createBrowserRouter } from 'react-router';
import { IdentityContext, type IIdentityContext } from "./contexts/identity-context.ts";
import type { IClaimsPrincipal } from "./core/identity";
import { routes } from "./routes.ts";
import { lightTheme } from "./strings/theme.ts";
import './styles/App.css';

const router = createBrowserRouter(routes)

function App() {
  const [claimsPrincipal, setClaimsPrincipal] = useState<IClaimsPrincipal | undefined>(undefined)

  const identityContext: IIdentityContext = {
    claimsPrincipal: claimsPrincipal,
    setClaimsPrincipal: setClaimsPrincipal
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
