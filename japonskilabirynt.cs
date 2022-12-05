namespace JaponskiLabirynt
{
    public partial class japonskilabirynt : Form
    {
        private static int WYSOKOSC = 40;
        private static int SZEROKOSC = 40;

        private static int[] USTAWIENIA = {2, 5, 20}; // PADDING, PENWIDTH, ROZMIARKOMORKI

        labirynt LABIRYNT;
        rysowanie RYSOWANIE;

        public japonskilabirynt()
        {
            InitializeComponent();
            LABIRYNT = new labirynt(WYSOKOSC, SZEROKOSC);
            RYSOWANIE = new rysowanie(LABIRYNT.GRID, USTAWIENIA, this);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}