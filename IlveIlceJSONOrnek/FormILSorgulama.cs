using ILveILCEJSON_ENTITYMODELS.Classlar;
using ILveILCEJSONBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IlveIlceJSONOrnek
{
    public partial class FormILSorgulama : Form
    {
        public FormILSorgulama()
        {
            InitializeComponent();
        }
        //Global Alan
        ILServis IlServisim = new ILServis();
        ILILCEServis ILILceServisim = new ILILCEServis();
        
        private void FormILSorgulama_Load(object sender, EventArgs e)
        {
            //Form yüklenirken burası çalışacak
            //combox içine illeri getirdim
            comboBoxILSecimi.DataSource=IlServisim.IlleriGetir();
            comboBoxILSecimi.DisplayMember = "ILAdi"; //Gözükecek öğe
            comboBoxILSecimi.ValueMember = "PlakaKodu";

            //ListView içerisini dolduracağım
            List<ILILCEBilgileri> SehireAitBilgilerListesi = ILILceServisim.BilgileriGetir();
            foreach (var item in SehireAitBilgilerListesi)
            {
                ListViewItem deger = new ListViewItem()
                {
                    Text=item.Ismi,
                    Tag=item,

                };
                deger.SubItems.Add(item.Tel);
                deger.SubItems.Add(item.Faks);
                deger.SubItems.Add(item.Mail);
                deger.SubItems.Add(item.Web);
                //listView'e ekleme yapılacak
                listView1.Items.Add(deger);
            }
      
            //GROUPBOX başlangıçta gizli olmalıdır
            groupBoxIL.Enabled = false;
            groupBoxIL.Visible = false;
            //detay göster şeklinde sağ tık menüsü gerekiyor
            
        }

        private void btnSec_Click(object sender, EventArgs e)
        {
            //comboBox'ta hangi ili seçtiyse onun bilgilerini ListView'de görelim
            IL secilenIL = (IL)comboBoxILSecimi.SelectedItem as IL;
            //kısa yol
            //IL secilenIL = (IL)comboBoxILSecimi.SelectedItem;

            //LINQ ile şart yazıyorum. WHERE ve FirstorDefault
            //WHERE --> verilen koşula göre bilgileri getirir
            //FirstOrDefault --> where den dönen liste elemanlarından sadece isteneni alır

             ILILCEBilgileri secilenIlBilgisi=ILILceServisim.BilgileriGetir().Where(x => x.Plaka == secilenIL.PlakaKodu).FirstOrDefault();

            listView1.Items.Clear();
            ListViewItem deger = new ListViewItem();
            deger.Text = secilenIlBilgisi.Ismi;
            deger.Tag = secilenIlBilgisi;
            deger.SubItems.Add(secilenIlBilgisi.Tel);
            deger.SubItems.Add(secilenIlBilgisi.Faks);
            deger.SubItems.Add(secilenIlBilgisi.Mail);
            deger.SubItems.Add(secilenIlBilgisi.Web);
            listView1.Items.Add(deger);
        }

        private void detayGosterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBoxIL.Visible = true;
            groupBoxIL.Enabled = true;

            ILILCEBilgileri secilenIL = (ILILCEBilgileri)listView1.FocusedItem.Tag;
            richTextBoxIL.Text = secilenIL.Bilgi;
        }
    }
}
