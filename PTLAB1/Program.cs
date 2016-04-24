using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTLAB1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(args[0]);
            Tree root = new Tree("C:\\Users\\Piotr\\Downloads\\World\\World\\World", "");
            Console.WriteLine("\n" + "Oldest file:" + Tree.date  + "\n");

           root.Serialize();
           root.Deserialize();
            
        }
    }
}
