using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zongjiemian1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo myStartInfo = new System.Diagnostics.ProcessStartInfo();
            myStartInfo.FileName = @"D:/mines/Minesweeper3.exe";
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo = myStartInfo;
            myProcess.Start();
            myProcess.WaitForExit(); //等待程序退出
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
                System.Diagnostics.ProcessStartInfo myStartInfo = new System.Diagnostics.ProcessStartInfo();
                myStartInfo.FileName = @"D:/mines/mima.exe";
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo = myStartInfo;
                myProcess.Start();
                myProcess.WaitForExit(); //等待程序退出

        }

        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            string sr = File.ReadAllText("D:mines/condition.txt");
            if (sr == "AAAA")
            {
                System.Diagnostics.ProcessStartInfo myStartInfo = new System.Diagnostics.ProcessStartInfo();
                myStartInfo.FileName = @"D:/mines/information1.exe";
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo = myStartInfo;
                myProcess.Start();
                myProcess.WaitForExit(); //等待程序退出
            }
            else
            {
                MessageBox.Show("您还未完成所有扫雷关卡");
            }
        }

        private void button5_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo myStartInfo = new System.Diagnostics.ProcessStartInfo();
            myStartInfo.FileName = @"D:/mines/Minesweeper.exe";
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo = myStartInfo;
            myProcess.Start();
            myProcess.WaitForExit(); //等待程序退出
        }

        private void button6_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo myStartInfo = new System.Diagnostics.ProcessStartInfo();
            myStartInfo.FileName = @"D:/mines/Minesweeper1.exe";
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo = myStartInfo;
            myProcess.Start();
            myProcess.WaitForExit(); //等待程序退出
        }

        private void button7_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo myStartInfo = new System.Diagnostics.ProcessStartInfo();
            myStartInfo.FileName = @"D:/mines/Minesweeper2.exe";
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo = myStartInfo;
            myProcess.Start();
            myProcess.WaitForExit(); //等待程序退出
        }
    }
}
