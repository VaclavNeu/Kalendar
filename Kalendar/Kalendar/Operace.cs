using System;
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
                    UzivatelManager.ZobrazUzivatele();
                    break;
                case 3:
                    
                    
                    break;
                default:
                    Console.WriteLine("Program ukoncen");
                    Program.UvodniObrazovka();
                    break;

            }
            
        }

        public static void ZobrazeniKalendare(int uzivatel = -1) //Zadavani zakladnich hodnot pro spusteni kalendare
        {   // uzivatel je -1 aby mi pri zobrazovani kalendare bez uzivatele fungovalo a nehazelo chyby
            Console.WriteLine("Zadej rok:                 (Minimalne 2024)");
           if (int.TryParse(Console.ReadLine(), out int VysledekRok))
            {
                Console.WriteLine("Zadej mesic: ");
                if (int.TryParse(Console.ReadLine(), out int VysledekMesic))
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
                Console.WriteLine("Vase poznamky:\n\n");
                string vypis = File.ReadAllText($"SavedData/{rok}/{mesic}/{den}_{id}.txt");
                Console.WriteLine($"\n{vypis}");
            }
            RozhraniDni(rok,mesic,den,id);
            
        }

        public static void RozhraniDni(int rok, int mesic, int den, int id)
        {
            Console.WriteLine("\nVyberte dalsi akci:\n");
            Console.WriteLine("1. Pridat data");
            Console.WriteLine("2. Vratit se na uvodni stranku");
            Console.WriteLine("3. Vratit se na vyber dni");
            Console.WriteLine("\"Libovolne tlacitko\". Vypnuti aplikace"); 


            if (int.TryParse(Console.ReadLine(), out int vyberAkce))
            {
                switch (vyberAkce)
                {
                    case 1:
                        Console.WriteLine("Sem napis sve plany");
                        string str = Console.ReadLine();
                        Directory.CreateDirectory($"SavedData/{rok}/{mesic}");

                        File.AppendAllText($"SavedData/{rok}/{mesic}/{den}_{id}.txt", str);
                        //Soubory se budou ukladat podle danych parametru at jsou jednodusse dohledatelne

                        RozhraniDni(rok, mesic, den, id);
                        break;
                    case 2:
                        Console.Clear();
                        Program.UvodniObrazovka();
                        break;
                    case 3:
                        Datumy.Rok(rok, mesic, id);
                        break;
                    default:
                        Console.WriteLine("Nashledanou!"); // funguje na jakekoliv jine cislo
                        break;
                }

            }
            else { Console.WriteLine("Nashledanou!"); } //kvuli stisku nahodneho tlacitka
        }
       
    }
}
