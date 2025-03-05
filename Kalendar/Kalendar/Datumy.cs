﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar
{
    /*    Z   K
    2023 ne  ne
    2024 po  ut
    2025 st  st
    2026 ct  ct
    2027 pa  pa
    2028 So  Ne
     
     */

    internal class Datumy
    {


        public static void Rok(int rok, int mesic, int uzivatel)
        {
            if (rok < 2024)
            {
                Console.WriteLine("Zadal jsi spatny rok");
                Program.UvodniObrazovka();
            }
            else
            {
                Console.Clear();
                if (mesic < 13 && mesic > 0) { 
                VykresleniKalendare(rok, mesic,uzivatel);
                }
                else { Console.WriteLine("Rok ma pouze 12 mesicu!"); Console.ReadLine(); Program.UvodniObrazovka(); }

            }


        }

            static void VykresleniKalendare(int rok, int mesic, int uzivatel)  // castecna Vypomoc od ChatGPT
            {
            Console.WriteLine($"{"",20}Planovac uzivatele {uzivatel}");
                string[] dnyTydne = { "Po", "Ut", "St", "Ct", "Pa", "So", "Ne" };

                // Získání prvního dne v měsíci (1 = Pondělí, 7 = Neděle)
                DateTime prvniDen = new DateTime(rok, mesic, 1);
                int zacatekDne = ((int)prvniDen.DayOfWeek + 6) % 7; // Přizpůsobení pondělí jako první den

                int pocetDniVMesici = DateTime.DaysInMonth(rok, mesic);

                // Výpis názvů dní v týdnu
                foreach (var den in dnyTydne)
                {
                    Console.Write($"{den,7} ");
                }
                Console.WriteLine("\n\n");

                // Odsazení pro první týden
                for (int i = 0; i < zacatekDne; i++)
                {
                    Console.Write($"   {"",5}"); // Zarovnani aby byl jiz prvni datum pod spravnym dnem

                }
               
                // Výpis dnů měsíce
                for (int den = 1; den <= pocetDniVMesici; den++)
                {
                    Console.Write($"{den,7} "); // Zarovná čísla na dvě místa

                    // Přechod na nový řádek vždy po neděli
                    if ((zacatekDne + den) % 7 == 0)
                    {
                        Console.WriteLine("\n\n");
                    }
                }

                Console.WriteLine("Vyber den, pote stiskni Enter"); 
                if(int.TryParse(Console.ReadLine(), out int vybranyDen))
               {
                UzivatelManager.VybiraniDni(rok,mesic,vybranyDen,uzivatel);
               }


            }
        }
    }


