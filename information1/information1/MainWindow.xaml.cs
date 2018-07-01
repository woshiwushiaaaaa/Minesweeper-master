using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace information1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //左上角的图片
        Point pos = new Point();
        private void Image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image tmp = (Image)sender;
            pos = e.GetPosition(null);
            tmp.CaptureMouse();
            tmp.Cursor = Cursors.Hand;
        }
        private void Image1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Image tmp = (Image)sender;
                double dx = e.GetPosition(null).X - pos.X + tmp.Margin.Left;
                double dy = e.GetPosition(null).Y - pos.Y + tmp.Margin.Top;
                tmp.Margin = new Thickness(dx, dy, 0, 0);
                pos = e.GetPosition(null);
            }
        }
        private void Image1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image tmp = (Image)sender;
            tmp.ReleaseMouseCapture();
        }


        
        //右上角的图片
        Point pos1 = new Point();
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image tmp = (Image)sender;
            pos1 = e.GetPosition(null);
            tmp.CaptureMouse();
            tmp.Cursor = Cursors.Hand;

        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Image tmp = (Image)sender;
                double dx = e.GetPosition(null).X - pos1.X + tmp.Margin.Left;
                double dy = e.GetPosition(null).Y - pos1.Y + tmp.Margin.Top;
                tmp.Margin = new Thickness(dx, dy, 0, 0);
                pos1 = e.GetPosition(null);
            }

        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image tmp = (Image)sender;
            tmp.ReleaseMouseCapture();
        }

        //左下角的图片
        Point pos2 = new Point();
        private void Image_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Image tmp2 = (Image)sender;
                double dx = e.GetPosition(null).X - pos2.X + tmp2.Margin.Left;
                double dy = e.GetPosition(null).Y - pos2.Y + tmp2.Margin.Top;
                tmp2.Margin = new Thickness(dx, dy, 0, 0);
                pos2 = e.GetPosition(null);
            }
            
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Image tmp2 = (Image)sender;
            pos2 = e.GetPosition(null);
            tmp2.CaptureMouse();
            tmp2.Cursor = Cursors.Hand;

        }

        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            Image tmp2 = (Image)sender;
            tmp2.ReleaseMouseCapture();
        }
        //右下角的图片
        Point pos3 = new Point();
        

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Image tmp2 = (Image)sender;
            pos3 = e.GetPosition(null);
            tmp2.CaptureMouse();
            tmp2.Cursor = Cursors.Hand;
        }

        private void Image_MouseMove_2(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Image tmp2 = (Image)sender;
                double dx = e.GetPosition(null).X - pos3.X + tmp2.Margin.Left;
                double dy = e.GetPosition(null).Y - pos3.Y + tmp2.Margin.Top;
                tmp2.Margin = new Thickness(dx, dy, 0, 0);
                pos3 = e.GetPosition(null);
            }
        }

        private void Image_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            Image tmp2 = (Image)sender;
            tmp2.ReleaseMouseCapture();
        }
    }
}
