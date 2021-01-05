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

            generator.Start();
        }        
    }
}
