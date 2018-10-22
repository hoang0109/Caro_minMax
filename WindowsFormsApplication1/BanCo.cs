using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class BanCo
    {
        private int _SoDong;
        private int _SoCot;

        public int SoCot
        {
            get
            {
                return _SoCot;
            }

            set
            {
                _SoCot = value;
            }
        }

        public int SoDong
        {
            get
            {
                return _SoDong;
            }

            set
            {
                _SoDong = value;
            }
        }

        public BanCo()
        {
            SoCot = 0;
            SoDong = 0;
        }
        public BanCo(int soDong,int soCot)
        {
            SoDong = soDong;
            SoCot = soCot;
        }
        public void Vebanco(Graphics g)
        {
            for (int i = 0; i <= SoCot; i++)
            {
                g.DrawLine(Pens.Black, i * Oco._ChieuRong, 0, i * Oco._ChieuRong, SoDong * Oco._ChieuCao);
            }
            for (int j   = 0; j <= SoDong; j++)
            {
                g.DrawLine(Pens.Black, 0, j * Oco._ChieuCao, SoCot * Oco._ChieuRong, j * Oco._ChieuCao);
            }
        }
        public void VeQuanCo(Graphics g, Point point,SolidBrush sb)
        {
            g.FillEllipse(sb, point.X+5, point.Y+5, Oco._ChieuRong-10, Oco._ChieuCao-10);
        }
        public void XoaQuanCo(Graphics g, Point p,SolidBrush sb)
        {
            g.FillRectangle(sb, p.X+5, p.Y+5, Oco._ChieuRong-10, Oco._ChieuCao-10);
        }
    }
}
