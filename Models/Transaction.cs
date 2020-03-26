using MongoDB.Bson.Serialization.Attributes;

namespace CreditCardApi.Models
{
    public class Transaction
    {
        public string CardNumber { get; set; }

        public string FlowDirection { get; set;}

        public double Amount { get; set; }
    }
}