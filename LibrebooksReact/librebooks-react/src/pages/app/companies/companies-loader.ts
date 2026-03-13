import type { ICompany } from "../../../core/companies";
import { serverData } from "../../../strings";

export async function LoadCompaniesAsync(): Promise<ICompany[]> {
    const response = await fetch(serverData.route("/companies"), {
        headers: {
            "Content-Type": "application/json"
        }
    })

    if (response.status == 200) {
        return await response.json()
    } else {
        return []
    }
}