using System;
using System.Net;
using FPLCore;

namespace FPL_SkavenBilicNextFixtureSummary
{
    class Program
    {
        static void Main(string[] args)
        {
            NextFixtureSummaryGenerator generator = new NextFixtureSummaryGenerator();

            try
            {
                generator.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }        
    }
}
