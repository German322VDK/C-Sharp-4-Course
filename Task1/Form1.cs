using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    public partial class Task1 : Form
    {
        private const string defaultButtonText = "Работает мышка";
        private const string defaultLabelXYMouseText = "Координаты мышки";

        public Task1()
        {
            InitializeComponent();
            labelXYMouse.Text = defaultLabelXYMouseText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Задание 1", "Подсказка");
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Text = "Курсор наведён на кнопку";
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Text = defaultButtonText;
        }


        private void Task1_MouseMove(object sender, MouseEventArgs e) =>
            SetMousePosition();

        private void button1_MouseMove(object sender, MouseEventArgs e) =>
            SetMousePosition();

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) =>
            SetMousePosition();

        private void labelXYMouse_Click(object sender, EventArgs e) =>
            SetMousePosition();

        private void SetMousePosition()
        {
            var xyA = Cursor.Position;

            var xA = xyA.X;

            var yA = xyA.Y;

            var xB = xA - Location.X;

            var yB = yA - Location.Y;

            labelXYMouse.Text = $"{defaultLabelXYMouseText} (относительные: {xB}:{yB}) / " +
                $"(абсолютные:{xA}:{yA})";
        }

    }
}
