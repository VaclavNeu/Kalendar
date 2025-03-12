using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.Marshalling;

namespace Kalendar
{
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
                WriteLine($"Kalendar uzivatele {vybrany.Jmeno} {vybrany.Prijmeni}");
                Operace.ZobrazeniKalendare(vybrany.UniqId);
            }
            else { WriteLine("Uzivatel nebyl nalezen"); }

        }
        public static void ZobrazUzivatele(int a) // Pridal jsem pro int abych si mohl ubrat par radku kodu navic
                                                  // a funguje to podle toho jestli je clovek v nastaveni
        {

            //Serazni podle jmena abecedne
            var serazeni = uzivatele.OrderBy(u => u.Jmeno).ToList();

           

            foreach (Uzivatel clovek in serazeni) //Vypsani uzivatelu
            {

                WriteLine($"{clovek.Jmeno} {clovek.Prijmeni}  ID: {clovek.UniqId}");

            }
            WriteLine();
            //Kontrola jestli zadany uzivatel existuje
           
            if (a != 1)
            {
                Write("Zadejte ID uzivatele: ");
                if (int.TryParse(ReadLine(), out int id))
                {
                    Clear();
                    UzivatelGUI(id);
                }
                else
                {
                    WriteLine("Neplatný vstup! Zadejte číslo.");
                    ZobrazUzivatele(0);
                }
            }
            else
            {
                WriteLine("\n\n\n1. Vyber uzivatele podle Id");
                WriteLine("2. Pridej uzivatele");
                WriteLine("3. Odstran uzivatele");
                if(int.TryParse(ReadLine(), out int vyber))
                {
                    switch (vyber) 
                    {
                        case 1:
                            WriteLine("Zadej id uzivatele");
                            if (int.TryParse(ReadLine(), out int id))
                            {
                                Uzivatel? vybrany = uzivatele.FirstOrDefault(u => u.UniqId == id);
                                if (vybrany != null)
                                {
                                    DataUzivatele(id); 
                                }
                                else { WriteLine("Uzivatel nebyl nalezen"); }
                            }
                            else { WriteLine("Vyber platneho uzivatele"); ZobrazUzivatele(1); }
                            break;
                        case 2:
                        case 3:
                        default:
                            Program.UvodniObrazovka();
                            break;
                    }
                    Program.UvodniObrazovka();
                }
            }
        }
        public static void VybiraniDni(int rok, int mesic, int den, int index) //Vybirani dni v kalendari
        {

            Clear();
            if (index >= 0 && index <= uzivatele.Count)
            {
                Uzivatel vybranyUzivatel = uzivatele[index - 1];
                WriteLine($"Rok:{rok,5} Mesic:{mesic,5} den:{den,5}.\n");
                WriteLine($"Plany uzivatele{vybranyUzivatel.Jmeno } {vybranyUzivatel.Prijmeni}");
                Operace.UkladaniDat(rok, mesic, den, index);
            }
            else { WriteLine("Musis si vybrat uzivatele aby ti toto misto bylo pristupne"); Program.UvodniObrazovka(); }

        }


        public static void DataUzivatele(int a) // promenna k urceni Id uzivatele ktereho vypisujem
        {
            WriteLine("Existuju");
        }
    }
}
