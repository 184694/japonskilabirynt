using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JaponskiLabirynt
{
    internal class rysowanie
    {

        private japonskilabirynt MAIN;
        labirynt.KIERUNEK[,] GRID;
        int[] USTAWIENIA;
        static public Image x = Image.FromFile("../../../pliki/1.png");

        public rysowanie(labirynt.KIERUNEK[,] GRID, int[] USTAWIENIA, japonskilabirynt MAIN)
        {
            this.GRID = GRID;
            this.USTAWIENIA = USTAWIENIA;
            MAIN.Paint += new PaintEventHandler(paintlabirynt);
        }


        private static void rysujkomorke(labirynt.KIERUNEK[,] GRID, int KOLUMNA, int WIERSZ, Graphics GRAFIKA, int[] USTAWIENIA)
        {
            using (Pen SCIANA = new Pen(Color.Black))
            using (SolidBrush KOMORKA = new SolidBrush(Color.White))
            {
                SCIANA.Width = USTAWIENIA[3];
                SCIANA.EndCap = LineCap.Round;
                SCIANA.StartCap = LineCap.Round;

                int OFFSETX = KOLUMNA * USTAWIENIA[2] + USTAWIENIA[3] / 2 + USTAWIENIA[3] + USTAWIENIA[0];
                int OFFSETY = WIERSZ * USTAWIENIA[2] + USTAWIENIA[3] / 2 + USTAWIENIA[3] + USTAWIENIA[1];

                GRAFIKA.FillRectangle(KOMORKA, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                
                GRAFIKA.DrawImage(x, OFFSETX-1, OFFSETY-1, USTAWIENIA[2], USTAWIENIA[2]+5);

                if (GRID[KOLUMNA, WIERSZ].HasFlag(labirynt.KIERUNEK.N))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY), new Point(OFFSETX + USTAWIENIA[2], OFFSETY));

                if (GRID[KOLUMNA, WIERSZ].HasFlag(labirynt.KIERUNEK.S))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY + USTAWIENIA[2]), new Point(OFFSETX + USTAWIENIA[2], OFFSETY + USTAWIENIA[2]));
                
                if (GRID[KOLUMNA, WIERSZ].HasFlag(labirynt.KIERUNEK.E))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX + USTAWIENIA[2], OFFSETY), new Point(OFFSETX + USTAWIENIA[2], OFFSETY + USTAWIENIA[2]));
                
                if (GRID[KOLUMNA, WIERSZ].HasFlag(labirynt.KIERUNEK.W))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY), new Point(OFFSETX, OFFSETY + USTAWIENIA[2]));
                
            }
        }

        public void paintlabirynt(object sender, PaintEventArgs e)
        {

            
            Graphics GRAFIKA = e.Graphics;

            for (int WIERSZ = 0; WIERSZ < japonskilabirynt.SZEROKOSC; WIERSZ++)
            {
                for (int KOLUMNA = 0; KOLUMNA < japonskilabirynt.WYSOKOSC; KOLUMNA++)
                {
                    rysujkomorke(GRID, KOLUMNA, WIERSZ, GRAFIKA, USTAWIENIA);
           
                }
            }
        }

    }
}
