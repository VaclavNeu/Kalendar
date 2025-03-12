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
                    WriteLine("Program ukoncen");
                    ReadLine();
                    break;

            }
            
        }

        public static void ZobrazeniKalendare(int uzivatel = -1) //Zadavani zakladnich hodnot pro spusteni kalendare
        {   // uzivatel je -1 aby mi pri zobrazovani kalendare bez uzivatele fungovalo a nehazelo chyby
            WriteLine("Zadej rok:                 (Minimalne 2024)");
           if (int.TryParse(ReadLine(), out int VysledekRok))
            {
                WriteLine("Zadej mesic: ");
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
            if (File.Exists($"SavedData/{rok}/{mesic}/{den}_{id}.txt")) // Pokud data uz existuji program je vypise
            {
                WriteLine("Vase poznamky:\n\n");
                string vypis = File.ReadAllText($"SavedData/{rok}/{mesic}/{den}_{id}.txt");
                WriteLine($"\n{vypis}");
            }
            RozhraniDni(rok,mesic,den,id);
            
        }

        public static void RozhraniDni(int rok, int mesic, int den, int id)
        {
            WriteLine("\nVyberte dalsi akci:\n");
            WriteLine("1. Pridat data");
            WriteLine("2. Vratit se na uvodni stranku");
            WriteLine("3. Vratit se na vyber dni");
            WriteLine("\"Libovolne tlacitko\". Vypnuti aplikace"); 


            if (int.TryParse(ReadLine(), out int vyberAkce))
            {
                switch (vyberAkce)
                {
                    case 1:
                        WriteLine("Sem napis sve plany");
                        string str = ReadLine();
                        Directory.CreateDirectory($"SavedData/{rok}/{mesic}");

                        File.AppendAllText($"SavedData/{rok}/{mesic}/{den}_{id}.txt", str);
                        //Soubory se budou ukladat podle danych parametru at jsou jednodusse dohledatelne

                        RozhraniDni(rok, mesic, den, id);
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
                        break;
                }

            }
            else { WriteLine("Nashledanou!"); } //kvuli stisku nahodneho tlacitka
        }

        public static void Nastaveni()
        {
            Clear();

            WriteLine("1. Zobrazit uzivatele");
           
            WriteLine("2. Tovarni nastaveni");
            WriteLine("x. Uvodni obrazovka");
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
                        
                        break;
                    
                    default:
                        Program.UvodniObrazovka();
                        break;

                }
            }
        }
       
    }
}
