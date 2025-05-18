using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Uygulamasi
{
    public class Musteri
    {
        public int MusteriId { get; set; }
        public string MusteriAdi { get; set; }
        public string IletisimBilgileri { get; set; }

        public Musteri(int musteriId, string musteriAdi, string iletisimBilgileri)
        {
            MusteriId = musteriId;
            MusteriAdi = musteriAdi;
            IletisimBilgileri = iletisimBilgileri;
        }

        public override string ToString()
        {
            return $"Müşteri ID: {MusteriId}, Adı: {MusteriAdi}, İletişim: {IletisimBilgileri}";
        }
    }

    public class Satis
    {
        public int SatisId { get; set; }
        public Musteri Musteri { get; set; }
        public string SatisDetaylari { get; set; }
        public DateTime SatisTarihi { get; set; }

        public Satis(int satisId, Musteri musteri, string satisDetaylari)
        {
            SatisId = satisId;
            Musteri = musteri;
            SatisDetaylari = satisDetaylari;
            SatisTarihi = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Satış ID: {SatisId}, Müşteri: {Musteri.MusteriAdi}, Detay: {SatisDetaylari}, Tarih: {SatisTarihi}";
        }
    }

    public class Destek
    {
        public int DestekId { get; set; }
        public Musteri Musteri { get; set; }
        public string TalepDetaylari { get; set; }
        public DateTime TalepTarihi { get; set; }

        public Destek(int destekId, Musteri musteri, string talepDetaylari)
        {
            DestekId = destekId;
            Musteri = musteri;
            TalepDetaylari = talepDetaylari;
            TalepTarihi = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Destek Talep ID: {DestekId}, Müşteri: {Musteri.MusteriAdi}, Talep: {TalepDetaylari}, Tarih: {TalepTarihi}";
        }
    }

    public class CRMSystem
    {
        public List<Musteri> Musteriler { get; set; }
        public List<Satis> Satislar { get; set; }
        public List<Destek> DestekTalepleri { get; set; }
        private int nextMusteriId = 1;
        private int nextSatisId = 1;
        private int nextDestekId = 1;

        public CRMSystem()
        {
            Musteriler = new List<Musteri>();
            Satislar = new List<Satis>();
            DestekTalepleri = new List<Destek>();
        }

        public void MusteriEkle()
        {
            Console.Write("Müşteri Adı: ");
            string musteriAdi = Console.ReadLine();
            Console.Write("İletişim Bilgileri: ");
            string iletisim = Console.ReadLine();

            Musteri yeniMusteri = new Musteri(nextMusteriId++, musteriAdi, iletisim);
            Musteriler.Add(yeniMusteri);

            Console.WriteLine("Müşteri başarıyla eklendi:");
            Console.WriteLine(yeniMusteri);
        }
        public void MusterileriListele()
        {
            if (Musteriler.Count == 0)
            {
                Console.WriteLine("Sistemde kayıtlı müşteri bulunmamaktadır.");
                return;
            }
            foreach (var musteri in Musteriler)
            {
                Console.WriteLine(musteri);
            }
        }

        public void SatisEkle()
        {
            Console.Write("Satış eklemek için müşteri ID giriniz: ");
            if (!int.TryParse(Console.ReadLine(), out int musteriId))
            {
                Console.WriteLine("Geçersiz ID!");
                return;
            }
            Musteri musteri = Musteriler.Find(m => m.MusteriId == musteriId);
            if (musteri == null)
            {
                Console.WriteLine("Müşteri bulunamadı.");
                return;
            }
            Console.Write("Satış Detaylarını Giriniz: ");
            string detay = Console.ReadLine();

            Satis yeniSatis = new Satis(nextSatisId++, musteri, detay);
            Satislar.Add(yeniSatis);

            Console.WriteLine("Satış başarıyla eklendi:");
            Console.WriteLine(yeniSatis);
        }

        public void SatislariListele()
        {
            if (Satislar.Count == 0)
            {
                Console.WriteLine("Sistemde satış kaydı bulunmamaktadır.");
                return;
            }
            foreach (var satis in Satislar)
            {
                Console.WriteLine(satis);
            }
        }
        public void DestekTalebiOlustur()
        {
            Console.Write("Destek talebi oluşturmak için müşteri ID giriniz: ");
            if (!int.TryParse(Console.ReadLine(), out int musteriId))
            {
                Console.WriteLine("Geçersiz ID!");
                return;
            }
            Musteri musteri = Musteriler.Find(m => m.MusteriId == musteriId);
            if (musteri == null)
            {
                Console.WriteLine("Müşteri bulunamadı.");
                return;
            }
            Console.Write("Destek Talebi Detaylarını Giriniz: ");
            string detay = Console.ReadLine();

            Destek yeniDestek = new Destek(nextDestekId++, musteri, detay);
            DestekTalepleri.Add(yeniDestek);

            Console.WriteLine("Destek talebi başarıyla oluşturuldu:");
            Console.WriteLine(yeniDestek);
        }

        public void DestekleriListele()
        {
            if (DestekTalepleri.Count == 0)
            {
                Console.WriteLine("Sistemde destek talebi bulunmamaktadır.");
                return;
            }
            foreach (var destek in DestekTalepleri)
            {
                Console.WriteLine(destek);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PerformLogin();

            CRMSystem crm = new CRMSystem();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n--- Müşteri İlişkileri Yönetimi (CRM) ---");
                Console.WriteLine("1. Müşteri Ekle");
                Console.WriteLine("2. Müşterileri Listele");
                Console.WriteLine("3. Satış Ekle");
                Console.WriteLine("4. Satışları Listele");
                Console.WriteLine("5. Destek Talebi Oluştur");
                Console.WriteLine("6. Destek Taleplerini Listele");
                Console.WriteLine("7. Çıkış");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();
                Console.WriteLine();

                switch (secim)
                {
                    case "1":
                        crm.MusteriEkle();
                        break;
                    case "2":
                        crm.MusterileriListele();
                        break;
                    case "3":
                        crm.SatisEkle();
                        break;
                    case "4":
                        crm.SatislariListele();
                        break;
                    case "5":
                        crm.DestekTalebiOlustur();
                        break;
                    case "6":
                        crm.DestekleriListele();
                        break;
                    case "7":
                        Console.WriteLine("Sistemden çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyiniz.");
                        break;
                }

                Console.WriteLine("\nDevam etmek için bir tuşa basınız...");
                Console.ReadKey();
            }
        }

        static void PerformLogin()
        {
            int maxAttempts = 3;
            int attempt = 0;
            bool isAuthenticated = false;

            while (attempt < maxAttempts && !isAuthenticated)
            {
                Console.Clear();
                WriteHeader();

                Console.Write("Kullanıcı Adı: ");
                string username = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(username))
                {
                    Console.WriteLine("Kullanıcı adı boş olamaz.");
                    attempt++;
                    Console.WriteLine("Devam etmek için bir tuşa basınız...");
                    Console.ReadKey();
                    continue;
                }

                Console.Write("Parola: ");
                string password = ReadPassword();
                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Parola boş olamaz.");
                    attempt++;
                    Console.WriteLine("Devam etmek için bir tuşa basınız...");
                    Console.ReadKey();
                    continue;
                }

                if (username.Equals("batuhan", StringComparison.OrdinalIgnoreCase) && password == "acar123")
                {
                    isAuthenticated = true;
                    Console.WriteLine("\nYönetici olarak giriş başarılı.");
                    Console.WriteLine("\nGiriş işlemi tamamlandı. Ana menüye yönlendiriliyorsunuz...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\nGiriş bilgileri hatalı. Tekrar deneyiniz.");
                    attempt++;
                    if (attempt < maxAttempts)
                    {
                        Console.WriteLine($"Kalan deneme hakkınız: {maxAttempts - attempt}");
                    }
                    Console.WriteLine("Devam etmek için bir tuşa basınız...");
                    Console.ReadKey();
                }
            }

            if (!isAuthenticated)
            {
                Console.WriteLine("Çok fazla hatalı giriş denemesi. Program sonlandırılıyor.");
                Environment.Exit(0);
            }
        }

        static void WriteHeader()
        {
            Console.WriteLine("***********************************************");
            Console.WriteLine("     Müşteri İlişkileri Yönetimi (CRM)         ");
            Console.WriteLine("***********************************************\n");
        }

        static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password.Remove(password.Length - 1, 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    Console.Write("*");
                    password.Append(key.KeyChar);
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password.ToString();
        }
    }
}
