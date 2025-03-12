using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar
{
    internal class Uzivatel
    {
        private static int globalId = 1; // pro dynamicke vytvareni novych uzivatelu
        public required string Jmeno { get; set; }
        public required string Prijmeni { get; set; }

        public int UniqId { get; }
        
        public Uzivatel() //Constructor pro automaticke pridavani osob 
        {
           UniqId = globalId++;
        }

        ~Uzivatel() 
        {
            WriteLine("Uživatel zrušen");
        }

    }
   
}
