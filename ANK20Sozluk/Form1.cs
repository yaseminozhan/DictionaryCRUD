using ANK20Sozluk.Data;
using System.Text;
using System.Text.Json;

namespace ANK20Sozluk
{
    //Kelime sýnýfýnda 2 adet prop. olduðu için liste de  kelime türünde olduðu için bu listedeki bütün kelimelerde bu iki özelliði de tutmak zorunda. Dolayýsýyla JSON'a dönerken de bu özellikler tutulur ve dosyaya yazýlýr.
    public partial class Form1 : Form
    {

        //Dosyadaki bütün kelimeleri tutan liste.
        //Bu listenin olmasýnýn sebebi, dosya iþlemlerini kolaylaþtýrmaktýr. Dosyaya yazarken direkt bu liste içerisinde dönüp yazýyoruz. Böylece bu liste dosya iþlemleri için iþimizi kolaylaþtýrýyor. Ayrýca ekleme, silme ve güncelleme iþlemlerinde de bu listeyi güncelleyeceðimiz için dosyaya yazma iþlemi yine kolay oluyor. Yani dosya düzenlemeyi kolaylaþtýrmak için bu liste ile senkron ilerliyoruz.
        List<Kelime> kelimeler;


        public Form1()
        {
            InitializeComponent();
            //kelimeler listesi newleniyor
            kelimeler = new List<Kelime>();
            //Dosyadan okuma yapýp listenin içerisine dolduruyor.
            KelimeleriGetir();

        }

        private void KelimeleriGetir()
        {
            //dosyadaki kelime JSON'larýný deserialize edip, kelimeler listesine ekler.

            //sozluk.txt'yi okumak için ortam oluþtur.
            StreamReader okuyucu = new StreamReader("sozluk.txt");

            
            StringBuilder kelimeJsonIcerik = new StringBuilder();


            //dosyadan okuyup ve listeye ekleyecek
            kelimeJsonIcerik.Append(okuyucu.ReadLine());


            //okunanlar bitmediði sürece dön
            while (kelimeJsonIcerik.ToString().Length != 0)
            {
                //her bir satýrý kelime nesnesine dönüþtür 
                Kelime bakilanKelime = JsonSerializer.Deserialize<Kelime>(kelimeJsonIcerik.ToString());

                //dönüþen nesneyi listeye ekle
                kelimeler.Add(bakilanKelime);

                //yeni satýrlar için builder'ý temizle
                kelimeJsonIcerik.Clear();
                //yeni satýrý oku 
                kelimeJsonIcerik.Append(okuyucu.ReadLine());
            }
            //okuyucuyu kapat
            okuyucu.Close();
        }

        private void DosyayiDuzenle()
        {
            //kelimeler listesinin içeriðini dosyaya yazar.


            //yazmak için ortam oluþturuluyor.
            StreamWriter yazici = new StreamWriter("sozluk.txt");


            //liste içerisinde dönüyoruz
            foreach (var kelime in kelimeler)
                //her bir nesneyi serialize edip, dosyaya yazýyoruz.
                yazici.WriteLine(JsonSerializer.Serialize(kelime));


            //iþlem bitince ortamý kapat.
            yazici.Close();



        }

        private bool KelimeVarMi(string kelime)
        {
            //Bu metot, parametrenin, liste içerisindeki kelimelerde olup olmadýðýný tespit eder. (büyük küçük harf duyarlýlýðý yoktur) Bu metoda  txtKelime'ye yazýlan kelimeleri göndereceðiz.
            foreach (var klm in kelimeler)
            {
                //büyük küçük hassasiyeti olmadan eþitliðine bakar.
                if (klm.Ad.ToLower() == kelime.Trim().ToLower())
                    return true;
            }
            return false;
        }

        private bool YazilaniIcerenVarmi(string kelime)
        {

            //Bu metot, parametrenin, liste içerisindeki kelimelerde geçip geçmediðini tespit eder. (büyük küçük harf duyarlýlýðý yoktur) Bu metoda  txtKelime'ye yazýlan kelimeleri göndereceðiz.
         
            foreach (var klm in kelimeler)
            {
                //büyük küçük hassasiyeti içerilip içerilmediðine  bakar.
                if (klm.Ad.ToLower().Contains(kelime.ToLower().Trim()))
                    return true;
            }
            return false;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            //yeni kelime ekleme metodu

            string eklenecekKelime = txtKelime.Text;

            //eðer eklenecek kelime zaten varsa ekleme, hata ver
            if (KelimeVarMi(eklenecekKelime))
                MessageBox.Show("Eklemek istediðiniz kelime mevcuttur!");
            else
            {
                //eklenecek kelime yoksa, yeni kelime oluþtur.
               //formdaki textboxlardan deðerleri al.

                Kelime yeniKelime = new Kelime()
                {
                    Ad = txtKelime.Text,
                    Anlam = txtAnlam.Text
                };

                //listeye ekle
                kelimeler.Add(yeniKelime);

                //güncel listeye göre dosyayý düzenle
                DosyayiDuzenle();

                //baþarý mesajý ver
                MessageBox.Show("Kelime baþarýyla eklenmiþtir.");
            }

        }
        private void Bul(string arartilanKelime)
        {

            //aratýlaný bulma metodudur.


            //önce açýklama textbox'ýný temizle
            txtAciklama.Clear();



            // (tam metin seçili ise ve kelime yoksa) ,

            // ya da

            // (tam metin seçili deðilse  ve aratýlan kelime listedeki kelimeler tarafýndan içerilmiyorsa)

            // HATA VER.

             if (
                (!KelimeVarMi(arartilanKelime) && chcKriter.Checked) 
                ||
                (!YazilaniIcerenVarmi(arartilanKelime) && !chcKriter.Checked)
                )
                txtAciklama.Text = "Aradýðýnýz kelime bulunamaýþtýr";
        



             //kelimeler içerinde dön
            foreach (var klm in kelimeler)
            {

                //tam metin seçili ise ve kelimelerde aratýlan varsa 
                //label'a yaz.
                if (chcKriter.Checked && arartilanKelime.ToLower().Trim() == klm.Ad.ToLower())
                {
                    //sözlükte var demektir. O zaman anlamýný yaz.
                    txtAciklama.Text = klm.Ad + " : " + klm.Anlam;
                    return;
                }
                //tam metin seçili deðilse ise ve kelimelerde içeriliyorsa  
                //label'a yaz.
                else if (!chcKriter.Checked && klm.Ad.ToLower().Contains(arartilanKelime.Trim().ToLower()))
                {
                    txtAciklama.Text += klm.Ad + " : " + klm.Anlam + Environment.NewLine;

                }


            }
        }
       

        private void txtKelime_TextChanged(object sender, EventArgs e)
        {
            //bu metod, txtKelime 'nin içeriði deðiþtiði anda çalýþan metottur.

            //Eðer içerik boþ olursa açýklamayý da temizle. (önceden aratýlanlarý temizlesin diye)
            if (string.IsNullOrWhiteSpace(txtKelime.Text))
                txtAciklama.Clear();
            else
                //boþ deðilse de bul metodunu çaðýr
                Bul(txtKelime.Text);




        }

        private void chcKriter_CheckedChanged(object sender, EventArgs e)
        {
            //burasý tam metin checkbox'ýnýn seçilme durumu deðiþtiði zaman çalýþýr. Seçili durumdan seçilmeyen duruma geldiði zaman VEYA seçili olmayan durumdan seçili duruma geldiði zaman.

            //yine txtKelime'nin text'i deðiþtiði zamanki metotunu çaðýracak. O yüzden aþaðýya o metodu çaðýrdýk.

            txtKelime_TextChanged(sender, e);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {


            //Nasýl olsa her kelime 1 KERE dosyada bulunacaðý için sileceðimiz kelimenin adýný yine ekleme yerinde olduðu gibi o textbox'a yazsak ve sil butonuna bassak, uygun olur. (Tabi öyle bir kelime varsa)

            //kelime adýný al
            var silinecek = txtKelime.Text;

            //ilk önce varMi durumunu false olarak atadýk.
            bool varMi = false;


            //kelimeler içerisinde dönüyoruz.
            for (int i = 0; i < kelimeler.Count; i++)
            {

                //eðer büyük küçük harf duyarlýlýðý olmadan yazýlan kelime listedeki kelimelerden birisi ile ayný ise (ÝÇERME DEÐÝL) o zaman listeden kaldýr ve varMý deðiþkenini true yap.
                if (kelimeler[i].Ad.ToLower() == silinecek.Trim().ToLower())
                {
                    //listeden kaldýr
                    kelimeler.Remove(kelimeler[i]);
                    //var mýyý true yap.
                    varMi = true;
                    //bulunca diðerlerine bakmaya gerek yok.
                    break;
                }
            }


            if (varMi)// varsa baþlarýyla silindi mesajýný ver
            {
                DosyayiDuzenle();
                MessageBox.Show("Baþarýyla silinmiþtir");
            }
            else //yoksa silinecek bir þey yok hatasýný ver
                MessageBox.Show("Silmek istediðiniz kelime bulunamamýþtýr");





        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //þimdi de tekrar ekle

            //güncellenecek kelimeyi al
            var guncellenecek = txtKelime.Text;


            //eðer text boxa yazýlan kelime kelimelerde yoksa bulunamamýþtýr uyarýsýný ver. (ÝÇERME DEÐÝL)
            if (!KelimeVarMi(guncellenecek))
            {
                MessageBox.Show("Güncellemek istediðiniz kelime mevcut deðildir!");
                return;
            }

            //önce listedekini güncelle

            //kelimeler içerisinde dön
            for (int i = 0; i < kelimeler.Count; i++)
            {

                //güncellenecek olan ad kelimelerde varsa (içerme deðil)
                if (kelimeler[i].Ad.ToLower() == guncellenecek.Trim().ToLower())
                {

                    //Eski deðerinin silinip yenisinin güncellenmesi isteniyorsa
                    if (chcGuncelle.Checked)
                        //eskinin üzerine yaz
                        kelimeler[i].Anlam = txtAnlam.Text;
                    else
                    {
                        //Eski deðeri ile yeni deðeri bir arada olsun    isteniyorsa

                        //eskisine EKLE
                        kelimeler[i].Anlam += " , " + txtAnlam.Text;
                        //stringBuilder.Append(kelimeler[i].Anlam);
                        //stringBuilder.Append(" , ");
                        //stringBuilder.Append(txtAnlam.Text);

                        //kelimeler[i].Anlam = stringBuilder.ToString();
                    }

                    //güncellenecek olaný bulduktan sonra diðerlerine bakmaya gerek yok
                    break;
                }
            }


            //güncellendikten sonra dosyayý yine güncelle.
            DosyayiDuzenle();


            //kutularý temizle
            txtAciklama.Clear();
            txtKelime.Clear();
            txtAnlam.Clear();


            //baþarý mesajýný ver.
            MessageBox.Show("Kelime baþarýyla güncellenmiþtir.");

        }
    }
}

