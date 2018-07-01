using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mima
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           

        }
      

        private void button1_Click_1(object sender, EventArgs e)
        {
            string str = textBox1.Text;        
            if (str == "ZJLG")
            {
                MessageBox.Show("恭喜你，成功逃出！！");
            }
            else
            {
                MessageBox.Show("密码错误");
            }
        }
    }
}
