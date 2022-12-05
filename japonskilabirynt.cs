namespace JaponskiLabirynt
{
    public partial class japonskilabirynt : Form
    {
        private static int WYSOKOSC = 4;
        private static int SZEROKOSC = 4;

        labirynt LABIRYNT;
        rysowanie RYSOWANIE;

        public japonskilabirynt()
        {
            InitializeComponent();
            LABIRYNT = new labirynt(WYSOKOSC, SZEROKOSC);
            RYSOWANIE = new rysowanie(LABIRYNT.GRID);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}