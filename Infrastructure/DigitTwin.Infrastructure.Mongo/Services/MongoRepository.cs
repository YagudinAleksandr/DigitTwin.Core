using System.Linq.Expressions;
using MongoDB.Driver;

namespace DigitTwin.Infrastructure.Mongo
{
    /// <inheritdoc cref="IMongoRepository{TDocument}"/>
    internal class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        #region CTOR
        /// <inheritdoc cref="IMongoCollection{TDocument}"/>
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(MongoDbService dbService, string collectionName)
        {
            _collection = dbService.Database.GetCollection<TDocument>(collectionName);
        }
        #endregion

        public async Task InsertOneAsync(TDocument document)
        {
            document.CreatedAt = DateTime.UtcNow;
            await _collection.InsertOneAsync(document);
        }

        public async Task<TDocument?> FindByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.ReplaceOneAsync(filter, document);
        }

        public async Task DeleteOneAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<TDocument>> FindAsync(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument>? sort = null,
            int? limit = null)
        {
            var query = _collection.Find(filter);

            if (sort != null) query = query.Sort(sort);
            if (limit.HasValue) query = query.Limit(limit);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TProjection>> ProjectAsync<TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            SortDefinition<TDocument>? sort = null)
        {
            var query = _collection.Find(filter).Project(projection);

            if (sort != null) query = query.Sort(sort);

            return await query.ToListAsync();
        }

        public async Task<long> CountAsync(Expression<Func<TDocument, bool>> filter)
        {
            return await _collection.CountDocumentsAsync(filter);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> filter)
        {
            return await _collection.CountDocumentsAsync(filter) > 0;
        }

        public IAggregateFluent<TDocument> Aggregate()
        {
            return _collection.Aggregate();
        }

        public async Task<(IEnumerable<TDocument> items, long totalCount)> PaginateAsync(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument> sort,
            int pageNumber,
            int pageSize)
        {
            var totalTask = _collection.CountDocumentsAsync(filter);
            var itemsTask = _collection.Find(filter)
                .Sort(sort)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            await Task.WhenAll(totalTask, itemsTask);

            return (itemsTask.Result, totalTask.Result);
        }
    }
}
