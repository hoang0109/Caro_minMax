using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoItX3Lib;
namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
        private CaroChess crChess;
        private Graphics grs;
        private Oco[,] _MangOco;
        private BanCo _BanCo;
        public int Top_Old  = 243;
        public int Left_Old = 596;
        public int Right_Old = 1066;
        public int Bottom_Old = 835;
        public string[,] Matrix = new string[12,15];
        public int Distance;
        public dynamic  aCoord;
        AutoItX3 au3 = new AutoItX3();
        public Form1()
        {
            
            InitializeComponent();
            crChess = new CaroChess();
            crChess.KhoitaoMangoCo();
            grs = panel1.CreateGraphics();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //Distance = (Bottom_Old - Top_Old) / 15;
            




        }
        private void LoadGameWindow()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    int top_ = Top_Old + i * Distance;
                    int left_ = Left_Old + j * Distance;
                    int right_ = left_ + Distance;
                    int Bottom_ = top_ + Distance;
                    aCoord = au3.PixelSearch(left_, top_, right_, Bottom_, 0x365899);
                    if (aCoord.ToString() !="0")
                    {
                        Matrix[i, j] = "x";
                    }
                    else
                    {
                        aCoord = au3.PixelSearch(left_, top_, right_, Bottom_, 0xBD1F2D);
                            if (aCoord.ToString() != "0")
                            {
                            Matrix[i, j] = "o";
                        }
                        else
                        {
                            Matrix[i, j] = "_";
                        }
                    }
                    aCoord = null;
                }
            }
        }

     
            
        private void button1_Click(object sender, EventArgs e)
        {
            
            Console.Clear();
            LoadGameWindow();
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.Write(Matrix[i, j]);
                }
                Console.WriteLine();
            }
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
            crChess.VeBanCo(grs);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (crChess.SanSang == false)
            {
                return;
            }
            crChess.Danhco(e.X, e.Y,grs);
            if (crChess.KiemtraChienThang()==true)
            {
                crChess.KetThucTroChoi();
            }
        }
        private void Bt_2man_Click(object sender, EventArgs e)
        {
            grs.Clear(panel1.BackColor);
            crChess.ManVsMan(grs);               
        }
        private void bt_ManvsCOm_Click(object sender, EventArgs e)
        {

        }

        private void Bt_undo_Click(object sender, EventArgs e)
        {

            crChess.Undo(grs);
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            crChess.Redo(grs);
        }
        ToolTip tip = new ToolTip();
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            


        }
    }
}
