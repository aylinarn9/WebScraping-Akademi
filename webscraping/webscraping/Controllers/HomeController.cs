using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Nest;
using webscraping.htmlclass;
using webscraping.Model;
using webscraping.Models;
using webscraping.Services;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;


namespace webscraping.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBilgiModel modelService;
        private readonly IMapper _mapper;

        public HomeController(IBilgiModel modelS, IMapper mapper)
        {
            this.modelService = modelS;
            _mapper = mapper;
        

        }
        public IActionResult BilgiList()
        {
            var value = modelService.GetBilgis();

            // var sonuc= _mapper.Map<List<BilgiModel>>(value);

            return View(value);
        }

        [HttpGet]

        public async Task<IActionResult> Index(string keywords)
        {
            //string url = $"https://scholar.google.com/scholar?q={keywords}";
            // ViewBag.results = htmlOkuma.htmlNode(url, "//*[@class='gs_r gs_or gs_scl']");
            string url;
            ViewBag.key = keywords;
            if (keywords != null)
            {
                //url = $"https://dergipark.org.tr/tr/search?q=Uri.EscapeDataString(keywords);

                url = $"https://dergipark.org.tr/tr/search?q={Uri.EscapeDataString(keywords)}";
                HttpContext.Session.SetString("AnahtarKelime", keywords);
             
                htmlOkuma.HtmlNodeAsync(url, keywords);
             

            }
            else
            {
                
                Console.Write("anahtar kelime girilmedi: ");
            }
         
         
          // anahtar kelimeye gore makale getirme
           var valuekey= modelService.GetBilgiKey(keywords);
           TempData["t"] = keywords;
         

            //  var sonuc = _mapper.Map<List<BilgiModel>>(value);

            return View(valuekey);
          
        }

       

        //makale ismine tıklandıgında ayrı sayfada göstermek için kullanılır.
        public IActionResult BilgiGetir(string id)
        {

            var yayin = modelService.GetBilgiMakale(id);
            return View(yayin);
        }
       

        //YAYIN TURUNE
        [HttpGet]
        public async Task<IActionResult> Filtre(string[] yayinTuru)
        {
                  /*  TempData["tur"] = yayinTuru;
                  //  string tanahtarkelime = (string)TempData["t"];
            string tanahtarkelime = HttpContext.Session.GetString("AnahtarKelime");
            var vk = modelService.GetBilgiKey(tanahtarkelime);

                    var tur = vk.Select(model => model.yayinTürü).Distinct().ToList();

                    var makaleler = modelService.GetBilgiYayinTur(yayinTuru);
                    ViewBag.mk = makaleler;

            var model = new FiltreModel
            {
                TurListesi = tur
            };

            return View(model);*/

            var secilenYayinTurleri = yayinTuru.ToList();
            string tanahtarkelime = HttpContext.Session.GetString("AnahtarKelime");

           
            var vk = modelService.GetBilgiKey(tanahtarkelime);

        
            var makaleler = modelService.GetBilgiYayinTur(secilenYayinTurleri);
            ViewBag.mk = makaleler;

        
            var model = new FiltreModel
            {
                TurListesi = vk.Select(model => model.yayinTürü).Distinct().ToList(),
               
            };

          
            return View(model);

        }

        //YAYINCI AD
        [HttpGet]
        public async Task<IActionResult> YayinciFiltre(string[] yayinciAd)
        {
            /*TempData["yayinciAd"] = yayinciAd;
           // string tanahtarkelime = (string)TempData["t"];
            string tanahtarkelime = HttpContext.Session.GetString("AnahtarKelime");
            var vk = modelService.GetBilgiKey(tanahtarkelime);
            var yayinci = vk.Select(model => model.yayinciAd).Distinct().ToList();

            var model = new YayinciModel
            {
                YayinciAdListesi = yayinci
            };

            var makaleler = modelService.GetBilgiYayinciAd(yayinciAd);
            ViewBag.mk = makaleler;
            return View(model);*/
            var secilenYayinAdlari = yayinciAd.ToList();
            string tanahtarkelime = HttpContext.Session.GetString("AnahtarKelime");
            var vk = modelService.GetBilgiKey(tanahtarkelime);
            var makaleler = modelService.GetBilgiYayinciAd(secilenYayinAdlari);
            ViewBag.mk = makaleler;
            var model = new YayinciModel
            {
                YayinciAdListesi = vk.Select(model => model.yayinciAd).Distinct().ToList(),
            };
            return View(model);
        }

        //YAYINLANMA TARİHİ filtre
        [HttpGet]
        public async Task<IActionResult> TarihFiltre(string[] yayinlanmaTarihi)
        {
            /* TempData["yayinlanmaTarihi"] = yayinlanmaTarihi;
             // string tanahtarkelime = (string)TempData["t"];
             string tanahtarkelime = HttpContext.Session.GetString("AnahtarKelime");
             var vk = modelService.GetBilgiKey(tanahtarkelime);
             var yayinlanmaTarih = vk.Select(model => model.yayinlanmaTarihi).Distinct().ToList();

             var model = new TarihModel
             {
                 TarihListesi = yayinlanmaTarih
             };

             var makaleler = modelService.GetBilgiTarih(yayinlanmaTarihi);
             ViewBag.mk = makaleler;
             return View(model);*/
            var secilenTarihler = yayinlanmaTarihi.ToList();
            string tanahtarkelime = HttpContext.Session.GetString("AnahtarKelime");
            var vk = modelService.GetBilgiKey(tanahtarkelime);
            var makaleler=modelService.GetBilgiTarih(secilenTarihler);
            ViewBag.mk = makaleler;

            var model = new TarihModel
            {
                TarihListesi = vk.Select(model => model.yayinlanmaTarihi).Distinct().ToList(),
            };
            return View(model);
        }

        //yayın adına filtre
        [HttpGet]
        public async Task<IActionResult> YayinAdFiltre(string[] yayinAD)
        {
            /*  TempData["yayinAd"] = yayinAD;
              // string tanahtarkelime = (string)TempData["t"];
              string tanahtarkelime = HttpContext.Session.GetString("AnahtarKelime");
              var vk = modelService.GetBilgiKey(tanahtarkelime);
              var yayinAd = vk.Select(model => model.yayinAd).Distinct().ToList();

              var model = new YayinAdModel
              {
                  YayinAdListesi = yayinAd
              };

              var makaleler = modelService.GetBilgiMakale(yayinAD);
              ViewBag.mk = makaleler;*/
            var secilenYayinAdlari = yayinAD.ToList();
            string tanahtarkelime = HttpContext.Session.GetString("AnahtarKelime");
            var vk = modelService.GetBilgiKey(tanahtarkelime);
           var makaleler= modelService.GetBilgiYayinAd(secilenYayinAdlari);
            ViewBag.mk = makaleler;
            var model = new YayinAdModel
            {
                YayinAdListesi = vk.Select(model => model.yayinAd).Distinct().ToList(),
            };


            return View(model);

        }



        //eskiden yeniye
        public async Task<IActionResult> tarihSırala()
        {
           // string tanahtarkelime = (string)TempData["t"];
            string tanahtarkelime = HttpContext.Session.GetString("AnahtarKelime");
            var makale = modelService.GetBilgiKey(tanahtarkelime).OrderBy(m => DateTime.Parse(m.yayinlanmaTarihi)).ToList();
            return View(makale);


        }

        //yeniden eskiye
        public async Task<IActionResult> tarihSırala1()
        {
            //string tanahtarkelime = (string)TempData["t"];
            string tanahtarkelime = HttpContext.Session.GetString("AnahtarKelime");
            var makale1 = modelService.GetBilgiKey(tanahtarkelime).OrderByDescending(m => DateTime.Parse(m.yayinlanmaTarihi)).ToList();
            return View(makale1);


        }


        /*
        [HttpGet]
        public IActionResult Filtre(string yayinTuru)
        {
            TempData["tur"] = yayinTuru;
            string tanahtarkelime = (string)TempData["t"];
            var vk = modelService.GetBilgiKey(tanahtarkelime);
            var tur = vk.Select(model => model.yayinTürü).Distinct().ToList();
           // ViewBag.tur =tur;
        

            var makaleler = modelService.GetBilgiYayinTur(yayinTuru);
           // ViewBag.mk = makaleler;
            var filtremodel = new FiltreModel
            {
                YayinTurleri = tur,
                Makaleler =makaleler
            };
            return PartialView("~/Views/Shared/FiltrePartial.cshtml",filtremodel);

        }
        */

        


    }

      
    }
