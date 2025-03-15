using static System.Console;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

namespace Kalendar
{
    static class UzivatelManager
    {
        //Ukladani uzivatelu
        static List<Uzivatel> uzivatele = new List<Uzivatel>();
        
        public static void UzivatelGUI(int a)
        {
            Uzivatel? vybrany = uzivatele.FirstOrDefault(u => u.UniqId == a);
            if (vybrany != null)//Kontrola cisla pri zadavani uzivatele
            {
                WriteLine($"Kalendar uzivatele {vybrany.Jmeno} {vybrany.Prijmeni}");
                Operace.ZobrazeniKalendare(vybrany.UniqId);
            }
            else { WriteLine("Uzivatel nebyl nalezen"); Program.UvodniObrazovka(); }

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
                                Uzivatel? vybrany = uzivatele.FirstOrDefault(u => u.UniqId == id);  //Kontrola Id
                                if (vybrany != null)
                                {
                                    DataUzivatele(id); 
                                }
                                else { WriteLine("Uzivatel nebyl nalezen\n"); }
                            }
                            else { WriteLine("Vyber platneho uzivatele"); ZobrazUzivatele(1); }
                            break;
                        case 2:
                            PridavaniUzivatelu();
                            break;


                        case 3:

                            OdebiraniUzivatelu();
                            break;
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
            Clear();
            Uzivatel vybranyUzivatel = uzivatele[a - 1];
            WriteLine($"Data uzivatele: {vybranyUzivatel.Jmeno} {vybranyUzivatel.Prijmeni}\n\n"); //Vypsani jmena uzivatele

            Operace.VypisDatUzivatele(a);
            
        }

        public static void NacitaniUzivatelu()
        {
            string cesta = "SavedData/Users.txt";

            if (File.Exists(cesta))
            {
                string[] radky = File.ReadAllLines(cesta);

                foreach (string radek in radky) 
                {
                    string[] data = radek.Split(',');
                    if(data.Length == 2)
                    {
                        uzivatele.Add(new Uzivatel(data[0], data[1]));
                    }
                }
            }
        }

        public static void PridavaniUzivatelu() //Program nejdriv zkontroluje jestli existuje soubor, pote textak
        {
            Clear();
            string cesta = "SavedData";
            if (!Directory.Exists(cesta))
            {
                Directory.CreateDirectory(cesta);
            }

            WriteLine("Zadej jmeno uzivatele: ");
            string? jmeno = ReadLine();
            WriteLine("Zadej prijmeni uzivatele: ");
            string? prijmeni = ReadLine();
            if (jmeno != null && prijmeni != null)  //stringy nesmeji byt null aby byla jmena cela
            {
                string UzivateleTextak = "SavedData/Users.txt";
                if (!File.Exists(UzivateleTextak))
                {
                    File.Create(UzivateleTextak);
                }
                File.AppendAllText(UzivateleTextak, $"\n{jmeno},{prijmeni}");
                WriteLine($"Uzivatel {jmeno} {prijmeni} uspesne vytvoren");
                uzivatele.Add(new Uzivatel(jmeno, prijmeni)); //Aby nedoslo k duplikacim a program rovnou vypsal uzivatele tak je zde jeste hned pridavam
            }
            else { WriteLine("Zadali jste neplatne udaje"); }
        }

        public static void OdebiraniUzivatelu()
        {
            Clear();
            string UlozeniUzivatele = "SavedData/Users.txt";
            if (File.Exists(UlozeniUzivatele))
            {
                var uzivateleVypis = uzivatele.Select(x => new Uzivatel(x.Jmeno,x.Prijmeni)).ToList();
                int i = 1;
                foreach(var item in uzivateleVypis)
                {
                    
                    WriteLine($"{i}. {item.Jmeno} {item.Prijmeni}");
                    i++;
                }
                WriteLine("Napis id uzivatele ktere chces odstranit");
                if (int.TryParse(ReadLine(), out int vybranyRadek)) 
                {
                    var radky = File.ReadAllLines(UlozeniUzivatele).ToList();
                    if (vybranyRadek > 0 && vybranyRadek <= radky.Count) 
                    {
                        radky.RemoveAt(vybranyRadek - 1);
                        uzivatele.RemoveAt(vybranyRadek - 1);
                    }
                    File.WriteAllLines(UlozeniUzivatele, radky);
                    
                }
            }
        }


    }
}
