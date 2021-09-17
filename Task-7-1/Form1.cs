using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_7_1
{
    public partial class Form1 : Form
    {
        private const string _label1Text = "Укажите количество размещений по m";
        private const string _label2Text = "m = ";
        private const string _label3Text = "n = ";
        private const string _label4Text = "Входная строка = ";
        private const string _label5TextFalse = "Файл не введён ";
        private const string _label5TextTrue = "Файл введён ";

        private string _fileText;

        private string[] _texts;

        public Form1()
        {
            InitializeComponent();

            label1.Text = _label1Text;
            label2.Text = _label2Text;
            label3.Text = _label3Text;
            label4.Text = _label4Text;
            label5.Text = _label5TextFalse;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearForm();

            var mResult = int.TryParse(textBox1.Text, out int m);

            if (!mResult)
            {
                MessageBox.Show("Вы ввели некоректное число", "Ошибка");

                return;
            }

            label2.Text += m;

            label3.Text += _fileText.Length;

            label4.Text += SplitFileText(_fileText);

            _texts = Placement(_fileText, m);
        }

        private void button2_Click(object sender, EventArgs e) =>
            Close();

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();

            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                MessageBox.Show("Проблема с выбранным файлом", "Ошибка");

                return;
            }

            var fileName = ofd.FileName;

            var result = ReadFromFile(fileName);

            if (!result)
            {
                MessageBox.Show("Проблема с выбранным файлом", "Ошибка");

                return;
            }

            label5.Text = _label5TextTrue;
        }

        private bool ReadFromFile(string fileName)
        {
            
            using (var reader = new StreamReader(fileName))
            {
                _fileText = reader.ReadToEnd();
            }

            return true;
        }

        private void ClearForm()
        {
            listBox1.Items.Clear();

            label2.Text = _label2Text;
            label3.Text = _label3Text;
            label4.Text = _label4Text;
        }

        private string SplitFileText(string text)
        {
            var strb = new StringBuilder(text.Length + 10);

            for (int i = 0; i < text.Length; i++)
            {
                strb.Append(text[i]);

                if (i % 20 == 0)
                    strb.Append('\n');
            }

            return strb.ToString();
        }

        //private string[] Placement(string s, int m)
        //{
        //    var texts = new List<string>();

        //    int n = s.Length;

            
        //}
    }
}
