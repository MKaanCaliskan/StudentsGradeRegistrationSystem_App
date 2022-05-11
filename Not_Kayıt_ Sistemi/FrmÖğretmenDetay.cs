using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Not_Kayıt__Sistemi
{
    public partial class FrmÖğretmenDetay : Form
    {
      
        public FrmÖğretmenDetay()
        {
            InitializeComponent();
        }

        private void FrmÖğretmenDetay_Load(object sender, EventArgs e)
        {
            
       
           GetData();
            
        }
         public void GetData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = GetDataSourceData();
            

        }
        string SqlConnectionString = "Server=DESKTOP-VDP575I\\MSSQLSERVERR;Database=DbNotKayıt;User Id = sa; Password=password35";
       
        public BindingList<StudentViewDto> GetDataSourceData()
        {
            byte a;
            byte b;
            byte c;
            decimal d;
         
            var data = new BindingList<StudentViewDto>();
            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
              var query = @"SELECT * FROM TBLDERS";
              connection.Open();
             
              var command = new SqlCommand(query, connection);
                 using (var reader = command.ExecuteReader())
                 {
                    while (reader.Read())
                   {
                        if (!reader.IsDBNull(4))
                        {
                             a = reader.GetByte(4);
                        }
                        else
                        {
                            a = Convert.ToByte("0");
                        }

                        if (!reader.IsDBNull(5))
                        {
                            b = reader.GetByte(5);
                        }
                        else
                        {
                            b = Convert.ToByte("0");
                        }

                        if (!reader.IsDBNull(6))
                        {
                            c = reader.GetByte(6);
                        }
                        else
                        {
                            c = Convert.ToByte("0");
                        }

                        if (!reader.IsDBNull(7))
                        {
                            d = reader.GetDecimal(7);
                        }
                        else
                        {
                            d = Convert.ToDecimal("0");
                        }

                 

                        data.Add(new StudentViewDto()
                        {
                           Id = reader.GetInt16(0),
                           Number = reader[1].ToString(),
                           Surname = reader[3].ToString(),
                           Name = reader[2].ToString(), 
                           ExamOne = a,
                           ExamTwo = b,
                           ExamThree = c,
                           Ortalama = d,
                           Durum = reader.GetBoolean(8),
                           
                        });
                    }
                 }

              
            }
            var counter = 0;
            var counter2= 0;
            decimal classAvarage;
            decimal total=0;
            for (int i = 0; i < data.Count; i++)
            {
                total += data[i].Ortalama.Value;
               

                if (data[i].Durum == true)
                {
                    counter++;
        
                }
                else
                {
                    counter2++;
                   
                }
            }
            classAvarage = Convert.ToDecimal(total / data.Count);
            lblOrtalama.Text = classAvarage.ToString("0.00");
            lblPass.Text = counter.ToString();
            LblKalan.Text = counter2.ToString();



            
           return data;

        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
                var query = @"INSERT INTO TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD,DURUM) VALUES (@P1,@P2,@P3,@P4)";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@P1",mskStudentNumber.Text);
                command.Parameters.AddWithValue("@P2",mskStudentName.Text);
                command.Parameters.AddWithValue("@P3",mskStudentLastName.Text);
                command.Parameters.AddWithValue("@P4",false);
                var reader = command.ExecuteNonQuery();
                MessageBox.Show("Öğrenci Sisteme Eklendi");

                  
  
            }
            GetData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;
            mskSınav1.Text = dataGridView1.Rows[selected].Cells[4].Value.ToString();
            mskSınav2.Text = dataGridView1.Rows[selected].Cells[5].Value.ToString();
            mskSınav3.Text = dataGridView1.Rows[selected].Cells[6].Value.ToString();
            mskStudentNumber.Text = dataGridView1.Rows[selected].Cells[1].Value.ToString();
            mskStudentName.Text = dataGridView1.Rows[selected].Cells[2].Value.ToString();
            mskStudentLastName.Text = dataGridView1.Rows[selected].Cells[3].Value.ToString();
            dataGridView1.DataSource = GetDataSourceData();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            double avarage, s1, s2, s3;
            string pass;
            s1 = Convert.ToDouble(mskSınav1.Text);
            s2 = Convert.ToDouble(mskSınav2.Text);
            s3 = Convert.ToDouble(mskSınav3.Text);

            avarage = (s1 * 0.3) + (s2 * 0.3) + (s3 * 0.4);
            var ortalama = avarage.ToString();

            if (avarage >= 60)
            {
                pass = "True";
            }
            else
            {
                pass = "False";
            }

            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
                var query = @"UPDATE TBLDERS SET OGRS1=@p1,OGRS2=@p2,OGRS3=@p3,ORTALAMA=@S4,DURUM=@S5 WHERE OGRNUMARA=@S6 ";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@p1", mskSınav1.Text);
                command.Parameters.AddWithValue("@p2", mskSınav2.Text);
                command.Parameters.AddWithValue("@p3", mskSınav3.Text);
                command.Parameters.AddWithValue("@S4", decimal.Parse(ortalama));
                command.Parameters.AddWithValue("@S5", pass);
                command.Parameters.AddWithValue("@S6", mskStudentNumber.Text);
                var reader = command.ExecuteNonQuery();
                MessageBox.Show("Notlar güncellendi");


            }
            GetData();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["DURUM"].Index)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = (value) ? "Geçti" : "Kaldı";
                    e.FormattingApplied = true;
                }
            }
        }
    }
    
}
