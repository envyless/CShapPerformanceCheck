# Reference Keyword , Class

생성일: 2021년 5월 2일 오후 12:59

![Reference%20Keyword%20,%20Class%20f1ef3b7953bd44b683d11d643a640529/Untitled.png](Reference%20Keyword%20,%20Class%20f1ef3b7953bd44b683d11d643a640529/Untitled.png)

```csharp
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
```