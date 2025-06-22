using DigitTwin.Core.Users.Logic.Data;
using DigitTwin.Infrastructure.LoggerSeq;
using DigitTwin.Lib.Abstractions.Services;
using DigitTwin.Lib.Misc.Tools;
using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Core.Users
{
    internal class DatabaseInitializer : IDatabaseInitializer
    {
        #region CTOR
        /// <summary>
        /// Название сервиса
        /// </summary>
        private const string ServiceName = nameof(DatabaseInitializer);

        /// <inheritdoc cref="Infrastructure.DataContext.IDbContextFactory{TContext}"/>
        private readonly Infrastructure.DataContext.IDbContextFactory<UserDbContext> _dbContextFactory;

        /// <inheritdoc cref="ILoggerService"/>
        private readonly ILoggerService _logger;

        public DatabaseInitializer(Infrastructure.DataContext.IDbContextFactory<UserDbContext> dbContextFactory, ILoggerService logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }
        #endregion
        public async Task Up()
        {
            using var context = _dbContextFactory.CreateDbContext();
            try
            {
                await context.Database.MigrateAsync();

                _logger.LogInformation(ServiceName, "Database updated");

                await SeedingDefaultData();
            }
            catch(Exception ex)
            {
                _logger.LogError(ServiceName, "Can not create database with error.", ex);
            }
        }

        private async Task SeedingDefaultData()
        {
            using var context = _dbContextFactory.CreateDbContext();
            DbSet<User> users = context.Set<User>();

            try
            {
                if(await users.AnyAsync())
                {
                    return;
                }

                PasswordHasherTool.CreatePasswordHash("defaultPass", out byte[] password, out byte[] salt);

                var defaultUser = new User
                {
                    Name = "Администратор",
                    Type = UserTypeEnum.Administrator,
                    Status = UserStatusEnum.Active,
                    Email = "test@test.ru",
                    Password = password,
                    PasswordSalt = salt
                };

                await context.AddAsync(defaultUser);
                await context.SaveChangesAsync();

                _logger.LogInformation(ServiceName, "Added default user");
            }
            catch(Exception ex)
            {
                _logger.LogError(ServiceName, "Can not create database with error.", ex);
            }
        }

    }
}
