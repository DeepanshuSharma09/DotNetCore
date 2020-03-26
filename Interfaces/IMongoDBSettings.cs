namespace CreditCardApi.Interfaces
{
    public interface IMongoDBSettings
    {
         string ConnectionString { get; set; }

         string DatabaseName { get; set; }

         string CollectionName { get; set; }
    }
}