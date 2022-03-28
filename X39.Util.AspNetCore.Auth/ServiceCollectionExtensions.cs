using Microsoft.Extensions.DependencyInjection;

namespace X39.Util.AspNet.Authorize;

public static class ServiceCollectionExtensions {
    /// <summary>
    /// Adds the mandatory services for the JWT-Login to work.
    /// </summary>
    /// <remarks>
    /// Requires <see cref="ApplicationBuilderExtensions.UseJwtLoginMiddleware"/> to work.
    /// </remarks>
    /// <param name="serviceCollection">The service collection of the service builder.</param>
    /// <param name="optionsFactory">The options factory to set-up</param>
    /// <typeparam name="TUserService">The actual <see cref="IJwtLoginService"/> implementation.</typeparam>
    /// <typeparam name="TUserServiceInterface">The <see cref="IJwtLoginService"/> to use for the singleton.</typeparam>
    public static void AddJwtLogin<TUserServiceInterface, TUserService>(
        this IServiceCollection serviceCollection,
        Action<LoginJwtMiddlewareOptions> optionsFactory)
        where TUserService : class, TUserServiceInterface
        where TUserServiceInterface : class, IJwtLoginService
    {
        serviceCollection.Configure(optionsFactory);
        serviceCollection.AddSingleton<TUserServiceInterface, TUserService>();
        serviceCollection.AddSingleton<IJwtLoginMiddleware, JwtLoginMiddleware<TUserServiceInterface>>();
    }

    /// <inheritdoc cref="AddJwtLogin{TUserServiceInterface,TUserService}"/>
    public static void AddJwtLogin<TUserService>(
        this IServiceCollection serviceCollection,
        Action<LoginJwtMiddlewareOptions> optionsFactory)
        where TUserService : class, IJwtLoginService
        => AddJwtLogin<IJwtLoginService, TUserService>(serviceCollection, optionsFactory);
}