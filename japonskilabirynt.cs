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
        public static int WYSOKOSC = 5;
        public static int SZEROKOSC = 5;

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
            RYSOWANIE = new rysowanie(LABIRYNT.GRID, ROZMIARKOMORKI, this);
            GRACZ = new gracz(ROZMIARKOMORKI, LABIRYNT.GRID, this);
            POLICJANT = new policja();
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

        public void start()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            //button1.Visible = false;

            label1.Top = 20;
            LABIRYNT.restart();
            Invalidate();
            start();
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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}