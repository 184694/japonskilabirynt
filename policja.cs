using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JaponskiLabirynt.labirynt;

namespace JaponskiLabirynt
{
    internal class policja
    {

        private japonskilabirynt MAIN;
        private PictureBox POLICJANT;
        private string KIERUNEKPOLICJANTA = "PRAWO";
        private Point POZYCJAPOLICJANTA;
        private int ROZMIARKOMORKI, WYSOKOSC, SZEROKOSC;
        private int OFFSETX, OFFSETY;
        static public Image POLICJANTIMG = Image.FromFile("../../../pliki/police.png");

        public policja(int ROZMIARKOMORKI, int WYSOKOSC, int SZEROKOSC, japonskilabirynt MAIN)
        {
            this.ROZMIARKOMORKI = ROZMIARKOMORKI;
            this.WYSOKOSC = WYSOKOSC;
            this.SZEROKOSC = SZEROKOSC;
            POZYCJAPOLICJANTA = new Point(0, 0);

            POLICJANT = new PictureBox();
            POLICJANT.SizeMode = PictureBoxSizeMode.Zoom;
            POLICJANT.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
            POLICJANT.Image = POLICJANTIMG;

            MAIN.Paint += new System.Windows.Forms.PaintEventHandler(paint);
        }

        public void start()
        {

        }
        public void ruch(int x, int y)
        {

        }


        private int liczPOLICJANTx()
        {
            if (KIERUNEKPOLICJANTA == "GORA" || KIERUNEKPOLICJANTA == "DOL")
            {
                OFFSETX = japonskilabirynt.X + POZYCJAPOLICJANTA.X * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUX - SZEROKOSC * ROZMIARKOMORKI) / 2 + POLICJANT.Width;
            }
            else
                OFFSETX = japonskilabirynt.X + POZYCJAPOLICJANTA.X * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUX - SZEROKOSC * ROZMIARKOMORKI) / 2 + POLICJANT.Width / 2;

            return OFFSETX;
        }

        private int liczPOLICJANTy()
        {
            //OFFSETY = USTAWIENIA[1] + POZYCJAPOLICJANTA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY / 2 - (ROZMIARKOMORKI * SZEROKOSC) / 2) + ROZMIARKOMORKI / 2 - POLICJANT.Height/ 2 - POLICJANT.Height / 2;

            if (KIERUNEKPOLICJANTA == "GORA" || KIERUNEKPOLICJANTA == "DOL")
            {
                OFFSETY = POZYCJAPOLICJANTA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY - WYSOKOSC * ROZMIARKOMORKI) / 2 + POLICJANT.Height / 2;
            }
            else
                OFFSETY = POZYCJAPOLICJANTA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY - WYSOKOSC * ROZMIARKOMORKI) / 2 + POLICJANT.Height;
            return OFFSETY;
        }

        public void orientacja(int kierunek)
        {
            switch (kierunek)
            {
                case 0: // a
                    if (KIERUNEKPOLICJANTA == "PRAWO")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKPOLICJANTA == "GORA")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKPOLICJANTA == "DOL")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    KIERUNEKPOLICJANTA = "LEWO";
                    break;
                case 1: // w
                    if (KIERUNEKPOLICJANTA == "LEWO")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKPOLICJANTA == "PRAWO")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKPOLICJANTA == "DOL")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    KIERUNEKPOLICJANTA = "GORA";
                    break;
                case 2: // s
                    if (KIERUNEKPOLICJANTA == "LEWO")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKPOLICJANTA == "GORA")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKPOLICJANTA == "PRAWO")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    KIERUNEKPOLICJANTA = "DOL";
                    break;
                case 3: // d
                    if (KIERUNEKPOLICJANTA == "LEWO")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKPOLICJANTA == "GORA")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKPOLICJANTA == "DOL")
                    {
                        POLICJANT.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        POLICJANT.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    KIERUNEKPOLICJANTA = "PRAWO";
                    break;
            }
        }

        private void paint(object sender, PaintEventArgs e)
        {
            if (POLICJANT.Visible == true)
            {
                e.Graphics.DrawImage(POLICJANT.Image, POLICJANT.Left, POLICJANT.Top, POLICJANT.Width, POLICJANT.Height);
            }
        }

    }
}
