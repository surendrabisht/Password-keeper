using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordKeeper
{
    internal class CommandLine
    {
        public static void RunCopy(string line)
        {
            Process.Start("CMD.exe", "cmd /c echo " + line + "| clip");
        }

        public static void RunClear()
        {
            Thread.Sleep(12000);
            Process.Start("CMD.exe", "cmd /c echo off | clip");
        }
    }
}
