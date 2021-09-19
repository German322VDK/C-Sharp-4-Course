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

namespace Task_8
{
    public partial class Form1 : Form
    {
        private string _text;

        private string[] _words;

        private int _matchesCount;

        private const string label1TextFalse = "Файл не выбран";
        private const string label1TextTrue = "Файл выбран";
        private const string label2Text = "Поиск";
        private const string label3Text = "Количество совпадений";


        public Form1()
        {
            InitializeComponent();

            label1.Text = label1TextFalse;
            label2.Text = label2Text;
            label3.Text = label3Text;
        }

        private void chooseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                MessageBox.Show("Проблема с выбранным файлом", "Ошибка");

                return;
            }

            var fileName = ofd.FileName;

            ReadTextFromFile(fileName);

            if (_words is null || _words.Length < 1)
            {
                MessageBox.Show("Проблема с выбранным файлом", "Ошибка");

                return;
            }
        }

        private void ReadTextFromFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                _text = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(_text))
            {
                MessageBox.Show("Проблема с выбранным файлом", "Ошибка");

                return;
            }

            _words = _text.Split(' ');

            label1.Text = label1TextTrue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();

            if (string.IsNullOrEmpty(_text))
            {
                MessageBox.Show("Проблема с выбранным файлом\nили файл не выбран", "Ошибка");

                return;
            }

            richTextBox1.AppendText(_text);

            richTextBox1.ForeColor = Color.Black;

            string serchingText = textBox1.Text;

            _matchesCount = SearchWord(serchingText);

            label3.Text = $"{label3Text}: {_matchesCount} слов";
        }

        private int SearchWord(string word)
        {
            int i = 0;
            int pos = 0;

            while (pos != -1)
            {
                pos = richTextBox1.Find(word, pos, RichTextBoxFinds.None);

                if (pos != -1)
                {
                    richTextBox1.SelectionStart = pos;

                    richTextBox1.SelectionLength = word.Length;

                    richTextBox1.SelectionColor = Color.Red;

                    pos++;

                    i++;
                }

            }

            return i;

        }


        private void Clear()
        {
            richTextBox1.ForeColor = Color.Black;

            richTextBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e) =>
            Close();
    }
}
