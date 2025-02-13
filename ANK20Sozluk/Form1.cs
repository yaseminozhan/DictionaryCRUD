using ANK20Sozluk.Data;
using System.Text;
using System.Text.Json;

namespace ANK20Sozluk
{
    //Kelime s�n�f�nda 2 adet prop. oldu�u i�in liste de  kelime t�r�nde oldu�u i�in bu listedeki b�t�n kelimelerde bu iki �zelli�i de tutmak zorunda. Dolay�s�yla JSON'a d�nerken de bu �zellikler tutulur ve dosyaya yaz�l�r.
    public partial class Form1 : Form
    {

        //Dosyadaki b�t�n kelimeleri tutan liste.
        //Bu listenin olmas�n�n sebebi, dosya i�lemlerini kolayla�t�rmakt�r. Dosyaya yazarken direkt bu liste i�erisinde d�n�p yaz�yoruz. B�ylece bu liste dosya i�lemleri i�in i�imizi kolayla�t�r�yor. Ayr�ca ekleme, silme ve g�ncelleme i�lemlerinde de bu listeyi g�ncelleyece�imiz i�in dosyaya yazma i�lemi yine kolay oluyor. Yani dosya d�zenlemeyi kolayla�t�rmak i�in bu liste ile senkron ilerliyoruz.
        List<Kelime> kelimeler;


        public Form1()
        {
            InitializeComponent();
            //kelimeler listesi newleniyor
            kelimeler = new List<Kelime>();
            //Dosyadan okuma yap�p listenin i�erisine dolduruyor.
            KelimeleriGetir();

        }

        private void KelimeleriGetir()
        {
            //dosyadaki kelime JSON'lar�n� deserialize edip, kelimeler listesine ekler.

            //sozluk.txt'yi okumak i�in ortam olu�tur.
            StreamReader okuyucu = new StreamReader("sozluk.txt");

            
            StringBuilder kelimeJsonIcerik = new StringBuilder();


            //dosyadan okuyup ve listeye ekleyecek
            kelimeJsonIcerik.Append(okuyucu.ReadLine());


            //okunanlar bitmedi�i s�rece d�n
            while (kelimeJsonIcerik.ToString().Length != 0)
            {
                //her bir sat�r� kelime nesnesine d�n��t�r 
                Kelime bakilanKelime = JsonSerializer.Deserialize<Kelime>(kelimeJsonIcerik.ToString());

                //d�n��en nesneyi listeye ekle
                kelimeler.Add(bakilanKelime);

                //yeni sat�rlar i�in builder'� temizle
                kelimeJsonIcerik.Clear();
                //yeni sat�r� oku 
                kelimeJsonIcerik.Append(okuyucu.ReadLine());
            }
            //okuyucuyu kapat
            okuyucu.Close();
        }

        private void DosyayiDuzenle()
        {
            //kelimeler listesinin i�eri�ini dosyaya yazar.


            //yazmak i�in ortam olu�turuluyor.
            StreamWriter yazici = new StreamWriter("sozluk.txt");


            //liste i�erisinde d�n�yoruz
            foreach (var kelime in kelimeler)
                //her bir nesneyi serialize edip, dosyaya yaz�yoruz.
                yazici.WriteLine(JsonSerializer.Serialize(kelime));


            //i�lem bitince ortam� kapat.
            yazici.Close();



        }

        private bool KelimeVarMi(string kelime)
        {
            //Bu metot, parametrenin, liste i�erisindeki kelimelerde olup olmad���n� tespit eder. (b�y�k k���k harf duyarl�l��� yoktur) Bu metoda  txtKelime'ye yaz�lan kelimeleri g�nderece�iz.
            foreach (var klm in kelimeler)
            {
                //b�y�k k���k hassasiyeti olmadan e�itli�ine bakar.
                if (klm.Ad.ToLower() == kelime.Trim().ToLower())
                    return true;
            }
            return false;
        }

        private bool YazilaniIcerenVarmi(string kelime)
        {

            //Bu metot, parametrenin, liste i�erisindeki kelimelerde ge�ip ge�medi�ini tespit eder. (b�y�k k���k harf duyarl�l��� yoktur) Bu metoda  txtKelime'ye yaz�lan kelimeleri g�nderece�iz.
         
            foreach (var klm in kelimeler)
            {
                //b�y�k k���k hassasiyeti i�erilip i�erilmedi�ine  bakar.
                if (klm.Ad.ToLower().Contains(kelime.ToLower().Trim()))
                    return true;
            }
            return false;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            //yeni kelime ekleme metodu

            string eklenecekKelime = txtKelime.Text;

            //e�er eklenecek kelime zaten varsa ekleme, hata ver
            if (KelimeVarMi(eklenecekKelime))
                MessageBox.Show("Eklemek istedi�iniz kelime mevcuttur!");
            else
            {
                //eklenecek kelime yoksa, yeni kelime olu�tur.
               //formdaki textboxlardan de�erleri al.

                Kelime yeniKelime = new Kelime()
                {
                    Ad = txtKelime.Text,
                    Anlam = txtAnlam.Text
                };

                //listeye ekle
                kelimeler.Add(yeniKelime);

                //g�ncel listeye g�re dosyay� d�zenle
                DosyayiDuzenle();

                //ba�ar� mesaj� ver
                MessageBox.Show("Kelime ba�ar�yla eklenmi�tir.");
            }

        }
        private void Bul(string arartilanKelime)
        {

            //arat�lan� bulma metodudur.


            //�nce a��klama textbox'�n� temizle
            txtAciklama.Clear();



            // (tam metin se�ili ise ve kelime yoksa) ,

            // ya da

            // (tam metin se�ili de�ilse  ve arat�lan kelime listedeki kelimeler taraf�ndan i�erilmiyorsa)

            // HATA VER.

             if (
                (!KelimeVarMi(arartilanKelime) && chcKriter.Checked) 
                ||
                (!YazilaniIcerenVarmi(arartilanKelime) && !chcKriter.Checked)
                )
                txtAciklama.Text = "Arad���n�z kelime bulunama��t�r";
        



             //kelimeler i�erinde d�n
            foreach (var klm in kelimeler)
            {

                //tam metin se�ili ise ve kelimelerde arat�lan varsa 
                //label'a yaz.
                if (chcKriter.Checked && arartilanKelime.ToLower().Trim() == klm.Ad.ToLower())
                {
                    //s�zl�kte var demektir. O zaman anlam�n� yaz.
                    txtAciklama.Text = klm.Ad + " : " + klm.Anlam;
                    return;
                }
                //tam metin se�ili de�ilse ise ve kelimelerde i�eriliyorsa  
                //label'a yaz.
                else if (!chcKriter.Checked && klm.Ad.ToLower().Contains(arartilanKelime.Trim().ToLower()))
                {
                    txtAciklama.Text += klm.Ad + " : " + klm.Anlam + Environment.NewLine;

                }


            }
        }
       

        private void txtKelime_TextChanged(object sender, EventArgs e)
        {
            //bu metod, txtKelime 'nin i�eri�i de�i�ti�i anda �al��an metottur.

            //E�er i�erik bo� olursa a��klamay� da temizle. (�nceden arat�lanlar� temizlesin diye)
            if (string.IsNullOrWhiteSpace(txtKelime.Text))
                txtAciklama.Clear();
            else
                //bo� de�ilse de bul metodunu �a��r
                Bul(txtKelime.Text);




        }

        private void chcKriter_CheckedChanged(object sender, EventArgs e)
        {
            //buras� tam metin checkbox'�n�n se�ilme durumu de�i�ti�i zaman �al���r. Se�ili durumdan se�ilmeyen duruma geldi�i zaman VEYA se�ili olmayan durumdan se�ili duruma geldi�i zaman.

            //yine txtKelime'nin text'i de�i�ti�i zamanki metotunu �a��racak. O y�zden a�a��ya o metodu �a��rd�k.

            txtKelime_TextChanged(sender, e);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {


            //Nas�l olsa her kelime 1 KERE dosyada bulunaca�� i�in silece�imiz kelimenin ad�n� yine ekleme yerinde oldu�u gibi o textbox'a yazsak ve sil butonuna bassak, uygun olur. (Tabi �yle bir kelime varsa)

            //kelime ad�n� al
            var silinecek = txtKelime.Text;

            //ilk �nce varMi durumunu false olarak atad�k.
            bool varMi = false;


            //kelimeler i�erisinde d�n�yoruz.
            for (int i = 0; i < kelimeler.Count; i++)
            {

                //e�er b�y�k k���k harf duyarl�l��� olmadan yaz�lan kelime listedeki kelimelerden birisi ile ayn� ise (��ERME DE��L) o zaman listeden kald�r ve varM� de�i�kenini true yap.
                if (kelimeler[i].Ad.ToLower() == silinecek.Trim().ToLower())
                {
                    //listeden kald�r
                    kelimeler.Remove(kelimeler[i]);
                    //var m�y� true yap.
                    varMi = true;
                    //bulunca di�erlerine bakmaya gerek yok.
                    break;
                }
            }


            if (varMi)// varsa ba�lar�yla silindi mesaj�n� ver
            {
                DosyayiDuzenle();
                MessageBox.Show("Ba�ar�yla silinmi�tir");
            }
            else //yoksa silinecek bir �ey yok hatas�n� ver
                MessageBox.Show("Silmek istedi�iniz kelime bulunamam��t�r");





        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //�imdi de tekrar ekle

            //g�ncellenecek kelimeyi al
            var guncellenecek = txtKelime.Text;


            //e�er text boxa yaz�lan kelime kelimelerde yoksa bulunamam��t�r uyar�s�n� ver. (��ERME DE��L)
            if (!KelimeVarMi(guncellenecek))
            {
                MessageBox.Show("G�ncellemek istedi�iniz kelime mevcut de�ildir!");
                return;
            }

            //�nce listedekini g�ncelle

            //kelimeler i�erisinde d�n
            for (int i = 0; i < kelimeler.Count; i++)
            {

                //g�ncellenecek olan ad kelimelerde varsa (i�erme de�il)
                if (kelimeler[i].Ad.ToLower() == guncellenecek.Trim().ToLower())
                {

                    //Eski de�erinin silinip yenisinin g�ncellenmesi isteniyorsa
                    if (chcGuncelle.Checked)
                        //eskinin �zerine yaz
                        kelimeler[i].Anlam = txtAnlam.Text;
                    else
                    {
                        //Eski de�eri ile yeni de�eri bir arada olsun    isteniyorsa

                        //eskisine EKLE
                        kelimeler[i].Anlam += " , " + txtAnlam.Text;
                        //stringBuilder.Append(kelimeler[i].Anlam);
                        //stringBuilder.Append(" , ");
                        //stringBuilder.Append(txtAnlam.Text);

                        //kelimeler[i].Anlam = stringBuilder.ToString();
                    }

                    //g�ncellenecek olan� bulduktan sonra di�erlerine bakmaya gerek yok
                    break;
                }
            }


            //g�ncellendikten sonra dosyay� yine g�ncelle.
            DosyayiDuzenle();


            //kutular� temizle
            txtAciklama.Clear();
            txtKelime.Clear();
            txtAnlam.Clear();


            //ba�ar� mesaj�n� ver.
            MessageBox.Show("Kelime ba�ar�yla g�ncellenmi�tir.");

        }
    }
}

