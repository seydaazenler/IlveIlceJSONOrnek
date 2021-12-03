using ILveILCEJSON_ENTITYMODELS.Classlar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ILveILCEJSONBLL
{
    public class ILServis
    {
        private string JSonString = string.Empty; //field
        public ILServis()
        {
            VeriKaynaginaBaglan();
            
        }
        private void VeriKaynaginaBaglan()
        {
            using(WebClient istemci =new WebClient())

            {
                byte[]data = istemci.DownloadData(@"C:\Users\103SABAH_ŞEYDA\source\repos\2Aralik2021\IlveIlceJSONOrnek\belediyeler.json");

                //JSON dosyasındaki ş ç ü gibi harfler çevrilirken bozulma yaşanır.Encoding UTF8 kullanılırsa tüm dillere göre çözümleme yapar
                JSonString = Encoding.UTF8.GetString(data);
            }
        }
        public List<IL> IlleriGetir()
        {
            List<IL> ILListesi = new List<IL>();

            //data ILJson classından alınacak. Oradaki propertyler küçük harfli olduğundan dolayı öyle tanımlı
            var jsonData = JsonConvert.DeserializeObject<List<ILJson>>(JSonString);

            //Alınan data bizim sistemimizdeki IL Classına aktarılacak
            foreach (var item in jsonData)
            {
                ILListesi.Add(
                    new IL()
                    {
                        ILAdi = item.il,
                        PlakaKodu = Convert.ToByte(item.plaka),
                        Ilceleri = item.ilceleri

                    });
            }
            return ILListesi;
        }

    }
}
