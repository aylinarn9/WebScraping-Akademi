using webscraping.Model;

namespace webscraping.Services
{
    public interface IBilgiModel
    {
        List<BilgiModel> GetBilgis();
        List<BilgiModel> GetBilgiKey(String key);
       

      ///  List<BilgiModel> GetBilgiTarih(String yayinlanmaTarihi);


        List<BilgiModel> GetBilgiMakale(String yayinad);

       // List<BilgiModel> GetBilgiYayinciAd(String yayinciAd);

        BilgiModel AddBilgi(BilgiModel bilgi);
        void DeleteBilgi(string id);
        BilgiModel UpdateBilgi(BilgiModel bilgi);


        dynamic GetBilgiTarih(List<string> secilenTarihler);
        dynamic GetBilgiYayinTur(List<string> secilenYayinTurleri);

        dynamic GetBilgiYayinciAd(List<string> secilenYayinciAdlari);

        dynamic GetBilgiYayinAd(List<string> secilenYayinAdlari);

    }
}
