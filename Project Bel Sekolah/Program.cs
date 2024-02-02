using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Component;
using Database;
using System.Threading;

namespace Project_Bel_Sekolah
{
    class Program
    {
        public static AccessDatabaseHelper DB = new AccessDatabaseHelper("./urip.accdb");

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Loading();

            Logo();

            //header
            new Kotak().SetXY(0, 0).SetWidthAndHeight(119, 4).SetBackColor(ConsoleColor.Gray).SetForeColor(ConsoleColor.Black).Tampil();
            //Kotak header = new Kotak();
            //header.X = 0;
            //header.Y = 0;
            //header.Width = 119;
            //header.Height = 4;
            //header.Tampil();

            //kiri
            new Kotak().SetXY(0, 5).SetWidthAndHeight(30, 18).SetBackColor(ConsoleColor.Gray).SetForeColor(ConsoleColor.Black).Tampil();
            //Kotak kiri = new Kotak();
            //kiri.X = 0;
            //kiri.Y = 5;
            //kiri.Width = 30;
            //kiri.Height = 20;
            //kiri.Tampil();

            //kanan
            new Kotak().SetXY(31, 5).SetWidthAndHeight(88, 18).SetBackColor(ConsoleColor.Gray).SetForeColor(ConsoleColor.Black).Tampil();

            //bawah
            new Kotak().SetXY(0, 24).SetWidthAndHeight(119, 4).SetBackColor(ConsoleColor.Gray).SetForeColor(ConsoleColor.Black).Tampil();

            //Judul
            new Tulisan().SetXY(3, 0).SetText("BEL SEKOLAH").SetBackColor(ConsoleColor.DarkRed).Tampil();

            //Sekolah
            new Tulisan().SetXY(0, 1).SetText("WEARNES EDUCATION CENTER").SetLength(120).TampilTengah();

            //alamat
            new Tulisan().SetXY(0, 2).SetText("Jl. Thamrin No. 35A Kota Madiun").SetLength(120).TampilTengah();
            new Tulisan().SetXY(0, 3).SetText("Welcome User!").SetLength(120).SetForeColor(ConsoleColor.Green).TampilTengah();

            //nama murid
            new Tulisan().
                SetXY(0, 25).
                SetText("Raffi Syahputra Atalla Kusuma").
                SetLength(120).
                TampilTengah();
            new Tulisan().
                SetXY(0, 26).
                SetText("INFORKOM II").
                SetLength(120).
                SetBackColor(ConsoleColor.Cyan).
                SetForeColor(ConsoleColor.Black).
                TampilTengah();
            new Tulisan().
                SetXY(0, 27).
                SetText("- Si Imoet -").
                SetLength(120).
                TampilTengah();
            new Tulisan().
                SetXY(105, 27).
                SetText("@bayasuuubaya").
                //SetBackColor(ConsoleColor.White).
                //SetForeColor(ConsoleColor.Black).
                Tampil();

            ////additional
            //new Tulisan().SetXY(0, 6).SetText("My ID:").SetLength(155).TampilTengah();
            //new Tulisan().SetXY(63, 8).SetText("- instagram  : @bayasuuubaya").SetLength(155).Tampil();
            //new Tulisan().SetXY(63, 9).SetText("- facebook   : Baya").SetLength(155).Tampil();
            //new Tulisan().SetXY(63, 10).SetText("- WhatsApp   : 083895309491").SetLength(155).Tampil();
            //new Tulisan().SetXY(63, 11).SetText("- Valorant   : NgabS XIX #cool").SetLength(155).Tampil();

            Menu menu = new Menu("JALANKEUN", "LIHAT JADWAL", "TAMBAH JADWAL", "EDIT JADWAL", "HAPUS JADWAL", "MY SOSMED", "BACK TO LOGO", "KELUAR");
            menu.SetXY(9, 10);
            menu.SetForeColor(ConsoleColor.Gray);
            menu.SelectedBackColor = ConsoleColor.Gray;
            menu.SelectedForeColor = ConsoleColor.Black;
            menu.Tampil();

            bool IsTheProgramRunning = true;

            while (IsTheProgramRunning)
            {
                ConsoleKeyInfo tombol = Console.ReadKey(true);

                if (tombol.Key == ConsoleKey.DownArrow)
                {
                    menu.Next();
                    menu.Tampil();
                }
                else if (tombol.Key == ConsoleKey.UpArrow)
                {
                    menu.Prev();
                    menu.Tampil();
                }
                else if (tombol.Key == ConsoleKey.Enter)
                {
                    int MenuTerpilih = menu.SelectedIndex;
                    if (MenuTerpilih == 0)
                    {
                        Jalankeun();
                    }
                    else if (MenuTerpilih == 1)
                    {
                        LihatJadwal();
                    }
                    else if (MenuTerpilih == 2)
                    {
                        TambahJadwal();
                    }
                    else if (MenuTerpilih == 3)
                    {
                        EditJadwal();
                    }
                    else if (MenuTerpilih == 4)
                    {
                        HapusData();
                    }
                    else if (MenuTerpilih == 5)
                    {
                        Sosmed();
                    }
                    else if (MenuTerpilih == 6)
                    {
                        Logo();
                    }
                    else if (MenuTerpilih == 7)
                    {
                        IsTheProgramRunning = false;
                    }
                }
            }
        }

        static void Jalankeun()
        {
            new Clear(32, 6, 86, 16).Tampil();

            new Tulisan("-=( MENJALANKAN )=-").SetXY(31, 6).SetLength(88).TampilTengah();

            Tulisan HariSekarang = new Tulisan().SetXY(31, 8).SetLength(88);
            Tulisan JamSekarang = new Tulisan().SetXY(31, 9).SetLength(88);

            string Qselect = "SELECT * FROM tbJadwal WHERE hari=@hari AND jam=@jam;";

            DB.Connect();

            bool Play = true;

            while (Play)
            {
                DateTime Sekarang = DateTime.Now;
                
                HariSekarang.SetText("Hari :" + Sekarang.ToString("dddd"));
                HariSekarang.TampilTengah();

                JamSekarang.SetText("Waktu :" + Sekarang.ToString("HH:mm:ss"));
                JamSekarang.TampilTengah();

                DataTable DT = DB.RunQuery(
                    Qselect,
                    new OleDbParameter("@hari", Sekarang.ToString("dddd")),
                    new OleDbParameter("@jam", Sekarang.ToString("HH:mm")));

                if (DT.Rows.Count > 0)
                {
                    Audio rawr = new Audio();
                    rawr.File = "./Moduleku/Suara/" + DT.Rows[0]["sound"];
                    rawr.Play();

                    new Tulisan("Bel Telah Berbunyi!!").SetXY(31, 11).SetLength(88).TampilTengah();
                    new Tulisan().SetXY(31, 12).SetLength(88).SetText(DT.Rows[0]["ket"].ToString()).TampilTengah();

                    Play = false;
                }
            }
        }

        static void LihatJadwal()
        {
            new Clear(32, 6, 86, 16).Tampil();

            new Tulisan("-=( LIHAT JADWAL )=-").SetXY(31, 6).SetLength(88).TampilTengah();

            DB.Connect();
            DataTable data = DB.RunQuery("SELECT * FROM tbJadwal;");
            DB.Disconnect();

            new Tulisan("┌─────┬──────────┬────────┬───────────────────────┐").SetXY(50, 8).Tampil();
            new Tulisan("│ NO. │   HARI   │  JAM   │ KETERANGAN            │").SetXY(50, 9).Tampil();
            new Tulisan("├─────┼──────────┼────────┼───────────────────────┤").SetXY(50,10).Tampil();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                string ID = data.Rows[i]["id"].ToString();
                string Jam = data.Rows[i]["jam"].ToString();
                string Hari = data.Rows[i]["hari"].ToString();
                string Keterangan = data.Rows[i]["ket"].ToString();

                String isi = String.Format("│{0, 5}│{1, -10}│{2, -8}│{3, -23}│", ID, Hari, Jam, Keterangan);

                new Tulisan(isi).SetXY(50, 11 + i).Tampil();
            }

            new Tulisan("└─────┴──────────┴────────┴───────────────────────┘").SetXY(50, 11 + data.Rows.Count).Tampil();

            Console.ReadKey();
            Logo();
        }

        static void TambahJadwal()
        {
            new Clear(32, 6, 86, 16).Tampil();

            new Tulisan("-=( TAMBAH JADWAL )=-").SetXY(31, 6).SetLength(88).TampilTengah();

            Inputan HariInput = new Inputan();
            HariInput.Text = "Masukkan Hari :";
            HariInput.SetXY(32, 8);

            Inputan JamInput = new Inputan();
            JamInput.Text = "Masukkan Jam :";
            JamInput.SetXY(32, 9);

            Inputan KetInput = new Inputan();
            KetInput.Text = "Masukkan Keterangan :";
            KetInput.SetXY(32, 10);

            //Inputan SoundInput = new Inputan();
            //SoundInput.Text = "Masukkan Sound :";
            //SoundInput.SetXY(32, 12);

            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(
                "pembuka.wav",
                "Istirahat I.wav",
                "Istirahat II.wav",
                "5 Menit Akhir Istirahat I.wav",
                "5 Menit Akhir Istirahat II.wav",
                "5 Menit Akhir Kegiatan Keagamaan.wav",
                "5 Menit Awal Kegiatan Keagamaan.wav",
                "5 Menit Awal Upacara.wav",
                "Akhir Pekan 1.wav",
                "Akhir Pekan 2.wav",
                "Akhir Pelajaran A.wav",
                "Akhir Pelajaran B.wav",
                "awal jam ke 1.wav",
                "Pelajaran ke 1.wav",
                "Pelajaran ke 2.wav",
                "Pelajaran ke 3.wav",
                "Pelajaran ke 4.wav",
                "Pelajaran ke 5.wav",
                "Pelajaran ke 6.wav",
                "Pelajaran ke 7.wav",
                "Pelajaran ke 8.wav",
                "Pelajaran ke 9.wav",
                "tak berjudul.wav");

            SoundInput.Text = "Masukkan Audio :";
            SoundInput.SetXY(32, 11);

            string Hari = HariInput.Read();
            string Jam = JamInput.Read();
            string Keterangan = KetInput.Read();
            string Sound = SoundInput.Read();

            DB.Connect();
            DB.RunNonQuery("INSERT INTO tbJadwal (hari, jam, ket, sound) VALUES (@hari, @jam, @ket, @sound);",
                new OleDbParameter("@hari", Hari),
                new OleDbParameter("@jam", Jam),
                new OleDbParameter("@ket", Keterangan),
                new OleDbParameter("@sound", Sound));

            DB.Disconnect();

            new Tulisan("Data Berhasil Disimpan!").SetBackColor(ConsoleColor.Green).SetForeColor(ConsoleColor.Black).SetXY(31, 13).SetLength(88).TampilTengah();

            Console.ReadKey();
            Logo();
        }
        
        static void EditJadwal()
        {
            new Clear(32, 6, 86, 16).Tampil();

            new Tulisan("-=( EDIT JADWAL )=-").SetXY(31, 6).SetLength(88).TampilTengah();

            Inputan IDInputRubah = new Inputan();
            IDInputRubah.Text = "Masukkan ID Yang Ingin Diubah :";
            IDInputRubah.SetXY(32, 9);

            Inputan HariInput = new Inputan();
            HariInput.Text = "Masukkan Hari :";
            HariInput.SetXY(32, 10);

            Inputan JamInput = new Inputan();
            JamInput.Text = "Masukkan Jam :";
            JamInput.SetXY(32, 11);

            Inputan KetInput = new Inputan();
            KetInput.Text = "Masukkan Keterangan :";
            KetInput.SetXY(32, 12);

            //Inputan SoundInput = new Inputan();
            //SoundInput.Text = "Masukkan Sound :";
            //SoundInput.SetXY(32, 13);

            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(
                "pembuka.wav",
                "Istirahat I.wav",
                "Istirahat II.wav",
                "5 Menit Akhir Istirahat I.wav",
                "5 Menit Akhir Istirahat II.wav",
                "5 Menit Akhir Kegiatan Keagamaan.wav",
                "5 Menit Awal Kegiatan Keagamaan.wav",
                "5 Menit Awal Upacara.wav",
                "Akhir Pekan 1.wav",
                "Akhir Pekan 2.wav",
                "Akhir Pelajaran A.wav",
                "Akhir Pelajaran B.wav",
                "awal jam ke 1.wav",
                "Pelajaran ke 1.wav",
                "Pelajaran ke 2.wav",
                "Pelajaran ke 3.wav",
                "Pelajaran ke 4.wav",
                "Pelajaran ke 5.wav",
                "Pelajaran ke 6.wav",
                "Pelajaran ke 7.wav",
                "Pelajaran ke 8.wav",
                "Pelajaran ke 9.wav",
                "tak berjudul.wav");

            SoundInput.Text = "Masukkan Audio :";
            SoundInput.SetXY(32, 13);

            string IdBaru = IDInputRubah.Read();
            string Hari = HariInput.Read();
            string Jam = JamInput.Read();
            string Keterangan = KetInput.Read();
            string Sound = SoundInput.Read();

            DB.Connect();
            DB.RunNonQuery("UPDATE tbJadwal SET hari=@hari, jam=@jam, ket=@ket, sound=@sound WHERE id=@id;",
                new OleDbParameter("@hari", Hari),
                new OleDbParameter("@jam", Jam),
                new OleDbParameter("@ket", Keterangan),
                new OleDbParameter("@sound", Sound),
                new OleDbParameter("@id", IdBaru));

            DB.Disconnect();

            new Tulisan("Data Baru Berhasil Disimpan!").SetBackColor(ConsoleColor.Green).SetForeColor(ConsoleColor.Black).SetXY(31, 15).SetLength(88).TampilTengah();

            Console.ReadKey();
            Logo();
        }

        static void HapusData()
        {
            new Clear(32, 6, 86, 16).Tampil();

            new Tulisan("-=( HAPUS JADWAL )=-").SetXY(31, 6).SetLength(88).TampilTengah();

            Inputan IDInput = new Inputan();
            IDInput.Text = "Masukkan ID Yang Akan Dihapus :";
            IDInput.SetXY(32, 8);
            string ID = IDInput.Read();

            // Cara Pertama
            DB.Connect();
            DB.RunNonQuery("DELETE FROM tbJadwal WHERE id=" + ID + ";");
            DB.Disconnect();

            // Cara Kedua
            //DB.Connect();
            //DB.RunNonQuery("DELETE FROM tbJadwal WHERE id=@id;", new OleDbParameter("@id", ID));

            new Tulisan("DATA BERHASIL DIHAPUS!!!").SetXY(31, 10).SetLength(88).SetBackColor(ConsoleColor.Red).TampilTengah();
        }

        static void Sosmed()
        {
            new Clear(32, 6, 86, 16).Tampil();

            new Tulisan("-=( Creator's Social Media )=-").SetXY(31, 6).SetLength(88).TampilTengah();

            new Tulisan("Instagram : @bayasuuubaya").SetXY(31, 8).SetLength(88).TampilTengah();
            new Tulisan("WhatsApp : 083895309491").SetXY(31, 9).SetLength(88).TampilTengah();
            new Tulisan("Facebook : Raffi Sak").SetXY(31, 10).SetLength(88).TampilTengah();
            new Tulisan("Saweria : https://saweria.co/Bayaaa").SetXY(31, 11).SetLength(88).TampilTengah();
            new Tulisan("Press Enter Again to Exit This Menu!").SetXY(31, 13).SetBackColor(ConsoleColor.Gray).SetForeColor(ConsoleColor.Black).SetLength(88).TampilTengah();
            new Tulisan("Press Any Key For The Animation!").SetXY(31, 14).SetBackColor(ConsoleColor.Gray).SetForeColor(ConsoleColor.Black).SetLength(88).TampilTengah();

            bool stop = true;

            //while (stop)
            {
                new Tulisan("T").SetXY(109, 22).Tampil();
                Thread.Sleep(90);
                new Tulisan("H").SetXY(110, 22).Tampil();
                Thread.Sleep(90);
                new Tulisan("A").SetXY(111, 22).Tampil();
                Thread.Sleep(90);
                new Tulisan("N").SetXY(112, 22).Tampil();
                Thread.Sleep(90);
                new Tulisan("K").SetXY(113, 22).Tampil();
                Thread.Sleep(90);
                new Tulisan("Y").SetXY(115, 22).Tampil();
                Thread.Sleep(90);
                new Tulisan("O").SetXY(116, 22).Tampil();
                Thread.Sleep(90);
                new Tulisan("U").SetXY(117, 22).Tampil();
                //Thread.Sleep(1000);

                Thread.Sleep(500);
                new Tulisan("Thanks For Your Support!!").SetXY(31, 22).SetLength(88).TampilTengah();

                Thread.Sleep(1000);
                //new Clear(32, 22, 87, 0).Tampil();

                ConsoleKeyInfo button = Console.ReadKey(true);

                Logo();

                //if (button.Key == ConsoleKey.Enter)
                {
                    stop = false;
                    Logo();
                }
            }
        }

        static void Logo()
        {
            new Clear(32, 6, 86, 16).Tampil();

            new Tulisan("^~~~~~^^^^^^^^:.       .:^.      :~~:        :!!:  ^~!!!!!!!!!!!!!~.").
                SetXY(31, 10).
                SetLength(88).
                SetForeColor(ConsoleColor.Red).
                TampilTengah();

            new Tulisan("~5555YJJ????77?77!.   .~7?JJ?:   :YP5YJ~      ?GG?.?5P5555555PPPPPP5:").
                SetXY(31, 11).
                SetLength(88).
                SetForeColor(ConsoleColor.DarkYellow).
                TampilTengah();

            new Tulisan("!5Y5~..      .~77?~  ^7??JY555:  .?YY5PPY!:   ?PP?^555~............").
                SetXY(31, 12).
                SetLength(88).
                SetForeColor(ConsoleColor.Yellow).
                TampilTengah();

            new Tulisan("!5Y5:  :^^^::^!777:.!7??~..~Y5^?^:??J7!?YP5?^ ?PP? 7Y5YYYYYYYYYY5YJ^").
                SetXY(31, 13).
                SetLength(88).
                SetForeColor(ConsoleColor.Green).
                TampilTengah();

            new Tulisan("!5Y5: .???????7~:.^!777:    .?^J5JYYY^  .^7Y5Y5PPJ  .::::::::::^?GGG:").
                SetXY(31, 14).
                SetLength(88).
                SetForeColor(ConsoleColor.Blue).
                TampilTengah();

            new Tulisan("~??J:   ...:!77!^^~7J!        .755555^     .^7JJ5PJJJYYYYYYJJJYYPGB5:").
                SetXY(31, 15).
                SetLength(88).
                SetForeColor(ConsoleColor.Magenta).
                TampilTengah();

            new Tulisan(".~~^         .::~!77:          .7????:       .^~^^!?JJYYJJJJJJJJJJ7:").
                SetXY(31, 16).
                SetLength(88).
                SetForeColor(ConsoleColor.DarkMagenta).
                TampilTengah();
        }

        static void Loading()
        {
            Console.SetCursorPosition(50, 4);
            Console.WriteLine("Tunggu say...");

            Console.SetCursorPosition(30, 6);
            Console.BackgroundColor = ConsoleColor.Green;
            for (int i = 1; i <= 50; i++)
            {
                Console.Write(" ");
                Thread.Sleep(30);
            }
        }
    }
}
