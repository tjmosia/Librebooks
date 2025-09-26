import { useEffect, useState } from "react"
import { ICompanyData } from "../contexts"
import { Button, Card, CardList, Dialog, DialogFooter, EntityTitle, H5, Overlay, Overlay2, Section, SectionCard, Text } from "@blueprintjs/core"
import './Home.scss'
import NewCompanyWizardComponent from '../components/new-company-form/NewCompanyWizardComponent'
import { DialogBody } from '@fluentui/react-components'


export default function HomePage() {
    const [companies, setCompanies] = useState<ICompanyData[]>([])
    const [newCompanyDialogOpen, setNewCompanyDialogOpen] = useState<boolean>(false)

    const toggleCompanyDialog = () => setNewCompanyDialogOpen(false)

    const renderCompanyCards = () => {
        if (companies.length > 0) {
            return companies.map((company) => (
                <Card key={company.id} interactive>
                    <EntityTitle title={company.name} icon="shop" />
                </Card>))
        }

        return (
            <Card>
                <Text>No Companies Found.</Text>
            </Card>
        )
    }

    useEffect(() => {
        setCompanies([
            {
                id: "UJKIANSDJANDLASD",
                logo: "https://picsum.photos/200/300?random=1",
                name: "Valve Supply Line",
            },
            {
                id: "JKASDLAKSDASKDN",
                logo: "https://picsum.photos/200/300?random=1",
                name: "Motocoil Electric Motors"
            }
        ])
    }, [])

    return (<>
        <div className="homePage-Container">
            <Section className="companyListContainer"
                title="Companies"
                subtitle="This is the list of companies you're linked to." >
                <SectionCard padded={false}>
                    <CardList title="List of Companies" bordered={false}>
                        {renderCompanyCards()}
                        <Card >
                            <Button onClick={() => setNewCompanyDialogOpen(true)} intent="primary" icon="plus">Add Company</Button>

                        </Card>
                    </CardList>
                </SectionCard>
            </Section>
        </div>
    </>)
}