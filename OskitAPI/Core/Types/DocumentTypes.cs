namespace MacbooksAPI.Core.Types
{
    public static class DocumentTypes
    {
        public static class Sales
        {
            public static string Quote = "doc/sales/quote";
            public static string Order = "doc/sales/order";
            public static string Invoice = "doc/sales/invoice";
            public static string Credit = "doc/sales/credit";
            public static string Receipt = "doc/sales/receipt";
        }

        public static class Purchases
        {
            public static string Order = "doc/purchases/order";
            public static string Invoice = "doc/purchases/invoice";
            public static string Return = "doc/purchases/return";
            public static string Receipt = "doc/purchases/receipt";
        }

    }
}
