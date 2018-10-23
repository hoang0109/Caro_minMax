using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public enum Ketthuc
    {
        Hoaco,
        Man1,
        Man2,
        Com
    }
    class CacNuocCoTheDi
    {
        Oco _oco;
        int _CacNuocTrong;

        public Oco Oco
        {
            get
            {
                return _oco;
            }

            set
            {
                _oco = value;
            }
        }

        public int CacNuocTrong
        {
            get
            {
                return _CacNuocTrong;
            }

            set
            {
                _CacNuocTrong = value;
            }
        }

        public CacNuocCoTheDi(Oco oco, int cacnuoctrong)
        {
            _oco = oco;
            _CacNuocTrong = cacnuoctrong;
        }
    }
    class CaroChess
    {

        public static SolidBrush sbRed;
        public static SolidBrush sbBlue;
        public static SolidBrush sbControl;
        private Oco[,] _MangOco;
        private CacNuocCoTheDi[] _CacNuocCoTheDi;
        private BanCo _BanCo;
        private Stack<Oco> Stack_CacNuocDaDi;
        private Stack<Oco> Stack_CacUndo;
        private int _LuotDi;
        private bool _SanSang;
        private int _CheDoChoi;
        private Ketthuc _ketthuc;
        private int _Sodong = 15;
        private int _SoCot = 12;
        public bool SanSang
        {
            get
            {
                return _SanSang;
            }

            set
            {
                SanSang = value;
            }
        }
        /// <summary>
        /// Khai báo bàn cờ các biến 
        /// </summary>
        public CaroChess()
        {
            sbRed = new SolidBrush(Color.Red);
            sbBlue = new SolidBrush(Color.Blue);
            sbControl = new SolidBrush(Color.FromArgb(224, 224, 224));
            _LuotDi = 1;
            _BanCo = new BanCo(_Sodong, _SoCot);
            _MangOco = new Oco[_BanCo.SoDong, _BanCo.SoCot];
            Stack_CacNuocDaDi = new Stack<Oco>();
            Stack_CacUndo = new Stack<Oco>();


        }
        #region Vebanco
        /// <summary>
        /// Gọi hàm vẽ bàn cờ
        /// </summary>
        /// <param name="g"></param>
        public void VeBanCo(Graphics g)
        {
            _BanCo.Vebanco(g);
        }
        /// <summary>
        /// Khởi tạo mảng bàn cờ với với các giá trị trong mảng bàn cờ với các Node ô cờ có sở hữu =0 (không có ai đánh)
        /// </summary>
        public void KhoitaoMangoCo()
        {
            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                    _MangOco[i, j] = new Oco(i, j, new Point(j * Oco._ChieuRong, i * Oco._ChieuCao), 0);
                }
            }

        }
        /// <summary>
        /// Hàm đánh cờ được gọi khi MouseClick tại Panel bàn cờ bên Form1
        /// </summary>
        /// <param name="MouseX">Giá trị X chuột ấn vô panel</param>
        /// <param name="MouseY">Giá trị Y chuột ấn vô panel</param>
        /// <param name="g">biến Graphic</param>
        /// <returns></returns>
        public bool Danhco(int MouseX, int MouseY, Graphics g)
        {
            if (MouseX % Oco._ChieuRong == 0 || MouseY % Oco._ChieuCao == 0)
            {
                return false;
            }
            int Cot = MouseX / Oco._ChieuRong;
            int Dong = MouseY / Oco._ChieuCao;
            if (_MangOco[Dong, Cot].SoHuu != 0)
            {
                return false;
            }
            switch (_LuotDi)
            {
                case 1:
                    _MangOco[Dong, Cot].SoHuu = 1;
                    _BanCo.VeQuanCo(g, _MangOco[Dong, Cot].Vitri, sbRed);
                    _LuotDi = 2;
                    break;
                case 2:
                    _MangOco[Dong, Cot].SoHuu = 2;
                    _BanCo.VeQuanCo(g, _MangOco[Dong, Cot].Vitri, sbBlue);
                    _LuotDi = 1;
                    break;
                default:
                    MessageBox.Show("Co loi");
                    break;
            }
            Stack_CacUndo.Clear();
            Oco oco = new Oco(_MangOco[Dong, Cot].Dong, _MangOco[Dong, Cot].Cot, _MangOco[Dong, Cot].Vitri, _MangOco[Dong, Cot].SoHuu);
            Stack_CacNuocDaDi.Push(oco);
            return true;
        }
        /// <summary>
        /// Vẽ lại quân cờ khi đã có nước vờ mới
        /// </summary>
        /// <param name="g">Biến Graphics bàn cờ</param>
        public void VeLaiQUanCo(Graphics g)
        {
            foreach (Oco item in Stack_CacNuocDaDi)
            {
                if (item.SoHuu == 1)
                {
                    _BanCo.VeQuanCo(g, item.Vitri, sbRed);
                }
                else
                {
                    if (item.SoHuu == 2)
                    {
                        _BanCo.VeQuanCo(g, item.Vitri, sbBlue);
                    }

                }

            }
        }
        #endregion

        #region Undo_reDo
        /// <summary>
        /// Hàm Undo hồi lại nước đã đi
        /// </summary>
        /// <param name="g"></param>
        public void Undo(Graphics g)
        {
            if (Stack_CacNuocDaDi.Count != 0)
            {
                Oco oco = Stack_CacNuocDaDi.Pop();
                _MangOco[oco.Dong, oco.Cot].SoHuu = 0;
                _BanCo.XoaQuanCo(g, oco.Vitri, sbControl);
                Stack_CacUndo.Push(new Oco(oco.Dong, oco.Cot, oco.Vitri, oco.SoHuu));
                if (_LuotDi == 1)
                {
                    _LuotDi = 2;
                }
                else
                {
                    _LuotDi = 1;
                }
            }

            //VeBanCo(g);
            //VeLaiQUanCo(g);
        }
        /// <summary>
        /// Hàm Redo (được dùng sau khi đã Undo)
        /// </summary>
        /// <param name="g"></param>
        public void Redo(Graphics g)
        {
            if (Stack_CacUndo.Count != 0)
            {

                Oco oco = Stack_CacUndo.Pop();
                _MangOco[oco.Dong, oco.Cot].SoHuu = oco.SoHuu;
                _BanCo.VeQuanCo(g, oco.Vitri, oco.SoHuu == 1 ? sbRed : sbBlue);
                Stack_CacNuocDaDi.Push(new Oco(oco.Dong, oco.Cot, oco.Vitri, oco.SoHuu));
                if (_LuotDi == 1)
                {
                    _LuotDi = 2;
                }
                else
                {
                    _LuotDi = 1;
                }

            }
        }
        
        #endregion
        /// <summary>
        /// Hàm gọi chế độ chơi giữa 2 người với nhau
        /// </summary>
        /// <param name="g"></param>
        public void ManVsMan(Graphics g)
        {
            _SanSang = true;
            Stack_CacNuocDaDi.Clear();
            Stack_CacUndo.Clear();
            _LuotDi = 1;
            _CheDoChoi = 1;
            KhoitaoMangoCo();
            VeBanCo(g);
        }
        /// <summary>
        /// Hàm gọi chế độ chơi giữa người với máy
        /// </summary>
        /// <param name="g"></param>
        public void ManVsCom(Graphics g)
        {
            _SanSang = true;
            Stack_CacNuocDaDi.Clear();
            Stack_CacUndo.Clear();
            _LuotDi = 1;
            _CheDoChoi = 2;
            KhoitaoMangoCo();
            VeBanCo(g);
        }

        #region Duyet Chien Thang
        /// <summary>
        /// Kiểm tra trạng thái của Game
        /// Hòa cờ hay người nao thắng
        /// </summary>
        public void KetThucTroChoi()
        {
            switch (_ketthuc)
            {
                case Ketthuc.Hoaco:
                    MessageBox.Show("Hoa co");
                    break;
                case Ketthuc.Man1:
                    MessageBox.Show("Nguoi so 1 thang");
                    break;
                case Ketthuc.Man2:
                    MessageBox.Show("Nguoi choi 2 thang");
                    break;
                case Ketthuc.Com:
                    MessageBox.Show("Computer thang");
                    break;
            }
            _SanSang = false;
        }
        /// <summary>
        /// Kiểm tra chiến thắng sau khi đã đi một nước cờ
        /// </summary>
        /// <returns></returns>
        public bool KiemtraChienThang()
        {
            if (Stack_CacNuocDaDi.Count == _BanCo.SoCot * _BanCo.SoDong)
            {
                _ketthuc = Ketthuc.Hoaco;
                return true;
            }
            foreach (Oco item in Stack_CacNuocDaDi)
            {
                if (DuyetDoc(item.Dong, item.Cot, item.SoHuu) == 5)
                {
                    if (item.SoHuu == 1)
                        _ketthuc = Ketthuc.Man1;
                    if (item.SoHuu == 2)
                        _ketthuc = Ketthuc.Man2;
                    if (item.SoHuu == 3)
                        _ketthuc = Ketthuc.Com;
                    return true;
                }
                if (DuyetNgang(item.Dong, item.Cot, item.SoHuu) == 5)
                {
                    if (item.SoHuu == 1)
                        _ketthuc = Ketthuc.Man1;
                    if (item.SoHuu == 2)
                        _ketthuc = Ketthuc.Man2;
                    if (item.SoHuu == 3)
                        _ketthuc = Ketthuc.Com;

                    return true;
                }
                if (DuyetCheoXuoi(item.Dong, item.Cot, item.SoHuu) == 5)
                {
                    if (item.SoHuu == 1)
                        _ketthuc = Ketthuc.Man1;
                    if (item.SoHuu == 2)
                        _ketthuc = Ketthuc.Man2;
                    if (item.SoHuu == 3)
                        _ketthuc = Ketthuc.Com;

                    return true;
                }
                if (DuyetCheoNguoc(item.Dong, item.Cot, item.SoHuu) == 5)
                {
                    if (item.SoHuu == 1)
                        _ketthuc = Ketthuc.Man1;
                    if (item.SoHuu == 2)
                        _ketthuc = Ketthuc.Man2;
                    if (item.SoHuu == 3)
                        _ketthuc = Ketthuc.Com;

                    return true;
                }
            }
            return false;
        }
        public int KiemtraChienThang(Oco[,] _Mangoco)
        {
            if (Stack_CacNuocDaDi.Count == _BanCo.SoCot * _BanCo.SoDong)
            {
                _ketthuc = Ketthuc.Hoaco;
                return 0;
            }
            foreach (Oco item in Stack_CacNuocDaDi)
            {
                if (DuyetDoc(item.Dong, item.Cot, item.SoHuu) == 5)
                {
                    if (item.SoHuu == 1)
                        _ketthuc = Ketthuc.Man1;
                    if (item.SoHuu == 2)
                        _ketthuc = Ketthuc.Man2;
                    if (item.SoHuu == 3)
                        _ketthuc = Ketthuc.Com;
                    return true;
                }
                if (DuyetNgang(item.Dong, item.Cot, item.SoHuu) == 5)
                {
                    if (item.SoHuu == 1)
                        _ketthuc = Ketthuc.Man1;
                    if (item.SoHuu == 2)
                        _ketthuc = Ketthuc.Man2;
                    if (item.SoHuu == 3)
                        _ketthuc = Ketthuc.Com;

                    return true;
                }
                if (DuyetCheoXuoi(item.Dong, item.Cot, item.SoHuu) == 5)
                {
                    if (item.SoHuu == 1)
                        _ketthuc = Ketthuc.Man1;
                    if (item.SoHuu == 2)
                        _ketthuc = Ketthuc.Man2;
                    if (item.SoHuu == 3)
                        _ketthuc = Ketthuc.Com;

                    return true;
                }
                if (DuyetCheoNguoc(item.Dong, item.Cot, item.SoHuu) == 5)
                {
                    if (item.SoHuu == 1)
                        _ketthuc = Ketthuc.Man1;
                    if (item.SoHuu == 2)
                        _ketthuc = Ketthuc.Man2;
                    if (item.SoHuu == 3)
                        _ketthuc = Ketthuc.Com;

                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Duyệt chiến thắng theo hàng dọc
        /// </summary>
        /// <param name="currDong">Giá trị dòng của ô</param>
        /// <param name="currCot">Giá trị cột của ô</param>
        /// <param name="currSohuu">Ô thuộc sở hữu của ai</param>
        /// <returns>Thắng hay không</returns>
        private int DuyetDoc(int currDong, int currCot, int currSohuu)
        {
            int Dem =0;
            if (currDong > _BanCo.SoDong - 5)
            {
                return Dem;
            }
            
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOco[currDong + Dem, currCot].SoHuu != currSohuu)
                {
                    return Dem;
                }
            }
            return Dem;
        }
        /// <summary>
        /// Duyệt chiến thắng theo hàng ngang
        /// </summary>
        /// <param name="currDong">Giá trị dòng của ô</param>
        /// <param name="currCot">Giá trị cột của ô</param>
        /// <param name="currSohuu">Ô thuộc sở hữu của ai</param>
        /// <returns>Thắng hay không</returns>
        private int DuyetNgang(int currDong, int currCot, int currSohuu)
        {
            int Dem = 0;
            if (currCot > _BanCo.SoCot - 5)
            {
                return Dem;
            }
            
            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOco[currDong, currCot + Dem].SoHuu != currSohuu)
                {
                    return Dem;
                }
            }
            return Dem;
        }
        /// <summary>
        /// Duyệt chiến thắng theo chéo xuôi
        /// </summary>
        /// <param name="currDong">Giá trị dòng của ô</param>
        /// <param name="currCot">Giá trị cột của ô</param>
        /// <param name="currSohuu">Ô thuộc sở hữu của ai</param>
        /// <returns>Thắng hay không</returns>
        private int DuyetCheoXuoi(int currDong, int currCot, int currSohuu)
        {
            int Dem = 0;
            if (currCot > _BanCo.SoCot - 5 || currDong > _BanCo.SoDong - 5)
            {
                return Dem;
            }

            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOco[currDong + Dem, currCot + Dem].SoHuu != currSohuu)
                {
                    return Dem;
                }
            }

                return Dem;
            
        }
        /// <summary>
        /// Duyệt chiến thắng theo chéo ngược
        /// </summary>
        /// <param name="currDong">Giá trị dòng của ô</param>
        /// <param name="currCot">Giá trị cột của ô</param>
        /// <param name="currSohuu">Ô thuộc sở hữu của ai</param>
        /// <returns>Thắng hay không</returns>
        private int DuyetCheoNguoc(int currDong, int currCot, int currSohuu)
        {
            int Dem = 0;
            if (currCot > _BanCo.SoCot - 5 || currDong < 4)
            {
                return Dem;
            }

            for (Dem = 1; Dem < 5; Dem++)
            {
                if (_MangOco[currDong - Dem, currCot + Dem].SoHuu != currSohuu)
                {
                    return Dem;
                }
            }

            return Dem;
        }

        #endregion

        #region AI
        private long[] MangDiemTanCong = new long[5] { 0, 1, 9, 81, 729 };
        private long[] MangDiemPhongNgu = new long[5] { 0, 2, 18, 162, 1458 };

        public void KhoiDongCom(Graphics g)
        {
            if (Stack_CacNuocDaDi.Count == 0)
            {
                Danhco(_BanCo.SoDong / 2 * Oco._ChieuCao + 1, _BanCo.SoCot / 2 * Oco._ChieuRong + 1, g);

            }
            else
            {
                //Oco oco = TimkiemNuocDi();
                // Danhco(oco.Vitri.X + 1, oco.Vitri.Y + 1,g);
            }
        }
        /// <summary>
        /// Tạo danh sách các nước có thể đi trên 1 ô (node)
        /// </summary>
        /// <returns>Danh sách Oco(i,j); Số ô có thể đi tại ô đó</returns>
        private CacNuocCoTheDi[] TaoCacNuocCoTheDi()
        {
            if (_SanSang == false)
            {
                return null;
            }
            CacNuocCoTheDi[] cacnuoc = new CacNuocCoTheDi[_BanCo.SoCot * _BanCo.SoDong];//+1
            for (int i = 0; i < _BanCo.SoCot * _BanCo.SoDong; i++)
            {
                Oco oco = new Oco();
                    cacnuoc[i] =new CacNuocCoTheDi(oco, -1);
            }
            
            int duyeto = 0;

            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                    int Dem = 0;
                    int h = i - 1;
                    int k = j - 1;
                    int l = i+1;
                    int m = j+1;
                    if (h<0)
                        h = 0;
                    if (k<0)
                        k = 0;
                    if (l>=_BanCo.SoDong)
                        l = _BanCo.SoDong - 1;
                    
                    if (m >= _BanCo.SoCot)
                        m = _BanCo.SoCot - 1;
                    for (int g = h; g <= l; g++)
                        for (int f = k; f <= m; f++)
                                if (_MangOco[g, f].SoHuu == 0)
                                    Dem++;

                    if (_MangOco[i, j].SoHuu==0)
                        Dem--;

                    cacnuoc[duyeto] = new CacNuocCoTheDi(_MangOco[i, j], Dem);
                    duyeto++;
                
                    
                }



            }




            return cacnuoc;

        }

        private long Eval(Oco oco, Oco[,] _MangOco_New)
        {
            int DiemTanCong, DiemPhongNgu;
            long DiemMax = 0;
            //Đánh giá theo hàng
            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot-4; j++)
                {

                    DiemTanCong = DuyetNgang(i, j, 3) + DuyetDoc(i, j, 3) + DuyetCheoNguoc(i, j, 3) + DuyetCheoXuoi(i, j, 3);
                    DiemPhongNgu = DuyetNgang(i, j, 1) + DuyetDoc(i, j, 1) + DuyetCheoNguoc(i, j, 1) + DuyetCheoXuoi(i, j, 1);
                    long DiemTam = DiemTanCong > DiemPhongNgu ? DiemTanCong : DiemPhongNgu;\
                    if (DiemMax < DiemTam)
                    {
                        DiemMax = DiemTam;
                    }
                }
            }
            return DiemMax;

        }
        private long TinhEval_duyetdoc(int currDong, int currCot, int sohuu)
        {
            long diemtong = 0;
            int Soquanta = 0;
            int Soquandich = 0;
            if (sohuu== 1)
            {
                Soquanta = DuyetDoc(currDong, currCot, 1);
                Soquandich = DuyetDoc(currDong, currCot, 3);
            }
            else
            {
                Soquanta = DuyetDoc(currDong, currCot, 3);
                Soquandich = DuyetDoc(currDong, currCot, 1);
            }

            diemtong -= MangDiemPhongNgu[Soquandich];
            diemtong += MangDiemTanCong[Soquanta];

            return diemtong;
        }
        private long TinhEval_duyetNgang(int currDong, int currCot, int sohuu)
        {
            long diemtong = 0;
            int Soquanta = 0;
            int Soquandich = 0;
            if (sohuu == 1)
            {
                Soquanta = DuyetNgang(currDong, currCot, 1);
                Soquandich = DuyetNgang(currDong, currCot, 3);
            }
            else
            {
                Soquanta = DuyetNgang(currDong, currCot, 3);
                Soquandich = DuyetNgang(currDong, currCot, 1);
            }

            diemtong -= MangDiemPhongNgu[Soquandich];
            diemtong += MangDiemTanCong[Soquanta];

            return diemtong;
        }
        private long TinhEval_DuyetCheoXuoi(int currDong, int currCot, int sohuu)
        {
            long diemtong = 0;
            int Soquanta = 0;
            int Soquandich = 0;
            if (sohuu == 1)
            {
                Soquanta = DuyetCheoXuoi(currDong, currCot, 1);
                Soquandich = DuyetCheoXuoi(currDong, currCot, 3);
            }
            else
            {
                Soquanta = DuyetCheoXuoi(currDong, currCot, 3);
                Soquandich = DuyetCheoXuoi(currDong, currCot, 1);
            }

            diemtong -= MangDiemPhongNgu[Soquandich];
            diemtong += MangDiemTanCong[Soquanta];

            return diemtong;
        }
        private long TinhEval_DuyetCheoNguoc(int currDong, int currCot, int sohuu)
        {
            long diemtong = 0;
            int Soquanta = 0;
            int Soquandich = 0;
            if (sohuu == 1)
            {
                Soquanta = DuyetCheoNguoc(currDong, currCot, 1);
                Soquandich = DuyetCheoNguoc(currDong, currCot, 3);
            }
            else
            {
                Soquanta = DuyetCheoNguoc(currDong, currCot, 3);
                Soquandich = DuyetCheoNguoc(currDong, currCot, 1);
            }

            diemtong -= MangDiemPhongNgu[Soquandich];
            diemtong += MangDiemTanCong[Soquanta];

            return diemtong;
        }


        public string XacDinhCacNuocXungQuanh(int MouseX, int MouseY, Graphics g)
        {
            if (_SanSang == false)
            {
                return null;
            }
            int Cot = MouseX / Oco._ChieuRong;
            int Dong = MouseY / Oco._ChieuCao;
            _CacNuocCoTheDi = TaoCacNuocCoTheDi();
            foreach (CacNuocCoTheDi item in _CacNuocCoTheDi)
            {
                if (item.Oco.Cot == Cot && item.Oco.Dong == Dong)
                {
                    return item.CacNuocTrong.ToString();
                }

            }
            return null;



        }
        /// <summary>
        /// Hàm tính MinMax sau khi đã sinh nước cờ
        /// </summary>
        /// <param name="_MangOco_New">Mảng ô cờ sau khi đã sinh nước </param>
        /// <param name="Depth">Độ sâu của Node</param>
        /// <param name="isMaximizingPlayer">Là Player hay Com để lấy giá trị MinMax</param>
        /// <returns>Giá trị MinMax sau khi đi đến độ sâu quy định</returns>
        private int MinMax(Oco[,] _MangOco_New, int Depth, bool isMaximizingPlayer)
        {
            if (_ketthuc == Ketthuc.Com)
                return
        }
        private int alphabeta(Oco oco, int depth, int alpha, int beta, bool maximizingPlayer)
        {

            int DiemTong = 0;

            return DiemTong;


        }


        #endregion
    }
}

