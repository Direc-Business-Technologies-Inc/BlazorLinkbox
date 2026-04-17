using Domain.Entities.System;
using System;

namespace Database.MsSql.Configurations;

public static class WebStructure
{
    public static readonly IEnumerable<NavigationRouteDEM> ParentRouteList =
    [
        NavigationRouteDEM.New(
            name: "Dashboard",
            protectedRoute: true,
            position: 0,
            icon: "dashboard",
            uri: "/dashboard"),

        NavigationRouteDEM.New(
            name: "Administration",
            position: 1,
            icon: "discover_tune"),

        NavigationRouteDEM.New(
            name: "Configuration",
            position: 2,
            icon: "manufacturing"),

        NavigationRouteDEM.New(
            name: "Feature",
            position: 3,
            icon: "app_registration"),

        NavigationRouteDEM.New(
            name: "Uploader",
            protectedRoute: true,
            position: 4,
            icon: "upload_file",
            uri: "/uploader"),

        NavigationRouteDEM.New(
            name: "Error Logs",
            protectedRoute: true,
            position: 5,
            icon: "error",
            uri: "/error-logs"),

        /*NavigationRouteDEM.New(
            name: "Transaction",
            position: 3,
            icon: "contract"),*/
    ];

    public static readonly IEnumerable<NavigationRouteDEM> SubRouteList1 =
    [
        NavigationRouteDEM.New(
            name: "User",
            position: 0,
            icon: "manage_accounts",
            parentId: ParentRouteList!.First(x => x.Name.Equals("Administration")).Id),
        NavigationRouteDEM.New(
            name: "Settings",
            position: 1,
            icon: "settings",
            parentId: ParentRouteList!.First(x => x.Name.Equals("Administration")).Id),


        NavigationRouteDEM.New(
            name: "Setup",
            position: 0,
            icon: "build_circle",
            parentId: ParentRouteList!.First(x => x.Name.Equals("Configuration")).Id),
        NavigationRouteDEM.New(
            name: "Field Mapping",
            protectedRoute: true,
            position: 1,
            icon: "flowchart",
            parentId: ParentRouteList!.First(x => x.Name.Equals("Configuration")).Id,
            uri: "/configuration/field-mapping"),

        NavigationRouteDEM.New(
            name: "Process Setup",
            protectedRoute: true,
            position: 0,
            icon: "rule_settings",
            parentId: ParentRouteList!.First(x => x.Name.Equals("Feature")).Id,
            uri: "/feature/process-setup"),
        NavigationRouteDEM.New(
            name: "Schedule Setup",
            protectedRoute: true,
            position: 1,
            icon: "calendar_month",
            parentId: ParentRouteList!.First(x => x.Name.Equals("Feature")).Id,
            uri: "/feature/schedule-setup"),

        /*NavigationRouteDEM.New(
            name: "Purchasing A/P",
            position: 0,
            icon: "archive",
            parentId: ParentRouteList!.First(x => x.Name.Equals("Transaction")).Id),
        NavigationRouteDEM.New(
            name: "Sales A/R",
            position: 1,
            icon: "unarchive",
            parentId: ParentRouteList!.First(x => x.Name.Equals("Transaction")).Id),
        NavigationRouteDEM.New(
            name: "Inventory",
            position: 2,
            icon: "inventory_2",
            parentId: ParentRouteList!.First(x => x.Name.Equals("Transaction")).Id),*/
    ];

    public static readonly IEnumerable<NavigationRouteDEM> SubRouteList2 =
    [
        NavigationRouteDEM.New(
            name: "User Management",
            protectedRoute: true,
            position: 0,
            parentId: SubRouteList1!.First(x => x.Name.Equals("User")).Id,
            icon: "account_circle",
            uri: "/administration/user/user-management"),
        NavigationRouteDEM.New(
            name: "User Roles",
            protectedRoute : true, 
            position: 1,
            parentId: SubRouteList1!.First(x => x.Name.Equals("User")).Id,
            icon: "groups",
            uri: "/administration/user/role-management"),
        NavigationRouteDEM.New(
            name: "User Authorization",
            protectedRoute : true, 
            position: 2,
            parentId: SubRouteList1!.First(x => x.Name.Equals("User")).Id,
            icon: "admin_panel_settings",
            uri: "/administration/user/authorization-management"),
        NavigationRouteDEM.New(
            name: "System Configuration",
            protectedRoute : true, 
            position: 0,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Settings")).Id,
            icon: "build_circle",
            uri: "/administration/settings/system-configuration"),

        NavigationRouteDEM.New(
            name: "Path Setup",
            protectedRoute: true,
            position: 0,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Setup")).Id,
            icon: "arrow_split",
            uri: "/configuration/setup/path-setup"),
        NavigationRouteDEM.New(
            name: "SAP Setup",
            protectedRoute: true,
            position: 1,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Setup")).Id,
            icon: "conversion_path",
            uri: "/configuration/setup/sap-setup"),
        NavigationRouteDEM.New(
            name: "API Setup",
            protectedRoute: true,
            position: 2,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Setup")).Id,
            icon: "settings_input_hdmi",
            uri: "/configuration/setup/api-setup"),
        NavigationRouteDEM.New(
            name: "Email Setup",
            protectedRoute: true,
            position: 3,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Setup")).Id,
            icon: "mail",
            uri: "/configuration/setup/email-setup"),

        /*NavigationRouteDEM.New(
            name: "Receiving",
            protectedRoute : true, 
            position: 0,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Purchasing A/P")).Id,
            icon: "stacked_inbox",
            uri: "/transactions/purchasing/receiving"),
        NavigationRouteDEM.New(
            name: "Goods Return",
            protectedRoute : true, 
            position: 1,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Purchasing A/P")).Id,
            icon: "outbox",
            uri: "/transactions/purchasing/goods-return"),
        NavigationRouteDEM.New(
            name: "Delivery",
            protectedRoute : true,
            position: 0,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Sales A/R")).Id,
            icon: "local_shipping",
            uri: "/transactions/sales/delivery"),
        NavigationRouteDEM.New(
            name: "Sales Return",
            protectedRoute : true,
            position: 1,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Sales A/R")).Id,
            icon: "pallet",
            uri: "/transactions/sales/sales-return"),
        NavigationRouteDEM.New(
            name: "Goods Issue",
            protectedRoute : true,
            position: 0,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Inventory")).Id,
            icon: "bottom_panel_close",
            uri: "/transactions/inventory/goods-issue"),
        NavigationRouteDEM.New(
            name: "Goods Receipt",
            protectedRoute : true,
            position: 1,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Inventory")).Id,
            icon: "bottom_panel_open",
            uri: "/transactions/inventory/goods-receipt"),
        NavigationRouteDEM.New(
            name: "Inventory Transfer",
            protectedRoute : true,
            position: 2,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Inventory")).Id,
            icon: "battery_android_share",
            uri: "/transactions/inventory/inventory-transfer"),
        NavigationRouteDEM.New(
            name: "Inventory Counting",
            protectedRoute : true,
            position: 3,
            parentId: SubRouteList1!.First(x => x.Name.Equals("Inventory")).Id,
            icon: "home_storage",
            uri: "/transactions/inventory/inventory-transfer"),*/
    ];
}
