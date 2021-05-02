using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace CSharpFormanceCheck.Checker
{
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter, RPlotExporter]
    public class CheckerCBVCBR
    {
        public class Specimen
        {
            public int ID;
            public int Atk;
            public int Hp;
        }

        private const int N = 10000;
        private Specimen data = new Specimen();

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public CheckerCBVCBR()
        {
        }

        [Benchmark]
        public int CallByValue()
        {
            return CBV(data);
        }

        private int CBV(Specimen spmen)
        {
            spmen.Atk++;
            return spmen.Atk;
        }

        [Benchmark]
        public int CallByRef()
        {
            return CBR(ref data);
        }

        private int CBR(ref Specimen spmen)
        {
            spmen.Atk++;
            return spmen.Atk;
        }
    }
}
