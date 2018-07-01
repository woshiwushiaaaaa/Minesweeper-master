using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form_Main : Form
    {
        const int MAX_WIDTH = 64;   
        const int MAX_HEIGHT = 32;  
        int[] dx = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };  
        int[] dy = new int[] { 1, 1, 1, 0, 0, -1, -1, -1 };    
        int[] px = new int[] { 1, -1, 0, 0, 1, -1, 1, -1 };  
        int[] py = new int[] { 0, 0, 1, -1, 1, 1, -1, -1 };  

        public int nWidth;          // 雷区的宽度
        public int nHeight;         // 雷区的高度
        public int nMineCnt;        // 地雷的数目

        bool bMark;     // 标记
        bool bAudio;    // 音效

        bool bMouseLeft;    // 鼠标左键
        bool bMouseRight;   // 鼠标右键

        bool bGame; // 游戏是否结束

        int[,] pMine = new int[MAX_WIDTH, MAX_HEIGHT];  
        int[,] pState = new int[MAX_WIDTH, MAX_HEIGHT]; 

        Point MouseFocus;   // 高亮


        System.Media.SoundPlayer soundTick; // 计时
        System.Media.SoundPlayer soundBomb; // 爆炸

        public Form_Main()
        {
            InitializeComponent();           
            this.DoubleBuffered = true; 
            nWidth = Properties.Settings.Default.Width;
            nHeight = Properties.Settings.Default.Height;
            nMineCnt = Properties.Settings.Default.MineCnt;       
            bMark = Properties.Settings.Default.Mark;
            bAudio = Properties.Settings.Default.Audio;
            UpdateSize();
                      
        }

        /// <summary>
        /// 游戏参数设置
        /// </summary>
        /// <param name="Width">雷区宽度</param>
        /// <param name="Height">雷区高度</param>
        /// <param name="MineCnt">地雷数目</param>
        private void SetGame(int Width, int Height, int MineCnt)
        {
            nWidth = Width;
            nHeight = Height;
            nMineCnt = MineCnt;
            UpdateSize();
        }


       

        private void Form_Main_Paint(object sender, PaintEventArgs e)
        {
            PaintGame(e.Graphics);
        }

        /// <summary>
        /// 绘制游戏区
        /// </summary>
        private void PaintGame(Graphics g)
        {
            g.Clear(Color.White);  
            int nOffsetX = 6;  
            int nOffsetY = 6 ;  
            for (int i = 1; i <= nWidth; i++)    // 绘制行
            {
                for (int j = 1; j <= nHeight; j++)   // 绘制列
                {
                    
                    if(pState[i, j] != 1)   // 未点开
                    {
                        // 绘制背景
                        if (i == MouseFocus.X && j == MouseFocus.Y)  // 是否为高亮点
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.SandyBrown)), new Rectangle(nOffsetX + 28 * (i - 1) + 1, nOffsetY + 28 * (j - 1) + 1, 26, 26));
                        }
                        else
                        {
                            g.FillRectangle(Brushes.SandyBrown, new Rectangle(nOffsetX + 28 * (i - 1) + 1, nOffsetY + 28 * (j - 1) + 1, 26, 26));   // 绘制雷区方块
                        }
                        
                        if(pState[i, j] == 2)
                        {
                            g.DrawImage(Properties.Resources.Flag, nOffsetX + 28 * (i - 1) + 1 + 4, nOffsetY + 28 * (j - 1) + 1 + 2);   // 绘制红旗
                        }
                        if(pState[i, j] == 3)
                        {
                            g.DrawImage(Properties.Resources.Doubt, nOffsetX + 28 * (i - 1) + 1 + 4, nOffsetY + 28 * (j - 1) + 1 + 2);   // 绘制问号
                        }
                    }
                    else if(pState[i, j] == 1)  // 点开
                    {
                        // 绘制背景
                        if(MouseFocus.X == i && MouseFocus.Y == j)
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.LightGray)), new Rectangle(nOffsetX + 28 * (i - 1) + 1, nOffsetY + 28 * (j - 1) + 1, 26, 26));
                        }
                        else
                        {
                            g.FillRectangle(Brushes.LightGray, new Rectangle(nOffsetX + 28 * (i - 1) + 1, nOffsetY + 28 * (j - 1) + 1, 26, 26));
                        }
                        // 绘制数字
                        if(pMine[i, j] > 0)
                        {
                            Brush DrawBrush = new SolidBrush(Color.Blue); 
                            if (pMine[i, j] == 2) { DrawBrush = new SolidBrush(Color.Green); }
                            if (pMine[i, j] == 3) { DrawBrush = new SolidBrush(Color.Red); }
                            if (pMine[i, j] == 4) { DrawBrush = new SolidBrush(Color.DarkBlue); }
                            if (pMine[i, j] == 5) { DrawBrush = new SolidBrush(Color.DarkRed); }
                            if (pMine[i, j] == 6) { DrawBrush = new SolidBrush(Color.DarkSeaGreen); }
                            if (pMine[i, j] == 7) { DrawBrush = new SolidBrush(Color.Black); }
                            if (pMine[i, j] == 8) { DrawBrush = new SolidBrush(Color.DarkGray); }
                            SizeF Size = g.MeasureString(pMine[i, j].ToString(), new Font("Consolas", 16));
                            g.DrawString(pMine[i, j].ToString(), new Font("Consolas", 16), DrawBrush, nOffsetX + 28 * (i - 1) + 1 + (26 - Size.Width) / 2, nOffsetY + 28 * (j - 1) + 1 + (26 - Size.Height) / 2);
                        }
                        // 绘制地雷
                        if(pMine[i, j] == -1)
                        {
                            g.DrawImage(Properties.Resources.Mine, nOffsetX + 28 * (i - 1)+1 , nOffsetY + 28 * (j - 1)+1 );   
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 自动更新窗口大小，使窗口大小适应雷区大小，否则会出现雷区显示不全或者雷区旁边存在白边的情况
        /// </summary>
        private void UpdateSize()
        {
            int nOffsetX = this.Width - this.ClientSize.Width; 
            int nOffsetY = this.Height - this.ClientSize.Height;   
            int nAdditionY =TableLayoutPanel_Main.Height; 
            this.Width = 12 + 28 * nWidth + nOffsetX;  
            this.Height = 12 + 28 * nHeight + nAdditionY + nOffsetY;   
            newGameNToolStripMenuItem_Click(new object(), new EventArgs()); 
        }

        private void settingSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Setting Setting = new Form_Setting(this);  
            Setting.ShowDialog();
            UpdateSize();
        }

        private void rankRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Rank Rank = new Form_Rank();
            Rank.ShowDialog();
        }
        /// <summary>
        /// 设定雷的位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newGameNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 清空数组
            Array.Clear(pMine, 0, pMine.Length);
            Array.Clear(pState, 0, pState.Length);
            MouseFocus.X = 0; MouseFocus.Y = 0;
            // 初始化地雷数据
            Random Rand = new Random();
            //==============================================
            pMine[1, 1] = pMine[3, 1] = pMine[9, 1] = pMine[10, 1] = pMine[19, 1] = -1;
            pMine[4, 2] = pMine[5, 2] = pMine[6, 2] = pMine[9, 2] = pMine[15, 2] = pMine[18, 2] = pMine[19, 2] = -1;
            pMine[14, 4] = pMine[16, 4] = pMine[18, 4] = -1;
            pMine[13, 5] = pMine[13, 5] = pMine[14, 5] = pMine[19, 5] = pMine[8, 5] = -1;
            pMine[7, 6] = pMine[15, 6] = pMine[9, 6] = -1;
            pMine[4, 8] = pMine[18, 8] = pMine[19, 8] = -1;
            pMine[3, 9] = pMine[13, 9] = -1;
            pMine[12, 10] = -1;
            pMine[13, 11] = pMine[19, 11] = -1;
            pMine[8, 12] = pMine[13, 12] = pMine[15, 12] = pMine[17, 12] = pMine[18, 12] = -1;
            pMine[3, 13] = pMine[4, 13] = pMine[9, 13] = -1;
            pMine[2, 15] = pMine[12, 15] = pMine[16, 15] = -1;
            pMine[2, 16] = pMine[3, 16] = pMine[11, 16] = pMine[13, 16] = pMine[14, 16] = pMine[15, 16] = pMine[17, 16] = -1;
            pMine[10, 17] = -1;
            pMine[3, 18] = pMine[6, 18] = pMine[10, 18] = -1;
            pMine[3, 19] = pMine[5, 19] = pMine[11, 19] = -1;
            pMine[3, 20] = pMine[10, 20] = pMine[14, 20] = -1;
            pMine[2, 21] = pMine[10, 21] = pMine[11, 21] = pMine[15, 21] = -1;
            pMine[3, 22] = pMine[7, 22] = pMine[11, 22] = pMine[17, 22] = pMine[18, 22] = -1;
            pMine[2, 23] = pMine[3, 23] = pMine[7, 23] = -1;
            pMine[2, 24] = pMine[10, 24] = pMine[11, 24] = pMine[18, 24] = -1;
            pMine[13, 25] = pMine[14, 25] = pMine[16, 25] = -1;

            //=======================================================
            for (int i = 1; i <= nWidth; i++)    
            {
                for(int j = 1; j <= nHeight; j++)   
                {
                    if(pMine[i, j] != -1)   // 不是地雷，显示周围地雷数
                    {
                        for(int k = 0; k < 8; k++)  
                        {
                            if(pMine[i + dx[k], j + dy[k]] == -1)  
                            {
                                pMine[i, j]++;  
                            }
                        }
                    }
                }
            }
            Label_Mine.Text = nMineCnt.ToString();  // 显示地雷数目
            Label_Timer.Text = "0"; 
            Timer_Main.Enabled = true;  
            bGame = false;  
        }
        /// <summary>
        /// 当鼠标移动到一块雷区时，显示高亮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Main_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.X < 6 || e.X > 6 + nWidth * 28 || 
                e.Y < 6  || 
                e.Y > 6  + nHeight * 28) 
            {
                MouseFocus.X = 0; MouseFocus.Y = 0;
            }
            else
            {
                int x = (e.X - 6) / 28 + 1; 
                int y = (e.Y - 6) / 28 + 1; 
                MouseFocus.X = x; MouseFocus.Y = y; // 设置当前高亮点
            }
            this.Refresh();    
        }

        private void Timer_Main_Tick(object sender, EventArgs e)
        {
            if(bAudio)
            {
                soundTick.Play();   // 播放
            }
            Label_Timer.Text = Convert.ToString(Convert.ToInt32(Label_Timer.Text) + 1); // 自增1秒
        }

        private void Form_Main_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)  
            {
                bMouseLeft = true;
            }
            if(e.Button == MouseButtons.Right)
            { 
                bMouseRight = true;
            }
        }

        private void Form_Main_MouseUp(object sender, MouseEventArgs e)
        {
            if ((MouseFocus.X == 0 && MouseFocus.Y == 0) || bGame)
            {
                return; 
            }

            if(bMouseLeft && bMouseRight)   
            {
                if(pState[MouseFocus.X, MouseFocus.Y] == 1 && pMine[MouseFocus.X, MouseFocus.Y] > 0)    
                {
                    int nFlagCnt = 0, nDoubtCnt = 0, nSysCnt = pMine[MouseFocus.X, MouseFocus.Y];  
                    for(int i = 0; i < 8; i++)
                    {                 
                        int x = MouseFocus.X + dx[i];
                        int y = MouseFocus.Y + dy[i];
                        if(pState[x, y] == 2)   
                        {
                            nFlagCnt++;
                        }
                        if(pState[x, y] == 3)   
                        {
                            nDoubtCnt++;
                        }
                        if(nFlagCnt == nSysCnt || nFlagCnt + nDoubtCnt == nSysCnt) // 打开九宫格
                        {
                            bool bFlag = OpenMine(MouseFocus.X, MouseFocus.Y);
                            if (!bFlag) 
                            {
                                // 结束游戏 
                                GameLost();
                            }
                        }
                    }
                }
            }
            else if(bMouseLeft) 
            {
                if(pMine[MouseFocus.X, MouseFocus.Y] != -1)
                {
                    if(pState[MouseFocus.X, MouseFocus.Y] == 0)
                    {
                        dfs(MouseFocus.X, MouseFocus.Y);
                    }
                }
                else
                {
                    GameLost();
                }
            }
            else if(bMouseRight)    // 右键被按下
            {
                if(bMark)   // 可以使用标记
                {
                    if(pState[MouseFocus.X, MouseFocus.Y] == 0) 
                    {
                        if (Convert.ToInt32(Label_Mine.Text) > 0)
                        {
                            pState[MouseFocus.X, MouseFocus.Y] = 2; //插红旗
                            Label_Mine.Text = Convert.ToString(Convert.ToInt32(Label_Mine.Text) - 1);   // 剩余地雷数目减1
                        }
                    }
                    else if(pState[MouseFocus.X, MouseFocus.Y] == 2) 
                    {
                        pState[MouseFocus.X, MouseFocus.Y] = 3; 
                        Label_Mine.Text = Convert.ToString(Convert.ToInt32(Label_Mine.Text) + 1);   // 剩余地雷数目加1
                    }
                    else if(pState[MouseFocus.X, MouseFocus.Y] == 3) // 问号
                    {
                        pState[MouseFocus.X, MouseFocus.Y] = 0;  
                    }
                }
            }
            this.Refresh();
            GameWin();
            bMouseLeft = bMouseRight = false;
        }

        private void dfs(int sx, int sy)
        {
            pState[sx, sy] = 1; 
            for(int i = 0; i < 8; i++)
            {
                // 获取相邻点的坐标
                int x = sx + px[i];
                int y = sy + py[i];
                if(x >= 1 && x <= nWidth && y >= 1 && y <= nHeight && 
                    pMine[x, y] != -1 && pMine[sx, sy] == 0 && 
                    (pState[x, y] == 0 || pState[x, y] == 3)) // 不是地雷，处于地雷区域，且未点开，或者标记为问号
                {
                    dfs(x, y);  
                }
            }
        }

        private bool OpenMine(int sx, int sy)
        {
            bool bFlag = true;  // 默认周围无雷
            for (int i = 0; i < 8; i++)
            {
                // 获取偏移量
                int x = MouseFocus.X + dx[i];
                int y = MouseFocus.Y + dy[i];
                if (pState[x, y] == 0)   // 问号
                {
                    pState[x, y] = 1;   // 打开
                    if(pMine[x, y] != -1)   // 无地雷
                    {
                        dfs(x, y);
                    }
                    else    // 有地雷
                    {
                        bFlag = false;
                        break;
                    }
                }
            }
            return bFlag;
        }

        private void GameLost()
        {
            for(int i = 1; i <= nWidth; i++)
            {
                for(int j = 1; j <= nHeight; j++)
                {
                    if(pMine[i, j] == -1 && (pState[i, j] == 0 || pState[i, j] == 3))   // 未点开或者标记为问号的地雷
                    {
                        pState[i, j] = 1;   // 点开该地雷
                    }
                }
            }
            if(bAudio)
            {
                soundBomb.Play();
            }
            Timer_Main.Enabled = false; // 停止计时
            bGame = true;
        }

        private void GameWin()
        {
            int nCnt = 0;   // 用户标记红旗数目、问号数目、以及无标记未点开区域总数
            for(int i = 1; i <= nWidth; i++)
            {
                for(int j = 1; j <= nHeight; j++)
                {
                    if(pState[i ,j] == 0 || pState[i, j] == 2 || pState[i, j] == 3) // 对应无标记未点开区域、红旗区域、问号区域
                    {
                        nCnt++;
                    }
                }
            }
            if(nCnt == nMineCnt)    // 胜利条件
            {
                Timer_Main.Enabled = false; // 关闭计时器
                //读取文本
                string sr = File.ReadAllText("D:mines/condition.txt");
                sr = sr.Replace("Z", "A");
                //更改保存文本  
                StreamWriter sw = new StreamWriter("D:mines/condition.txt", false);
                sw.WriteLine(sr);
                sw.Close();
                MessageBox.Show(String.Format("游戏胜利，耗时：{0} 秒", Label_Timer.Text), "提示", MessageBoxButtons.OK);
                bGame = true;
            }
        }
    }
}