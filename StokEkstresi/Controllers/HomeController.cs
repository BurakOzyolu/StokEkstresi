using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using StokEkstresi.Models;
using StokEkstresi.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StokEkstresi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        TestContext _textContext;
        public HomeController(ILogger<HomeController> logger, TestContext textContext)
        {
            _logger = logger;
            _textContext = textContext;
        }
        /*
         Sayfa açılırken ilk olarak çalışan metod  */
        public IActionResult Index()
        {
            var stiList = _textContext.Stis.ToList();
            List<StokViewModel> stokList = new List<StokViewModel>();
            var stok = new StokViewModel();
            stok.IslemTuru = "Giriş";
            stok.GirisMiktar = 0;
            stok.CikisMiktar = 0;
            stok.StokMiktar = 0;
            stok.EvrakNo = "-";
            stok.Tarih = 0;
            stok.SiraNum = 0;
            stokList.Add(stok);
            return View(stokList);
        }
        [HttpPost]
        public IActionResult Index(IFormCollection formCollection)
        {
            /*
             * Formdan gelen 3 tane veriye göre SQL komutunu çalıştıyor
             * malKodu ilgili madın kodu
             * basTarih Başlangıç tarihi int olarak alıyor tür dönüşümlerinde hata aldım o yüzden böyle yazdım
             * bitTarih te aynı şekilde int değer alıyor
             */
            string malKodu = formCollection["MalKodu"];
            int basTarih = Convert.ToInt32(formCollection["BasTarih"]);
            int bitTarih = Convert.ToInt32(formCollection["BitTarih"]);
            //Veritabanında prosedür burada çalışıyor istek neticesinde gerile veri listesi dönüyor.
            var dataList = _textContext.MalKodAra(malKodu, basTarih, bitTarih).ToList();

            //Gelen verileri stok listesi View Model'ine dönüştürdüm.
            List<StokViewModel> stokList = new List<StokViewModel>();
            int siraNumarasi = 1;
            int StokMiktar = 0; //Stok miktarı işlem türüne göre artıp veya azalıyor.
            //Veri listesindeki her bir veriyi işleyip stok listesine ekliyor
            foreach (var item in dataList)
            {
                var stok = new StokViewModel();
                stok.SiraNum = siraNumarasi;
                if (item.IslemTur == 0)
                {
                    stok.IslemTuru = "Giriş";
                    stok.GirisMiktar = (int)item.Miktar;
                    stok.CikisMiktar = 0;
                    StokMiktar += (int)item.Miktar;
                    stok.StokMiktar = StokMiktar;
                }
                else
                {
                    stok.IslemTuru = "Çıkış";
                    stok.GirisMiktar = 0;
                    stok.CikisMiktar = (int)item.Miktar;
                    StokMiktar -= (int)item.Miktar;
                    stok.StokMiktar = StokMiktar;
                }
                stok.EvrakNo = item.EvrakNo;
                stok.Tarih = item.Tarih;
                siraNumarasi++;
                stokList.Add(stok);
            }
            //Stok listesi view'a gönderiliyor
            var result = stokList;
            return View(stokList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
