using System;
using System.Collections.Generic;

#nullable disable

namespace StokEkstresi.Models
{
    public partial class View1
    {
        public short IslemTur { get; set; }
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public int Id { get; set; }
        public long? SiraNo { get; set; }
    }
}
