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

        public int MAXROZMIAR = 30;
        public int WYSOKOSC, SZEROKOSC;
        public int WJAZD;
        public KIERUNEK[,] GRID;
    
        public labirynt(int WYSOKOSC, int SZEROKOSC)
        {
            this.WYSOKOSC = WYSOKOSC;
            this.SZEROKOSC = SZEROKOSC;
   
            GRID = new KIERUNEK[MAXROZMIAR, MAXROZMIAR];
            reset(WYSOKOSC, SZEROKOSC);
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
                int NOWAKOLUMNA = KOLUMNA + kierunkiX[KIERUNEK];
                int NOWYWIERSZ = WIERSZ + kierunkiY[KIERUNEK];
                
                if (NOWAKOLUMNA <= SZEROKOSC - 1 
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

        public void reset(int WYSOKOSC, int SZEROKOSC)
        {
            this.WYSOKOSC = WYSOKOSC;
            this.SZEROKOSC = SZEROKOSC;

            wypelnianie(GRID);
            GRID = kret(GRID, 0, 0);
            WJAZD = random(0, WYSOKOSC);
            GRID[WJAZD, 0] &= ~KIERUNEK.W;

            switch (random(1, 4))
            {
                case 1:
                    GRID[0, SZEROKOSC - random(1,SZEROKOSC)] &= ~KIERUNEK.N;
                    break;
                case 2:
                    GRID[WYSOKOSC - random(1, WYSOKOSC), SZEROKOSC - 1] &= ~KIERUNEK.E;
                    break;
                case 3:
                    GRID[WYSOKOSC - 1, SZEROKOSC - random(1, SZEROKOSC)] &= ~KIERUNEK.S;
                    break;
            }
        }

        private int random(int min, int max)
        {
            Random RANDOM = new Random();
            int LICZBALOSOWA = RANDOM.Next(min, max);
            return LICZBALOSOWA;
        }
    }
}
