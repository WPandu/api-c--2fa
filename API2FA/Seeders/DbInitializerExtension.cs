using API2FA.Helpers;

namespace API2FA.Seeders
{
    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder UseItToSeedDatabase(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception e)
            {
                throw;
            }

            return app;
        }
    }
}
