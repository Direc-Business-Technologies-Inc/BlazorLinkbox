using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.BlazorServer.Handlers.Implementations.Administration.Authorization;
using Web.BlazorServer.Handlers.Implementations.Administration.Role;
using Web.BlazorServer.Handlers.Implementations.Administration.User;
using Web.BlazorServer.Handlers.Implementations.Configuration.Setup.Api;
using Web.BlazorServer.Handlers.Implementations.Configuration.Setup.Email;
using Web.BlazorServer.Handlers.Implementations.Configuration.Setup.Path;
using Web.BlazorServer.Handlers.Implementations.Configuration.Setup.Sap;
using Web.BlazorServer.Handlers.Implementations.System;
using Web.BlazorServer.Handlers.Repositories.Administration.Authorization;
using Web.BlazorServer.Handlers.Repositories.Administration.Role;
using Web.BlazorServer.Handlers.Repositories.Administration.User;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Api;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Email;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Path;
using Web.BlazorServer.Handlers.Repositories.Configuration.Setup.Sap;
using Web.BlazorServer.Handlers.Repositories.System;

namespace Web.BlazorServer.Handlers;

public static class BlazorServerHandlersDI
{
    public static IServiceCollection AddBlazorServerHandlers(this IServiceCollection services)
    {
        services.TryAddTransient<INavigationRouteHandler, NavigationRouteHandler>();

        // Administration
        services.TryAddTransient<IUserManagementHandler, UserManagementHandler>();
        services.TryAddTransient<IRoleManagementHandler, RoleManagementHandler>();

        // Configuration - Setup
        services.TryAddTransient<IPathManagementHandler, PathManagementHandler>();
        services.TryAddTransient<ISapManagementHandler, SapManagementHandler>();
        services.TryAddTransient<IApiManagementHandler, ApiManagementHandler>();
        services.TryAddTransient<IEmailManagementHandler, EmailManagementHandler>();

        // System
        services.TryAddTransient<IModuleHandler, ModuleHandler>();
        services.TryAddTransient<IDocumentNumberHandler, DocumentNumberHandler>();
        services.TryAddTransient<IAuthorizationHandler, AuthorizationHandler>();

        return services;
    }
}
