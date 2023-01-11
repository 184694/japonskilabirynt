using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JaponskiLabirynt.labirynt;

namespace JaponskiLabirynt
{

    public class auto
    {
        /// <summary>
        /// Ruchy auta
        /// </summary>
        public List<KIERUNEK> RUCHY = new List<KIERUNEK>();

        /// <summary>
        /// Uzywane tylko dla gracza, czy zwyciezyl
        /// </summary>
        public bool ZWYCIESTWO = false;

        /// <summary>
        /// Pozycja XY auta
        /// </summary>
        public int OFFSETX, OFFSETY;

        /// <summary>
        /// Potrzebne do policzenia pozycji auta
        /// </summary>
        public int ROZMIARKOMORKI;

        /// <summary>
        /// Informacje o scianach labiryntu
        /// </summary>
        private KIERUNEK[,] GRID;

        private japonskilabirynt MAIN;
        public PictureBox AUTO;
        public Point POZYCJAGRACZA;

        /// <summary>
        /// Okresla w ktora strone obrocony jest gracz
        /// </summary>
        private string KIERUNEKGRACZA;

        /// <summary>
        /// Wymiary labiryntu
        /// </summary>
        private int WYSOKOSC, SZEROKOSC;
        static public Image GRACZIMG = Image.FromFile("../../../pliki/car.png");
        static public Image POLICJANTIMG = Image.FromFile("../../../pliki/police.png");

        /// <summary>
        /// Poruszanie sie aut, obrot aut, reset pozycji aut
        /// </summary>
        /// <param name="ROZMIARKOMORKI">Rozmiar komorki labiryntu</param>
        /// <param name="GRID">Tablica z labiryntem</param>
        /// <param name="WJAZD">Wjazd do labiryntu</param>
        /// <param name="WYSOKOSC">Wysokosc labiryntu</param>
        /// <param name="SZEROKOSC">Szerokosc labiryntu</param>
        /// <param name="WYBORPOJAZDU">Wybor pojazdu (gracz,policjant)</param>
        /// <param name="MAIN"></param>
        public auto(int ROZMIARKOMORKI, KIERUNEK[,] GRID, int WJAZD, int WYSOKOSC, int SZEROKOSC, string WYBORPOJAZDU, japonskilabirynt MAIN)
        {
            this.ROZMIARKOMORKI = ROZMIARKOMORKI;
            this.MAIN = MAIN;
            this.GRID = GRID;
            this.SZEROKOSC = SZEROKOSC;
            this.WYSOKOSC = WYSOKOSC;
            POZYCJAGRACZA = new Point(0, WJAZD);

            AUTO = new PictureBox();
            AUTO.SizeMode = PictureBoxSizeMode.Zoom;
            AUTO.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
            
            if (WYBORPOJAZDU == "GRACZ")
            {
                AUTO.Image = GRACZIMG;
            }
            else
            {
                AUTO.Image = POLICJANTIMG;
            }
            AUTO.Visible = false;

            KIERUNEKGRACZA = "PRAWO";
            AUTO.BackColor = Color.Transparent;
            AUTO.Location = new Point(liczgraczx(), liczgraczy());

            MAIN.Paint += new System.Windows.Forms.PaintEventHandler(paint);
        }

        /// <summary>
        /// Pozycja gracza Y na ekranie
        /// </summary>
        /// <returns></returns>
        private int liczgraczx()
        {
            if (KIERUNEKGRACZA == "GORA" || KIERUNEKGRACZA == "DOL")
            {
                OFFSETX = japonskilabirynt.X + POZYCJAGRACZA.X * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUX - SZEROKOSC * ROZMIARKOMORKI) / 2 + AUTO.Width;
            }
            else
                OFFSETX = japonskilabirynt.X + POZYCJAGRACZA.X * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUX - SZEROKOSC * ROZMIARKOMORKI) / 2 + AUTO.Width / 2;

            return OFFSETX;
        }

        /// <summary>
        /// Pozycja auta Y na ekranie
        /// </summary>
        /// <returns></returns>
        private int liczgraczy()
        {
            //OFFSETY = USTAWIENIA[1] + POZYCJAGRACZA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY / 2 - (ROZMIARKOMORKI * SZEROKOSC) / 2) + ROZMIARKOMORKI / 2 - GRACZ.Height/ 2 - GRACZ.Height / 2;

            if (KIERUNEKGRACZA == "GORA" || KIERUNEKGRACZA == "DOL")
            {
                OFFSETY = POZYCJAGRACZA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY - WYSOKOSC * ROZMIARKOMORKI) / 2 + AUTO.Height / 2;
            }
            else
                OFFSETY = POZYCJAGRACZA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY - WYSOKOSC * ROZMIARKOMORKI) / 2 + AUTO.Height;
            return OFFSETY;
        }

        /// <summary>
        /// Obracanie autem zaleznie od ostatniego ruchu
        /// </summary>
        /// <param name="kierunek"></param>
        public void orientacja(int kierunek)
        {
            switch (kierunek)
            {
                case 0: // a
                    if (KIERUNEKGRACZA == "PRAWO")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKGRACZA == "GORA")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKGRACZA == "DOL")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    KIERUNEKGRACZA = "LEWO";
                    break;
                case 1: // w
                    if (KIERUNEKGRACZA == "LEWO")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKGRACZA == "PRAWO")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKGRACZA == "DOL")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    KIERUNEKGRACZA = "GORA";
                    break;
                case 2: // s
                    if (KIERUNEKGRACZA == "LEWO")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKGRACZA == "GORA")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    else if (KIERUNEKGRACZA == "PRAWO")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 3, ROZMIARKOMORKI / 2);
                    }
                    KIERUNEKGRACZA = "DOL";
                    break;
                case 3: // d
                    if (KIERUNEKGRACZA == "LEWO")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKGRACZA == "GORA")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    else if (KIERUNEKGRACZA == "DOL")
                    {
                        AUTO.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        AUTO.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
                    }
                    KIERUNEKGRACZA = "PRAWO";
                    break;
            }
        }
    
        /// <summary>
        /// Warunek wygranej, poruszanie autem - sprawdza czy jest sciana, jezeli nie to wykonuje ruch
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ruch(int x, int y)
        {

                if (x == 1 && !GRID[POZYCJAGRACZA.Y, POZYCJAGRACZA.X].HasFlag(KIERUNEK.E))
                {
                    if (POZYCJAGRACZA.X + x == SZEROKOSC)
                    {
                        ZWYCIESTWO = true;
                    }
                    else
                    {
                        RUCHY.Add(KIERUNEK.E);
                        POZYCJAGRACZA.X += x;
                    }
                }
                else if (x == -1 && !GRID[POZYCJAGRACZA.Y, POZYCJAGRACZA.X].HasFlag(KIERUNEK.W))
                {
                    //zeby nie wyjsc przez start poza tablice
                    if (POZYCJAGRACZA.X != 0)
                    {
                        RUCHY.Add(KIERUNEK.W);
                        POZYCJAGRACZA.X += x;
                    }
                }
                else if (y == 1 && !GRID[POZYCJAGRACZA.Y, POZYCJAGRACZA.X].HasFlag(KIERUNEK.S))
                {
                    if (POZYCJAGRACZA.Y + y == WYSOKOSC)
                    {
                        ZWYCIESTWO = true;
                    }
                    else
                    {
                        RUCHY.Add(KIERUNEK.S);
                        POZYCJAGRACZA.Y += y;
                    }

                }
                else if (y == -1 && !GRID[POZYCJAGRACZA.Y, POZYCJAGRACZA.X].HasFlag(KIERUNEK.N))
                {
                    if (POZYCJAGRACZA.Y + y == -1)
                    {
                        ZWYCIESTWO = true;
                    }
                    else
                    {
                        RUCHY.Add(KIERUNEK.N);
                        POZYCJAGRACZA.Y += y;
                    }
                }

            AUTO.Location = new Point(liczgraczx(), liczgraczy());
            MAIN.Invalidate();

        }

        /// <summary>
        /// Resetuje pozycje, orientacje, rozmiar auta
        /// </summary>
        /// <param name="WJAZD">Wjazd do labiryntu</param>
        /// <param name="WYSOKOSC">Wysokosc labiryntu</param>
        /// <param name="SZEROKOSC">Szerokosc labiryntu</param>
        /// <param name="ROZMIARKOMORKI">Rozmiar komorki labiryntu</param>
        public void reset(int WJAZD, int WYSOKOSC, int SZEROKOSC, int ROZMIARKOMORKI)
        {
            this.WYSOKOSC = WYSOKOSC;
            this.SZEROKOSC = SZEROKOSC;
            this.ROZMIARKOMORKI = ROZMIARKOMORKI;
            RUCHY.Clear();
            POZYCJAGRACZA.X = 0;
            POZYCJAGRACZA.Y = WJAZD;
            AUTO.Size = new Size(ROZMIARKOMORKI / 2, ROZMIARKOMORKI / 3);
            OFFSETY = POZYCJAGRACZA.Y * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUY - WYSOKOSC * ROZMIARKOMORKI) / 2 + AUTO.Height;
            OFFSETX = japonskilabirynt.X + POZYCJAGRACZA.X * ROZMIARKOMORKI + (japonskilabirynt.POLELABIRYNTUX - SZEROKOSC * ROZMIARKOMORKI) / 2 + AUTO.Width / 2;
            AUTO.Location = new Point(OFFSETX, OFFSETY);
            orientacja(3);
            MAIN.Invalidate();
        }

        private void paint(object sender, PaintEventArgs e)
        {
            if (AUTO.Visible == true)
                e.Graphics.DrawImage(AUTO.Image, AUTO.Left, AUTO.Top, AUTO.Width, AUTO.Height);
        }
    }
}
