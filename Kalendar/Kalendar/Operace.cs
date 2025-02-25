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
                    Console.WriteLine("Program ukoncen");
                    
                    break;
                default:
                    Console.WriteLine("Zadali jste moc velke cislo");
                    break;

            }
            
        }

        public static void ZobrazeniKalendare() //Zadavani zakladnich hodnot pro spusteni kalendare
        {   
            Console.WriteLine("Zadej rok:                 (Minimalne 2024)");
            if (int.TryParse(Console.ReadLine(), out int VysledekRok))
            {
                Console.WriteLine("Zadej mesic: ");
                if (int.TryParse(Console.ReadLine(), out int VysledekMesic))
                {
                    Datumy.Rok(VysledekRok, VysledekMesic);
                }
                
            }
            
            
          
        }


       
    }
}
