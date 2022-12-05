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
            BRAK = 1 << 0,
            N = 1 << 1,
            S = 1 << 2,
            W = 1 << 3,
            E = 1 << 4
        }
   
        public int WYSOKOSC, SZEROKOSC;
        public KIERUNEK[,] GRID;
    
        public labirynt(int WYSOKOSC, int SZEROKOSC)
        {
            this.WYSOKOSC = WYSOKOSC;
            this.SZEROKOSC = SZEROKOSC;
   
            GRID = new KIERUNEK[japonskilabirynt.WYSOKOSC, japonskilabirynt.SZEROKOSC];
            wypelnianie(GRID);

            GRID = kret(GRID, 1, 1);
        }
        
        private void wypelnianie(KIERUNEK[,] GRID)
        {
            for (int i = 0; i < japonskilabirynt.WYSOKOSC; i++)
            {
                for (int j = 0; j < japonskilabirynt.SZEROKOSC; j++)
                {
                        GRID[i, j] = WSZYSTKIEKIERUNKI;
                }
            }

        }

        public KIERUNEK[,] kret(KIERUNEK[,] GRID, int KOLUMNA, int WIERSZ)
        {
            Random LOSOWA = new Random();
            KIERUNEK[] KIERUNKI = { KIERUNEK.N, KIERUNEK.S, KIERUNEK.W, KIERUNEK.E };
            KIERUNKI = KIERUNKI.OrderBy(x => LOSOWA.Next()).ToArray();

            foreach (KIERUNEK KIERUNEK in KIERUNKI)
            {
                int NOWAKOLUMNA = KOLUMNA + kierunkiY[KIERUNEK];
                int NOWYWIERSZ = WIERSZ + kierunkiX[KIERUNEK];
                
                if (NOWAKOLUMNA <= japonskilabirynt.SZEROKOSC - 1 && NOWAKOLUMNA >= 0 && NOWYWIERSZ <= japonskilabirynt.WYSOKOSC - 1 && NOWYWIERSZ >= 0 && GRID[NOWYWIERSZ, NOWAKOLUMNA] == WSZYSTKIEKIERUNKI)
                {
                    GRID[WIERSZ, KOLUMNA] &= ~KIERUNEK;
                    GRID[NOWYWIERSZ, NOWAKOLUMNA] &= ~naopak[KIERUNEK];

                    GRID = kret(GRID, NOWAKOLUMNA, NOWYWIERSZ);

                }
            }
            return GRID;
        }

        private const KIERUNEK WSZYSTKIEKIERUNKI = KIERUNEK.E | KIERUNEK.S | KIERUNEK.W | KIERUNEK.N;
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


    }
}
