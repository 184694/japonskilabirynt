using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JaponskiLabirynt.labirynt;

namespace JaponskiLabirynt
{

    internal class labirynt
    {

        [Flags]
        public enum KIERUNEK
        {
            N = 1 << 0,
            S = 1 << 1,
            W = 1 << 2,
            E = 1 << 3
        }
   
        public int WYSOKOSC, SZEROKOSC;
        public KIERUNEK[,] GRID;
    
        public labirynt(int WYSOKOSC, int SZEROKOSC)
        {
            this.WYSOKOSC = WYSOKOSC;
            this.SZEROKOSC = SZEROKOSC;
   
            GRID = new KIERUNEK[WYSOKOSC, SZEROKOSC];
            wypelnianie(GRID);
            Point START = znajdzstart(GRID);
            Point KONIEC = znajdzkoniec(GRID);
            
            GRID = kret(GRID, START.X, START.Y);
            
            GRID[START.Y, START.X] &= ~KIERUNEK.W;
            GRID[KONIEC.Y, KONIEC.X] &= ~KIERUNEK.E;

        }

        public void wypelnianie(KIERUNEK[,] GRID)
        {
            for (int i = 0; i < WYSOKOSC; i++)
            {
                for (int j = 0; j < SZEROKOSC; j++)
                {
                        GRID[i, j] = WSZYSTKIEKIERUNKI;
                }
            }

        }

        public KIERUNEK[,] kret(KIERUNEK[,] GRID, int KOLUMNA, int WIERSZ)
        {
            Random LOSOWA = new Random();
            KIERUNEK[] KIERUNKI = { KIERUNEK.N, KIERUNEK.N, KIERUNEK.S, KIERUNEK.W, KIERUNEK.W, KIERUNEK.W, KIERUNEK.E, KIERUNEK.E };
            KIERUNKI = KIERUNKI.OrderBy(x => LOSOWA.Next()).ToArray();

            foreach (KIERUNEK KIERUNEK in KIERUNKI)
            {
                int NOWAKOLUMNA = KOLUMNA + kierunkiY[KIERUNEK];
                int NOWYWIERSZ = WIERSZ + kierunkiX[KIERUNEK];
                
                if (NOWAKOLUMNA <= japonskilabirynt.SZEROKOSC - 1 
                    && NOWAKOLUMNA >= 0 && NOWYWIERSZ <= WYSOKOSC - 1 
                    && NOWYWIERSZ >= 0 
                    && GRID[NOWYWIERSZ, NOWAKOLUMNA] == WSZYSTKIEKIERUNKI)
                {
                    GRID[WIERSZ, KOLUMNA] &= ~KIERUNEK;
                    GRID[NOWYWIERSZ, NOWAKOLUMNA] &= ~naopak[KIERUNEK];

                    GRID = kret(GRID, NOWAKOLUMNA, NOWYWIERSZ);

                }
            }
            return GRID;
        }

        public const KIERUNEK WSZYSTKIEKIERUNKI = KIERUNEK.E | KIERUNEK.S | KIERUNEK.W | KIERUNEK.N;
        readonly Dictionary<KIERUNEK, int> kierunkiX = new Dictionary<KIERUNEK, int>()
        {
            { KIERUNEK.E,  1},
            { KIERUNEK.N,  0},
            { KIERUNEK.W, -1},
            { KIERUNEK.S,  0}
        };

        readonly Dictionary<KIERUNEK, int> kierunkiY = new Dictionary<KIERUNEK, int>()
        {
            { KIERUNEK.E,  0},
            { KIERUNEK.N,  -1},
            { KIERUNEK.W,  0},
            { KIERUNEK.S,  1}
        };

        readonly Dictionary<KIERUNEK, KIERUNEK> naopak = new Dictionary<KIERUNEK, KIERUNEK>()
        {
            { KIERUNEK.E, KIERUNEK.W},
            { KIERUNEK.N, KIERUNEK.S},
            { KIERUNEK.W, KIERUNEK.E},
            { KIERUNEK.S, KIERUNEK.N}
        };

        private Point znajdzstart(KIERUNEK[,] GRID)
        {
            for (int i = 0; i < WYSOKOSC; i++)
            {
                for (int j = 0; j < SZEROKOSC; j++)
                {
                    if (GRID[i, j].HasFlag(WSZYSTKIEKIERUNKI))
                        return new Point(i, j);
                }
            }

            return new Point(0, 0);
        }
        
        private Point znajdzkoniec(KIERUNEK[,] GRID)
        {
            for (int i = WYSOKOSC - 1; i > 0; i--)
            {
                for (int j = SZEROKOSC - 1; j > 0; j--)
                {
                    if (GRID[i, j].HasFlag(WSZYSTKIEKIERUNKI))
                        return new Point(i, j);
                }
            }

            return new Point(0, 0);
        }

        public void restart()
        {
            wypelnianie(GRID);
            Point START = znajdzstart(GRID);
            Point KONIEC = znajdzkoniec(GRID);

            GRID = kret(GRID, START.X, START.Y);

            GRID[START.Y, START.X] &= ~KIERUNEK.W;
            GRID[KONIEC.Y, KONIEC.X] &= ~KIERUNEK.E;
        }
    }
}
