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
        private string Lejar_ido { get; set; }



        public Utazas(string allomas, string ido, int id, string berlet_type, string lejar_ido)
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
        public string GetLejar_ido() => Lejar_ido;

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
                    string lejar_ido = elemek[4];

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

        static string LegnepszerubbAllomas(List<Utazas> utazasAdatok)
        {
            Dictionary<string, int> allomasSzamlalo = new Dictionary<string, int>();

            foreach (var utazas in utazasAdatok)
            {
                if (allomasSzamlalo.ContainsKey(utazas.GetAllomas()))
                {
                    allomasSzamlalo[utazas.GetAllomas()]++;
                }
                else
                {
                    allomasSzamlalo[utazas.GetAllomas()] = 1;
                }
            }

            string legtobbAllomas = "";
            int maxSzam = 0;

            foreach (var par in allomasSzamlalo)
            {
                if (par.Value > maxSzam)
                {
                    legtobbAllomas = par.Key;
                    maxSzam = par.Value;
                }
            }

            return legtobbAllomas;
        }

        static void mennyiutas(List<Utazas> utazas)
        {
            Console.WriteLine("2. feladat");
            int mennyi = utazas.Count;
            Console.WriteLine($"A buszra {mennyi} utas akart felszállni");
        }

        static void nemszallhatfel(List<Utazas> utazas)
        {
            int mennyi = 0;
            foreach (var item in utazas)
            {
                if (item.GetLejar_ido() == "0")
                {
                    mennyi++;
                }
            }
            Console.WriteLine($"A buszra {mennyi} utas nem szálhatott fel");
        }

        static void kendezmenyesingyenes(List<Utazas> utazas)
        {
            int kedvezmenyes = 0;
            int ingyenes = 0;
            foreach (var item in utazas)
            {
                if (item.GetBerlet_type() == "TAB" || item.GetBerlet_type() == "NYB")
                {
                    kedvezmenyes++;
                }
                else if (item.GetBerlet_type() == "NYP" || item.GetBerlet_type() == "RVS" || item.GetBerlet_type() == "GYK")
                {
                    ingyenes++;
                }
            }
            Console.WriteLine($"A kedvezmenyesen utazók száma: {kedvezmenyes}");
            Console.WriteLine($"Ingyenesen utazók száma: {ingyenes}");
        }

        static void napokszama(List<Utazas> utazas)
        {
            int h1 = 0;
            int e1 = 0;
            int d1 = 0;
            int h2 = 0;
            int e2 = 0;
            int d2 = 0;

            Console.WriteLine();
            h1 = (h1 + 9) % 12;

            e1 = e1 - h1 / 10;

            d1 = 365 * e1 + e1 / 4 - e1 / 100 + e1 / 400 + (h1 * 306 + 5) / 10 + d1 - 1;

            h2 = (h2 + 9) % 12;

            e2 = e2 - h2 / 10;

            d2 = 365 * e2 + e2 / 4 - e2 / 100 + e2 / 400 + (h2 * 306 + 5) / 10 + d2 - 1;

            int napokszama = d2 - d1;

            Console.WriteLine($"Napok száma: {napokszama}");
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

            string legnepszerubb = LegnepszerubbAllomas(utazasAdatok);
            Console.WriteLine("\nA legtöbb felszállási próbálkozás itt történt: " + legnepszerubb);

            mennyiutas(utazasAdatok);
            nemszallhatfel(utazasAdatok);
            kendezmenyesingyenes(utazasAdatok);
            napokszama(utazasAdatok);

            Console.ReadKey();

        }
    }
}

