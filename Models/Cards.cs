using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CreditCardApi.Models
{
    [BsonIgnoreExtraElements]
    public class Cards
    {
        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public string Id { get; set; }

        public string CardNumber { get; set; }

        public string HolderName { get; set; }

        public double Balance { get; set; }
    }
}