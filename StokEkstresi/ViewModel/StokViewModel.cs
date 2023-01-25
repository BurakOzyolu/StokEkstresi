using System;

namespace StokEkstresi.ViewModel
{
    public class StokViewModel
    {
        public int SiraNum { get; set; }
        public string IslemTuru { get; set; }
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public int GirisMiktar { get; set; }
        public int CikisMiktar { get; set; }
        public int StokMiktar { get; set; }
    }
}

