namespace JaponskiLabirynt
{
    public partial class japonskilabirynt : Form
    {
        private static int WYSOKOSC = 4;
        private static int SZEROKOSC = 4;

        labirynt LABIRYNT;

        public japonskilabirynt()
        {
            InitializeComponent();
            LABIRYNT = new labirynt(WYSOKOSC, SZEROKOSC);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}