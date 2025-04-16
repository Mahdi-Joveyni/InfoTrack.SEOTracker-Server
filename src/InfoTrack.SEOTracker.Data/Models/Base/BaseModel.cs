using MongoDB.Bson;

namespace InfoTrack.SEOTracker.Data.Models.Base;

public abstract class BaseModel
{
   public ObjectId Id { get; set; }
}
