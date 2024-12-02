import { Button, makeStyles, Toolbar } from "@fluentui/react-components";

interface ICustomersToolBarComponentProps {
    styling: ReturnType<typeof makeStyles>
}

export default function CustomersToolBarComponent({ styling }: ICustomersToolBarComponentProps) {
    const styles = styling()
    return (
        <div className={styles.toolbarWrapper}>
            <Toolbar className={styles.toolbar}>
                <Button appearance='subtle' shape="square">Customers</Button>
                <Button appearance='subtle'>Quotes</Button>
                <Button appearance='subtle'>Orders</Button>
                <Button appearance='subtle'>Invoices</Button>
                <Button appearance='subtle'>Receipts</Button>
                <Button appearance='subtle'>Reports</Button>
            </Toolbar>
        </div>
    )
}