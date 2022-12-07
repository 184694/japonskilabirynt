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
        public int OFFSETX, OFFSETY;
        public int[] USTAWIENIA;
        private japonskilabirynt MAIN;
        private PictureBox GRACZ;
        private Point POZYCJAGRACZA;
        static public Image GRACZIMG = Image.FromFile("../../../pliki/car.png");
        private int POLELABIRYNTUX, POLELABIRYNTUY;
        private int ZOOM = 4;

        public gracz(int[] USTAWIENIA, japonskilabirynt MAIN)
        {
            this.USTAWIENIA = USTAWIENIA;
            this.MAIN = MAIN;

            POZYCJAGRACZA = new Point(0, 0);

            GRACZ = new PictureBox();
            GRACZ.Image = GRACZIMG;
            GRACZ.Size = new Size(GRACZIMG.Width/(japonskilabirynt.WYSOKOSC), GRACZIMG.Height / (japonskilabirynt.SZEROKOSC));
            GRACZ.BackColor = Color.Transparent;
            
            POLELABIRYNTUX = 1024 - USTAWIENIA[0];
            POLELABIRYNTUY = 768;

            //    GDZIE ZACZYNA SIE   
            OFFSETX = USTAWIENIA[0] + POZYCJAGRACZA.X * USTAWIENIA[2] + (POLELABIRYNTUX / 2 - (USTAWIENIA[2] * japonskilabirynt.WYSOKOSC) / 2) + (GRACZ.Width / ZOOM) / 2;
            OFFSETY = USTAWIENIA[1] + POZYCJAGRACZA.Y * USTAWIENIA[2] + (POLELABIRYNTUY / 2 - (USTAWIENIA[2] * japonskilabirynt.SZEROKOSC) / 2) + USTAWIENIA[2] / 2 - (GRACZ.Height / ZOOM) / 2 - GRACZ.Height/2;
            GRACZ.Location = new Point(OFFSETX, OFFSETY);

            MAIN.Paint += new System.Windows.Forms.PaintEventHandler(paint);
        }

        private int liczgraczx()
        {
            OFFSETX = USTAWIENIA[0] + POZYCJAGRACZA.X * USTAWIENIA[2] + (POLELABIRYNTUX / 2 - (USTAWIENIA[2] * japonskilabirynt.WYSOKOSC) / 2) + (GRACZ.Width / ZOOM) / 2;
            return OFFSETX;
        }

        private int liczgraczy()
        {
            OFFSETY = USTAWIENIA[1] + POZYCJAGRACZA.Y * USTAWIENIA[2] + (POLELABIRYNTUY / 2 - (USTAWIENIA[2] * japonskilabirynt.SZEROKOSC) / 2) + USTAWIENIA[2] / 2 - (GRACZ.Height / ZOOM) / 2 - GRACZ.Height / 2;
            return OFFSETY;
        }

        public void orientacja(int kierunek)
        {
            switch (kierunek)
            {
                case 0:
                    GRACZ.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    GRACZ.Size = new Size(GRACZIMG.Width / (japonskilabirynt.WYSOKOSC), GRACZIMG.Height / (japonskilabirynt.SZEROKOSC));
                    break;
                case 1:
                    GRACZ.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    GRACZ.Size = new Size(GRACZIMG.Width / (japonskilabirynt.WYSOKOSC), GRACZIMG.Height / (japonskilabirynt.SZEROKOSC));
                    break;
                case 2:
                    GRACZ.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    GRACZ.Size = new Size(GRACZIMG.Width / (japonskilabirynt.WYSOKOSC), GRACZIMG.Height / (japonskilabirynt.SZEROKOSC));
                    break;
                case 3:
                    break;
            }

        }

        public void cofnijorientacje(int kierunek)
        {
            switch (kierunek)
            {
                case 0:
                    GRACZ.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    GRACZ.Size = new Size(GRACZIMG.Width / (japonskilabirynt.WYSOKOSC), GRACZIMG.Height / (japonskilabirynt.SZEROKOSC));
                    break;
                case 1:
                    GRACZ.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    GRACZ.Size = new Size(GRACZIMG.Width / (japonskilabirynt.WYSOKOSC), GRACZIMG.Height / (japonskilabirynt.SZEROKOSC));
                    break;
                case 2:
                    GRACZ.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    GRACZ.Size = new Size(GRACZIMG.Width / (japonskilabirynt.WYSOKOSC), GRACZIMG.Height / (japonskilabirynt.SZEROKOSC));
                    break;
                case 3:
                    break;
            }
        }

        public void ruch(int x, int y)
        {

            POZYCJAGRACZA.X += x;
            POZYCJAGRACZA.Y += y;
            
            GRACZ.Location = new Point(liczgraczx(), liczgraczy());
            MAIN.Invalidate();

        }

        private void paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(GRACZ.Image, GRACZ.Left, GRACZ.Top, GRACZ.Width, GRACZ.Height);
        }
    }
}
