using static System.Console;
using System.Text.RegularExpressions; // Pridal jsem tuto tridu abych mohl kontrolovad vklad spravnych znaku
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

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
                WriteLine($"Kalendář uživatele {vybrany.Jmeno} {vybrany.Prijmeni}");
                Operace.ZobrazeniKalendare(vybrany.UniqId);
            }
            else { WriteLine("Uživatel nebyl nalezen"); Program.UvodniObrazovka(); }

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
                Write("Zadejte ID uživatele: ");
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
                WriteLine("2. Přidej uživatele");
                WriteLine("3. Odstraň uživatele");
                WriteLine("\"Libovolné tlačítko\". Úvodní obrazovka");
                if (int.TryParse(ReadLine(), out int vyber))
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
                            else { WriteLine("Vyber platného uživatele"); ZobrazUzivatele(1); }
                            break;
                        case 2:
                            PridavaniUzivatelu();
                            break;


                        case 3:

                            OdebiraniUzivatelu();
                            break;
                        
                    }
                    Clear();
                  
                }
                Clear();
            }
        }
        public static void VybiraniDni(int rok, int mesic, int den, int index) //Vybirani dni v kalendari
        {

            Clear();
            if (index >= 0) // pridat kontrolu k vetsimu ID ; neni potreba jelikoz nejde zadat vetsi ID
            {

                Uzivatel vybranyUzivatel = uzivatele[index - 1]; // osetreni chyby, kdy mi index z nejakeho duvodu vybira uzivatele s indexem o 2 vice
                
                
                
                Operace.UkladaniDat(rok, mesic, den, index);
            } 
            else { WriteLine("\nMusíš si vybrat uživatele aby ti toto místo bylo přístupné\n\n"); Program.UvodniObrazovka(); }

        }


        public static void DataUzivatele(int a) // promenna k urceni Id uzivatele ktereho vypisujem
        {
            Clear();
            Uzivatel vybranyUzivatel = uzivatele[a - 1];
            WriteLine($"Data uživatele: {vybranyUzivatel.Jmeno} {vybranyUzivatel.Prijmeni}\n\n"); //Vypsani jmena uzivatele

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
                    if(data.Length == 3 && int.TryParse(data[2], out int id)) 
                    {
                        uzivatele.Add(new Uzivatel(data[0], data[1], id)); //Vypisovani uzivatelu podle toho jak jsou ulozeni v textaku
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
            File.SetAttributes(cesta, FileAttributes.Hidden);
            WriteLine("Jména nesmějí obsahovat mezery, nebo speciální znaky\n\n");
            WriteLine("Zadej jméno uživatele: ");
            string? jmeno = ReadLine();
            WriteLine("Zadej příjmení uživatele: ");
            string? prijmeni = ReadLine();
            Clear();
            if (jmeno != null && prijmeni != null && Regex.IsMatch(jmeno, "^[a-zA-Zá-žÁ-Ž]+$") && Regex.IsMatch(prijmeni, "^[a-zA-Zá-žÁ-Ž]+$"))  //stringy nesmeji byt null aby byla jmena cela
            {
               // osetreni proti neplatnym udajum
                string UzivateleTextak = "SavedData/Users.txt";
                List<int> existujiciId = uzivatele.Select(x => x.UniqId).ToList();
                int noveId = existujiciId.Count > 0 ? existujiciId.Max() + 1 : 1; //Nejvyssi ID + 1
                File.AppendAllText(UzivateleTextak, $"\n{jmeno.Trim()},{prijmeni.Trim()},{noveId}");
                WriteLine($"Uživatel {jmeno} {prijmeni} úspěšně vytvořen");

                

                uzivatele.Add(new Uzivatel(jmeno, prijmeni, noveId)); //Aby nedoslo k duplikacim a program rovnou vypsal uzivatele tak je zde jeste hned pridavam
                
            }
            else { WriteLine("Zadali jste neplatné údaje\n\n\n"); }
            ReadLine();
        }

        public static void OdebiraniUzivatelu() //Pridat mazani souboru s uzivatelem
        {
            Clear();
            string UlozeniUzivatele = "SavedData/Users.txt";
            if (File.Exists(UlozeniUzivatele))
            {
                var uzivateleVypis = uzivatele.Select(x => new Uzivatel(x.Jmeno, x.Prijmeni, x.UniqId)).ToList();
                
                foreach (var item in uzivateleVypis)
                {

                    WriteLine($"{item.UniqId}. {item.Jmeno} {item.Prijmeni}");
                    
                }

                WriteLine("Napiš ID uživatele, kterého chceš odstranit");
                if(int.TryParse(ReadLine(), out int id))
                {
                    uzivatele = uzivatele.Where(x => x.UniqId != id).ToList(); //Cteni ID aby byly unikatni i pri zanikani souboru
                    var radky = File.ReadAllLines(UlozeniUzivatele).Where(radek => !radek.EndsWith($",{id}")).ToList(); //Pomoc od AI

                    File.WriteAllLines(UlozeniUzivatele, radky);
                    WriteLine($"Uživatel s ID {id} byl odstraněn");
                    if (Directory.Exists($"SavedData/Uzivatel_{id}")) //Pokud ma uzivatel nejake soubory s jeho ID tak je to smaze
                    {
                        Directory.Delete($"SavedData/Uzivatel_{id}");
                    }
                }
                else
                {
                    WriteLine("Neplatné ID");
                }
            }
            
        }
        public static void ZobrazovaniUzivatele(int idU)
        {
            Uzivatel? vybrany = uzivatele.FirstOrDefault(u => u.UniqId == idU);
            if (vybrany != null)
            {
                WriteLine($"Uživatel: {vybrany.Jmeno}  {vybrany.Prijmeni} ID: {vybrany.UniqId}");
            }
            else { WriteLine("Uživatel: Host"); }
            WriteLine();
        }


    }
}
