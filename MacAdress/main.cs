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
using testzip;
namespace MacAdress
{
    public partial class main : Form
    {
        public string userid = null;
        public string username = null;
        public string teacher = null;
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
                pa.downftp(ConfigurationManager.AppSettings["ftpip"].ToString() + teacher+"/download", @"/", @"c:\" + userid + currentTime.ToString("m")+@"download");
                GenInfo.GenFile(username, userid, Getinfo.GetClientLocalIPv4Address(), Getinfo.GetMacAddress());
                if(first)   GenInfo.Copy(@"c:\" + userid + currentTime.ToString("m") + @"download", @"c:\" + userid + username + "-" + currentTime.ToString("m") + @"上传文件夹");

                MessageBox.Show("下载完成");
                button2.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = false;
            }
            catch
            {
                MessageBox.Show("请正确填写和选择信息下载不成功 ");
            }


            //////////////////////////////////////////

         
            label1.Text = "";




        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "上传文件中。。。";
            UploadFileFTP up = new UploadFileFTP();
            System.DateTime currentTime = DateTime.Now;
            up.UploadDirectory(@"c:\", ConfigurationManager.AppSettings["ftpip"]+teacher+@"/upload/", userid + username + "-"+currentTime.ToString("m") + @"上传文件夹");
            MessageBox.Show("上传成功!");
            label1.Text = "";




        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            string message = "确定要退出吗?";
            string caption = "退出";

            result = MessageBox.Show(message, caption, buttons);
           
            if(result== System.Windows.Forms.DialogResult.Yes)
                { 
                try
                {
                    label1.Text = "正在退出。。。";
                    System.DateTime currentTime = DateTime.Now;
                    Dzip.ZipFile(@"c:\" + userid + username + "-" + currentTime.ToString("m") + @"上传文件夹", @"c:\" + userid + username + ".zip");
                    UploadFileFTP up = new UploadFileFTP();
                    up.UpLoadFile(@"c:\" + userid + username + ".zip", ConfigurationManager.AppSettings["ftpip"] + teacher + @"/bak/" + userid + username + ".zip");
                    label1.Text = "";
                }
                catch
                {
                    MessageBox.Show("非正常退出请联系老师 ");
                }
                Application.Exit();
            }

        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
