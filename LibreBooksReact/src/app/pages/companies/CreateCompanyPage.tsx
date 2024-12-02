import { Button, Divider, Field, Input, makeStyles, ProgressBar, Select, SelectOnChangeData, Spinner, Text, Textarea, tokens } from "@fluentui/react-components"
import { useAppSettings, usePageTitle } from "../../../hooks"
import { borders, breakpoints } from "../../../strings/ui"
import { ChangeEvent, FormEvent, useCallback, useEffect, useMemo, useState } from "react"
import { IFormField } from "../../../core/forms"
import { TbArrowRight } from "react-icons/tb"
import { Depths } from "@fluentui/react"
import { useValidators } from "../../../core/extensions"
import { MdBusiness } from "react-icons/md"
import { ajax } from "rxjs/ajax"
import { ApiRoutes } from "../../../strings"

interface ICompanyModelState {
    [key: string]: IFormField<string> | undefined
    legalName: IFormField<string>
    tradingName: IFormField<string>
    VATNumber: IFormField<string>
    regNumber: IFormField<string>
    physicalAddress: IFormField<string>
    postalAddress: IFormField<string>
    telephoneNumber: IFormField<string>
    emailAddress: IFormField<string>
    faxNumber: IFormField<string>,
    yearsInBusiness: IFormField<string>,
    businessSector: IFormField<string>,
}

interface BusinessSector {
    name: string
    id: string
}

export default function CreateCompanyPage() {
    usePageTitle("Create a company")
    const { emailvalidator } = useValidators()
    const styles = MakeCreateCompanyStyles()
    const [modelState, setModelState] = useState<ICompanyModelState>(initialModelState)
    const [loading, setLoading] = useState(false)
    const [sectors, setSectors] = useState<BusinessSector[]>([])
    const { createApiPath } = useAppSettings()

    function updateState(name: string, value: string) {
        setModelState(state => ({
            ...state,
            [name]: {
                value
            }
        }))
    }

    function onInputChange(changeEvent: ChangeEvent) {
        const { value, name } = (changeEvent.target instanceof HTMLInputElement)
            ? changeEvent.target as HTMLInputElement :
            changeEvent.target as HTMLTextAreaElement

        updateState(name, value)
    }

    function copyAddressToPostal() {
        if (modelState.physicalAddress.value)
            setModelState(state => ({
                ...state,
                postalAddress: {
                    value: state.physicalAddress.value
                }
            }))
    }

    function onEmailInputBlur() {
        if (!modelState.emailAddress.value)
            return

        if (modelState.emailAddress.error)
            return

        if (!emailvalidator.validate(modelState.emailAddress.value))
            setModelState(state => ({
                ...state,
                emailAddress: {
                    ...state.emailAddress,
                    error: "Enter a valid email address."
                }
            }))
    }

    function validateModel() {
        const model = { ...modelState }
        if (!modelState.legalName.value)
            model.legalName.error = "Registered Name if required."
        if (!modelState.physicalAddress.value)
            model.physicalAddress.error = "Physical Address is required."

        if (!modelState.postalAddress.value)
            model.postalAddress.error = "Postal Address is required."

        if (!modelState.emailAddress.value)
            model.emailAddress.error = "Email is required."

        if (!modelState.telephoneNumber.value)
            model.telephoneNumber.error = "Telephone is required."


        console.log(model)
        setModelState(model)

        return !(model.legalName.error || model.emailAddress.error || model.telephoneNumber.error || model.physicalAddress.error || model.postalAddress.error)
    }

    function onSubmit(submitEvent: FormEvent<HTMLFormElement>) {
        submitEvent.preventDefault()
        setLoading(true);
        if (!validateModel()) {
            setLoading(false)
            return
        }

        setTimeout(() => setLoading(false), 5000)
    }

    function onSelect(event: ChangeEvent<HTMLSelectElement>, data: SelectOnChangeData) {
        setModelState(state => ({
            ...state,
            [event.target.name]: {
                value: data.value
            }
        }))
    }

    const renderYearsInBusinessSelect = useMemo(() => {
        const values = [
            "0-1",
            "2-4",
            "5-9",
            "10+"
        ]

        return values.map(value => {
            return (<option value={value}> {value} Years</option>)
        })
    }, [])

    const renderSectorOptions = useCallback(() => {
        if (sectors.length > 0)
            return sectors.map(sector => {
                return (<option value={sector.id}> {sector.name}</option>)
            })
        else
            return ""
    }, [sectors])

    const fetchSectors = useCallback(() => {
        ajax<BusinessSector[]>({
            method: "GET",
            withCredentials: true,
            url: createApiPath(ApiRoutes.System.BusinessSector.GetAll)
        }).subscribe({
            next: (resp) => {
                console.log(resp)
                setSectors(resp.response ?? [])
            }
        })
    }, [setSectors, createApiPath])

    useEffect(() => {
        if (sectors.length < 1)
            fetchSectors()

    }, [fetchSectors, sectors])

    return <div className={styles.root}>
        <div className={styles.contentContainer}>
            {loading ? <ProgressBar /> : ""}
            <div className={styles.formLabel}>
                <MdBusiness size={24} /><Text size={400} weight="semibold">Create Your Company</Text>
            </div>
            <div className={styles.formWrapper}>
                <form className={styles.formElement} onSubmit={onSubmit}>
                    <div className={`row ${styles.formFields}`}>
                        <div className={`col-12 mt-3 mb-1`}>
                            <Divider>Business Information</Divider>
                        </div>
                        <div className="col-6">
                            <Field label="Registered Name"
                                validationState={modelState.legalName.error ? "error" : "none"}
                                validationMessage={modelState.legalName.error}>
                                <Input name="legalName"
                                    onChange={onInputChange}
                                    disabled={loading}
                                    value={modelState.legalName.value} />
                            </Field>
                        </div>
                        <div className="col-6">
                            <Field label="Trading Name"
                                validationState={modelState.tradingName.error ? "error" : "none"}
                                validationMessage={modelState.tradingName.error}>
                                <Input name="tradingName"
                                    onChange={onInputChange}
                                    disabled={loading}
                                    value={modelState.tradingName.value} />
                            </Field>
                        </div>
                        <div className="col-6">
                            <Field label="Registration Number"
                                validationState={modelState.regNumber.error ? "error" : "none"}
                                validationMessage={modelState.regNumber.error}>
                                <Input name="regNumber"
                                    onChange={onInputChange}
                                    disabled={loading}
                                    value={modelState.regNumber.value} />
                            </Field>
                        </div>
                        <div className="col-6">
                            <Field label="VAT Number"
                                validationState={modelState.VATNumber.error ? "error" : "none"}
                                validationMessage={modelState.VATNumber.error}>
                                <Input name="VATNumber"
                                    onChange={onInputChange}
                                    disabled={loading}
                                    value={modelState.VATNumber?.value} />
                            </Field>
                        </div>
                        <div className={`col-12 mt-3 mb-1`}>
                            <Divider>Addresses</Divider>
                        </div>
                        <div className="col-5">
                            <Field size="small"
                                label="Physical Address"
                                className={styles.addressField}
                                validationState={modelState.physicalAddress.error ? "error" : "none"}
                                validationMessage={modelState.physicalAddress.error}>
                                <Textarea className={styles.addressInput}
                                    name="physicalAddress"
                                    disabled={loading}
                                    value={modelState.physicalAddress.value}
                                    onChange={onInputChange}
                                    placeholder="Street Address / P.O Box &#10;Town/City&#10;State&#10;Postal Code" />
                            </Field>
                        </div>
                        <div className={`col-2 ${styles.addressShareWrapper}`}>
                            <div className={styles.addressShare}>
                                <Button onClick={copyAddressToPostal} icon={<TbArrowRight />}></Button>
                                <Text align="center" size={200}>Copy</Text>
                            </div>
                        </div>
                        <div className="col-5">
                            <Field size="small"
                                label="Postal Address"
                                className={styles.addressField}
                                validationState={modelState.postalAddress.error ? "error" : "none"}
                                validationMessage={modelState.postalAddress.error}>
                                <Textarea className={styles.addressInput}
                                    name="postalAddress"
                                    value={modelState.postalAddress.value}
                                    disabled={loading}
                                    onChange={onInputChange}
                                    placeholder="Street Address / P.O Box &#10;Town/City&#10;State&#10;Postal Code" />
                            </Field>
                        </div>

                        <div className={`col-12 mt-3 mb-1`}>
                            <Divider>Contact Information</Divider>
                        </div>
                        <div className="col-4">
                            <Field label="Email Address"
                                validationState={modelState.emailAddress.error ? "error" : "none"}
                                validationMessage={modelState.emailAddress.error}>
                                <Input name="emailAddress"
                                    onChange={onInputChange}
                                    disabled={loading}
                                    onBlur={onEmailInputBlur}
                                    value={modelState.emailAddress?.value} />
                            </Field>
                        </div>
                        <div className="col-4">
                            <Field label="Telephone"
                                validationState={modelState.telephoneNumber?.error ? "error" : "none"}
                                validationMessage={modelState.telephoneNumber.error}>
                                <Input name="telephoneNumber"
                                    onChange={onInputChange}
                                    disabled={loading}
                                    value={modelState.telephoneNumber.value} />
                            </Field>
                        </div>
                        <div className="col-4">
                            <Field label="Fax Number"
                                validationState={modelState.faxNumber?.error ? "error" : "none"}
                                validationMessage={modelState.faxNumber.error}>
                                <Input name="faxNumber"
                                    onChange={onInputChange}
                                    disabled={loading}
                                    value={modelState.faxNumber.value} />
                            </Field>
                        </div>
                        <div className={`col-12 mt-3 mb-1`}>
                            <Divider>Other Information</Divider>
                        </div>
                        <div className="col-4">
                            <Field label="Years in Business"
                                validationState={modelState.yearsInBusiness.error ? "error" : "none"}
                                validationMessage={modelState.yearsInBusiness.error}>
                                <Select value={modelState.yearsInBusiness.value}
                                    onChange={onSelect}>
                                    <option hidden></option>
                                    {renderYearsInBusinessSelect}
                                </Select>
                            </Field>
                        </div>
                        <div className="col-8">
                            <Field label="Business Sector"
                                className={styles.selectField}
                                validationState={modelState.businessSector?.error ? "error" : "none"}
                                validationMessage={modelState.businessSector.error}>
                                <Select value={modelState.businessSector.value}
                                    onChange={onSelect}
                                    className={styles.sectorSelect}>
                                    <option></option>
                                    {renderSectorOptions()}
                                </Select>
                            </Field>
                        </div>
                        <div className={`col-12 mt-3 mb-1`}>
                            <Divider />
                        </div>
                        <div className={`"col-12" mt-3`}>
                            <div className="row">
                                <div className="col-4">
                                    <Button type="button" disabled={loading} onClick={() => setModelState(initialModelState)} className={styles.submitBtn}>Reset Form</Button>
                                </div>
                                <div className="col-8">
                                    <Button appearance="primary" type="submit" className={styles.submitBtn} >{!loading ? "Create" : <Spinner size="tiny" />}</Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
}

const initialModelState: ICompanyModelState = {
    legalName: {
        value: ""
    },
    tradingName: {
        value: ""
    },
    VATNumber: {
        value: ""
    },
    regNumber: {
        value: ""
    },
    physicalAddress: {
        value: ""
    },
    postalAddress: {
        value: ""
    },
    telephoneNumber: {
        value: ""
    },
    yearsInBusiness: {
        value: ""
    },
    businessSector: {
        value: ""
    },
    faxNumber: {
        value: ""
    },
    emailAddress: {
        value: ""
    }
}

const MakeCreateCompanyStyles = makeStyles({
    root: {
        width: "100%",
        padding: tokens.spacingHorizontalL,
        height: "100%"
    },
    contentContainer: {
        width: breakpoints.large,
        margin: "0 auto",
        height: "100%",
        backgroundColor: tokens.colorNeutralBackground1,
        borderRadius: tokens.borderRadiusMedium,
        boxShadow: Depths.depth4,
        overflow: "hidden"
    },
    formWrapper: {
        padding: tokens.spacingHorizontalL,
        paddingBottom: tokens.spacingVerticalXXXL,
        borderTop: borders.thinNeutral,
        //minHeight: breakpoints.small
    },
    formElement: {
        maxWidth: breakpoints.medium,
        margin: "0 auto"
    },
    formFields: {
        rowGap: tokens.spacingVerticalS
    },
    formLabel: {
        backgroundColor: tokens.colorNeutralBackground1Hover,
        padding: tokens.spacingHorizontalL,
        display: "flex",
        columnGap: tokens.spacingHorizontalM,
        flexDirection: "row"
    },
    addressField: {
        display: "flex",
        flexDirection: "column",
        rowGap: tokens.spacingHorizontalS
    },
    addressInput: {
        height: "100px"
    },
    submitBtnWrapper: {

    },
    submitBtn: {
        width: "100%"
    },
    addressShareWrapper: {
        padding: "0"
    },
    addressShare: {
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        height: "100%"
    },
    selectField: {
        maxWidth: "100%",
    },
    sectorSelect: {
        width: "100%",
        overflow: "auto"
    }
})
