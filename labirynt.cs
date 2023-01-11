using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JaponskiLabirynt.labirynt;

namespace JaponskiLabirynt
{

    public class labirynt
    {

        /// <summary>
        /// Okreslaja kierunek rozbicia sciany labiryntu lub w ktorym kierunku poruszylo sie auto
        /// </summary>
        [Flags]
        public enum KIERUNEK
        {
            N = 1 << 0,
            S = 1 << 1,
            W = 1 << 2,
            E = 1 << 3
        }

        /// <summary>
        /// Maksymalny rozmiar labiryntu
        /// </summary>
        public int MAXROZMIAR = 30;
        public int WYSOKOSC, SZEROKOSC;
        public int WJAZD;
        
        /// <summary>
        /// Tablica zawierajaca labirynt
        /// </summary>
        public KIERUNEK[,] GRID;
    
        /// <summary>
        /// Deklaracja nowego labiryntu
        /// </summary>
        /// <param name="WYSOKOSC">wys. labiryntu</param>
        /// <param name="SZEROKOSC">szer. labiryntu</param>
        public labirynt(int WYSOKOSC, int SZEROKOSC)
        {
            this.WYSOKOSC = WYSOKOSC;
            this.SZEROKOSC = SZEROKOSC;
   
            GRID = new KIERUNEK[MAXROZMIAR, MAXROZMIAR];
            reset(WYSOKOSC, SZEROKOSC);
        }

        /// <summary>
        /// Zapelnia labirynt scianami
        /// </summary>
        /// <param name="GRID"></param>
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

        /// <summary>
        /// "wykopuje" sciany zaczynajac od punktu (0,0) po czym przesuwa sie losowo wedlug podanych kierunkow, a nastepnie cofa sie aby sprawdzic
        /// czy nie zostala jakas komorka do ktorej nie da sie wejsc. Jest to Backtracking algorithm.
        /// </summary>
        /// <param name="GRID"></param>
        /// <param name="KOLUMNA"></param>
        /// <param name="WIERSZ"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Ustawianie scian
        /// </summary>
        public const KIERUNEK WSZYSTKIEKIERUNKI = KIERUNEK.E | KIERUNEK.S | KIERUNEK.W | KIERUNEK.N;

        /// <summary>
        /// Kierunki poruszania sie po tablicy w lewo i prawo
        /// </summary>
        readonly Dictionary<KIERUNEK, int> kierunkiX = new Dictionary<KIERUNEK, int>()
        {
            { KIERUNEK.E,  1},
            { KIERUNEK.N,  0},
            { KIERUNEK.W, -1},
            { KIERUNEK.S,  0}
        };

        /// <summary>
        /// Kierunki poruszania sie po tablicy w gore i w dol
        /// </summary>
        readonly Dictionary<KIERUNEK, int> kierunkiY = new Dictionary<KIERUNEK, int>()
        {
            { KIERUNEK.E,  0},
            { KIERUNEK.N,  -1},
            { KIERUNEK.W,  0},
            { KIERUNEK.S,  1}
        };

        /// <summary>
        /// Kierunki poruszania sie po tablicy xy na odwrót
        /// </summary>
        readonly Dictionary<KIERUNEK, KIERUNEK> naopak = new Dictionary<KIERUNEK, KIERUNEK>()
        {
            { KIERUNEK.E, KIERUNEK.W},
            { KIERUNEK.N, KIERUNEK.S},
            { KIERUNEK.W, KIERUNEK.E},
            { KIERUNEK.S, KIERUNEK.N}
        };

        /// <summary>
        /// Wypelnienie labiryntu wszystkimi scianami, wykopanie kretem, otworzenie wjazu i wyjazdu
        /// </summary>
        /// <param name="WYSOKOSC"></param>
        /// <param name="SZEROKOSC"></param>
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
