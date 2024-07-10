using MongoDB.Bson;
using MongoDB.Driver;
using webscraping.Model;
using webscraping.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace webscraping.Services
{
    public class BilgiModelService : IBilgiModel
    {
        private readonly IMongoCollection<BilgiModel> _bilgimodels;

        public BilgiModelService(IDatabaseSetting settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _bilgimodels = database.GetCollection<BilgiModel>(settings.CollectionName);
        }
        public BilgiModel AddBilgi(BilgiModel bilgi)
        {
            throw new NotImplementedException();
        }

        public void DeleteBilgi(string id)
        {
            throw new NotImplementedException();
        }

       /* public List<BilgiModel> Get()
        {
            return _bilgimodels.Find(BilgiModel => true).ToList();
        }*/
       

        public BilgiModel UpdateBilgi(BilgiModel bilgi)
        {
            throw new NotImplementedException();
        }

        public List<BilgiModel> GetBilgis()
        => _bilgimodels.Find(bilgimodel => true).ToList();



        public List<BilgiModel> GetBilgiKey(string keywords)

        => _bilgimodels.Find(Builders<BilgiModel>.Filter.Eq(x => x.aratilanAnahtarkelime, keywords)).ToList();
       
        

        public List<BilgiModel> GetBilgiMakale(string yayinad)
             => _bilgimodels.Find(Builders<BilgiModel>.Filter.Eq(x => x.yayinAd, yayinad)).ToList();

      /*  public List<BilgiModel> GetBilgiYayinTur(string tur)
         => _bilgimodels.Find(Builders<BilgiModel>.Filter.Eq(x => x.yayinTürü, tur)).ToList();*/


       /* public List<BilgiModel> GetBilgiYayinciAd(string yayinciAd)
         => _bilgimodels.Find(Builders<BilgiModel>.Filter.Eq(x => x.yayinciAd, yayinciAd)).ToList();*/

       /* public List<BilgiModel> GetBilgiTarih(string yayinlanmaTarihi)
        => _bilgimodels.Find(Builders<BilgiModel>.Filter.Eq(x => x.yayinlanmaTarihi,yayinlanmaTarihi)).ToList();*/

        public dynamic GetBilgiYayinTur(List<string> secilenYayinTurleri)
        {
            var filter = Builders<BilgiModel>.Filter.In(x => x.yayinTürü,secilenYayinTurleri);
            return _bilgimodels.Find(filter).ToList();
        }

        public dynamic GetBilgiYayinciAd(List<string> secilenYayinciAdlari)
        {
            var filter = Builders<BilgiModel>.Filter.In(x => x.yayinciAd, secilenYayinciAdlari);
            return _bilgimodels.Find(filter).ToList();
        }

        public dynamic GetBilgiTarih(List<string> secilenTarihler)
        {
            var filter = Builders<BilgiModel>.Filter.In(x => x.yayinlanmaTarihi, secilenTarihler);
            return _bilgimodels.Find(filter).ToList();
        }

        public dynamic GetBilgiYayinAd(List<string> secilenYayinAdlari)
        {
            var filter = Builders<BilgiModel>.Filter.In(x => x.yayinAd, secilenYayinAdlari);
            return _bilgimodels.Find(filter).ToList();
        }
    }
}
