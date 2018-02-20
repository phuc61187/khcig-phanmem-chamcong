using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int i;
            float f;
            double d;

            Console.WriteLine("min int: {0}; max int: {1}", int.MinValue, int.MaxValue);
            Console.WriteLine("min float: {0}; max float: {1}", float.MinValue.ToString("#0.0#"), float.MaxValue);
            Console.WriteLine("min float: {0}; max float: {1}", ushort.MinValue.ToString("#0.#"), float.MaxValue);
            Console.WriteLine("min double: {0}; max double: {1}", double.MinValue.ToString("#0.#"), double.MaxValue);
            Console.ReadLine();
        }
    }
}
