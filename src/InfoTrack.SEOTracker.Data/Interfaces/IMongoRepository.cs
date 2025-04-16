using InfoTrack.SEOTracker.Data.Models.Base;
using InfoTrack.SEOTracker.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace InfoTrack.SEOTracker.Data.Interfaces;

public interface IMongoRepository<TDocument> where TDocument : BaseModel
{
   Task<List<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);
   Task<DataTableResult<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression, DataTableRequest parameters,
      CancellationToken cancellationToken = default,
       params LookupOptions[]? lookupOptions);
   Task<TDocument?> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);
   Task<TDocument?> FindByIdAsync(ObjectId id, CancellationToken cancellationToken = default);
   Task<TDocument> InsertOneAsync(TDocument document, CancellationToken cancellationToken = default);
   Task InsertManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default);
   Task<TDocument?> ReplaceOneAsync(TDocument document, CancellationToken cancellationToken = default);
   Task<TDocument?> DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);
   Task<TDocument?> DeleteByIdAsync(ObjectId id, CancellationToken cancellationToken = default);
   Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);
   Task<long> CountDocumentsAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);
}