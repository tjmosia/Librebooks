import { useLoaderData } from "react-router"
import type { ICompany } from "../../../../core/companies"




export function CompaniesListPage() {
    const { companies } = useLoaderData() as { companies: ICompany[] }
    console.log(companies)
    return (<div>
        <h1>Companies List Works</h1>
        <div className="">
            <div>List of Companies</div>
        </div>
        {
            companies.map(company => (
                <div key={company.id}>{company.name}</div>
            ))
        }
    </div>)
}