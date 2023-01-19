using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    public class CPU
    {
        private int broj_Jezgra;
        private string naziv_CPU;
        private DateTime datum_Izlaska;
        private string slika_Proizvodjaca;
        private string tekstualni_Fajl;


        public CPU() { }

        public CPU(int broj_Jezgra, string naziv_CPU, DateTime datum_Izlaska, string slika_Proizvodjaca, string tekstualni_Fajl)
        {
            Broj_Jezgra = broj_Jezgra;
            Naziv_CPU = naziv_CPU;
            Datum_Izlaska = datum_Izlaska;
            Slika_Proizvodjaca = slika_Proizvodjaca;
            Tekstualni_Fajl = tekstualni_Fajl;            
        }

        public int Broj_Jezgra { get => broj_Jezgra; set => broj_Jezgra = value; }
        public string Naziv_CPU { get => naziv_CPU; set => naziv_CPU = value; }
        public DateTime Datum_Izlaska { get => datum_Izlaska; set => datum_Izlaska = value; }
        public string Slika_Proizvodjaca { get => slika_Proizvodjaca; set => slika_Proizvodjaca = value; }
        public string Tekstualni_Fajl { get => tekstualni_Fajl; set => tekstualni_Fajl = value; }
    }
}
