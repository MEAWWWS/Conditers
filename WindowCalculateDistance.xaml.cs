using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ConfectioneryFactoryDll;

namespace Conditer
{
    /// <summary>
    /// Логика взаимодействия для WindowCalculateDistance.xaml
    /// </summary>
    public partial class WindowCalculateDistance : Window
    {
        public WindowCalculateDistance()
        {
            InitializeComponent();
        }

        private void textBoxSugarCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(c => char.IsDigit(c) || c == '.'))
            {
                e.Handled = true;
            }

        }

        private void textBoxSugarCount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            if (!double.TryParse(textBoxDistance.Text, formatter, out double km))
            {
                MessageBox.Show("Введите километры");
                return;
            }
            if (!double.TryParse(textBoxKilometrPrice.Text, formatter, out double price))
            {
                MessageBox.Show("Введите километры");
                return;
            }
            MessageBox.Show("Цена: " + ConfectioneryFactory.CountLogisticPrice(km, price) + " rub");
            Debugger.Log("Расчитана логистика");
        }
    }
}
