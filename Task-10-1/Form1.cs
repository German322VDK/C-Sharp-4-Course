using System;
using System.Windows.Forms;

namespace Task_10_1
{
    public partial class Form1 : Form
    {
        private const string label1Text = "Вектор";
        private const string label2Text = "Введите размерность вектора";
        private const string label3Text = "Координаты вектора";

        private const string label7Text = "Скалярное произведение";

        private int _dem1;
        private int _dem2;

        private double _mult;

        private Vector _vector1;
        private Vector _vector2;

        public Form1()
        {
            InitializeComponent();

            label1.Text = $"{label1Text} - 1";
            label2.Text = label2Text;
            label3.Text = label3Text;

            label4.Text = $"{label1Text} - 2";
            label5.Text = label2Text;
            label6.Text = label3Text;

            label7.Text = label7Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();

            var result1 = int.TryParse(textBox1.Text, out _dem1);

            var result2 = int.TryParse(textBox2.Text, out _dem2);

            if(! (result1 && result2))
            {
                MessageBox.Show("Проблема с введённым вектором", "Ошибка");

                return;
            }

            _vector1 = new Vector(_dem1);

            _vector2 = new Vector(_dem2);

            if (_vector1.Dimension == _vector2.Dimension)
            {
                _mult = _vector1 * _vector2;
                textBox3.Text = $"{ _mult}";
            }
            else
                textBox3.Text = "Разная размерность";

            WriteToForm();
        }

        private void button2_Click(object sender, EventArgs e) =>
            Close();

        private void WriteToForm()
        {
            for (int i = 0; i < _vector1.Dimension; i++)
            {
                listBox1.Items.Add($"{_vector1[i]}");
            }

            for (int i = 0; i < _vector2.Dimension; i++)
            {
                listBox2.Items.Add($"{_vector2[i]}");
            }
        }

        private void Clear()
        {
            listBox1.Items.Clear();

            listBox2.Items.Clear();
        }
    }
}
