﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileGue.Data
{
    public class AppConstants
    {
        public static string StorageEndpoint = "";
        public static string StorageAccess = "";
        public static string StorageSecret = "";
        public static string StorageBucket = "";

        public static long MaxAllowedFileSize = 10*1024000;
        public static long DefaultStorageSize = 21474836480;

        public static string UploadUrlPrefix = "https://storagemurahaje.blob.core.windows.net/FileGue";
        public const int FACE_WIDTH = 180;
        public const int FACE_HEIGHT = 135;
        public const string AppName = "FileGue";
        public const string FACE_SUBSCRIPTION_KEY = "a068e60df8254cc5a187e3e8c644f316";
        public const string FACE_ENDPOINT = "https://southeastasia.api.cognitive.microsoft.com/";


        public static string ProxyIP = "";
        public static string SQLConn = "";
        public static string EmailAccount = "FileGue@gmail.com";
        public const string GemLic = "EDWG-SKFA-D7J1-LDQ5";
        public static string RedisCon { set; get; }
        public static string RedisPassword { set; get; }

        public static string GMapApiKey { get; set; }
        public static string BlobConn { get; set; }
        public static string? DefaultPass { get; set; }
        public static string[] DaftarProfesi = new string[] { "Akuntan",
"Aktor",
"Aktris",
"Aktuaris",
"Administratur",
"Manajer Periklanan",
"Ahli Ilmu Tanah",
"Pengontrol Lalu Lintas Udara",
"Mekanik Pesawat",
"Duta Besar",
"Animator",
"Antropolog",
"Arkeolog",
"Arsitek",
"Kritikus Seni",
"Kurator Galeri",
"Fotografer Seni",
"Seniman",
"Juru Taksir",
"Asisten Rumah Tangga",
"Ahli Nujum",
"Astronot",
"Ahli Astronomi",
"Atlet",
"Akuntan",
"Penulis",
"Tukang Roti",
"Tukang Cukur",
"Pelayan Bar",
"Pemain Bass",
"Ahli Kecantikan",
"Peternak Lebah",
"Bibliograf",
"Ahli Biologi",
"Blogger",
"Ahli Botani",
"Pembuat Bir",
"Inspektur Bangunan",
"Supir Bus",
"Pengusaha",
"Kepala Pelayan",
"Montir",
"Tukang Kayu",
"Pemahat",
"Kasir",
"Selebritis",
"Koki",
"Koreografer",
"Pemain Sirkus",
"Tukang Bersih-Bersih",
"Pelawak",
"Komikus",
"Penyusun",
"Insinyur Komputer",
"Konduktor",
"Supir Bus",
"Pembuat Kue dan Manisan",
"Koki",
"Penari",
"Penagih Hutang",
"Perawat Gigi",
"Ahli bedah gigi",
"Dokter Gigi",
"Desainer",
"Detektif",
"Penata Diet",
"Penggali",
"Diplomat",
"Direktur",
"Pencuci Piring",
"Supir",
"Dokter",
"Pelatih Anjing",
"Penjaga Pintu",
"Supir",
"Instruktur Menyetir",
"Pemain Drum",
"Ahli Ekologi",
"Ahli Ekonomi",
"Editor",
"Pedidik",
"Karyawan",
"Insinyur",
"Wiraswasta",
"Ahli Ergonomi",
"Ahli Etnografi",
"Buruh Tani",
"Petani",
"Perancang Busana",
"Kritikus Film",
"Editor Film",
"Penata Latar Film",
"Analis Keuangan",
"Petugas Keuangan",
"Ahli Seni Murni",
"Pemadam Kebakaran",
"Pemadam Kebakaran",
"Peternak Ikan",
"Nelayan",
"Insinyur Penerbangan",
"Ahli Perhutanan",
"Peramal",
"Tukang Kebun",
"Ahli Genetika",
"Ahli Ilmu Bumi",
"Perancang Grafis",
"Gitaris",
"Peretas",
"Penata Rambut",
"Tukang",
"Penjual Topi",
"Kepala Sekolah",
"Ahli Herbal",
"Sejarawan",
"Pembawa Acara",
"Tukang Angkut Barang di Hotel",
"Ahli Hidrologi",
"Ilustrator",
"Inspektur",
"Penerjemah",
"Jurnalis",
"Hakim",
"Raja",
"Perajut",
"Buruh",
"Pengukur Tanah",
"Induk Semang / Pemilik Rumah",
"Pengacara",
"Dosen",
"Pustakawan",
"Penjaga Pantai",
"Inspektur Permesinan",
"Juru Rias",
"Akuntan Manajemen",
"Konsultan Manajemen",
"Manajer",
"Juru Rawat Kuku Tangan",
"Insinyur Kelautan",
"Aktor",
"Manajer Pemasaran",
"Tukang Pijat",
"Pembawa Acara",
"Ahli Matematika",
"Insinyur Mekanik",
"Insinyur Mekatronik",
"Pedagang / Saudagar",
"Ahli Metalurgi",
"Ahli Meteorologi",
"Ahli Metrologi",
"Ahli Mikrobiologi",
"Bidan",
"Tukang Giling",
"Buruh Tambang",
"Model",
"Kurator Museum",
"Direktur Musik",
"Musisi",
"Pengasuh Balita",
"Pembawa Berita",
"Editor Koran",
"Novelis",
"Perawat",
"Perawat Anak-anak",
"Ahli Nutrisi",
"Pesuruh Kantor",
"Operator",
"Penghancur Bijih",
"Dokter Spesialis Anak",
"Pelukis",
"Pastur",
"Pendeta",
"Agen Paten",
"Juru Rawat Kuku Kaki",
"Apoteker",
"Filsuf",
"Juru Potret",
"Ahli Fisika",
"Ahli Pengobatan Badan",
"Pianis",
"Pengatur Sistem Piano",
"Pilot",
"Polisi",
"Asisten Polisi",
"Polisi Penyidik",
"Politikus",
"Tukang Pos",
"Presiden",
"Perdana Menteri",
"Programmer",
"Jaksa",
"Psikiater",
"Psikolog",
"Notaris",
"Dalang",
"Petugas Pembelian",
"Teknisi Kontrol Kualitas",
"Inspektur Kualitas",
"Ratu",
"Agen Perumahan",
"Resepsionis",
"Wasit",
"Wartawan",
"Peneliti",
"Insinyur Keselamatan",
"Pelaut",
"Asisten Penjualan",
"Manajer Penjualan",
"Representatif Penjualan",
"Pedagang",
"Ilmuwan",
"Editor Naskah",
"Pengukir / Pemahat",
"Nahkoda",
"Sekretaris",
"Satpam",
"Penjaga Keamanan",
"Penjual",
"Gembala",
"Nahkoda",
"Penyanyi",
"Relawan",
"Ahli Sosiologi",
"Tentara",
"Penggubah Lagu",
"Ahli Statistik",
"Pramugara",
"Pramugari",
"Makelar Saham",
"Petugas Toko",
"Pemeran Pengganti",
"Penjahit",
"Konsultan Pajak",
"Guru",
"Teknisi",
"Operator Telepon",
"Petugas Bank",
"Pemandu Wisata",
"Pedagang",
"Masinis",
"Operator Kereta Api",
"Pelatih",
"Penerjemah",
"Pemain Biola",
"Pramusaji",
"Pramusaji",
"Penenun",
"Pembuat Wig",
"Penjaga Kebun Binatang",
"Ibu Rumah Tangga",
"Tidak Bekerja"
        };
        
    }
}
