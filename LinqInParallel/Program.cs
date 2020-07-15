using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
namespace LinqInParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            WriteLine("Please press 'enter' to start: ");
            ReadLine();
            watch.Start();
            IEnumerable<int> nembers = Enumerable.Range(1,400_000_000);
            var query = nembers.AsParallel().Select(number => number * number).ToArray();

            watch.Stop();
            WriteLine("{0:#,##0} elapsed milliseconds.",watch.ElapsedMilliseconds);
            
        }
    }
}
