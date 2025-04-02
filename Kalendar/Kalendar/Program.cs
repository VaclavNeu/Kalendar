using static System.Console;  // Abych vsude nemusel psat Console.          kvuli ctivosti kodu
namespace Kalendar
{
    internal class Program
    {
       
       
        static void Main(string[] args)
        {
            UzivatelManager.NacitaniUzivatelu(); //pri startu se ihned nactou ulozeni uzivatele

            UvodniObrazovka();

           
            
             //Overovani zadanych hodnot pri vyberu
            

           
            
            
            
        }

        public static void UvodniObrazovka()
        {
            
            WriteLine("Tvuj pocitacovy kalendar");


            WriteLine("\nVyber akci");
            WriteLine("\n1. Zobrazit kalendar");
            WriteLine("2. Zobrazit uzivatele");
            WriteLine("3. Nastaveni");
            WriteLine("\"Libovole cislo.\" Ukoncit program\n");

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
   

    }
}
