namespace JaponskiLabirynt
{
    public partial class japonskilabirynt : Form
    {
        public static int WYSOKOSC = 3;
        public static int SZEROKOSC = 3;

        private static int[] USTAWIENIA = {311, 0, 238, 10}; // POZYCJAX, POZYCJAY, ROZMIARKOMORKI, PENWIDTH
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
            textBox1.Visible = false;
            //button1.Visible = false;
            label1.Top = 20;
            LABIRYNT.restart();
            Invalidate();
        }
    }
}