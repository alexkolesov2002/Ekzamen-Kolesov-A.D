using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekzamen_Kolesov_A.D
{/// <summary>
/// Главный класс программы
/// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Listeners.Add(new TextWriterTraceListener(File.CreateText("info.txt")));
            Debug.AutoFlush = true;
            Critical MM = new Critical();
            MM.Reshenie();
        }
    }
}
