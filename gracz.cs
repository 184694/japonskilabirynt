using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JaponskiLabirynt.labirynt;

namespace JaponskiLabirynt
{

    internal class gracz
    {
        public bool ZWYCIESTWO = false;
        public int OFFSETX, OFFSETY;
        public int ROZMIARKOMORKI;
        private KIERUNEK[,] GRID;
        private japonskilabirynt MAIN;
        private PictureBox GRACZ;
        private Point POZYCJAGRACZA;
        private string KIERUNEKGRACZA;
        private int WYSOKOSC, SZEROKOSC;
        static public Image GRACZIMG = Image.FromFile("../../../pliki/car.png");

        public gracz(int ROZMIARKOMORKI, KIERUNEK[,] GRID, int WYSOKOSC, int SZEROKOSC, japonskilabirynt MAIN)
        {
            this.ROZMIARKOMORKI = ROZMIARKOMORKI;
            this.MAIN = MAIN;
            this.GRID = GRID;
            this.SZEROKOSC = SZEROKOSC;
            this.WYSOKOSC = WYSOKOSC;
            POZYCJAGRACZA = new Point(0, 0);

            GRACZ = new PictureBox();
            GRACZ.SizeMode = PictureBoxSizeMode.Zoom;
            GRACZ.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
            GRACZ.Image = GRACZIMG;


            KIERUNEKGRACZA = "PRAWO";
            GRACZ.BackColor = Color.Transparent;

            //         [PRZES. MENU]     + [KOORD. GRACZ.] * [PRZESKOK CO KOM.] + [WYRÓWNANIE LABIRYNTU NA ŚRODEK W POZIOMIE]   
            OFFSETX = japonskilabirynt.X + POZYCJAGRACZA.X * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUX - SZEROKOSC * ROZMIARKOMORKI) / 2 + GRACZ.Width / 2;

            //        [KOORD. GRACZ.] * [PRZESKOK CO KOM.] + [WYRÓWNANIE LABIRYNTU NA ŚRODEK W PIONIE]                                    + PRZESUNIECIE NA SRODEK KOMORKI
            OFFSETY = POZYCJAGRACZA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY - WYSOKOSC * ROZMIARKOMORKI) / 2 + GRACZ.Height;
            GRACZ.Location = new Point(OFFSETX, OFFSETY);

            MAIN.Paint += new System.Windows.Forms.PaintEventHandler(paint);
        }

        private int liczgraczx()
        {
            if (KIERUNEKGRACZA == "GORA" || KIERUNEKGRACZA == "DOL")
            {
                OFFSETX = japonskilabirynt.X + POZYCJAGRACZA.X * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUX - SZEROKOSC * ROZMIARKOMORKI) / 2 + GRACZ.Width;
            }
            else
                OFFSETX = japonskilabirynt.X + POZYCJAGRACZA.X * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUX - SZEROKOSC * ROZMIARKOMORKI) / 2 + GRACZ.Width / 2;

            return OFFSETX;
        }

        private int liczgraczy()
        {
            //OFFSETY = USTAWIENIA[1] + POZYCJAGRACZA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY / 2 - (ROZMIARKOMORKI * SZEROKOSC) / 2) + ROZMIARKOMORKI / 2 - GRACZ.Height/ 2 - GRACZ.Height / 2;

            if (KIERUNEKGRACZA == "GORA" || KIERUNEKGRACZA == "DOL")
            {
                OFFSETY = POZYCJAGRACZA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY - WYSOKOSC * ROZMIARKOMORKI) / 2 + GRACZ.Height / 2;
            }
            else
                OFFSETY = POZYCJAGRACZA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY - WYSOKOSC * ROZMIARKOMORKI) / 2 + GRACZ.Height;
            return OFFSETY;
        }

        public void orientacja(int kierunek)
        {
            switch (kierunek)
            {
                case 0: // a
                    if (KIERUNEKGRACZA == "PRAWO")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKGRACZA == "GORA")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKGRACZA == "DOL")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    KIERUNEKGRACZA = "LEWO";
                    break;
                case 1: // w
                    if (KIERUNEKGRACZA == "LEWO")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKGRACZA == "PRAWO")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKGRACZA == "DOL")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    KIERUNEKGRACZA = "GORA";
                    break;
                case 2: // s
                    if (KIERUNEKGRACZA == "LEWO")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKGRACZA == "GORA")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKGRACZA == "PRAWO")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    KIERUNEKGRACZA = "DOL";
                    break;
                case 3: // d
                    if (KIERUNEKGRACZA == "LEWO")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKGRACZA == "GORA")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKGRACZA == "DOL")
                    {
                        GRACZ.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        GRACZ.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    KIERUNEKGRACZA = "PRAWO";
                    break;
            }
        }

        public void ruch(int x, int y)
        {
            if (POZYCJAGRACZA.X != SZEROKOSC)
            {
                if (x == 1 && !GRID[POZYCJAGRACZA.Y, POZYCJAGRACZA.X].HasFlag(KIERUNEK.E))
                {
                    POZYCJAGRACZA.X += x;
                }
                else if (x == -1 && !GRID[POZYCJAGRACZA.Y, POZYCJAGRACZA.X].HasFlag(KIERUNEK.W))
                {
                    //zeby nie wyjsc przez start poza tablice
                    if (POZYCJAGRACZA.X != 0)
                    {
                        POZYCJAGRACZA.X += x;
                    }
                }
                else if (y == 1 && !GRID[POZYCJAGRACZA.Y, POZYCJAGRACZA.X].HasFlag(KIERUNEK.S))
                {
                    POZYCJAGRACZA.Y += y;
                }
                else if (y == -1 && !GRID[POZYCJAGRACZA.Y, POZYCJAGRACZA.X].HasFlag(KIERUNEK.N))
                {
                    POZYCJAGRACZA.Y += y;
                }
            }
            else
            {
                ZWYCIESTWO = true;
                POZYCJAGRACZA.X = 0;
                POZYCJAGRACZA.Y = 0;
            }

            GRACZ.Location = new Point(liczgraczx(), liczgraczy());
            MAIN.Invalidate();

        }

        public void reset(int WYSOKOSC, int SZEROKOSC, int ROZMIARKOMORKI)
        {
            this.WYSOKOSC = WYSOKOSC;
            this.SZEROKOSC = SZEROKOSC;
            this.ROZMIARKOMORKI = ROZMIARKOMORKI;
            POZYCJAGRACZA.X = 0;
            POZYCJAGRACZA.Y = 0;
            GRACZ.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
            OFFSETY = POZYCJAGRACZA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY - WYSOKOSC * ROZMIARKOMORKI) / 2 + GRACZ.Height;
            OFFSETX = japonskilabirynt.X + POZYCJAGRACZA.X * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUX - SZEROKOSC * ROZMIARKOMORKI) / 2 + GRACZ.Width / 2;
            GRACZ.Location = new Point(OFFSETX, OFFSETY);
            orientacja(3);
            MAIN.Invalidate();
        }

        private void paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(GRACZ.Image, GRACZ.Left, GRACZ.Top, GRACZ.Width, GRACZ.Height);
        }
    }
}
