using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace MacAdress
{
    public partial class main : Form
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
        public string userid = null;
        public string username = null;
        public string teacher = null;
       // private bool autoup = true;
        public main(string userid1,string username1,string teacher1)
        {
            InitializeComponent();
            userid = userid1;
            username = username1;
            teacher = teacher1;
            this.ControlBox = false;
           

        }
        /// <summary>
        /// /////////////////////
        /// 
        /// 
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
                                    UploadFileFTP up = new UploadFileFTP();
                                    up.GenFileUpFTP(username, userid, Getinfo.GetClientLocalIPv4Address(), Getinfo.GetMacAddress(), "ftp://192.168." + ConfigurationManager.AppSettings["ftpip"].ToString() + teacher + "/upload");

                                    MessageBox.Show("同学你插入U盘了，如是考试，我要告诉老师哦，盘符为:" + drive.Name.ToString());


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

        /// //////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Load(object sender, EventArgs e)
        {
            System.DateTime currentTime = DateTime.Now;
            label1.Text = "";
            button2.Enabled = false;
            button3.Enabled = false;
            if(ConfigurationManager.AppSettings["exam"].ToString().Equals("1"))
            Dnshelp.setDNS("8.6.6.6");
            if (Directory.Exists(@"c:\" + userid + username + "-" + currentTime.ToString("m") + @"上传文件夹"))
            {
                button1.Enabled = false;
                button2.Enabled =true;
                button3.Enabled = true;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "下载实验或试卷中。。。";
            button1.Enabled = false;
            System.DateTime currentTime =  DateTime.Now;
            ////////////////////////////////////////////
            try
            {
                bool first ;
                first = true;
                if (Directory.Exists(@"c:\downloadFTP"))
                {
                    GenInfo.deldir(@"c:\downloadFTP");

                }

                if (Directory.Exists(@"c:\" + userid + username + "-" + currentTime.ToString("m") + @"上传文件夹"))
                {
                    first = false;

                }
                downloadPaper pa = new downloadPaper();
                pa.downftp("ftp://192.168."+ConfigurationManager.AppSettings["ftpip"].ToString() + teacher+"/download", @"/", @"c:\downloadFTP" );
                GenInfo.GenFile(username, userid, Getinfo.GetClientLocalIPv4Address(), Getinfo.GetMacAddress());
                if(first)   GenInfo.Copy(@"c:\downloadFTP", @"c:\" + userid + username + "-" + currentTime.ToString("m") + @"上传文件夹");
                ////////////
                string filePath = @"c:\" + userid + currentTime.ToString("m") + @"download";

                if (Directory.Exists(@"c:\downloadFTP"))
                {
                    GenInfo.deldir(@"c:\downloadFTP");

                }

                ///////////
                
                button2.Enabled = true;
                button3.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"请正确填写和选择信息下载不成功 ");
            }


            //////////////////////////////////////////

         
            label1.Text = "";
            MessageBox.Show("下载完成");
            // button1.Enabled = false;




        }

        private void button2_Click(object sender, EventArgs e)
        {
            UploadFileFTP up = new UploadFileFTP();
            System.DateTime currentTime = DateTime.Now;

            if (GenInfo.Checkfile(@"c:\" + userid + username + "-" + currentTime.ToString("m") + @"上传文件夹\", "log.dat"))
            {

                button2.Enabled = false;
                label1.Text = "上传文件中。。。";
                
                up.UploadDirectory(@"c:\", "ftp://192.168." + ConfigurationManager.AppSettings["ftpip"] + teacher + @"/upload/", userid + username + "-" + currentTime.ToString("m") + @"上传文件夹");
                MessageBox.Show("上传成功!");
                label1.Text = "";
                button2.Enabled = true;
            }
            else
            {

                MessageBox.Show("你删除了系统文件,联系老师!");
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            Dnshelp.setDNS("202.121.241.8");

            string message = "确定要退出吗?";
            string caption = "退出";
            abnormal ab = new abnormal();

            result = MessageBox.Show(message, caption, buttons);
            System.DateTime currentTime = DateTime.Now;
            UploadFileFTP up = new UploadFileFTP();
            if (result== System.Windows.Forms.DialogResult.Yes)
                { 
                try
                {      // if (up.CheckDirectoryExist2("ftp://192.168." + ConfigurationManager.AppSettings["ftpip"] + teacher + @"/upload/", userid + username + "-" + currentTime.ToString("m") + @"上传文件夹"))
                    if(ab.CompareDir (@"c:\","ftp://192.168." + ConfigurationManager.AppSettings["ftpip"] + teacher + @"/upload/", userid + username + "-" + currentTime.ToString("m") + @"上传文件夹"))
                    {
                        label1.Text = "正在退出。。。";

                        Application.Exit();

                    }
                    else
                    {
                        MessageBox.Show("文件有更新请上传文件后退出");
                    } 
                   
                    

                }
                catch(Exception ex)
                {
                    // MessageBox.Show(ex.Message+"异常，请联系老师 ");

                    // Application.Exit();
                    MessageBox.Show("文件有更新请上传文件后退出");



                }
               
            }

        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

      

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
    }
}
