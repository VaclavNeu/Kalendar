using static System.Console;  // Abych vsude nemusel psat Console.          kvuli ctivosti kodu
namespace Kalendar
{
    internal class Program
    {
       
       
        static void Main(string[] args)
        {


            UvodniObrazovka();

           
            
             //Overovani zadanych hodnot pri vyberu
            

           
            
            
            
        }

        public static void UvodniObrazovka()
        {
            WriteLine("Tvuj pocitacovi kalendar");


            WriteLine("\nVyber akci");
            WriteLine("\n1. Zobrazit kalendar");
            WriteLine("2. Zobrazit uzivatele");
            WriteLine("3. Nastaveni");
            WriteLine("\"Libovolne tlacitko\". Ukoncit program\n");

            if (int.TryParse(ReadLine(), out int vyberAkce))
            {
                Clear();
                Operace.Vyber(vyberAkce);
            }
            else
            {
                Clear();
                WriteLine("Zadej cele cislo!");
                UvodniObrazovka();
            }
        }
        public static void UkonceniProgramu()
        {
            Environment.Exit(0);
        }

    }
}
