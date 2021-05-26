using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        // Объявление переменных
        private float a;
        private float b;
        private float c;

        public Form1()
        {
            InitializeComponent();

            // Заполнение выпадающего списка арифметическими операциями.
            comboBox1.Items.Add("+");
            comboBox1.Items.Add("—");

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ввод переменных с клавиатуры.
            a = float.Parse(textBox1.Text);
            b = float.Parse(textBox2.Text);

            // Выполнение арифметической операции, выбранной из списка. 
            switch (comboBox1.SelectedItem)
            {
                case "+":                    
                    c = a + b;
                    textBox3.Text = c.ToString();
                    break;
                case "—":
                    c = a - b;
                    textBox3.Text = c.ToString();
                    break;
            }
        }       

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Запрет на ввод лишних символов.
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }
    }
}
