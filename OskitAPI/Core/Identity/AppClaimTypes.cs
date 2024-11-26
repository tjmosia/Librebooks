namespace MacbooksAPI.Core.Identity
{
    public class AppClaimTypes
    {
        public static class Roles
        {
            public static string Master = "app/claims/roles/master";
            public static string Admin = "app/claims/roles/admin";
            public static string User = "app/claims/roles/user";
            public static string Accountant = "app/claims/roles/accountant";
        }

        public static class Modules
        {
            public static string Accounting = "app/claims/modules/accounting";
            public static string Inventory = "app/claims/modules/inventory";
            public static string Selling = "app/claims/modules/selling";
            public static string Purchasing = "app/claims/modules/purchasing";
        }
    }
}
