using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using eUtazás;

namespace eUtazás
{

    internal struct Utazas
    {
        private string Allomas { get; set; }
        private string Ido { get; set; }
        private int Id { get; set; }
        private string Berlet_type { get; set; }
        private int Lejar_ido { get; set; }



        public Utazas(string allomas, string ido, int id, string berlet_type, int lejar_ido)
        {
            Allomas = allomas;
            Ido = ido;
            Id = id;
            Berlet_type = berlet_type;
            Lejar_ido = lejar_ido;
        }

        public string GetAllomas() => Allomas;
        public string GetIdo() => Ido;
        public int GetId() => Id;
        public string GetBerlet_type() => Berlet_type;
        public int GetLejar_ido() => Lejar_ido;

    }
    internal class Program
    {
        static List<Utazas> BeolvasasAdatok(string fajlNev)
        {
            List<Utazas> adatok = new List<Utazas>();

            try
            {
                string[] sorok = File.ReadAllLines(fajlNev);
                foreach (string sor in sorok)
                {
                    var elemek = sor.Split(' ');

                    string allomas = elemek[0];
                    string ido = elemek[1];
                    int id = int.Parse(elemek[2]);
                    string berlet_type = elemek[3];
                    int lejar_ido = int.Parse(elemek[4]);

                    adatok.Add(new Utazas(allomas, ido, id, berlet_type, lejar_ido));
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
            }
            return adatok;
        }

        static void mennyiutas (List<Utazas> utazas)
        {
            Console.WriteLine("2. feladat");
            int mennyi = 0;
            foreach (var item in utazas)
            {
                mennyi++;
            }
            
            Console.WriteLine($"A buszra {mennyi} utas akart felszállni");
        }

        static void nemszallhatfel (List<Utazas> utazas)
        {
            int mennyi = 0;
            foreach (var item in utazas)
            {
                if (item.GetLejar_ido() <= 20190326 || item.GetLejar_ido() == 0)
                {
                    mennyi++;
                }
            }
            Console.WriteLine($"A buszra {mennyi} utas nem szálhatott fel");
        }
        static void Main(string[] args)
        {
            var fajlNev = "utasadat.txt";

            List<Utazas> utazasAdatok = BeolvasasAdatok(fajlNev);
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var adat in utazasAdatok)
            {
                Console.WriteLine($"|{adat.GetAllomas(),2}|{adat.GetIdo()}|{adat.GetId()}|{adat.GetBerlet_type()}|{adat.GetLejar_ido(),8}|");
            }

            mennyiutas( utazasAdatok);
            nemszallhatfel( utazasAdatok);

            Console.ReadKey();

        }
    }
}

