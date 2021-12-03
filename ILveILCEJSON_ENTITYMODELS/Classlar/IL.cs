using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILveILCEJSON_ENTITYMODELS.Classlar
{
    public class IL
    {
        //C# property kurallarına göre IL isimli Class oluşturduk.
        //JSONIL class deserialize olunca oradaki dataları IL Class'ında türeteceğimiz nesneye aktaracağız
        public string ILAdi { get; set; }
        public byte  PlakaKodu { get; set; }
        public List<String> Ilceleri { get; set; }
    }
}
