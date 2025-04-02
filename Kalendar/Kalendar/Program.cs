using System.Text;
using static System.Console;  // Abych vsude nemusel psat Console.          kvuli ctivosti kodu
namespace Kalendar
{
    internal class Program
    {
       
       
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;
            UzivatelManager.NacitaniUzivatelu(); //pri startu se ihned nactou ulozeni uzivatele

            UvodniObrazovka();

           
            
             //Overovani zadanych hodnot pri vyberu
            

           
            
            
            
        }

        public static void UvodniObrazovka()
        {
            
            WriteLine("Tvůj počítačový kalendář");


            WriteLine("\nVyber akci");
            WriteLine("\n1. Zobrazit kalendář");
            WriteLine("2. Zobrazit uživatele");
            WriteLine("3. Nastavení uživatele");
            WriteLine("\"Libovolné cislo.\" Ukončit program\n");

            if (int.TryParse(ReadLine(), out int vyberAkce))
            {
                Clear();
                Operace.Vyber(vyberAkce);
            }
            else
            {
                Clear();
                WriteLine("Zadej celé číslo!");
                UvodniObrazovka();
            }
        }
   

    }
}
