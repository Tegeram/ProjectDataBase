using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace ProjectDataBase
{
    public partial class Form3 : Form
    {
        public SqlConnection sqlConnection = null;
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            
        }      

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            sqlConnection.Open();

            try
            {
                SqlCommand command = new SqlCommand("insert into [Providers](Provider) Values(@Provider)", sqlConnection);
                command.Parameters.AddWithValue("Provider", textBox1.Text);

                command.ExecuteNonQuery();

                MessageBox.Show("Saved","Message", MessageBoxButtons.OK);

                sqlConnection.Close();                
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
