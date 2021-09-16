using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {

        private const string formText = "Итерационное вычесление сумм";

        private const string button1Text = "Вычислить";
        private const string button2Text = "Запись в файл";
        private const string button3Text = "Выход";

        private const string label1Text = "Введите значение аргумента \nв градусах";
        private const string label2Text = "Начальное";
        private const string label3Text = "Конечное";

        private const int argumentsCount = 10;

        private CosRowObject[] objs;

        public Form1()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            InitializeComponent();

            Text = formText;

            button1.Text = button1Text;
            button2.Text = button2Text;
            button3.Text = button3Text;

            label1.Text = label1Text;
            label2.Text = label2Text;
            label3.Text = label3Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();

            var firstResult = double.TryParse(textBox1.Text, out var firstBord);

            var endResult = double.TryParse(textBox2.Text, out var endBord);

            if (!(firstResult && endResult))
            {
                MessageBox.Show("Введены некорректные данные(", "Ошибка");

                return;
            }

            ComputeCosRowObject(firstBord, endBord);

            WriteInfoToForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(objs is null)
            {
                MessageBox.Show("Выполните вычисления", "Ошибка");
                return;
            }

            using (var writer = new StreamWriter("1.txt"))
            {
                WriteInfoToFile(writer);
            }

            MessageBox.Show("Запись в файл выполнилась успешно", "Успех");

        }

        private void button3_Click(object sender, EventArgs e) => 
            Close();

        private void ComputeCosRowObject(double firstBorder, double endBorder)
        {

            objs = new CosRowObject[argumentsCount];

            var args = GetArguments(firstBorder, endBorder);

            for (int i = 0; i < argumentsCount; i++)
            {
                objs[i] = new CosRowObject(args[i]);

                objs[i].ComputeAll();
            }

        }

        private double[] GetArguments(double firstBorder, double endBorder)
        {
            var step = (endBorder - firstBorder) / argumentsCount;

            double[] args = new double[argumentsCount];

            for (int i = 0; i < argumentsCount; i++)
            {
                args[i] = firstBorder + step * i;

                args[i] = Math.Round(args[i], 2);
            }

            return args;
        } 

        private void WriteInfoToForm()
        {
            listBox1.Items.Add("Аргумент   Точность  Функция    Итерация");

            foreach (var obj in objs)
            {
                
                for (int i = 0; i < obj.FunValue.Length; i++)
                {
                    listBox1.Items.Add($"{obj.Argument}\t  {obj.Accuracy[i].ToString("0.000000")}\t  " +
                        $"{obj.FunValue[i]}\t\t  {obj.Iteration[i]}\t");
                }

                listBox1.Items.Add("");
            }
        }

        private void WriteInfoToFile(StreamWriter sw)
        {
            sw.WriteLine("Аргумент   Точность  Функция    Итерация");

            foreach (var obj in objs)
            {

                for (int i = 0; i < obj.FunValue.Length; i++)
                {
                    sw.WriteLine($"{obj.Argument} {obj.Accuracy[i].ToString("0.000000")} " +
                        $"{obj.FunValue[i]} {obj.Iteration[i]}");
                }

                sw.WriteLine();
            }
        }
    }
}
