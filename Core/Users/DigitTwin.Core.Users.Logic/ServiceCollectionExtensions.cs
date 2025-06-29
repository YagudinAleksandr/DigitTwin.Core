﻿using DigitTwin.Core.ActionService;
using DigitTwin.Core.Users.Logic.Data;
using DigitTwin.Core.Users.Logic.Validators.Users;
using DigitTwin.Infrastructure.ApacheKafka;
using DigitTwin.Infrastructure.DataContext;
using DigitTwin.Infrastructure.LoggerSeq;
using DigitTwin.Infrastructure.Redis;
using DigitTwin.Lib.Abstractions.Services;
using DigitTwin.Lib.Contracts;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Расширения DI для работы с сервисом для пользователей
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Формирование DI для сервиса работы с пользователями
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddUserServiceLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogger(configuration);
            services.AddDatabaseContext(configuration);
            services.AddAutoMapper(typeof(UserDtoProfile).Assembly);
            services.AddValidators();
            services.AddRepositories();
            services.AddServices();
            services.AddActionService();
            services.AddKafka(configuration);
            services.AddRedisService(configuration);

            return services;
        }

        /// <summary>
        /// Подключение всех зависимостей для работы с базой данных
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.SectionName));

            services.AddSingleton<IDbContextFactory<UserDbContext>, DbContextFactory<UserDbContext>>();
            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

            return services;
        }

        /// <summary>
        /// Подключение репозиториев
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IRepository<,>), typeof(Repository<,>));

            return services;
        }

        /// <summary>
        /// Добавление сервисов
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrganizationService, OrganizationService>();

            return services;
        }

        /// <summary>
        /// Валидаторы
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddValidators(this IServiceCollection services) 
        {
            // FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddScoped<IValidator<UserCreateDto>, UserCreationValidator>();
            services.AddScoped<IValidator<OrganizationCreateRequestDto>, OrganizationCreationValidator>();

            return services;
        }
    }
}
