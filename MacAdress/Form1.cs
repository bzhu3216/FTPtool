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

namespace MacAdress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            textBox6.Text=abnormal.PrintFileVersionInfo2(@"C:\11111111111zb-6月5日上传文件夹\log.dat");
            textBox7.Text = abnormal.PrintFileVersionInfo2(@"C:\11111111111zb-6月5日上传文件夹\key.xlsx");
        }
    }
}
