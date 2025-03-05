using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar
{
    internal class Uzivatel
    {
        private static int globalId = 1; // pro dynamicke vytvareni novych uzivatelu
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }

        public int UniqId { get; }
        
        public Uzivatel() //Constructor pro automaticke pridavani osob 
        {
           UniqId = globalId++;
        }

        ~Uzivatel() 
        {
            Console.WriteLine("Uživatel zrušen");
        }

    }
    static class UzivatelManager
    {
        //Ukladani uzivatelu
        static List<Uzivatel> uzivatele = new List<Uzivatel>
            {
                new Uzivatel {Jmeno = "Vaclav", Prijmeni = "Neuman"},
                new Uzivatel { Jmeno = "Pepa", Prijmeni = "Depa"}
            };
        public static void UzivatelGUI(int a)
        {
            Uzivatel? vybrany = uzivatele.FirstOrDefault(u => u.UniqId == a);
            if (vybrany != null)//Kontrola cisla pri zadavani uzivatele
            {
                Console.WriteLine($"Kalendar uzivatele {vybrany.Jmeno} {vybrany.Prijmeni}");
                Operace.ZobrazeniKalendare(vybrany.UniqId);
            }
            else { Console.WriteLine("Uzivatel nebyl nalezen"); }

        }
        public static void ZobrazUzivatele()
        {

            //Serazni podle jmena abecedne
            var serazeni = uzivatele.OrderBy(u => u.Jmeno).ToList();

            Console.WriteLine("Vyber uzivatele pomoci ID\n");

            foreach (Uzivatel clovek in serazeni) //Vypsani uzivatelu
            {

                Console.WriteLine($"{clovek.Jmeno} {clovek.Prijmeni}  ID: {clovek.UniqId}");

            }
            Console.WriteLine();
            //Kontrola jestli zadany uzivatel existuje
            Console.Write("Zadejte ID uzivatele: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Clear();
                UzivatelGUI(id);
            }
            else
            {
                Console.WriteLine("Neplatný vstup! Zadejte číslo.");
                ZobrazUzivatele();
            }

        }
        public static void VybiraniDni(int rok, int mesic, int den, int index) //Vybirani dni v kalendari
        {

            Console.Clear();
            if (index >= 0 && index <= uzivatele.Count) 
            {
                Uzivatel vybranyUzivatel = uzivatele[index - 1];
                Console.WriteLine($"Rok:{rok,5} Mesic:{mesic,5} den:{den,5}.\n");
                Console.WriteLine($"Plany uzivatele{vybranyUzivatel.Jmeno} {vybranyUzivatel.Prijmeni}");
                Operace.UkladaniDat(rok,mesic,den,index);
            }
            else { Console.WriteLine("Musis si vybrat uzivatele aby ti toto misto bylo pristupne"); }
            
        }
    }
}
