using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar
{
    internal class Uzivatel
    {
       
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }

        public int UniqId { get; set; }
        
        public Uzivatel(string jmeno, string prijmeni,int Id) //Constructor pro automaticke pridavani osob 
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
           UniqId = Id;

        }
       
        ~Uzivatel() 
        {
            WriteLine("Uživatel zrušen");
         
        }

    }
   
}
