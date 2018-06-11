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
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }
        public string userid = null;
        public string username = null;
        public string teacher = null;
        /// <summary>
        /// //////////////////////////////////////////
        

        /// ///////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var intValue = textBox1.GetValue<long>();

                if (textBox1.Text.Length != 11 || comboBox1.Text.Equals(""))
                {
                    textBox1.Focus();
                    textBox1.SelectAll();
                    MessageBox.Show("输入学号格式不正确，或未选择老师！", "学号为11为数字", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    userid = textBox1.Text;
                    username= textBox2.Text;
                    teacher = comboBox1.Text;
                    main mf = new main(userid, username, teacher);
                    // Form4 mf = new Form4(userid, username, teacher);
                    GenInfo.deldir(@"c:\downloadFTP");
                    mf.Show();
                    this.Hide();
                    

                                    }
                
            }
            catch
            {
           
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            String userss = ConfigurationManager.AppSettings["username"].ToString();
            String[] users = userss.Split('|');
            foreach (string user in users)
            {
                comboBox1.Items.Add(user);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process pro = new Process();
            // 设置命令行、参数 
            pro.StartInfo.FileName = "cmd.exe";
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardInput = true;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.RedirectStandardError = true;
            pro.StartInfo.CreateNoWindow = true;
            // 启动CMD 
            pro.Start();
            // 运行端口检查命令 
            pro.StandardInput.WriteLine("netstat -ano");
            pro.StandardInput.WriteLine("exit");
            // 获取结果 
            Regex reg = new Regex("\\s+", RegexOptions.Compiled);
            string line = null;
            while ((line = pro.StandardOutput.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("TCP", StringComparison.OrdinalIgnoreCase))
                {
                    line = reg.Replace(line, ",");
                    string[] arr = line.Split(',');
                    if (arr[1].EndsWith(":21"))
                    {
                        Console.WriteLine("80端口的进程ID：{0}", arr[4]);
                        int pid = Int32.Parse(arr[4]);
                        Process pro80 = Process.GetProcessById(pid);
                        // 处理该进程 
                        break;
                    }
                }
            }
            pro.Close();
        }
    }
}
