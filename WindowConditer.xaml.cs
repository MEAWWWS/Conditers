using ConfectioneryFactoryDll;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
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

namespace Conditer
{
    /// <summary>
    /// Логика взаимодействия для WindowConditer.xaml
    /// </summary>
    public partial class WindowConditer : Window
    {
        public WindowConditer()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        void UpdateDataGrid()
        {
            Garbage garbage = DataBaseClass.GetDatabase().Garbages.FirstOrDefault(g=>g.Id > 0);
            if (garbage != null)
            {
                dataGridProducts.ItemsSource = new List<Garbage>() { garbage };
            }
        }
        private void dataGridProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Garbage garbage = dataGridProducts.SelectedItem as Garbage;
            if(garbage!= null)
            {
                textBoxButterCount.Text = garbage.ButterCount.ToString();
                textBoxCerealsCount.Text = garbage.CerealsCount.ToString();
                textBoxSugarCount.Text = garbage.SugarCount.ToString();
                textBoxWaterCount.Text = garbage.WaterCount.ToString();
                textBoxCacaoCount.Text = garbage.CacaoCount.ToString();
            }
        }
        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            if (!double.TryParse(textBoxButterCount.Text, formatter, out double butter))
            {
                MessageBox.Show("Введите правильно масло");
                return;
            }
            if (!double.TryParse(textBoxCerealsCount.Text, formatter, out double cereal))
            {
                MessageBox.Show("Введите правильно манку");
                return;
            }
            if (!double.TryParse(textBoxSugarCount.Text, formatter, out double sugar))
            {
                MessageBox.Show("Введите правильно сахар");
                return;
            }
            if (!double.TryParse(textBoxWaterCount.Text, formatter, out double water))
            {
                MessageBox.Show("Введите правильно воду");
                return;
            }
            if (!double.TryParse(textBoxCacaoCount.Text, formatter, out double cacao))
            {
                MessageBox.Show("Введите правильно какао");
                return;
            }
            Garbage garbage = DataBaseClass.GetDatabase().Garbages.FirstOrDefault(g => g.Id > 0);
            if (garbage != null)
            {
                garbage.ButterCount = butter;
                garbage.CerealsCount = cereal;
                garbage.SugarCount = sugar;
                garbage.WaterCount = water;
                garbage.CacaoCount = cacao;
                DataBaseClass.GetDatabase().SaveChanges();
                UpdateDataGrid();
                MessageBox.Show("Успешно");
                Debugger.Log("Изменено кол-во материалов");
            }
        }
        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
        private void buttonCalculateCandyCount_Click(object sender, RoutedEventArgs e)
        {
            Garbage garbage = DataBaseClass.GetDatabase().Garbages.FirstOrDefault(g => g.Id > 0);
            if (garbage != null)
            {
                MessageBox.Show("Конфет: " + ConfectioneryFactory.CountPossibleProducts(garbage.CerealsCount, garbage.ButterCount, garbage.SugarCount, garbage.CacaoCount, garbage.WaterCount).ToString());
                Debugger.Log("Расчитано кол-во конфет");
            }
            else
            {
                MessageBox.Show("Выберите склад");
            }
        }
        private void buttonCalculateDistancePrice_Click(object sender, RoutedEventArgs e)
        {
            WindowCalculateDistance window = new WindowCalculateDistance();
            window.Show();
            Debugger.Log("Открыто окно расчета логистики");
        }
        private void textBoxSugarCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(c => char.IsDigit(c) || c=='.'))
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

    }
}
