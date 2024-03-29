using System.Diagnostics;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace JaponskiLabirynt
{
    public partial class japonskilabirynt : Form
    {
        /// <summary>
        /// Przesuniecie labiryntu o szerokosc menu
        /// </summary>
        public static int X = 311;

        public static int POLEOKNA_W = 1024;
        public static int POLEOKNA_H = 768;
        
        /// <summary>
        /// Pozycja lewego gornego rogu labiryntu
        /// </summary>
        public static int POLELABIRYNTUX = POLEOKNA_W - X;
        /// <summary>
        /// Pozycja lewego gornego rogu labiryntu
        /// </summary>
        public static int POLELABIRYNTUY = POLEOKNA_H;

        private string[] ciekawostki = System.IO.File.ReadAllLines("../../../pliki/ciekawostki.txt");
        private int numerciekawostki = 0;
        /// <summary>
        /// Ilosc zaladowanych ciekawostek
        /// </summary>
        private int iloscciekawostek;

        /// <summary>
        /// Zegarek gry do liczenia punktow, im szybciej gracz przejdzie poziom tym wiecej punktow dostanie
        /// </summary>
        Stopwatch stopwatch;
        private string NAZWAGRACZA = "GRACZ 1";
        private int ZYCIA = 3;
        private int POZIOM = 1;

        /// <summary>
        /// [WA�NE!] Okresla przedzial w ktorym bedzie generowany rozmiar losowego labirynt, jest to gorna granica
        /// </summary>
        private int ROZMIARLABIRYNTU = 4;

        /// <summary>
        /// Wysokosc labiryntu podana w komorkach
        /// </summary>
        private int WYSOKOSC = 2;

        /// <summary>
        /// Szerokosc labiryntu podana w komorkach
        /// </summary>
        private int SZEROKOSC = 2;

        private int PUNKTY = 0;

        /// <summary>
        /// Wzor na BONUS = 5000 - (int)stopwatch.Elapsed.TotalSeconds * 100;
        /// </summary>
        private int BONUS = 0;

        /// <summary>
        /// Zlicza wykonane ruchy gracza
        /// </summary>
        private int RUCHY = 0;

        /// <summary>
        /// Sprawdza czy rozgrywka jest w toku
        /// </summary>
        private bool GRA = false;

        private int ROZMIARKOMORKI;
        labirynt LABIRYNT;
        rysowanie RYSOWANIE;
        auto GRACZ;
        auto POLICJANT;

        public japonskilabirynt()
        {
            stopwatch = new Stopwatch();
            InitializeComponent();
            rozmiarkomorkilabiryntu();
            LABIRYNT = new labirynt(WYSOKOSC, SZEROKOSC);
            RYSOWANIE = new rysowanie(LABIRYNT.GRID, ROZMIARKOMORKI, WYSOKOSC, SZEROKOSC, this);
            GRACZ = new auto(ROZMIARKOMORKI, LABIRYNT.GRID, LABIRYNT.WJAZD, WYSOKOSC, SZEROKOSC, "GRACZ", this);

            POLICJANT = new auto(ROZMIARKOMORKI, LABIRYNT.GRID, LABIRYNT.WJAZD, WYSOKOSC, SZEROKOSC, "POLICJANT", this);
            iloscciekawostek = ciekawostki.Length;
        }

        /// <summary>
        /// Ustawia ROZMIARKOMORKI na taki, aby labirynt zmiescil sie w oknie
        /// </summary>
        public void rozmiarkomorkilabiryntu()
        {
            if (SZEROKOSC >= WYSOKOSC)
            {
                ROZMIARKOMORKI = (POLEOKNA_W - X) / SZEROKOSC;
            }
            else
            {
                ROZMIARKOMORKI = POLEOKNA_H / WYSOKOSC;
            }
        }

        /// <summary>
        /// Startuje gre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            if (textBox1.Text != "Podaj imie gracza..")
            {
                NAZWAGRACZA = textBox1.Text;
            }
            label2.Text = NAZWAGRACZA;
            label4.Text = "POZIOM: " + POZIOM;
            label5.Text = "PUNKTY: " + PUNKTY;

            GRACZ.AUTO.Visible = true;
            ukryjpokazgracza();

            startstopgra();
            button1.Visible = false;

            Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Poruszanie sie autem gracza + poruszanie sie policja + warunek wygranej + warunek przegranej
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void japonskilabirynt_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (GRA == true) {
                
                if (e.KeyChar == 'a' || e.KeyChar == 'A')
                {
                    //LEWO
                    GRACZ.orientacja(0);
                    //JE�ELI PO LEWEJ NIE MA POLICJANTA
                    if (POLICJANT.POZYCJAGRACZA.X == GRACZ.POZYCJAGRACZA.X - 1 && POLICJANT.POZYCJAGRACZA.Y == GRACZ.POZYCJAGRACZA.Y && POLICJANT.AUTO.Visible) ;
                    else
                        GRACZ.ruch(-1, 0);
                }
                else if (e.KeyChar == 'w' || e.KeyChar == 'W')
                {
                    GRACZ.orientacja(1);
                    //GORA
                    if (POLICJANT.POZYCJAGRACZA.Y == GRACZ.POZYCJAGRACZA.Y - 1 && POLICJANT.POZYCJAGRACZA.X == GRACZ.POZYCJAGRACZA.X && POLICJANT.AUTO.Visible) ;
                    else
                    GRACZ.ruch(0, -1);
                }
                else if (e.KeyChar == 's' || e.KeyChar == 'S')
                {
                    //DOL
                    GRACZ.orientacja(2);
                    if (POLICJANT.POZYCJAGRACZA.Y == GRACZ.POZYCJAGRACZA.Y + 1 && POLICJANT.POZYCJAGRACZA.X == GRACZ.POZYCJAGRACZA.X && POLICJANT.AUTO.Visible) ;
                    else    
                    GRACZ.ruch(0, 1);
                }
                else if (e.KeyChar == 'd' || e.KeyChar == 'D')
                {
                    //PRAWO
                    GRACZ.orientacja(3);
                    if (POLICJANT.POZYCJAGRACZA.X == GRACZ.POZYCJAGRACZA.X + 1 && POLICJANT.POZYCJAGRACZA.Y == GRACZ.POZYCJAGRACZA.Y && POLICJANT.AUTO.Visible) ;
                    else    
                    GRACZ.ruch(1, 0);
                }
                RUCHY++;

                if (GRACZ.ZWYCIESTWO == true)
                {
                    nastepnylabirynt();
                }

                if (RUCHY == 5)
                {
                    POLICJANT.AUTO.Visible = true;
                    Invalidate();
                }

                if (RUCHY > 5 && !GRACZ.ZWYCIESTWO)
                {
                    ruchpolicji();
                }

                if (POLICJANT.POZYCJAGRACZA.X == GRACZ.POZYCJAGRACZA.X && POLICJANT.POZYCJAGRACZA.Y == GRACZ.POZYCJAGRACZA.Y && POLICJANT.AUTO.Visible)
                {
                    MessageBox.Show("Zostales zlapany przez policje! Poziom zacznie si� od nowa.");
                    ZYCIA--;
                    if (ZYCIA == 2)
                    {
                        pictureBox2.Visible = false;
                        resetplanszy();
                    }
                    else if (ZYCIA == 1)
                    {
                        pictureBox3.Visible = false;
                        resetplanszy();
                    }
                    else if (ZYCIA == 0)
                    {
                        pictureBox4.Visible = false;
                        gameover();
                    }
                }

            }

        }

        /// <summary>
        /// Poruszanie sie policji po sladach gracza
        /// </summary>
        private void ruchpolicji()
        {
            if (GRACZ.RUCHY.Count != 0)
            {
                if (GRACZ.RUCHY[0] == labirynt.KIERUNEK.N)
                {
                    POLICJANT.orientacja(1);
                    POLICJANT.ruch(0, -1);
                }
                else if (GRACZ.RUCHY[0] == labirynt.KIERUNEK.S)
                {
                    POLICJANT.orientacja(2);
                    POLICJANT.ruch(0, 1);
                }
                else if (GRACZ.RUCHY[0] == labirynt.KIERUNEK.W)
                {
                    POLICJANT.orientacja(0);
                    POLICJANT.ruch(-1, 0);
                }
                else if (GRACZ.RUCHY[0] == labirynt.KIERUNEK.E)
                {
                    POLICJANT.orientacja(3);
                    POLICJANT.ruch(1, 0);
                }
                GRACZ.RUCHY.RemoveAt(0);
            }

            Invalidate();
        }

        /// <summary>
        /// Zatrzymuje czas i gre
        /// </summary>
        private void startstopgra()
        {
            if (GRA == true)
            {
                GRA = false;
                stopwatch.Stop();
            }
            else
            {
                GRA = true;
                stopwatch.Start();
            }
        }

        /// <summary>
        /// Resetuje pozycje gracza, policjanta, tworzy nowy labirynt
        /// </summary>
        private void resetplanszy()
        {
            
            Random rand = new Random();
            if (POZIOM % 3 == 0 && ROZMIARLABIRYNTU + 2 < LABIRYNT.MAXROZMIAR)
            {
                ROZMIARLABIRYNTU += 2;
            }
            
            SZEROKOSC = rand.Next(1, ROZMIARLABIRYNTU);
            WYSOKOSC = rand.Next(1, ROZMIARLABIRYNTU);

            rozmiarkomorkilabiryntu();
            RUCHY = 0;
            LABIRYNT.reset(WYSOKOSC, SZEROKOSC);
            RYSOWANIE.reset(WYSOKOSC, SZEROKOSC, ROZMIARKOMORKI);
            GRACZ.reset(LABIRYNT.WJAZD, WYSOKOSC, SZEROKOSC, ROZMIARKOMORKI);
            POLICJANT.reset(LABIRYNT.WJAZD, WYSOKOSC, SZEROKOSC, ROZMIARKOMORKI);
            POLICJANT.AUTO.Visible = false;
            Invalidate();
        }

        /// <summary>
        /// Przechodzi do nastepnego labiryntu i dodaje punkty graczowi, resetuje zegarek bonusu i ustawia nowe wartosci, koncowo pokazuje ciekawostke co 6 poziom i resetuje status skonczenia poziomu
        /// </summary>
        private void nastepnylabirynt()
        {
            resetplanszy();

            BONUS = 5000 - (int)stopwatch.Elapsed.TotalSeconds * 100;
            if (BONUS < 0)
                BONUS = 0;
            stopwatch.Restart();

            PUNKTY += BONUS;
           
            POZIOM += 1;
            label5.Text = "PUNKTY: " + PUNKTY;
            label4.Text = "POZIOM: " + POZIOM;
            if (POZIOM % 4 == 0 && numerciekawostki <= iloscciekawostek)
            {
                ciekawostka();
            }
            GRACZ.ZWYCIESTWO = false;
        }

        /// <summary>
        /// Jezeli gracz osiagnie 0 zyc konczy gre
        /// </summary>
        private void gameover()
        {
            startstopgra();
            label7.Text = "Koniec gry!, mo�esz rozpocz�� gr� ponownie od pierwszego poziomu klikaj�c poni�szy przycisk lub wyj�� z gry.";
            pictureBox5.Visible = true;
            label7.Visible = true;
            button3.Visible = true;
        }

        /// <summary>
        /// Wyswietla ciekawostki
        /// </summary>
        private void ciekawostka()
        {
            startstopgra();
            if (numerciekawostki >= iloscciekawostek)
            {
                label7.Text = "Ups.. Koniec ciekawostek, mo�esz kontynuowa� rozgrywk� jednak nie wy�wietli si� wi�cej ciekawostek. Powodzenia!";
                pictureBox5.Image = Image.FromFile("../../../pliki/c0.jpg");
            }
            else
                label7.Text = ciekawostki[numerciekawostki];
                pictureBox5.Image = Image.FromFile("../../../pliki/c" + numerciekawostki + ".jpg"); 
            pictureBox5.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            numerciekawostki++;
            button3.Visible = true;
        }

        /// <summary>
        /// Chowa ciekawostki i komunikat przegranej
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox5.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            button3.Visible = false;
            if (ZYCIA == 0)
            {
                ROZMIARLABIRYNTU = 3;
                restartgry();
            }
            startstopgra();
        }

        /// <summary>
        /// Resetuje gracza, poziom i plansze
        /// </summary>
        private void restartgry()
        {
            resetplanszy();
            stopwatch.Restart();
            PUNKTY = 0;
            POZIOM = 1;
            ROZMIARLABIRYNTU = 3;
            label5.Text = "PUNKTY: " + PUNKTY;
            label4.Text = "POZIOM: " + POZIOM;

            ZYCIA = 3;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
        }
        
        /// <summary>
        /// Chowa menu gracza
        /// </summary>
        private void ukryjpokazgracza()
        {
            if (label2.Visible == true)
            {
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
            }
            else
            {
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                if (ZYCIA == 3)
                {
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = true;
                    pictureBox4.Visible = true;
                }
                else if (ZYCIA == 2)
                {
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = true;
                }
                else if (ZYCIA == 1)
                {
                    pictureBox2.Visible = true;
                }
            }
        }

        #region
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}