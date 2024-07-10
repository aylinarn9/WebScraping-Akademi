using Nest;
using webscraping.Model;
using webscraping.Models;

namespace webscraping.htmlclass
{
    public class ElasticsearchManager
    {
        private readonly IElasticClient _client;

        public ElasticsearchManager()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("index_name"); 

            _client = new ElasticClient(settings);
        }

     /*   public async Task<bool> AddDocumentToElasticsearch(BilgiModel model)
        {
            try
            {
                var indexResponse = await _client.IndexDocumentAsync(model);
                return indexResponse.IsValid;
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya işleme ekleme
                Console.WriteLine($"Hata: {ex.Message}");
                return false;
            }
        }*/


        public async Task<bool> AddDocumentToElasticsearch(List<BilgiModel> list)
        {
            try
            {
                var indexResponse = await _client.IndexDocumentAsync(list);
                return indexResponse.IsValid;
            }
            catch (Exception ex)
            {
  
                Console.WriteLine($"Hata: {ex.Message}");
                return false;
            }
        }
    }

}
