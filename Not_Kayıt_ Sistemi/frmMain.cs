namespace Not_Kayıt__Sistemi
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmOgrenciDetay frm = new FrmOgrenciDetay();
            frm.numara = MskNumber.Text;
            frm.Show();
        }

        private void MskNumber_TextChanged(object sender, EventArgs e)
        {
            if (MskNumber.Text == "1111")
            {
                FrmÖğretmenDetay form = new FrmÖğretmenDetay();
                form.Show();
            }
        }
    }
}