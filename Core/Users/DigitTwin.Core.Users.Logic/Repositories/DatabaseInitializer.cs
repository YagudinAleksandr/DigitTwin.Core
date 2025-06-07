using DigitTwin.Core.Users.Logic.Data;
using DigitTwin.Infrastructure.LoggerSeq;
using DigitTwin.Lib.Abstractions.Services;
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
            }
            catch(Exception ex)
            {
                _logger.LogError(ServiceName, "Can not create database with error.", ex);
            }
        }

    }
}
