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
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }

        public int UniqId { get; }
        
        public Uzivatel(string jmeno, string prijmeni) //Constructor pro automaticke pridavani osob 
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
           UniqId = globalId++;
        }

        ~Uzivatel() 
        {
            WriteLine("Uživatel zrušen");
        }

    }
   
}
