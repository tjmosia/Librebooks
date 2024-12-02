
export default {
    Home: "/",
    Dashboard: "/dashboard",
    Auth: {
        Username: "/auth",
        Login: "/auth/login",
        Register: "/auth/register",
        ForgottenPassword: "/auth/forgotten-password",
        ResetPassword: "/auth/reset-password",
        VerifyEmail: "/auth/verify",
    },
    Account: {
        Profile: "/account/personal-info",
        Logout: "/account/logout",
        ContactInfo: "/account/contact-info",
        ResetPassword: "/account/reset-password",
        ChangePassword: "/account/change-password",
        Deletion: "/account/deletion"
    },
    Companies: {
        Create: "/companies/create"
    },
    Customers: {
        Customers: "/customers",
        CustomerCreate: "/customers/create",
        CustomerEdit: "/customers/:id/edit",
        CustomerView: "/customers",

        // ***********************************************************************
        Quotes: "/sales/quotes",
        QuoteCreate: "/customers/quotes/create",
        QuoteEdit: "/customers/quotes/:id/edit",
        QuoteView: "/customers/quote/:id",

        // ***********************************************************************
        Invoices: "/customers/invoices",
        InvoiceCreate: "/customers/invoices/create",
        InvoiceView: "/customers/invoices/:id",
        InvoiceEdit: "/customers/invoices/:id/edit",

        // ***********************************************************************
        CreditNotes: "/customers/credits",
        CreditNoteCreate: "/customers/credits/create",
        CreditNoteView: "/customers/credits/:id",

        // ***********************************************************************
        Receipts: "/customers/receipts",
        ReceiptCreate: "/customers/receipts",
        ReceiptView: "/customers/credits/:id",
        ReceiptAllocation: "/customers/receipts/allocate",
    },
    Suppliers: {
        Suppliers: "/suppliers",
        SupplierCreate: "/suppliers/create",
        SupplierView: "/suppliers/:id",
        SupplierEdit: "/suppliers/:id/edit",

        // ***********************************************************************
        Orders: "/suppliers/orders",
        OrderView: "/suppliers/orders/:id",
        OrderCreate: "/suppliers/orders/create",
        OrderEdit: "/suppliers/orders/:id/edit",

        // ***********************************************************************
        Invoices: "/suppliers/invoices",
        InvoiceCreate: "/suppliers/invoices/create",
        InvoiceView: "/suppliers/invoices/:id",

        // ***********************************************************************
        Return: "/suppliers/returns",
        ReturnCreate: "/suppliers/returns/create",
        ReturnView: "/suppliers/returns/:id",

        // ***********************************************************************
        Payment: "/suppliers/payments",
        PaymentCreate: "/suppliers/payments/create",
        PaymentView: "/suppliers/payments/:id",
        PaymentAllocation: "/suppliers/payments/allocations",
    },
    Inventory: {
        Items: "/items",
        ItemView: "/items/:id",
        ItemCreate: "/items/create",
        ItemEdit: "/items/:id/edit",

        // ***********************************************************************
        ItemAdjustments: "/items/adjustments",
        ItemAdjustmentsCreate: "/items/adjustments/create",
        ItemAdjustmentsView: "/items/adjustments/:id/view",
    },
    Banking: {
        Journal: "/accounting/journal",
        BankAccounts: "/accounting/bank-accounts",
        BankAccountCreate: "/accounting/bank-accounts/create",
        BankAccountEdit: "/accounting/bank-accounts/Edit",
        ChartOfAccounts: "/accounting/accounts",
        ledger: "/accounting/ledger",
    },
}
