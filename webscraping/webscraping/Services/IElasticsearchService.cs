using webscraping.Model;

namespace webscraping.Services
{
    public interface IElasticsearchService
    {
        Task InsertBulkDocuments(string indexName, List<BilgiModel> cities);
    }
}
