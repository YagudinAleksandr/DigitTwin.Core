using DigitTwin.Lib.EventBroker;
using DigitTwin.Lib.PipelineBuilder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DigitTwin.Lib.Misc
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление библиотечных сервисов
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddMiscServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddSingleton<IEventBroker, EventBroker.EventBroker>();

            return services;
        }

        /// <summary>
        /// Регистрирует синхронный и асинхронный пайплайны с шагами из указанных сборок
        /// </summary>
        /// <typeparam name="TContext">Тип контекста обработки</typeparam>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="assemblies">Сборки для поиска шагов</param>
        public static IServiceCollection AddPipelines<TContext>(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
                assemblies = [Assembly.GetExecutingAssembly()];

            // Регистрация ВСЕХ синхронных шагов
            var syncStepTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IPipelineStep<TContext>).IsAssignableFrom(t)
                            && !t.IsInterface
                            && !t.IsAbstract);

            foreach (var type in syncStepTypes)
            {
                services.AddTransient(typeof(IPipelineStep<TContext>), type);
            }

            // Регистрация ВСЕХ асинхронных шагов
            var asyncStepTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IAsyncPipelineStep<TContext>).IsAssignableFrom(t)
                            && !t.IsInterface
                            && !t.IsAbstract);

            foreach (var type in asyncStepTypes)
            {
                services.AddTransient(typeof(IAsyncPipelineStep<TContext>), type);
            }

            // Регистрация самого пайплайна
            services.AddTransient<IPipeline<TContext>, Pipeline<TContext>>();
            services.AddTransient<IAsyncPipeline<TContext>, AsyncPipeline<TContext>>();

            return services;
        }
    }
}
