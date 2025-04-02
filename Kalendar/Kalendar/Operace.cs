using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar
{

    internal class Operace //Trida urcena pro lepsi ctivost kodu
    {
        
        
        public static void Vyber(int a) //Vybirani cisel k akci u kalendare
        {
            switch (a)
            {
                case 1:
                    ZobrazeniKalendare();
                    break;
                case 2:
                    UzivatelManager.ZobrazUzivatele(0);
                    break;
                case 3:

                    Nastaveni();
                    break;
                default:
                    WriteLine("Program ukončen");
                    ReadLine();
                    Environment.Exit(0);
                    break;

            }
            
        }

        public static void ZobrazeniKalendare(int uzivatel = -1) //Zadavani zakladnich hodnot pro spusteni kalendare
        {   // uzivatel je -1 aby mi pri zobrazovani kalendare bez uzivatele fungovalo a nehazelo chyby
            UzivatelManager.ZobrazovaniUzivatele(uzivatel);

            WriteLine("Zadej rok:                 (Minimálně 2024, Maximálně 9999)");
           if (int.TryParse(ReadLine(), out int VysledekRok))
            {
                WriteLine("Zadej měsíc: ");
                if (int.TryParse(ReadLine(), out int VysledekMesic))
                {
                    Datumy.Rok(VysledekRok, VysledekMesic, uzivatel);
                }
                else { Program.UvodniObrazovka(); }

            }
            else { Program.UvodniObrazovka(); }
            
            
          
        }
        public static void UkladaniDat(int rok, int mesic, int den, int id)
        {
            if (File.Exists($"SavedData/Uzivatel_{id}/{rok}/{mesic}_{den}.txt")) // Pokud data uz existuji program je vypise
            {
                WriteLine("Vaše poznámky:\n\n");
                string vypis = File.ReadAllText($"SavedData/Uzivatel_{id}/{rok}/{mesic}_{den}.txt");
                WriteLine($"\n{vypis}\n\nStiskni libovolné tlačítko pro pokračovaní.");
                ReadLine();
            }
            RozhraniDni(rok,mesic,den,id);
            
        }

        public static void RozhraniDni(int rok, int mesic, int den, int id) //Zde se clovek rozhodne zda li chce dalsi data pridat nebo ne
        {
            Clear();
            WriteLine($"Rok:{rok,5} Měsíc:{mesic,5} den:{den,5}.\n");
            UzivatelManager.ZobrazovaniUzivatele(id);

            WriteLine("\nVyberte další akci:\n");
            WriteLine("1. Přidat data");
            WriteLine("2. Vrátit se na úvodní stránku");
            WriteLine("3. Vrátit se na výběr dní");
            WriteLine("\"Libovolné tlačítko\". Vypnutí aplikace"); 


            if (int.TryParse(ReadLine(), out int vyberAkce))
            {
                switch (vyberAkce)
                {
                    case 1:
                        WriteLine("Sem napiš své plány");
                        string? str = ReadLine();
                        Directory.CreateDirectory($"SavedData/Uzivatel_{id}/{rok}");

                        File.AppendAllText($"SavedData/Uzivatel_{id}/{rok}/{mesic}_{den}.txt", "\n" + str);
                        //Soubory se budou ukladat podle danych parametru at jsou jednodusse dohledatelne

                        UkladaniDat(rok, mesic, den, id);
                        break;
                    case 2:
                        Clear();
                        Program.UvodniObrazovka();
                        break;
                    case 3:
                        Datumy.Rok(rok, mesic, id);
                        break;
                    default:
                        WriteLine("Nashledanou!"); // funguje na jakekoliv jine cislo
                        Environment.Exit(0);
                        break;
                }

            }
            else { WriteLine("Nashledanou!");
                Environment.Exit(0);
            } //kvuli stisku nahodneho tlacitka
        }

        public static void Nastaveni()
        {
            Clear();
            
            WriteLine("1. Zobrazit uživatele");
           
            WriteLine("2. Tovární nastavení");
            WriteLine("\"Libovolne tlacitko\". Uvodni obrazovka");
            if (int.TryParse(ReadLine(), out int vyber))
            {
                switch (vyber)
                {
                    case 1:
                        Clear();
                        UzivatelManager.ZobrazUzivatele(1);
                        WriteLine();
                      
                        
                        break;
                    
                    case 2:
                        WriteLine($"Opravdu chcete vymazat všechna data?\nPokud ano, napiste \"ANO\"");
                        string? mazani = ReadLine();
                        if(mazani == "ANO")
                        {
                            Directory.Delete($"SavedData", true); // Vymazeme vse i s vnitrnimi soubory
                            WriteLine("Data resetovana\n\nProgram bude nyní ukončen!");
                            ReadLine();
                           Environment.Exit(0);
                        }
                        else { 
                            WriteLine("Akce zrušena");
                            ReadLine();
                            Program.UvodniObrazovka();
                        }
                        break;

                }Program.UvodniObrazovka();
            }
        }
       public static void VypisDatUzivatele(int a) // a = index uzivatele
        {
            string VypisDat = $"SavedData/Uzivatel_{a}";  //Vypsani slozek podle jednotlivych roku

            if (Directory.Exists(VypisDat)) // Ověří, zda složka existuje
            {
                string[] nalezeneSlozky = Directory.GetDirectories(VypisDat);
                WriteLine("\n\nVyber si rok o kterém chceš informace\n");
                foreach (string slozka in nalezeneSlozky)
                {
                    string posledniCislo = Path.GetFileName(slozka); // Získá poslední část cesty
                    WriteLine(posledniCislo);
                }
                WriteLine("\n\n\n");

                VypsaniTextaku(a);

            }
            else
            {
                WriteLine("Uživatel nemá žádné složky!\n\n");
            }
        }

        public static void VypsaniTextaku(int a) // a = index uzivatele
        {
            if (int.TryParse(ReadLine(), out int rok))   // Zde vypiseme dane soubory {textaky} ktere jsou uz ulozene pro dane dny
            {
                if (Directory.Exists($"SavedData/Uzivatel_{a}/{rok}")) {
                    string VypisMesicu = $"SavedData/Uzivatel_{a}/{rok}";
                    string[] VypsaneMesice = Directory.GetFiles(VypisMesicu);
                    Clear();
                    WriteLine("\nMesic_Den");
                    foreach (string mesic in VypsaneMesice)
                    {
                        string pouzeMesic = Path.GetFileNameWithoutExtension(mesic); //Vypsani souboru bez pripon
                        WriteLine(pouzeMesic);
                    }
                    WriteLine("\nVyber si datum které chceš zobrazit");
                    string? textak = ReadLine();
                    if (File.Exists($"SavedData/Uzivatel_{a}/{rok}/{textak}.txt"))
                    {
                       string vypisTextaku = File.ReadAllText($"SavedData/Uzivatel_{a}/{rok}/{textak}.txt");
                        WriteLine("\n" + vypisTextaku + "\n\n");   //Vypsani textaku

                        WriteLine($"Zvol další akci\n1. Vrátit se na výběr roku\n2. Vrátit se na úvodní obrazovku\nx. Ukončit program");
                       
                        if(int.TryParse(ReadLine(), out int vyber))
                        {
                            switch (vyber)
                            {
                                case 1:
                                    VypisDatUzivatele(a);
                                    break;
                                case 2:
                                    Program.UvodniObrazovka();
                                    break;
                                default:
                                    Environment.Exit(0);
                                    break;
                            }
                        }
                        else { Environment.Exit(0); }
                    }
                    else { Datumy.SpatnyRok(a); }
                }
                else { Datumy.SpatnyRok(a); }
            }
            else
            {
                Datumy.SpatnyRok(a); // vytvoril jsem metodu abych zde zbytecne 2x neopisoval stejny kod
            }
        }
    }
}
