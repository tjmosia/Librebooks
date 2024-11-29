
export default {
    Home: "/",
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
    Sales: {
        Quotes: "/sales/quotes",
        QuoteCreate: "/sales/quotes/create",
        QuoteView: "/sales/quote/:id",

        // ***********************************************************************
        Invoices: "/sales/invoices",
        InvoiceCreate: "/sales/invoices/create",
        InvoiceView: "/sales/invoices/:id",

        // ***********************************************************************
        CreditNotes: "/sales/credits",
        CreditNoteCreate: "/sales/credits/create",
        CreditNoteView: "/sales/credits/:id",

        // ***********************************************************************
        Receipts: "/sales/receipts",
        ReceiptCreate: "/sales/receipts",
        ReceiptView: "/sales/credits/:id",
        ReceiptAllocation: "/sales/receipts/allocate",
    }
}
