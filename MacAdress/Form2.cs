using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

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
    }
}
