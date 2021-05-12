using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace CSharpFormanceCheck.Checker
{
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter, RPlotExporter]
    public class CheckerCBVCBR
    {
        public struct Specimen
        {
            public int ID;
            public int Atk;
            public int Hp;
        }
        
        private Specimen data;

        public CheckerCBVCBR()
        {
        }

        [GlobalSetup]
        public void SetUp()
        {
            data = new Specimen();
        }

        [Benchmark]
        public int CallByValue()
        {
            var spemn = CBV(data);
            return spemn.Atk;
        }

        private Specimen CBV(Specimen spmen)
        {            
            ++spmen.Atk;
            return spmen;
        }

        [Benchmark]
        public int CallByRef()
        {
            var spemn = CBR(ref data);
            return spemn.Atk;
        }

        private Specimen CBR(ref Specimen spmen)
        {
            ++spmen.Atk;
            return spmen;
        }
    }
}
