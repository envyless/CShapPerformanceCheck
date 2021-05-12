using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Extension Methods
/// </summary>
public class EXM
{
    public unsafe static void ShowPtr(object o)
    {
        TypedReference tr = __makeref(o);
        IntPtr ptr = **(IntPtr**)(&tr);
        Console.WriteLine(ptr);
    }
}
