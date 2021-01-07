namespace FlowerChainAPI.Database
{
    // Changing settings for MongoDB
    public class FlowerChainDatabaseSettings : IFlowerChainDatabaseSettings
    {
        public string FlowerBouquetOrderCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IFlowerChainDatabaseSettings
    {
        string FlowerBouquetOrderCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}