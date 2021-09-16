using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3
{
    public partial class Form1 : Form
    {
        private const string label1Text = "Введите основание первой системы счисления";
        private const string label2Text = "Введите значение разрядов числа через пробел";
        private const string label3Text = "Введите основание второй системы счисления";
        private const string label4Text = "p =";
        private const string label5Text = "q =";

        public Form1()
        {
            InitializeComponent();

            label1.Text = label1Text;
            label2.Text = label2Text;
            label3.Text = label3Text;
            label4.Text = label4Text;
            label5.Text = label4Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pResult = int.TryParse(textBox1.Text, out var p);

            var qResult = int.TryParse(textBox3.Text, out var q);

            var nums = textBox2.Text.Split(" ");

            bool[] numResult = new bool[nums.Length];

            int[] numbers = new int[nums.Length];

            bool result = pResult & qResult & p >= 2 & p <= 10000 & q >= 2 & q <= 10000;

            for (int i = 0; i < nums.Length; i++)
            {
                numResult[i] = int.TryParse(nums[i], out numbers[i]);

                if (numResult[i])
                {
                    if(numbers[i] >= p || (numbers[i] < 0) )
                        numResult[i] = false;
                }
                   
                result &= numResult[i];
            }

            if (!result)
            {
                MessageBox.Show("Введены некорректные данные", "Ошибка");

                return;
            }

            string pSplitter = GetSplitter(p);
            string qSplitter = GetSplitter(q);

            var oldNum = string.Join<int>(pSplitter, numbers);

            int num10 = FromNumeralSystemTo10(numbers, p);

            var newNum = From10ToNumeralSystem(num10, q, qSplitter);

            label4.Text = $"{label4Text} {p}";
            label5.Text = $"{label4Text} {q}";

            listBox1.Items.Add($"{p}ая система счисления, число: {oldNum}");
            listBox1.Items.Add($"{10}ая система счисления, число: {num10}");
            listBox1.Items.Add($"{q}ая система счисления, число: {newNum}");
            listBox1.Items.Add("");

        }

        private void button2_Click(object sender, EventArgs e) =>
            Close();

        private int FromNumeralSystemTo10(int[] numbers, int sys)
        {
            int num10 = 0;
            int itr = 0;

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                num10 += numbers[i] * (int)Math.Pow(sys, itr);

                itr++;
            }

            return num10;
        }

        private string From10ToNumeralSystem(int tmp, int sys, string splitter)
        {
            int temp1 = 0;
            var s = new List<int>();
            while (tmp > 0)
            {
                temp1 = tmp % sys;
                tmp = tmp / sys;
                s.Add(temp1);
            }
            return Reverse(s, splitter);
        }

        private string Reverse(List<int> norm, string splitter)
        {
            int[] s = new int[norm.Count];

            for (int i = norm.Count - 1; i >= 0; i--)
            {
                s[norm.Count - 1 - i] = norm[i];
            }

            return string.Join<int>(splitter, s);
        }

        private string GetSplitter(int n)
        {
            if (n > 9)
                return ".";
            else
                return "";

        }

    }
}
