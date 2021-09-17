using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_6_1
{
    public partial class Form1 : Form
    {
        private const string _label1Text = "Рандомный массив";
        private const string _label2Text = "Отсортированный массив";
        private const string _label3Text = "Количество элементов массива";

        private ArrayList _randArrayList;
        private ArrayList _sortArrayList;

        public Form1()
        {
            InitializeComponent();

            label1.Text = _label1Text;
            label2.Text = _label2Text;
            label3.Text = _label3Text;

            var itemsInt = Enumerable.Range(1, 100).ToArray();

            var items = new string[itemsInt.Length];

            for (int i = 0; i < itemsInt.Length; i++)
            {
                items[i] = $"{itemsInt[i]}";
            }

            comboBox1.Items.AddRange(items);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Не выбрано количество элементов", "Ошибка");

                return;
            }

            var count = comboBox1.SelectedIndex + 1;

            _randArrayList = RandArray(count);

            _sortArrayList = (ArrayList)_randArrayList.Clone();

            QuickSort();

            WriteForm();
        }

        private void button2_Click(object sender, EventArgs e) =>
            Close();

        private ArrayList RandArray(int count)
        {
            var arrayList = new ArrayList(count);

            var rand = new Random();

            for (int i = 0; i < count; i++)
            {
                arrayList.Add(rand.Next(-999, 1000));
            }

            return arrayList;
        }

        private void QuickSort() =>
            _sortArrayList = QuickSort(_sortArrayList, 0, _sortArrayList.Count - 1);

        private ArrayList QuickSort(ArrayList array, int minIndex, int maxIndex)
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

        private int Partition(ArrayList array, int minIndex, int maxIndex)
        {
            int tmp;

            var pivot = minIndex - 1;

            for (var i = minIndex; i < maxIndex; i++)
            {
                if ((int)array[i] < (int)array[maxIndex])
                {
                    pivot++;

                    tmp = (int)array[pivot];
                    array[pivot] = array[i];
                    array[i] = tmp;
                }
            }

            pivot++;

            tmp = (int)array[pivot];
            array[pivot] = array[maxIndex];
            array[maxIndex] = tmp;

            return pivot;
        }

        private bool IsSorted(ArrayList array)
        {
            for (int i = 0; i < array.Count - 1; i++)
            {
                if ( (int)array[i] > (int)array[i + 1])
                    return false;
            }

            return true;
        }

        private void Clear()
        {
            listBox1.Items.Clear();

            listBox2.Items.Clear();

            label4.Text = "";

        }

        private void WriteForm()
        {
            for (int i = 0; i < _randArrayList.Count; i++)
            {
                listBox1.Items.Add($"Номер:{i} ; Значение: {_randArrayList[i]}");
            }

            for (int i = 0; i < _sortArrayList.Count; i++)
            {
                listBox2.Items.Add($"Номер:{i} ; Значение: {_sortArrayList[i]}");
            }

            if (IsSorted(_sortArrayList))
            {
                label4.Text = "Сортировка успешна";
            }
            else
            {
                label4.Text = "Сортировка провалена";
            }
        }
    }
}
