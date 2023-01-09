using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace JaponskiLabirynt
{
    public partial class japonskilabirynt : Form
    {
        //POZYCJA LABIRYNTU [311 BO TO SZEROKOSC MENU]
        public static int X = 311;

        public static int POLEOKNA_W = 1024;
        public static int POLEOKNA_H = 768;
        public static int POLELABIRYNTUX = POLEOKNA_W - X;
        public static int POLELABIRYNTUY = POLEOKNA_H;

        //ROZMIAR LABIRYNTU W KOMORKACH
        private int WYSOKOSC = 5;
        private int SZEROKOSC = 5;

        private string[] ciekawostki = System.IO.File.ReadAllLines("../../../pliki/ciekawostki.txt");
        private int numerciekawostki = 0;
        private int iloscciekawostek;
        private string NAZWAGRACZA = "GRACZ 1";
        private int ZYCIA = 3;
        private int POZIOM = 1;
        private int PUNKTY = 0;
        private bool GRA = false;

        private int ROZMIARKOMORKI;
        labirynt LABIRYNT;
        rysowanie RYSOWANIE;
        gracz GRACZ;
        policja POLICJANT;

        public japonskilabirynt()
        {
            InitializeComponent();
            rozmiarlabiryntu();
            LABIRYNT = new labirynt(WYSOKOSC, SZEROKOSC);
            RYSOWANIE = new rysowanie(LABIRYNT.GRID, ROZMIARKOMORKI, WYSOKOSC, SZEROKOSC, this);
            GRACZ = new gracz(ROZMIARKOMORKI, LABIRYNT.GRID, WYSOKOSC, SZEROKOSC, this);
            POLICJANT = new policja();
            iloscciekawostek = ciekawostki.Length;
        }

        public void rozmiarlabiryntu()
        {
            //USTAWIANIE SZEROKOSCI LABIRYNTU DYNAMICZNIE DO ROZMIARU OKNA ABY GO ZMIEŒCIÆ
            if (SZEROKOSC >= WYSOKOSC)
            {
                ROZMIARKOMORKI = (POLEOKNA_W - X) / SZEROKOSC;
            }
            else
            {
                ROZMIARKOMORKI = POLEOKNA_H / WYSOKOSC;
            }
        }

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

            ukryjpokazgracza();

            button1.Visible = false;

            GRA = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void japonskilabirynt_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (GRA == true) {
                if (e.KeyChar == 'a' || e.KeyChar == 'A')
                {
                    GRACZ.orientacja(0);
                    GRACZ.ruch(-1, 0);
                }
                else if (e.KeyChar == 'w' || e.KeyChar == 'W')
                {

                    GRACZ.orientacja(1);
                    GRACZ.ruch(0, -1);

                }
                else if (e.KeyChar == 's' || e.KeyChar == 'S')
                {
                    GRACZ.orientacja(2);
                    GRACZ.ruch(0, 1);
                }
                else if (e.KeyChar == 'd' || e.KeyChar == 'D')
                {
                    GRACZ.orientacja(3);
                    GRACZ.ruch(1, 0);
                }
            }

            if (GRACZ.ZWYCIESTWO == true)
            {
                nastepnylabirynt();
            }

        }
        private void resetplanszy()
        {
            Random rand = new Random();
            SZEROKOSC = rand.Next(3, 5);
            WYSOKOSC = rand.Next(3, 5);

            rozmiarlabiryntu();
            LABIRYNT.reset(WYSOKOSC, SZEROKOSC);
            RYSOWANIE.reset(WYSOKOSC, SZEROKOSC, ROZMIARKOMORKI);
            GRACZ.reset(WYSOKOSC, SZEROKOSC, ROZMIARKOMORKI);
            Invalidate();
        }

        private void nastepnylabirynt()
        {
            resetplanszy();
            PUNKTY += 1000;
            POZIOM += 1;
            label5.Text = "PUNKTY: " + PUNKTY;
            label4.Text = "POZIOM: " + POZIOM;
            if (POZIOM % 3 == 0 && numerciekawostki <= iloscciekawostek)
            {
                ciekawostka();
            }
            GRACZ.ZWYCIESTWO = false;
        }

        private void ciekawostka()
        {
            GRA = false;
            if (numerciekawostki >= iloscciekawostek)
            {
                label7.Text = "Ups.. Koniec ciekawostek, mo¿esz kontynuowaæ rozgrywkê jednak nie wyœwietli siê wiêcej ciekawostek. Powodzenia!";
            }
            else
                label7.Text = ciekawostki[numerciekawostki];
            pictureBox5.Visible = true;
            label7.Visible = true;
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            numerciekawostki++;

            pictureBox5.Visible = false;
            label7.Visible = false;
            button3.Visible = false;
            GRA = true;
        }

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