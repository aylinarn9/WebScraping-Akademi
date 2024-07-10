using Nest;
using webscraping.Model;

namespace webscraping.Mapping
{
    public static class Mapping
    {
        public static CreateIndexDescriptor MakaleMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<BilgiModel>(m => m.Properties(p => p
                .Keyword(k => k.Name(n => n.Id))
                .Text(t => t.Name(n => n.yayinAd))
                .Text(t => t.Name(n => n.yazarAdlari))
                 .Text(t => t.Name(n => n.yayinTürü))
                  .Text(t => t.Name(n => n.yayinciAd))
                   .Text(t => t.Name(n => n.yayinlanmaTarihi))
                    .Text(t => t.Name(n => n.anahtarkelimeler))
                     .Text(t => t.Name(n => n.aratilanAnahtarkelime))
                    .Text(t => t.Name(n => n.ozet))
                     .Text(t => t.Name(n => n.kaynakca))
                      .Text(t => t.Name(n => n.pdfUrl)))

            );
        }
    }
}
