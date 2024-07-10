using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Nest;
using NuGet.Packaging.Signing;
using System;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using webscraping.htmlclass;
using webscraping.Model;
using webscraping.Models;
using webscraping.Services;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

public class htmlOkuma
    {
 
    public static async Task HtmlNodeAsync(string url,string key)
    {
        try
        {
            if (!string.IsNullOrEmpty(url))
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;

                string html = webClient.DownloadString(url);
                if (!string.IsNullOrEmpty(html))
                {
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);

                    //    List<string> lists = new List<string>();
                    if (doc != null)
                    {

                        HtmlNodeCollection makaleler = doc.DocumentNode.SelectNodes("//*[@class='article-cards']//*[@class='card article-card dp-card-outline']");

                        if (makaleler != null)
                        {
                            Console.WriteLine("Arama Sonuçları:\n");
                            List<BilgiModel> models = new List<BilgiModel>();
                            ///girilen keywords///

                            // Console.WriteLine("aratılan anahtar kelime:" +);
                            int count = 0;
                            foreach (var article in makaleler)
                            {
                                count++;
                                string yayinadi = article.SelectSingleNode(".//*[@class='card-title']//a[@href]").InnerText.Trim();
                                Console.WriteLine("yayinadi:" + yayinadi);

                                var kısaozet = article.SelectSingleNode(".//*[@class='kt-list kt-list--badge matches']");
                                string kısaozett;
                                if (kısaozet != null)
                                {
                                    //birlestirmedim
                                    // kısaozett = Regex.Replace(kısaozet.InnerText.Trim(), @"\s+", "");
                                    kısaozett = kısaozet.InnerText.Trim();

                                    Console.WriteLine("ozet:" + kısaozett);
                                }
                                else
                                {
                                    kısaozett = null;
                                }

                                //  HtmlAgilityPack.HtmlNodeCollection yayintur = article.SelectNodes("//span[@class='badge badge-secondary']");

                                /*
                                ///yayıntur///
                                HtmlNode yayinturr = article.SelectSingleNode(".//small[1][@class='article-meta']");
                                string yayintur;
                                if (yayinturr != null)
                                {
                                    yayintur=yayinturr.InnerText.Trim();
                                    Console.WriteLine("yayın türü:" + yayintur);
                                }
                                else
                                {
                                    yayintur = null;
                                }
                                */

                                //////yazarlar//
                              /*  var s = article.SelectSingleNode(".//small[2][@class='article-meta']");
                                string clean = Regex.Replace(s.InnerText.Trim(), @"\s+", "");

                                Match matchyazar = Regex.Match(clean, @"^(.*?)(?=\(\d+\))");
                                string yazarlarresult=null;
                                if (matchyazar.Success)
                                {
                                    yazarlarresult = matchyazar.Groups[1].Value.Trim();
                                    Console.WriteLine("yazarlar:" + yazarlarresult);

                                }*/
                                //////////yayıncı ad//////////


                                string cleanHtml = Regex.Replace(article.InnerText.Trim(), @"\s+", "");
                                string patternyayıncı = @"\),([^,]+),";

                                Match match2yayıncı = Regex.Match(cleanHtml, patternyayıncı);
                                string yayıncıadresult=null;
                                if (match2yayıncı.Success)
                                {
                                   yayıncıadresult = match2yayıncı.Groups[1].Value;
                                    Console.WriteLine("yayinci adı:" + yayıncıadresult);
                                }
                                /*
                                /// yayınlanma yıl///
                                Match matchyıl = Regex.Match(cleanHtml, @"\b\d{4}\b");
                                string year=null;
                                if (matchyıl.Success)
                                {
                                    year = matchyıl.Value;
                                    Console.WriteLine("yayimlanma tarihi:" + year);
                                }
                                */

                                //////////////////////////////////linkkk///////////////////////////////////////////////



                                HtmlNode linkdeger = article.SelectSingleNode(".//*[@class='card-title']//a");

                                if (linkdeger != null)
                                {
                                    string sonlink = linkdeger.GetAttributeValue("href", "");
                                    Console.WriteLine("url adresi:" + sonlink);
                                  
                                        if (!string.IsNullOrEmpty(sonlink))
                                        {


                                            string html1 = webClient.DownloadString(sonlink);
                                            if (!string.IsNullOrEmpty(html1))
                                            {
                                                HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
                                                doc1.LoadHtml(html1);



                                                if (doc1 != null)
                                                {



                                                //string anahtarkelime = doc1.DocumentNode.SelectSingleNode("//div[@class='article-keywords data-section']/p").InnerText.Trim();
                                                HtmlNode selectedanahtarkelime=doc1.DocumentNode.SelectSingleNode("//div[@class='article-keywords data-section']/p");
                                                string resultanahtarkelime;
                                                if (selectedanahtarkelime != null)
                                                {
                                                    string[] words = selectedanahtarkelime.InnerText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                                                    for (int i = 0; i < words.Length; i++)
                                                    {
                                                        words[i] = words[i].Trim();
                                                    }

                                                    resultanahtarkelime = string.Join(",", words);
                                                    Console.WriteLine("anahtar kelimeler:" + resultanahtarkelime);
                                                }

                                                else
                                                {
                                                    resultanahtarkelime = null;
                                                }
                                              
                                                string yayınturdenenemevol = doc1.DocumentNode.SelectSingleNode("//table[@class='record_properties table']//tr/th[text()='Bölüm']/following-sibling::td").InnerText.Trim();
                                                string tarihden = doc1.DocumentNode.SelectSingleNode("//table[@class='record_properties table']//tr/th[text()='Yayımlanma Tarihi']/following-sibling::td").InnerText.Trim();
                                                /*
                                                // string yayımlanmatarih = doc.DocumentNode.SelectSingleNode("//table[@class='record_properties table']//tr[th='Yayımlanma Tarihi']/td").InnerText.Trim();
                                                if (DateTime.TryParseExact(tarihden, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime tarihdenn))
                                                {
                                                    Console.WriteLine("Dönüştürülen tarih: " + tarihdenn.ToString("dd/MM/yyyy"));
                                                }*/
                                                string yazarrrrrrr= doc1.DocumentNode.SelectSingleNode("//p[@class='article-authors']").InnerText.Trim();
                                                string yazarlarstring = Regex.Replace(yazarrrrrrr, @"\s+", "");


                                                //  string kaynakca = doc1.DocumentNode.SelectSingleNode("//div[@class='article-citations data-section']//ul[@class='fa-ul']").InnerText.Trim();

                                                HtmlNode selectedkaynakca = doc1.DocumentNode.SelectSingleNode("//div[@class='article-citations data-section']//ul[@class='fa-ul']");
                                                string kaynakca;
                                                if (selectedkaynakca != null)
                                                {
                                                   
                                                   kaynakca = selectedkaynakca.InnerText.Trim();
                                                    Console.WriteLine("kaynakca:" + kaynakca);
                                                }
                                                else
                                                {
                                                    kaynakca = null;
                                                  
                                                }
                                                HtmlNode pdf = doc1.DocumentNode.SelectSingleNode("//div[@id='article-toolbar']/a");
                                                string fullUrl=null;
                                                if (pdf != null)
                                                {
                                                 
                                                    string pdfdowloand = pdf.GetAttributeValue("href", "");
                                                    fullUrl = "https://dergipark.org.tr" + pdfdowloand;

                                              
                                                    Console.WriteLine("Link: " + fullUrl);

                                                   
                                                }



                                                var client = new MongoClient("mongodb://localhost:27017");
                                                var database = client.GetDatabase("yazlab_db");
                                                var bilgimodelcollection = database.GetCollection<BilgiModel>("bilgiler");



                                                var BilgiModel = new BilgiModel
                                                {
                                                    Id = ObjectId.GenerateNewId(),
                                                    yayinAd = yayinadi,
                                                    yazarAdlari = yazarlarstring,
                                                    yayinTürü = yayınturdenenemevol,
                                                    yayinciAd = yayıncıadresult,
                                                    yayinlanmaTarihi = tarihden,
                                                    anahtarkelimeler = resultanahtarkelime,
                                                    ozet = kısaozett,
                                                    kaynakca = kaynakca,
                                                    aratilanAnahtarkelime = key,
                                                    pdfUrl = fullUrl


                                                };
                                                bilgimodelcollection.InsertOne(BilgiModel);

                                                BilgiModel model = new BilgiModel
                                                  {

                                                      yayinAd=yayinadi,
                                                      yazarAdlari=yazarlarstring,
                                                      yayinTürü=yayınturdenenemevol,
                                                      yayinciAd=yayıncıadresult,
                                                      yayinlanmaTarihi=tarihden,
                                                      anahtarkelimeler=resultanahtarkelime,
                                                      ozet=kısaozett,
                                                      kaynakca=kaynakca,
                                                      pdfUrl=sonlink
                                                  };
                                                  models.Add(model);




                                            }
                                                
                                            }
                                        }
                                    
                                       
                                  
                                }

                                //////////////////////////////
                                if (count >= 10)
                                {
                                    break;
                                }

                            }



                         //   return models;

                        }
                    }


                }

            }
          // return null;
          
        }
        catch 
        {
           //return null;
        }



    }





}

