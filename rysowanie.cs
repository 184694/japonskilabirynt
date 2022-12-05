using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaponskiLabirynt
{
    internal class rysowanie
    {

        private japonskilabirynt MAIN;
        labirynt.KIERUNEK[,] GRID;
        int[] USTAWIENIA;
        public rysowanie(labirynt.KIERUNEK[,] GRID, int[] USTAWIENIA, japonskilabirynt MAIN)
        {
            this.GRID = GRID;
            this.USTAWIENIA = USTAWIENIA;
            MAIN.Paint += new PaintEventHandler(paintlabirynt);
        }


        private static void rysujKomorke(labirynt.KIERUNEK[,] GRID, int KOLUMNA, int WIERSZ, Graphics GRAFIKA, int[] USTAWIENIA)
        {

            using (Pen SCIANA = new Pen(Color.Black))
            using (SolidBrush KOMORKA = new SolidBrush(Color.Green))
            {
                SCIANA.Width = USTAWIENIA[3];
                SCIANA.EndCap = LineCap.Round;
                SCIANA.StartCap = LineCap.Round;

                int OFFSETX = KOLUMNA * USTAWIENIA[2] + USTAWIENIA[3] / 2 + USTAWIENIA[3] + USTAWIENIA[0];
                int OFFSETY = WIERSZ * USTAWIENIA[2] + USTAWIENIA[3] / 2 + USTAWIENIA[3] + USTAWIENIA[1];

                if (GRID[WIERSZ, KOLUMNA].HasFlag(labirynt.KIERUNEK.N))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY), new Point(OFFSETX + USTAWIENIA[2], OFFSETY));

                if (GRID[WIERSZ, KOLUMNA].HasFlag(labirynt.KIERUNEK.S))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY + USTAWIENIA[2]), new Point(OFFSETX + USTAWIENIA[2], OFFSETY + USTAWIENIA[2]));
                
                if (GRID[WIERSZ, KOLUMNA].HasFlag(labirynt.KIERUNEK.E))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX + USTAWIENIA[2], OFFSETY), new Point(OFFSETX + USTAWIENIA[2], OFFSETY + USTAWIENIA[2]));

                if (GRID[WIERSZ, KOLUMNA].HasFlag(labirynt.KIERUNEK.W))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY), new Point(OFFSETX, OFFSETY + USTAWIENIA[2]));

            }
            Thread.Sleep(100);

        }

        private void paintlabirynt(object sender, PaintEventArgs e)
        {

            
            Graphics GRAFIKA = e.Graphics;

            for (int WIERSZ = 0; WIERSZ < japonskilabirynt.SZEROKOSC; WIERSZ++)
            {
                for (int KOLUMNA = 0; KOLUMNA < japonskilabirynt.WYSOKOSC; KOLUMNA++)
                {
                    rysujKomorke(GRID, WIERSZ, KOLUMNA, GRAFIKA, USTAWIENIA);

                }
            }
        }

    }
}
