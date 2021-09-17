using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Task_4
{

    public partial class Form1 : Form
    {
        private const string label1Text = "Пузырком";
        private const string label2Text = "Выбором";
        private const string label3Text = "Быстрая";
        private const string label4Text = "Данные";
        private const string label5Text = "";
        private const string label6Text = "";
        private const string label7Text = "";
        private const string label8Text = "Введите число элементов для сортировки";

        private const string label567 = "Время:";

        private const string checkBoxText = "Показывать данные";

        private const int resultsCount = 5;

        private int[] _randArray;

        private int[] _bobbleArray;
        private int[] _selectArray;
        private int[] _quickArray;

        private int _count;

        //private int[] _randArray;

        //private int[] _randArray;

        private CountAndTime[] _bobbleData;

        private CountAndTime[] _selectData;

        private CountAndTime[] _quickData;

        public Form1()
        {
            InitializeComponent();

            label1.Text = label1Text;
            label2.Text = label2Text;
            label3.Text = label3Text;
            label4.Text = label4Text;
            label5.Text = label5Text;
            label6.Text = label6Text;
            label7.Text = label7Text;
            label8.Text = label8Text;

            checkBox1.Text = checkBoxText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearForm();

            bool countResult = int.TryParse(textBox1.Text, out _count);

            if (!countResult)
            {
                MessageBox.Show("Введено неккоректное число", "Ошибка");

                return;
            }

            _randArray = RandArray(_count);

            _bobbleData = ComputeMethods(Methods.Bubble);
            _selectData = ComputeMethods(Methods.Select);
            _quickData = ComputeMethods(Methods.Quick);

            WriteForm();

        }

        private void button2_Click(object sender, EventArgs e) =>
            Close();

        private int[] RandArray(int count)
        {
            if (count < 10000)
                count = 10000;
            
            int[] a = new int[count];

            var rand = new Random();

            for (int i = 0; i < count; i++)
            {
                a[i] = rand.Next(-999, 1000);

            }

            return a;
        }

        private CountAndTime[] ComputeMethods(Methods m)
        {
            var results = new CountAndTime[resultsCount];

            int ch = 1;

            for (int i = 0; i < results.Length - 1; i++)
            {
                ch *= 10;

                results[i] = new CountAndTime { Count = ch};
            }

            results[results.Length - 1] = new CountAndTime { Count = _count };

            switch (m)
            {
                case Methods.Bubble:
                    results = ComputeBubbles(results);
                    break;
                case Methods.Select:
                    results = ComputeSelects(results);
                    break;
                case Methods.Quick:
                    results = ComputeQuicks(results);
                    break;
            }
            
            return results;
        }

        private CountAndTime[] ComputeBubbles(CountAndTime[] objs)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i] = ComputeBubble(objs[i]);
            }

            return objs;
        }

        private CountAndTime ComputeBubble(CountAndTime obj)
        {
            var count = obj.Count;

            _bobbleArray = new int[count];

            for (int i = 0; i < count; i++)
            {
                _bobbleArray[i] = _randArray[i];
            }

            int tmp;

            var timer = Stopwatch.StartNew();

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (_bobbleArray[j] > _bobbleArray[j + 1])
                    {
                        // меняем элементы местами
                        tmp = _bobbleArray[j];
                        _bobbleArray[j] = _bobbleArray[j + 1];
                        _bobbleArray[j + 1] = tmp;
                    }
                }
            }

            obj.TimeSec = Math.Round(timer.Elapsed.TotalSeconds, 4);

            return obj;
        }

        private CountAndTime[] ComputeSelects(CountAndTime[] objs)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i] = ComputeSelect(objs[i]);
            }

            return objs;
        }

        private CountAndTime ComputeSelect(CountAndTime obj)
        {
            var count = obj.Count;

            _selectArray = new int[count];

            for (int i = 0; i < count; i++)
            {
                _selectArray[i] = _randArray[i];
            }

            int tmp;

            var timer = Stopwatch.StartNew();

            for (int i = 0; i < count - 1; i++)
            {
                int imin = i;

                for (int j = i + 1; j < count; j++)
                {
                    if (_selectArray[imin] > _selectArray[j])
                    {
                        imin = j;
                        
                    }
                }

                tmp = _selectArray[i];
                _selectArray[i] = _selectArray[imin];
                _selectArray[imin] = tmp;
            }

            obj.TimeSec = Math.Round(timer.Elapsed.TotalSeconds, 4);

            return obj;
        }

        private CountAndTime[] ComputeQuicks(CountAndTime[] objs)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i] = ComputeQuick(objs[i]);
            }

            return objs;
        }

        private CountAndTime ComputeQuick(CountAndTime obj)
        {
            var count = obj.Count;

            _quickArray = new int[count];

            for (int i = 0; i < count; i++)
            {
                _quickArray[i] = _randArray[i];
            }

            var timer = Stopwatch.StartNew();

            _quickArray = QuickSort(_quickArray, 0, _quickArray.Length - 1);

            obj.TimeSec = Math.Round(timer.Elapsed.TotalSeconds, 4);

            return obj;
        }

        private int[] QuickSort(int[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        private int Partition(int[] array, int minIndex, int maxIndex)
        {
            int tmp;

            var pivot = minIndex - 1;

            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;

                    tmp = array[pivot];
                    array[pivot] = array[i];
                    array[i] = tmp;
                }
            }

            pivot++;

            tmp = array[pivot];
            array[pivot] = array[maxIndex];
            array[maxIndex] = tmp;

            return pivot;
        }

        private bool IsSorted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                    return false;
            }

            return true;
        }

        private void WriteForm()
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < _randArray.Length; i++)
                {
                    listBox1.Items.Add($"{_randArray[i]}");
                }

                for (int i = 0; i < _bobbleArray.Length; i++)
                {
                    listBox2.Items.Add($"{_bobbleArray[i]}");
                }

                for (int i = 0; i < _selectArray.Length; i++)
                {
                    listBox3.Items.Add($"{_selectArray[i]}");
                }

                for (int i = 0; i < _quickArray.Length; i++)
                {
                    listBox4.Items.Add($"{_quickArray[i]}");
                }
            }

            string isBobleSort;

            string isSelectSort;

            string isQuickSort;

            if (IsSorted(_bobbleArray))
            {
                isBobleSort = "Успешно";
            }

            else
            {
                isBobleSort = "Неудачно";
            }

            if (IsSorted(_bobbleArray))
            {
                isSelectSort = "Успешно";
            }

            else
            {
                isSelectSort = "Неудачно";
            }

            if (IsSorted(_bobbleArray))
            {
                isQuickSort = "Успешно";
            }

            else
            {
                isQuickSort = "Неудачно";
            }

            label5.Text = $"{isBobleSort} {label567}{_bobbleData[_bobbleData.Length - 1].TimeSec}с";

            for (int i = 0; i < _bobbleData.Length; i++)
            {
                listBox5.Items.Add($"{_bobbleData[i].Count}-{_bobbleData[i].TimeSec}c");
            }

            label6.Text = $"{isSelectSort} {label567}{_selectData[_selectData.Length - 1].TimeSec}с";

            for (int i = 0; i < _selectData.Length; i++)
            {
                listBox6.Items.Add($"{_selectData[i].Count}-{_selectData[i].TimeSec}c");
            }

            label7.Text = $"{isQuickSort} {label567}{_quickData[_quickData.Length - 1].TimeSec}с";

            for (int i = 0; i < _quickData.Length; i++)
            {
                listBox7.Items.Add($"{_quickData[i].Count}-{_quickData[i].TimeSec}c");
            }
        }

        private void ClearForm()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            listBox7.Items.Clear();

            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
        }
    }


}
