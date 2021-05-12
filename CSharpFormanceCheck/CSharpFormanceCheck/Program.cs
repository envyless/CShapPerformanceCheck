using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CSharpFormanceCheck.Checker;

namespace MyBenchmarks
{
    public class Program
    {
        

        public unsafe static void Main(string[] args)
        {
            string str = new string("Hello World");
            string str2 = str;

            str2 = str2.Replace("W", "A");

            EXM.ShowPtr(str);
            EXM.ShowPtr(str2);

            return;



            //check CBV vs CBR 
            //var summary = BenchmarkRunner.Run<CheckerCBVCBR>();
        }
    }
}