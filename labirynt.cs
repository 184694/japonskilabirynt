using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaponskiLabirynt
{

    internal class labirynt
    {
         
        public int WYSOKOSC, SZEROKOSC;

        [Flags]
        public enum KIERUNEK
        {
            BRAK = 1 << 0,
            N = 1 << 1,
            S = 1 << 2,
            W = 1 << 3,
            E = 1 << 4
        }
    

        public labirynt(int WYSOKOSC, int SZEROKOSC)
        {
            this.WYSOKOSC = WYSOKOSC;
            this.SZEROKOSC = SZEROKOSC;
            
            KIERUNEK[,] GRID = new KIERUNEK[WYSOKOSC,SZEROKOSC];
            wypelnianie(GRID);
            kret(GRID);

        }
        
        private void wypelnianie(KIERUNEK[,] GRID)
        {
            for (int i = 0; i < WYSOKOSC; i++)
            {
                for (int j = 0; j < SZEROKOSC; j++)
                {
                    GRID[i, j] = KIERUNEK.N | KIERUNEK.S | KIERUNEK.W | KIERUNEK.E;
                }
            }
        }

        private KIERUNEK[,] kret(KIERUNEK[,] GRID)
        {
            Random LOSOWA = new Random();
            string[] KIERUNKI = { "KIERUNEK.N", "KIERUNEK.S", "KIERUNEK.W", "KIERUNEK.E" };
            KIERUNKI = KIERUNKI.OrderBy(x => LOSOWA.Next()).ToArray();

            
            

            return GRID; 
        }

        readonly Dictionary<KIERUNEK, int> kierunkiX = new Dictionary<KIERUNEK, int>()
        {
            { KIERUNEK.N,  0},
            { KIERUNEK.S,  0},
            { KIERUNEK.W, -1},
            { KIERUNEK.E,  1}
        };

        readonly Dictionary<KIERUNEK, int> kierunkiY = new Dictionary<KIERUNEK, int>()
        {
            { KIERUNEK.N, -1},
            { KIERUNEK.S,  1},
            { KIERUNEK.W,  0},
            { KIERUNEK.E,  0}
        };

        readonly Dictionary<KIERUNEK, KIERUNEK> naopak = new Dictionary<KIERUNEK, KIERUNEK>()
        {
            { KIERUNEK.N, KIERUNEK.S},
            { KIERUNEK.S, KIERUNEK.N},
            { KIERUNEK.W, KIERUNEK.E},
            { KIERUNEK.E, KIERUNEK.W}
        };


    }
}
