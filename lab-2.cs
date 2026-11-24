using System;
using System.Collections.Generic;

namespace CihazYonetimi
{
    // 1. Cihaz türlerini temsil eden enum
    public enum CihazTuru
    {
        Bilgisayar = 1,
        Telefon,
        Tablet,
        Yazici,
        Televizyon
    }

    // 2. Cihaz sınıfı
    public class Cihaz
    {
        // 3. Kimlik, Ad, Tur ve AcikMi bilgileri
        public int Kimlik { get; private set; }
        public string Ad { get; private set; }
        public CihazTuru Tur { get; private set; }
        public bool AcikMi { get; private set; }

        // 4. Bilgiler constructor ile verilir, cihaz başlangıçta kapalıdır
        public Cihaz(int kimlik, string ad, CihazTuru tur)
        {
            Kimlik = kimlik;
            Ad = ad;
            Tur = tur;
            AcikMi = false;
        }

        // 5. Ac() ve Kapat() metotları
        public void Ac()
        {
            if (AcikMi)
            {
                Console.WriteLine($"{Ad} zaten açık.");
                return;
            }
            AcikMi = true;
            Console.WriteLine($"{Ad} açıldı.");
        }

        public void Kapat()
        {
            if (!AcikMi)
            {
                Console.WriteLine($"{Ad} zaten kapalı.");
                return;
            }
            AcikMi = false;
            Console.WriteLine($"{Ad} kapatıldı.");
        }

        // 6. Bilgileri ekrana yazdıran metot
        public void BilgileriGoster()
        {
            string durum = AcikMi ? "Açık" : "Kapalı";
            Console.WriteLine($"Kimlik: {Kimlik} | Ad: {Ad} | Tür: {Tur} | Durum: {durum}");
        }
    }

    class Program
    {
        // 7. Cihazlar List<Cihaz> içinde saklanır
        static List<Cihaz> cihazlar = new List<Cihaz>();

        static void Main(string[] args)
        {
            // 8. Basit menü
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== CİHAZ YÖNETİMİ =====");
                Console.WriteLine("1. Cihazları Listele");
                Console.WriteLine("2. Yeni Cihaz Ekle");
                Console.WriteLine("3. Cihaz Aç");
                Console.WriteLine("4. Cihaz Kapat");
                Console.WriteLine("0. Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        CihazlariListele();
                        break;
                    case "2":
                        CihazEkle();
                        break;
                    case "3":
                        CihazAc();
                        break;
                    case "4":
                        CihazKapat();
                        break;
                    case "0":
                        Console.WriteLine("Programdan çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }
            }
        }

        static void CihazlariListele()
        {
            if (cihazlar.Count == 0)
            {
                Console.WriteLine("Kayıtlı cihaz yok.");
                return;
            }

            foreach (Cihaz c in cihazlar)
            {
                c.BilgileriGoster();
            }
        }

        static void CihazEkle()
        {
            int kimlik = SayiOku("Cihaz kimliği: ");

            // 9. Aynı kimliğe sahip iki cihaz eklenemez
            if (CihazBul(kimlik) != null)
            {
                Console.WriteLine("Bu kimliğe sahip bir cihaz zaten var!");
                return;
            }

            Console.Write("Cihaz adı: ");
            string ad = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ad))
            {
                Console.WriteLine("Cihaz adı boş olamaz!");
                return;
            }

            Console.WriteLine("Cihaz türleri:");
            foreach (CihazTuru t in Enum.GetValues(typeof(CihazTuru)))
            {
                Console.WriteLine($"{(int)t}. {t}");
            }

            int turNo = SayiOku("Tür seçiniz: ");
            if (!Enum.IsDefined(typeof(CihazTuru), turNo))
            {
                Console.WriteLine("Geçersiz cihaz türü!");
                return;
            }

            Cihaz yeniCihaz = new Cihaz(kimlik, ad, (CihazTuru)turNo);
            cihazlar.Add(yeniCihaz);
            Console.WriteLine("Cihaz başarıyla eklendi.");
        }

        static void CihazAc()
        {
            int kimlik = SayiOku("Açılacak cihazın kimliği: ");
            Cihaz cihaz = CihazBul(kimlik);

            // 10. Olmayan cihaz kontrolü
            if (cihaz == null)
            {
                Console.WriteLine("Bu kimliğe sahip bir cihaz bulunamadı!");
                return;
            }

            cihaz.Ac();
        }

        static void CihazKapat()
        {
            int kimlik = SayiOku("Kapatılacak cihazın kimliği: ");
            Cihaz cihaz = CihazBul(kimlik);

            // 10. Olmayan cihaz kontrolü
            if (cihaz == null)
            {
                Console.WriteLine("Bu kimliğe sahip bir cihaz bulunamadı!");
                return;
            }

            cihaz.Kapat();
        }

        static Cihaz CihazBul(int kimlik)
        {
            foreach (Cihaz c in cihazlar)
            {
                if (c.Kimlik == kimlik)
                    return c;
            }
            return null;
        }

        // Kullanıcıdan geçerli bir sayı alana kadar tekrar sorar
        static int SayiOku(string mesaj)
        {
            int sayi;
            Console.Write(mesaj);
            while (!int.TryParse(Console.ReadLine(), out sayi))
            {
                Console.Write("Lütfen geçerli bir sayı giriniz: ");
            }
            return sayi;
        }
    }
}
