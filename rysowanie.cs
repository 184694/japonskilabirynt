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

        public labirynt.KIERUNEK[,] GRID;
        int[] USTAWIENIA;
        
        static public Image PROSTA = Image.FromFile("../../../pliki/1.png");
        static public Image ZAKRET = Image.FromFile("../../../pliki/2.png");
        static public Image SKRZYZOWANIE3 = Image.FromFile("../../../pliki/3.png");
        static public Image SKRZYZOWANIE4 = Image.FromFile("../../../pliki/5.png");
        static public Image DEADEND = Image.FromFile("../../../pliki/4.png");

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


                //int OFFSETX = KOLUMNA * USTAWIENIA[2] + USTAWIENIA[3] / 2 + USTAWIENIA[3] + USTAWIENIA[0];

                int POLELABIRYNTUX = 1024 - USTAWIENIA[0];
                int POLELABIRYNTUY = 768;
                
                //USTAWIANIE POZYCJI LABIRYNTU
                int OFFSETX = USTAWIENIA[0] + KOLUMNA * USTAWIENIA[2] + (POLELABIRYNTUX / 2 - (USTAWIENIA[2] * japonskilabirynt.WYSOKOSC)/2);
                int OFFSETY = USTAWIENIA[1] + WIERSZ * USTAWIENIA[2] + (POLELABIRYNTUY / 2 - (USTAWIENIA[2] * japonskilabirynt.SZEROKOSC) / 2);

                int ILOSCKIERUNKOW = ileflag((int)GRID[KOLUMNA, WIERSZ]);              
           
                //DEAD END
                if (ILOSCKIERUNKOW == 3)
                {
                    //DEAD END W GORE
                    if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.W) && warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.E))
                    {

                        if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.S))
                        {
                            DEADEND.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            GRAFIKA.DrawImage(DEADEND, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                            DEADEND.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        }
                        else GRAFIKA.DrawImage(DEADEND, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);


                    }
                    //DEAD END W BOK
                    else if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.N) && warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.S))
                    {
                        
                        if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.E))
                        {
                            DEADEND.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            DEADEND.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            GRAFIKA.DrawImage(DEADEND, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                            DEADEND.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            DEADEND.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        }
                        else
                        {
                            DEADEND.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            DEADEND.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                            GRAFIKA.DrawImage(DEADEND, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                            DEADEND.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                            DEADEND.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        }
                        
                    }
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
                    //SKRZYZOWANIE WYCHODZACE W DOL
                    if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.N))
                    {
                        GRAFIKA.DrawImage(SKRZYZOWANIE3, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                    }

                    //SKRZYZOWANIE WYCHODZACE W GORE
                    if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.S))
                    {
                        SKRZYZOWANIE3.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        GRAFIKA.DrawImage(SKRZYZOWANIE3, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                        SKRZYZOWANIE3.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    }
                    
                    //SKRZYZOWANIE WYCHODZACE W PRAWO
                    if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.W))
                    {
                        SKRZYZOWANIE3.RotateFlip(RotateFlipType.Rotate90FlipXY);
                        GRAFIKA.DrawImage(SKRZYZOWANIE3, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                        SKRZYZOWANIE3.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    }

                    //SKRZYZOWANIE WYCHODZACE W LEWO
                    if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.E))
                    {
                        SKRZYZOWANIE3.RotateFlip(RotateFlipType.Rotate90FlipXY);
                        SKRZYZOWANIE3.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        GRAFIKA.DrawImage(SKRZYZOWANIE3, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                        SKRZYZOWANIE3.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        SKRZYZOWANIE3.RotateFlip(RotateFlipType.Rotate270FlipXY);

                    }

                }
                else if (ILOSCKIERUNKOW == 0)
                {
                    GRAFIKA.DrawImage(SKRZYZOWANIE4, OFFSETX, OFFSETY, USTAWIENIA[2], USTAWIENIA[2]);
                }
         
                // WYSWIETLA ILE SCIAN MA ZABLOKOWANYCH DROGA
                Font FONT = new Font("Arial", 16);
                //GRAFIKA.DrawString(ILOSCKIERUNKOW.ToString(), FONT, KOMORKA, OFFSETX + USTAWIENIA[2]/2, OFFSETY + USTAWIENIA[2]/2-16);


                /* RYSOWANIE SCIAN LABIRYNTU
                if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.N))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY), new PointF(OFFSETX + USTAWIENIA[2], OFFSETY));

                if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.S))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY + USTAWIENIA[2]), new PointF(OFFSETX + USTAWIENIA[2], OFFSETY + USTAWIENIA[2]));

                if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.W))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX, OFFSETY), new PointF(OFFSETX, OFFSETY + USTAWIENIA[2]));

                if (warunekkierunku(GRID[KOLUMNA, WIERSZ], labirynt.KIERUNEK.E))
                    GRAFIKA.DrawLine(SCIANA, new PointF(OFFSETX + USTAWIENIA[2], OFFSETY), new PointF(OFFSETX + USTAWIENIA[2], OFFSETY + USTAWIENIA[2]));
                */
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
