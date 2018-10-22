using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Oco
    {
        public const int _ChieuRong = 30;
        public const int _ChieuCao = 30;

        private int _Dong;
        public int Dong
        {
            get
            { return _Dong;}
            set
            { _Dong = value; }
        }
        private int _Cot;
        public int Cot
        {
            get
            {
                return _Cot;
            }

            set
            {
                _Cot = value;
            }
        }
        public Point Vitri
        {
            get
            {
                return _Vitri;
            }

            set
            {
                _Vitri = value;
            }
        }
        private Point _Vitri;
        public int SoHuu
        {
            get
            {
                return _SoHuu;
            }

            set
            {
                _SoHuu = value;
            }
        }
        private int _SoHuu;
        public Oco()
        {

        }
        public Oco(int dong, int cot, Point vitri, int sohuu)
        {
            _Dong = dong;
            _Cot = cot;
            _Vitri = vitri;
            _SoHuu = sohuu;
        }

    }
}
