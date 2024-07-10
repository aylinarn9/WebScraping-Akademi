using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webscraping.Model


{
    public class BilgiModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string yayinAd { get; set; }

        public string yazarAdlari { get; set; }
        public string yayinTürü { get; set; }
        public string yayinciAd { get; set; }
       public string yayinlanmaTarihi { get; set; }

        public string anahtarkelimeler { get; set; }

         public string aratilanAnahtarkelime { get; set; }

        public string ozet { get; set; }

        public string kaynakca { get; set; }

        public string pdfUrl { get; set; }

        //  public int alintiSayisi { get; set; }
        //  public int doiNo { get; set; }












    }
}
