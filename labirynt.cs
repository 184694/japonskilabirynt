using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaponskiLabirynt
{

    internal class labirynt
    {
        
        public static int WYSOKOSC = 4;
        public static int SZEROKOSC = 4;

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
            KIERUNEK[,] GRID = new KIERUNEK[WYSOKOSC,SZEROKOSC];
            wypelnianie(GRID);
            

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




    }
}
