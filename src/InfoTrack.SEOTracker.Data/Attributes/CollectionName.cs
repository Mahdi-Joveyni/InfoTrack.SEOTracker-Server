using MongoDB.Bson.Serialization.Attributes;

namespace InfoTrack.SEOTracker.Data.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class CollectionName(string collectionName) : BsonIgnoreExtraElementsAttribute
{
   public string Name { get; } = collectionName;
}
