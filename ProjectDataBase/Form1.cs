using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace ProjectDataBase
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;

        private void SelectItems()
        {
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT Sources.ID, Sources.Data, Procurements.ID, Providers.Provider, Products.ID, Products.Product, Procurements.price, Procurements.quantity, Procurements.cost, Storages.Storage from Sources inner join Procurements on Sources.Id_procurement = Procurements.ID inner join Providers on Procurements.id_provider = Providers.ID inner join Products on Procurements.id_product = Products.ID inner join Storages on Procurements.id_storage = Storages.ID; ", sqlConnection);
            try
            {
                sqlReader = command.ExecuteReader();

                ListViewItem item = null;

                while (sqlReader.Read())
                {
                    
                    item = new ListViewItem(sqlReader[0].ToString());
                    item.SubItems.Add(DateTime.Parse(sqlReader[1].ToString()).ToString("dd-MM-yyyy"));
                    item.SubItems.Add(sqlReader[2].ToString());
                    item.SubItems.Add(sqlReader[3].ToString());
                    item.SubItems.Add(sqlReader[4].ToString());
                    item.SubItems.Add(sqlReader[5].ToString());
                    item.SubItems.Add(sqlReader[6].ToString());
                    item.SubItems.Add(sqlReader[7].ToString());
                    item.SubItems.Add(sqlReader[8].ToString());
                    item.SubItems.Add(sqlReader[9].ToString());
                    

                    listView1.Items.Add(item);
                }
                sqlReader.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }
        
        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {     
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            sqlConnection.Open();           
            SelectItems();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 createform = new Form2();                 
            createform.ShowDialog();

            listView1.Items.Clear();
            SelectItems();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (listView1.SelectedItems.Count > 0)
            {
                SqlCommand select1 = new SqlCommand("select ID from Providers where Provider = @Provider",sqlConnection);
                select1.Parameters.AddWithValue("Provider", listView1.FocusedItem.SubItems[3].Text);
                int i = Convert.ToInt32(select1.ExecuteScalar());
                SqlCommand select2 = new SqlCommand("select ID from Products where Product = @Product", sqlConnection);
                select2.Parameters.AddWithValue("Product", listView1.FocusedItem.SubItems[5].Text);
                int j = Convert.ToInt32(select2.ExecuteScalar());
                SqlCommand select3 = new SqlCommand("select ID from Storages where Storage = @Storage", sqlConnection);
                select3.Parameters.AddWithValue("Storage", listView1.FocusedItem.SubItems[9].Text);
                int k = Convert.ToInt32(select3.ExecuteScalar());
                Form6 changeform = new Form6();
                changeform.dateTimePicker1.Value = DateTime.Parse(listView1.FocusedItem.SubItems[1].Text);
                changeform.comboBox1.SelectedValue = i;
                changeform.comboBox2.SelectedValue = j;
                changeform.textBox2.Text = listView1.FocusedItem.SubItems[6].Text;
                changeform.textBox3.Text = listView1.FocusedItem.SubItems[7].Text;
                changeform.comboBox3.SelectedValue = k;

                changeform.ShowDialog();
            }
            else
            {
                MessageBox.Show("String is not checked", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            listView1.Items.Clear();
            SelectItems();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();        
            SelectItems();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int i = int.Parse(listView1.FocusedItem.SubItems[2].Text);
                string sql = "Delete from Procurements where Procurements.Id = @i";
;               SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;

                SqlParameter Row = new SqlParameter();
                Row.ParameterName = "@i";
                Row.IsNullable = false;
                Row.Value = i;

                command.Parameters.Add(Row);      
                command.ExecuteNonQuery();

                listView1.Items.Clear();
                SelectItems();

            }
        }        
    }
}
