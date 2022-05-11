using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Not_Kayıt__Sistemi
{
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }

        public string numara;


        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {

            LblNumara.Text = numara;
            var connection = new SqlConnection("Server=DESKTOP-VDP575I\\MSSQLSERVERR;Database=DbNotKayıt;User Id=sa;Password=password35");

            connection.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM dbo.TBLDERS WHERE OGRNUMARA=@p1",connection);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader reader = komut.ExecuteReader();
            while(reader.Read())
            {
               LblAdSoyad.Text = reader[2].ToString()+" "+reader[3].ToString();
               LblSınav1.Text = reader[4].ToString();
               LblSınav2.Text = reader[5].ToString();
               LblSınav3.Text = reader[6].ToString();
               LblOrtalama.Text = reader[7].ToString();
               var a = reader[8].ToString();
                if (a == "True")
                {
                    LblDurum.Text = "GEÇTİ";
                }
                else
                {
                    LblDurum.Text = "KALDI";
                }

            }
            connection.Close();

         





        }

        
    }
}
