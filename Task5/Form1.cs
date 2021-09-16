using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Task5
{

    public partial class Form1 : Form
    {
        private const string _label1TextFalse = "Файл не выбран";
        private const string _label1TextTrue = "Файл выбран";
        private const string _label2Text = "Данные графика";
        private const string _label3Text = "Таблица графика";
        private const string _label4Text = "График функции";
        private const string _label5Text = "График точности и итераций";
        private const string _chart1Ser1Text = "y = f(x)";
        private List<Graphic> _graphics;

        public Form1()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            InitializeComponent();

            label1.Text = _label1TextFalse;
            label2.Text = _label2Text;
            label3.Text = _label3Text;
            label4.Text = _label4Text;
            label5.Text = _label5Text;

            dataGridView1.Columns.Add("Argument", "Аргумент");
            dataGridView1.Columns.Add("Func", "Функция");
            chart1.Series[0].Name = _chart1Ser1Text;

        }

        private void selectoolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (ofd.ShowDialog(this) != DialogResult.OK)
            {
                MessageBox.Show("Проблема с выбранным файлом", "Ошибка");

                return;
            }

            var fileName = ofd.FileName;

            var result = SetGraphicsFromFile(fileName);

            if (!result)
            {
                MessageBox.Show("Проблема с выбранным файлом", "Ошибка");

                return;
            }
        }

        private bool SetGraphicsFromFile(string fileName)
        {
            List<string> lines = new List<string>();

            using (var reader = new StreamReader(fileName) )
            {
                reader.ReadLine();

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }

            }

            List<List<string>> grafs = new List<List<string>>();

            _graphics = new List<Graphic>();

            var list = new List<string>();

            foreach (var item in lines)
            {
                
                if (item != "")
                {
                    list.Add(item);
                }
                else
                {
                    grafs.Add(list);

                    list = new List<string>(); 
                }
            }

            foreach (var graf in grafs)
            {
                double x = 0, y = 0;

                List<int> iterations = new List<int>();

                List<double> accuracies = new List<double>();

                foreach (var item in graf)
                {
                    var itemsGraf = item.Split(' ');

                    var xResult = double.TryParse(itemsGraf[0], out x);

                    var acResult = double.TryParse(itemsGraf[1], out var acur);

                    var yResult = double.TryParse(itemsGraf[2], out y);

                    var itrResult = int.TryParse(itemsGraf[3], out var it);


                    if( !(xResult & acResult & yResult & itrResult))
                    {
                        _graphics = null;

                        return false;
                    }

                    accuracies.Add(acur);

                    iterations.Add(it);
                }

                var graph = new Graphic 
                { 
                    X = x,
                    Y = y,
                    Accuracies = accuracies,
                    Iterations = iterations

                };

                _graphics.Add(graph);
            }

            label1.Text = $"{_label1TextTrue}: {fileName}";

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormClear();

            if (_graphics is null)
            {
                MessageBox.Show("Проблема с выбранным файлом\nили файл не выбран", "Ошибка");

                return;
            }

            FormWrite();
        }

        private void FormClear()
        {
            dataGridView1.Rows.Clear();
            listBox1.Items.Clear();
            chart1.Series[0].Points.Clear();

            chart2.Series.Clear();
        }

        private void FormWrite()
        {
            int i = 0;
            foreach (var grafitems in _graphics)
            {
                dataGridView1.Rows.Add($"{grafitems.X}", $"{grafitems.Y}");
                listBox1.Items.Add($"{grafitems.X} : {grafitems.Y}");
                chart1.Series[0].Points.AddXY(grafitems.X, grafitems.Y);
                

                chart2.Series.Add(new Series());

                for (int j = 0; j < grafitems.Accuracies.Count; j++)
                {
                    var ser = chart2.Series[i];

                    ser.ChartType = SeriesChartType.Spline;

                    ser.Name = $"{grafitems.X} : {grafitems.Y}";

                    ser.Points.AddXY(grafitems.Iterations[j], grafitems.Accuracies[j]);
                }

                i++;
            }

            
        }

        private void button2_Click(object sender, EventArgs e) =>
            Close();
    }
}
