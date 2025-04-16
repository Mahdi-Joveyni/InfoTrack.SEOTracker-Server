using InfoTrack.SEOTracker.Data.Attributes;
using InfoTrack.SEOTracker.Data.Helpers;
using InfoTrack.SEOTracker.Data.Interfaces;
using InfoTrack.SEOTracker.Data.Models.Base;
using InfoTrack.SEOTracker.Domain;
using InfoTrack.SEOTracker.Utilities.Appsettings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace InfoTrack.SEOTracker.Data.Repositories;

public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : BaseModel
{
   public bool DisableAudit { get; set; } = false;
   private readonly IMongoCollection<TDocument> _collection;
   public MongoRepository(IOptions<MongoDbConfig> settings)
   {
      var database = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.DatabaseName);
      _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
   }

   public virtual async Task<List<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default)
   {
      return await _collection.Find(filterExpression).ToListAsync(cancellationToken: cancellationToken);
   }

   public virtual async Task<DataTableResult<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression,
       DataTableRequest parameters, CancellationToken cancellationToken = default, params LookupOptions[]? lookupOptions)
   {
      var aggregate = _collection.Aggregate();

      if (lookupOptions != null)
      {
         foreach (var option in lookupOptions)
         {
            aggregate = aggregate.Lookup(
                 option.From,
                 option.LocalField,
                 option.ForeignField,
                 option.AsField)
                .Unwind(option.AsField, options: new AggregateUnwindOptions<TDocument>() { PreserveNullAndEmptyArrays = true });
         }
      }

      aggregate = aggregate.Match(filterExpression);
      aggregate = aggregate.ApplySort(parameters.Sort ?? "Id", parameters.SortType);
      if (parameters.PageSize > 0)
         aggregate = aggregate.Skip((parameters.PageNumber - 1) * (parameters.PageSize))
            .Limit(parameters.PageSize);

      return new DataTableResult<TDocument>()
      {
         Items = await aggregate.ToListAsync(cancellationToken),
         TotalCount = (int)_collection.Find(filterExpression).CountDocuments(),
      };
   }
   public virtual async Task<TDocument?> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default)
   {
      return await _collection.Find(filterExpression).FirstOrDefaultAsync(cancellationToken);
   }

   public virtual async Task<TDocument?> FindByIdAsync(ObjectId id, CancellationToken cancellationToken = default)
   {
      return await _collection.Find(Builders<TDocument>.Filter.Eq(doc => doc.Id, id)).FirstOrDefaultAsync(cancellationToken);
   }

   public virtual async Task<TDocument> InsertOneAsync(TDocument document, CancellationToken cancellationToken = default)
   {
      await _collection.InsertOneAsync(document, cancellationToken: cancellationToken);
      return document;
   }
   public virtual async Task InsertManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default)
   {
      await _collection.InsertManyAsync(documents, cancellationToken: cancellationToken);
   }

   public virtual async Task<TDocument?> ReplaceOneAsync(TDocument document, CancellationToken cancellationToken = default)
   {
      var builderFilter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
      await _collection.FindOneAndReplaceAsync(builderFilter, document, cancellationToken: cancellationToken);
      return document;
   }

   public virtual async Task<TDocument?> DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default)
   {
      return await _collection.FindOneAndDeleteAsync(filterExpression, cancellationToken: cancellationToken);
   }

   public virtual async Task<TDocument?> DeleteByIdAsync(ObjectId id, CancellationToken cancellationToken = default)
   {
      return await _collection.FindOneAndDeleteAsync(Builders<TDocument>.Filter.Eq(doc => doc.Id, id), cancellationToken: cancellationToken);
   }

   public virtual async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default)
   {
      await _collection.DeleteManyAsync(filterExpression, cancellationToken: cancellationToken);
   }
   public virtual async Task<long> CountDocumentsAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default)
   {
      return await _collection.CountDocumentsAsync(filterExpression, cancellationToken: cancellationToken);
   }

   public static string GetCollectionName(Type documentType)
   {
      return ((CollectionName?)documentType.GetCustomAttributes(typeof(CollectionName), true)
          .FirstOrDefault())?.Name ?? documentType.Name;
   }
}