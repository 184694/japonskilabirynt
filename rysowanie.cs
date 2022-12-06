using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JaponskiLabirynt
{
    internal class rysowanie
    {

        Font drawFont = new Font("Arial", 16);
        private japonskilabirynt MAIN;
        public labirynt.KIERUNEK[,] GRID;
        int[] USTAWIENIA;
        public PictureBox DROGA;
        public Image PROSTA = Image.FromFile("../../../pliki/1.png");

        static public Image ZAKRET = Image.FromFile("../../../pliki/2.png");
        static public Image SKRZYZOWANIE = Image.FromFile("../../../pliki/3.png");

        public rysowanie(labirynt.KIERUNEK[,] GRID, int[] USTAWIENIA, japonskilabirynt MAIN)
        {
            this.GRID = GRID;
            this.USTAWIENIA = USTAWIENIA;
            MAIN.Paint += new PaintEventHandler(paintlabirynt);
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


        private void rysujkomorke(labirynt.KIERUNEK[,] GRID, int KOLUMNA, int WIERSZ, Graphics GRAFIKA, int[] USTAWIENIA)
        {

            using (Pen SCIANA = new Pen(Color.Black))
            using (SolidBrush KOMORKA = new SolidBrush(Color.White))
            {

                SCIANA.Width = USTAWIENIA[3];
                SCIANA.EndCap = LineCap.Round;
                SCIANA.StartCap = LineCap.Round;

                int OFFSETX = KOLUMNA * USTAWIENIA[2] + USTAWIENIA[3] / 2 + USTAWIENIA[3] + USTAWIENIA[0];
                int OFFSETY = WIERSZ * USTAWIENIA[2] + USTAWIENIA[3] / 2 + USTAWIENIA[3] + USTAWIENIA[1];
                //GRAFIKA.FillRectangle(KOMORKA, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);


                string ORIENTACJA = Convert.ToString((int)GRID[KOLUMNA, WIERSZ], 2).PadLeft(4, '0');

                int ILOSCKIERUNKOW = ileflag((int)GRID[KOLUMNA, WIERSZ]);

                GRAFIKA.DrawString(ILOSCKIERUNKOW.ToString(), drawFont, KOMORKA, OFFSETX, OFFSETY);


                //DEAD END
                if (ILOSCKIERUNKOW == 3)
                {

                }
                
                //PROSTA I ZAKRET
                else if (ILOSCKIERUNKOW == 2){
                   
                    //PROSTA W GORE
                    if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.W) && warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.E))
                    {
                        GRAFIKA.DrawImage(PROSTA, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                    }
                    //PROSTA W BOK
                    else if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.N) && warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.S))
                    {
                        PROSTA.RotateFlip(RotateFlipType.Rotate90FlipXY);
                        GRAFIKA.DrawImage(PROSTA, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                        PROSTA.RotateFlip(RotateFlipType.Rotate270FlipXY);

                    }

                    //ZAKRET GORA LEWO
                    if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.S) && warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.E))
                    {
                        GRAFIKA.DrawImage(ZAKRET, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                    }

                    //ZAKRET GORA PRAWO
                    if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.S) && warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.W))
                    {
                        ZAKRET.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        GRAFIKA.DrawImage(ZAKRET, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                        ZAKRET.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }

                    //ZAKRET DOL LEWO
                    if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.N) && warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.E))
                    {
                        ZAKRET.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        GRAFIKA.DrawImage(ZAKRET, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                        ZAKRET.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    }

                    //ZAKRET DOL PRAWO
                    if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.N) && warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.W))
                    {
                        ZAKRET.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        ZAKRET.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        GRAFIKA.DrawImage(ZAKRET, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                        ZAKRET.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        ZAKRET.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    }

                }
                //SKRZYZOWANIE
                else if (ILOSCKIERUNKOW == 1)
                {

                }

                if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.N))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY), new PointF(OFFSETX + USTAWIENIA[2], OFFSETY));

                if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.S))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY + USTAWIENIA[2]), new PointF(OFFSETX + USTAWIENIA[2], OFFSETY + USTAWIENIA[2]));

                if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.W))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY), new PointF(OFFSETX, OFFSETY + USTAWIENIA[2]));

                if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.E))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX + USTAWIENIA[2], OFFSETY), new PointF(OFFSETX + USTAWIENIA[2], OFFSETY + USTAWIENIA[2]));

            }
        }


        public static int ileflag(long KOMORKA)
        {
            int LICZNIK = 0;

            while (KOMORKA != 0)
            {
                KOMORKA = KOMORKA & (KOMORKA - 1);
                LICZNIK++;
            }

            return LICZNIK;
        }

        private static bool warunekkierunku(labirynt.KIERUNEK GRID, labirynt.KIERUNEK KIERUNEK)
        {
            if (GRID.HasFlag(KIERUNEK))
            {
                return true;
            }
            else return false;
        }

    }
}
