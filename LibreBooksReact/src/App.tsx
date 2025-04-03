import './App.scss'
import Routing from './routing'
import { BrowserRouter } from 'react-router'
import { BlueprintProvider } from "@blueprintjs/core";

function App() {
    return (
        <>
            <BlueprintProvider>
                <BrowserRouter>
                    <Routing />
                </BrowserRouter>
            </BlueprintProvider>
        </>
    )
}

export default App
