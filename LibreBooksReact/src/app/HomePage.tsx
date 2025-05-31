import { useState } from "react"
import { ICompanyData } from "../contexts"
import { Card, CardList } from "@blueprintjs/core"
import { CardHeader } from "@fluentui/react-components"



export default function HomePage() {
    const [companies, setCompanies] = useState<ICompanyData[]>([])

  return (<>
    <h1>Welcome home</h1>
    <CardList title="List of Companies">
      <Card>
        <CardHeader>Topspec Engineering</CardHeader>
      </Card>
    </CardList>
  </>)
}