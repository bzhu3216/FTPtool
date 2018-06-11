using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

namespace MacAdress
{
    public partial class Form1 : Form
    {
        public const int WM_DEVICECHANGE = 0x219;
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_CONFIGCHANGECANCELED = 0x0019;
        public const int DBT_CONFIGCHANGED = 0x0018;
        public const int DBT_CUSTOMEVENT = 0x8006;
        public const int DBT_DEVICEQUERYREMOVE = 0x8001;
        public const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        public const int DBT_DEVICEREMOVEPENDING = 0x8003;
        public const int DBT_DEVICETYPESPECIFIC = 0x8005;
        public const int DBT_DEVNODES_CHANGED = 0x0007;
        public const int DBT_QUERYCHANGECONFIG = 0x0017;
        public const int DBT_USERDEFINED = 0xFFFF;
        public Form1()
        {
            InitializeComponent();
        }
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == WM_DEVICECHANGE)
                {
                    switch (m.WParam.ToInt32())
                    {
                        case WM_DEVICECHANGE:
                            break;
                        case DBT_DEVICEARRIVAL://U盘插入   
                            DriveInfo[] s = DriveInfo.GetDrives();
                            foreach (DriveInfo drive in s)
                            {
                                if (drive.DriveType == DriveType.Removable)
                                {
                                   // label1.Text = "U盘已插入，盘符为:" + drive.Name.ToString();
                                    MessageBox.Show("U盘已插入，盘符为:" + drive.Name.ToString());
                                    break;
                                }
                            }
                            break;
                        case DBT_CONFIGCHANGECANCELED:
                            break;
                        case DBT_CONFIGCHANGED:
                            break;
                        case DBT_CUSTOMEVENT:
                            break;
                        case DBT_DEVICEQUERYREMOVE:
                            break;
                        case DBT_DEVICEQUERYREMOVEFAILED:
                            break;
                        case DBT_DEVICEREMOVECOMPLETE:   //U盘卸载 
                            //label1.Text = "";
                            break;
                        case DBT_DEVICEREMOVEPENDING:
                            break;
                        case DBT_DEVICETYPESPECIFIC:
                            break;
                        case DBT_DEVNODES_CHANGED:
                            break;
                        case DBT_QUERYCHANGECONFIG:
                            break;
                        case DBT_USERDEFINED:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            base.WndProc(ref m);
        }


        private void button1_Click(object sender, EventArgs e)
        {

           
            textBox1.Text = Getinfo.GetMacAddress();
            textBox2.Text = Getinfo.GetClientLocalIPv4Address();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenInfo.GenFile(textBox3.Text, textBox4.Text, textBox2.Text,"");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            UploadFileFTP up = new UploadFileFTP();
            //up.UploadDirectory(@"c:\" , "ftp://192.168.131.219/", textBox3.Text + textBox2.Text);
            up.UploadDirectory(@"c:\", ConfigurationManager.AppSettings["ftpip"], textBox3.Text + textBox2.Text);


            MessageBox.Show("上传成功!");
            // up.MakeDir("ftp://192.168.131.219/", textBox3.Text + textBox2.Text);
            //textBox3.Text=up.CheckDirectoryExist("ftp://192.168.131.219/", textBox3.Text + textBox2.Text).ToString();
            // textBox5.Text = (up.CheckDirectoryExist2("ftp://192.168.131.219/", textBox3.Text + textBox2.Text)).ToString();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            






        }

        private void button6_Click(object sender, EventArgs e)
        {
           




        }

        private void button7_Click(object sender, EventArgs e)
        {

           // string _ipaddress = "192.168.131.219";
           // string _submask = "255.255.255.0";
           // string _gateway = "192.168.131.254";
            string _dns1 = "0.2.0.2";
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
