using ILveILCEJSON_ENTITYMODELS.Classlar;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ILveILCEJSONBLL
{
    public class ILILCEServis
    {
        private string JsonString = string.Empty;
        ILServis ilServisi = new ILServis();
        public ILILCEServis()
        {
            VeriKaynaginaBaglan();
        }
        private void VeriKaynaginaBaglan() 
        {
            using(WebClient istemci = new WebClient())
            {
                //byte[] data = istemci.DownloadData(System.Windows.Forms.Application.StartPath + "/belediyelerfull.json");

                byte[] data = istemci.DownloadData(@"C:\Users\103SABAH_ŞEYDA\source\repos\2Aralik2021\IlveIlceJSONOrnek\belediyelerfull.json");
                JsonString = Encoding.UTF8.GetString(data);
            }
        }
        public List<ILILCEBilgileri> BilgileriGetir()
        {
            List<ILILCEBilgileri> liste = new List<ILILCEBilgileri>();
            JObject j = JObject.Parse(JsonString);
            List<string> illerim = ilServisi.IlleriGetir().Select(x => x.ILAdi).ToList();

            illerim =illerim.Select(x=> DilIslemleri.TurkceKarakterleriIngilizceyeCevir(x.ToLower())).ToList();

            foreach (string item in illerim)
            {
                //her bir il için bilgileri belediyelerfull.json dan çekeceğiz
                var data = j.SelectToken(item).SelectToken("il");
                ILILCEBilgileri detayliBilgi = new ILILCEBilgileri();
                detayliBilgi.Plaka = Convert.ToByte(data["plaka"].ToObject<string>());
                detayliBilgi.Tel = data["belediye-tel"].ToObject<string>();
                detayliBilgi.Faks = data["belediye-faks"].ToObject<string>();
                detayliBilgi.Ismi = data["belediye-ismi"].ToObject<string>();

                detayliBilgi.Mail = data["belediye-mail"] == null ? " ": data["belediye-mail"].ToObject<string>();
                detayliBilgi.Web = data["belediye-web"] == null ? " " : data["belediye-web"].ToObject<string>();
                
                detayliBilgi.Nufus = data["nufus"].ToObject<string>();

                detayliBilgi.Alankodu = data["alankodu"] == null ? " " : data["alankodu"].ToObject<string>();
                detayliBilgi.Bolge = data["bolge"].ToObject<string>();
                detayliBilgi.Bilgi = data["bilgi"].ToString();

                liste.Add(detayliBilgi);

            }

            return liste;
        }
        public List<ILILCEBilgileri> ILAdinaGoreIlceleriGetir(string ilAdi)
        {
            List<ILILCEBilgileri> liste = new List<ILILCEBilgileri>();

            JObject j = JObject.Parse(JsonString);
            
            List<string> ilcelerListem = ilServisi.IlleriGetir().Single(x => x.ILAdi == ilAdi).Ilceleri;

//ağrı ---> agri ismi jSon içindeki gibi değiştirdik
            ilAdi = DilIslemleri.TurkceKarakterleriIngilizceyeCevir(ilAdi.ToLower());
            ilcelerListem = ilcelerListem.Select(x => DilIslemleri.TurkceKarakterleriIngilizceyeCevir(x.ToLower())).ToList();

            foreach (var item in ilcelerListem)
            {
                var data = j.SelectToken(ilAdi.ToLower()).SelectToken(item);
                //bazı illerin ilçelerinde null gelmesin
                if(data != null)
                {
                    ILILCEBilgileri bilgim = new ILILCEBilgileri();
                    bilgim.Ismi = data["belediye-ismi"] == null ? "" : data["belediye-ismi"].ToObject<string>();
                    bilgim.Tel = data["belediye-tel"] == null ? "" : data["belediye-tel"].ToObject<string>();
                    bilgim.Faks = data["belediye-faks"] == null ? "" : data["belediye-faks"].ToObject<string>();
                    bilgim.Mail = data["belediye-mail"] == null ? "" : data["belediye-mail"].ToObject<string>();
                    bilgim.Web = data["belediye-web"] == null ? "" : data["belediye-web"].ToObject<string>();
                    bilgim.Nufus = data["nufus"] == null ? "" : data["nufus"].ToObject<string>();
                    bilgim.Bilgi = data["bilgi"] == null ? "" : data["bilgi"].ToObject<string>();

                    liste.Add(bilgim);
                }
            }

            return liste;
        }


    }
}
