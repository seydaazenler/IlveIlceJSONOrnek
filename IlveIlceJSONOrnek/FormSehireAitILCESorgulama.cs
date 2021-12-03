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
    public partial class FormSehireAitILCESorgulama : Form
    {
        public FormSehireAitILCESorgulama()
        {
            InitializeComponent();
        }

        //Global Alan
        ILServis ilServisim = new ILServis();
        ILILCEServis ilceServis = new ILILCEServis();
        private void FormSehireAitILCESorgulama_Load(object sender, EventArgs e)
        {
            //Formun üzerinde çift tıklayarak bu sayfaya ulaşırız
            comboBoxILLER.DataSource = ilServisim.IlleriGetir();
            comboBoxILLER.DisplayMember = "ILAdi";
            comboBoxILLER.ValueMember = "PlakaKodu";
        }

        private void comboBoxILLER_SelectedIndexChanged(object sender, EventArgs e)
        {
            //yani içindeki değer değiştiğinde bu Event(metot) çalışacak
            IL secilenIL = comboBoxILLER.SelectedItem as IL;

            //BLL'in bilgileri getirmesine ihtiyaç var
            //BLL'de öyle bir metot olmalı ki il ismini parametre olarak verince ilçeye dair detay bilgileri versin
            List<ILILCEBilgileri> sehreAitIlcelerListem= ilceServis.ILAdinaGoreIlceleriGetir(secilenIL.ILAdi);

            listView1.Items.Clear();
            foreach (var item in sehreAitIlcelerListem)
            {
                //Her birini listView Item olarak tutup
                ListViewItem li = new ListViewItem();
                li.Text = item.Ismi;
                li.Tag = item;
                li.SubItems.Add(item.Tel);
                li.SubItems.Add(item.Mail);

                //listView içine eklesin
                listView1.Items.Add(li);

            }
        }
    }
}
