import { RouteObject } from "react-router"
import ItemsPage from "./ItemsPage"
import CreateItemPage from "./CreateItemPage"
import ItemAdjustmentsPage from "./ItemAdjustmentsPage"
import CreateItemAdjustmentPage from "./CreateItemAdjustmentPage"
import ItemCategoriesPage from "./ItemCategoriesPage"
import CreateItemCategoryPage from "./CreateItemCategoryPage"

const InventoryRoutes: RouteObject[] = [
    {
        path: "inventory/items",
        element: <ItemsPage />
    },
    {
        path: "inventory/items/create",
        element: <CreateItemPage />
    },
    {
        path: "inventory/adjustments",
        element: <ItemAdjustmentsPage />
    },
    {
        path: "inventory/adjustments/create",
        element: <CreateItemAdjustmentPage />
    },
    {
        path: "inventory/categories",
        element: <ItemCategoriesPage />
    },
    {
        path: "inventory/categories/create",
        element: <CreateItemCategoryPage />
    },
]

export default InventoryRoutes