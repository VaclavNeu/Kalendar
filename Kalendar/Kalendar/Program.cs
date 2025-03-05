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
            Console.WriteLine("Tvuj pocitacovi kalendar");


            Console.WriteLine("\nVyber akci");
            Console.WriteLine("\n1. Zobrazit kalendar");
            Console.WriteLine("2. Zobrazit uzivatele");
            Console.WriteLine("3. Nastaveni");
            Console.WriteLine("\"Libovolne tlacitko\". Ukoncit program\n");

            if (int.TryParse(Console.ReadLine(), out int vyberAkce))
            {
                Console.Clear();
                Operace.Vyber(vyberAkce);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Zadej cele cislo!");
                UvodniObrazovka();
            }
        }

    }
}
