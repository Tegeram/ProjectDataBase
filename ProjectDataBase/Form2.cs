﻿using System;
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
    public partial class Form2 : Form
    {
        private SqlConnection sqlConnection = null;
        private void ComboBoxSelect()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
            sqlConnection.Open();
            SqlCommand select1 = new SqlCommand("select * from Providers", sqlConnection);
            SqlCommand select2 = new SqlCommand("select * from Products", sqlConnection);
            SqlCommand select3 = new SqlCommand("select * from Storages", sqlConnection);
            SqlDataAdapter sda1 = new SqlDataAdapter(select1);
            SqlDataAdapter sda2 = new SqlDataAdapter(select2);
            SqlDataAdapter sda3 = new SqlDataAdapter(select3);
            DataTable tbl = new DataTable();
            DataTable tb2 = new DataTable();
            DataTable tb3 = new DataTable();
            sda1.Fill(tbl);
            sda2.Fill(tb2);
            sda3.Fill(tb3);
            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "Provider";
            comboBox1.ValueMember = "ID";
            comboBox2.DataSource = tb2;
            comboBox2.DisplayMember = "Products";
            comboBox2.ValueMember = "ID";
            comboBox3.DataSource = tb3;
            comboBox3.DisplayMember = "Storages";
            comboBox3.ValueMember = "ID";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }
        public Form2()
        {
            InitializeComponent();
            ComboBoxSelect();
        }      

        private void Form2_Load(object sender, EventArgs e)
        {            
        }
            private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            ComboBoxSelect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
            ComboBoxSelect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
            ComboBoxSelect();
        }

        private void button4_Click(object sender, EventArgs e)
        {     
            try
            {                
                SqlCommand command4 = new SqlCommand("insert into Procurements(id_provider,id_product,quantity,price,id_storage)  values (@Provider, @Product, @quantity, @price,@Storage); select SCOPE_IDENTITY()", sqlConnection);
                command4.Parameters.AddWithValue("Provider", comboBox1.SelectedValue);
                command4.Parameters.AddWithValue("Product", comboBox2.SelectedValue);                
                command4.Parameters.AddWithValue("quantity", textBox3.Text);                
                command4.Parameters.AddWithValue("price", textBox2.Text);
                command4.Parameters.AddWithValue("Storage", comboBox3.SelectedValue);
                command4.ExecuteNonQuery();
                int i = Convert.ToInt32(command4.ExecuteScalar());
                
                SqlCommand command5 = new SqlCommand("insert into Sources(Data, Id_procurement) Values(@Data, @i)", sqlConnection);
                command5.Parameters.AddWithValue("Data", dateTimePicker1.Value);
                command5.Parameters.AddWithValue("i", i);
                command5.ExecuteNonQuery();               

                MessageBox.Show("Saved", "Message", MessageBoxButtons.OK);
                sqlConnection.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sqlConnection.Close();
            this.Close();
        }        
    }
}
