using LogManagerAsBackgroundService.Implements;
using LogManagerAsBackgroundService.Interfaces.Business;

namespace LogManagerAsBackgroundService.Extensions
{
    public static class ServiceExtensions
    {
        public static IApplicationBuilder DoWork(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            scope.ServiceProvider.GetRequiredService<IBusinessService>().DoWork();
            scope.ServiceProvider.GetRequiredService<LogWorker>().StartAsync(CancellationToken.None);

            return app;
        }
    }
}
