using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CSharpFormanceCheck.Checker;

namespace MyBenchmarks
{

    public class Program
    {
        public static void Main(string[] args)
        {
            //check CBV vs CBR 
            var summary = BenchmarkRunner.Run<CheckerCBVCBR>();
        }
    }
}