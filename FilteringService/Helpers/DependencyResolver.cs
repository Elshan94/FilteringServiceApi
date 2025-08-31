using FilteringService.Application;
using FilteringService.Application.Services.Abstract;
using FilteringService.Application.Services.Concrete;
using FilteringService.Application.Services.Hosted;
using System.Collections.Concurrent;

namespace FilteringService.Helpers
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<BlockingCollection<QueueItemModel>>(
    _ => new BlockingCollection<QueueItemModel>());

            services.AddSingleton<ISessionManagingService, SessionManagingService>();
            services.AddSingleton<IUploadQueueService, UploadQueueService>();
            services.AddSingleton<IUploadProcessingService, UploadProcessingService>();
            services.AddSingleton<IMemoryFilteredStorage, MemoryFilteredStorage>();
            services.AddSingleton<IFilterService, FilterService>();

            services.AddHostedService<FilteringBackgroundService>();

            return services;
        }
    }
}
