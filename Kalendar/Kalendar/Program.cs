namespace Kalendar
{
    internal class Program
    {
       
       
        static void Main(string[] args)
        {
            
           


            Console.WriteLine("Tvuj pocitacovi kalendar");
            

            Console.WriteLine("\nVyber akci");
            Console.WriteLine("\n1. Zobrazit kalendar");
            Console.WriteLine("2. Zobrazit uzivatele");
            Console.WriteLine("3. Ukoncit program\n");
           
            
             //Overovani zadanych hodnot pri vyberu
            

            if(int.TryParse(Console.ReadLine(), out int vyberAkce))
            {
                Console.Clear();
                Operace.Vyber(vyberAkce);
            }
            else
            {
                Console.WriteLine("Zadej cele cislo!");
            }
        }
        
    }
}
