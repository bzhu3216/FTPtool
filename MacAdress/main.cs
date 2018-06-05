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
namespace MacAdress
{
    public partial class main : Form
    {
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
       
        private void main_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            button2.Enabled = false;
            button3.Enabled = false;

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
                first = true;                    ; 
                if (Directory.Exists(@"c:\" + userid + currentTime.ToString("m") + @"download"))
                {
                    first = false;
                }
                downloadPaper pa = new downloadPaper();
                pa.downftp("ftp://192.168."+ConfigurationManager.AppSettings["ftpip"].ToString() + teacher+"/download", @"/", @"c:\downloadFTP" );
                GenInfo.GenFile(username, userid, Getinfo.GetClientLocalIPv4Address(), Getinfo.GetMacAddress());
                if(first)   GenInfo.Copy(@"c:\downloadFTP", @"c:\" + userid + username + "-" + currentTime.ToString("m") + @"上传文件夹");
                ////////////
                string filePath = @"c:\" + userid + currentTime.ToString("m") + @"download";

                if (Directory.Exists(filePath))
                {
                  //  Directory.Delete(filePath);
                   
                }

                ///////////
                MessageBox.Show("下载完成");
                button2.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"请正确填写和选择信息下载不成功 ");
            }


            //////////////////////////////////////////

         
            label1.Text = "";
            button1.Enabled = true;




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

            string message = "确定要退出吗?";
            string caption = "退出";

            result = MessageBox.Show(message, caption, buttons);
            System.DateTime currentTime = DateTime.Now;
            UploadFileFTP up = new UploadFileFTP();
            if (result== System.Windows.Forms.DialogResult.Yes)
                { 
                try
                {       if (up.CheckDirectoryExist2("ftp://192.168." + ConfigurationManager.AppSettings["ftpip"] + teacher + @"/upload/", userid + username + "-" + currentTime.ToString("m") + @"上传文件夹"))
                    {
                        label1.Text = "正在退出。。。";

                        Application.Exit();

                    }
                    else
                    {
                        MessageBox.Show("请上传文件后退出");
                    } 
                   
                    

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message+"异常，请联系老师 ");
                   
                    Application.Exit();



                }
               
            }

        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
