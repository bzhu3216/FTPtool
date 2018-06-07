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
            //textBox5.Text = ConfigurationManager.AppSettings["ftpip"].ToString();

            downloadPaper pa = new downloadPaper();
            pa.downftp(ConfigurationManager.AppSettings["ftpip"].ToString(), ConfigurationManager.AppSettings["paperdir"].ToString(), @"c:\test1\");
            MessageBox.Show("ok");






        }

        private void button6_Click(object sender, EventArgs e)
        {
            //textBox6.Text=abnormal.PrintFileVersionInfo2(@"C:\11111111111zb-6月5日上传文件夹\log.dat");
            /// textBox7.Text = abnormal.PrintFileVersionInfo2(@"C:\11111111111zb-6月5日上传文件夹\key.xlsx");
            /// 

            string p_1 = @"C:\11111111111zb-6月5日上传文件夹\log.dat";
            string p_2 = @"C:\11111111111zb-6月5日上传文件夹\log1.dat"; 

            //计算第一个文件的哈希值
            var hash = System.Security.Cryptography.HashAlgorithm.Create();
            var stream_1 = new System.IO.FileStream(p_1, System.IO.FileMode.Open);
            byte[] hashByte_1 = hash.ComputeHash(stream_1);
            stream_1.Close();
            //计算第二个文件的哈希值
            var stream_2 = new System.IO.FileStream(p_2, System.IO.FileMode.Open);
            byte[] hashByte_2 = hash.ComputeHash(stream_2);
            stream_2.Close();

            //比较两个哈希值
            if (BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2))
                //Console.WriteLine("两个文件相等");
                MessageBox.Show("两个文件相等");
            else
                // Console.WriteLine("两个文件不等");
                MessageBox.Show("两个文件不等");



        }
    }
}
