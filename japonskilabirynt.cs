namespace JaponskiLabirynt
{
    public partial class japonskilabirynt : Form
    {
        public static int POLEOKNA_W = 1024;
        public static int POLEOKNA_H = 768;
        public static int WYSOKOSC = 10;
        public static int SZEROKOSC = 10;
        public string ORIENTACJA = "PRAWO";

        private static int[] USTAWIENIA = {311, 0, 0, 0}; // POZYCJAX, POZYCJAY, ROZMIARKOMORKI, PENWIDTH
        labirynt LABIRYNT;
        rysowanie RYSOWANIE;
        gracz GRACZ;
        policja POLICJANT;

        public japonskilabirynt()
        {
            InitializeComponent();
            rozmiarlabiryntu();
            LABIRYNT = new labirynt(WYSOKOSC, SZEROKOSC);
            RYSOWANIE = new rysowanie(LABIRYNT.GRID, USTAWIENIA, this);
            GRACZ = new gracz(USTAWIENIA, this);
            POLICJANT = new policja();
        }

        public void rozmiarlabiryntu()
        {
            //USTAWIANIE SZEROKOSCI LABIRYNTU DYNAMICZNIE DO ROZMIARU OKNA
            if (SZEROKOSC >= WYSOKOSC)
            {
                USTAWIENIA[2] = (POLEOKNA_W - USTAWIENIA[0]) / SZEROKOSC;

            }
            else
            {
                USTAWIENIA[2] = POLEOKNA_H / WYSOKOSC;
            }
        }

        public void start()
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            button1.Visible = false;

            label1.Top = 20;
            LABIRYNT.restart();
            Invalidate();
            start();
        }

        private void japonskilabirynt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a' || e.KeyChar == 'A')
            {
                if (ORIENTACJA == "LEWO") ;
                else
                {
                    ORIENTACJA = "LEWO";
                    GRACZ.orientacja(0);
                }
                
                GRACZ.ruch(-1, 0);
                GRACZ.cofnijorientacje(0);

            }
            else if (e.KeyChar == 'w' || e.KeyChar == 'W')
            {
                if (ORIENTACJA == "GORA") ;
                else
                {
                    ORIENTACJA = "GORA";
                    GRACZ.orientacja(1);
                }

                GRACZ.ruch(0, -1);
                GRACZ.cofnijorientacje(1);

            }
            else if (e.KeyChar == 's' || e.KeyChar == 'S')
            {
                if (ORIENTACJA != "DOL")
                {
                    GRACZ.orientacja(2);
                }

                GRACZ.ruch(0, 1);
                Invalidate();
                
                if (ORIENTACJA != "DOL") {
                    ORIENTACJA = "DOL";
                    GRACZ.cofnijorientacje(2);
                }
            }
            else if (e.KeyChar == 'd' || e.KeyChar == 'D')
            {
                if (ORIENTACJA == "PRAWO") ;
                else
                {
                    ORIENTACJA = "PRAWO";
                    GRACZ.orientacja(3);
                }
                GRACZ.ruch(1, 0);
                GRACZ.cofnijorientacje(3);

            }
        }
    }
}