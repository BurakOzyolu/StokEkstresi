using System;
using System.Collections.Generic;

#nullable disable

namespace StokEkstresi.Models
{
    public partial class Sti
    {
        public int Id { get; set; }
        public short IslemTur { get; set; }
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Tutar { get; set; }
        public string Birim { get; set; }
    }
}
