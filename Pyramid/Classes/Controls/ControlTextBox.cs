using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pyramid.Classes.Controls
{
    public sealed class ControlTextBox : TextBox
    {
        public ControlTextBox()
        {
            Enter += ControlTextBox_Enter;
            Leave += ControlTextBox_Leave;
            KeyPress += ControlTextBox_KeyPress;
            Text = _placeholder;
            ForeColor = _color;
        }

        private readonly string _placeholder = "Введите кол-во пирамид";
        private readonly Color _color = Color.FromArgb(91, 91, 91);

        private void ControlTextBox_Enter(object sender, EventArgs e)
        {
            if (Text == _placeholder)
            {
                Text = "";
                ForeColor = Color.Black;
            }
        }

        private void ControlTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                Text = _placeholder;
                ForeColor = _color;
            }
        }

        private void ControlTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8) // 8 - backspace
                e.Handled = true;
        }

        public bool CheckPyramidValue()
        {
            if (int.TryParse(Text, out int v) && v <= 0 || Text == _placeholder)
            {
                MessageBox.Show(@"Введите значение больше нуля", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
