namespace JaponskiLabirynt
{
    public partial class japonskilabirynt : Form
    {
        public static int WYSOKOSC = 10;
        public static int SZEROKOSC = 10;

        private static int[] USTAWIENIA = {350, 60, 60, 5}; // POZYCJAX, POZYCJAY, ROZMIARKOMORKI, PENWIDTH
                                    //PADDING
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

        private void button1_Click(object sender, EventArgs e)
        {
            LABIRYNT.wypelnianie(LABIRYNT.GRID);
            LABIRYNT.kret(LABIRYNT.GRID, 0, 0);
            Invalidate();
        }
    }
}