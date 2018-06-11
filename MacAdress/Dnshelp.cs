using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MacAdress
{
    class Dnshelp
    {

        public static void setDNS(string ipdns)

        {
            string _dns1 = ipdns;
            string _doscmd = "";

            // string _doscmd = "netsh interface ip set address 本地连接 static " + _ipaddress + " " + _submask + " " + _gateway + " 1";
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            // p.StandardInput.WriteLine(_doscmd.ToString());
            _doscmd = "netsh interface ip set dns 本地连接 static " + _dns1;
            p.StandardInput.WriteLine(_doscmd.ToString());

            p.StandardInput.WriteLine("exit");

        }



        }
    }
