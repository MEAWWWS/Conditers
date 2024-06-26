using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WindowRegister.xaml
    /// </summary>
    public partial class WindowRegister : Window
    {
        public WindowRegister()
        {
            InitializeComponent();
            UpdateUserJobs();
        }
        void UpdateUserJobs()
        {
            List<string> jobs = new List<string>();
            foreach (UserJob job in DataBaseClass.GetDatabase().UserJobs)
            {
                jobs.Add(job.JobName);
            }
            comboBoxJob.ItemsSource = jobs;
            comboBoxJob.SelectedIndex = 0;
        }
        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxJob.SelectedItem == null)
            {
                MessageBox.Show("Выберите работу");
                return;
            }
            if (textBoxLogin.Text.Length < 6)
            {
                MessageBox.Show("минимум 6 символов в логине");
                return;
            }
            if (passwordBoxPass.Password.Length < 6)
            {
                MessageBox.Show("минимум 6 символов в пароле");
                return;
            }
            if (textBoxName.Text.Length < 5)
            {
                MessageBox.Show("минимум 5 символов в имени");
                return;
            }
            if (textBoxSurname.Text.Length < 5)
            {
                MessageBox.Show("минимум 5 символов в фамилии");
                return;
            }
            if (textBoxPatronymic.Text.Length < 5)
            {
                MessageBox.Show("минимум 5 символов в отчестве");
                return;
            }
            if (DataBaseClass.GetDatabase().Users.FirstOrDefault(u => u.Login == textBoxLogin.Text) != null)
            {
                MessageBox.Show("Логин используется");
                return;
            }
            UserJob job = DataBaseClass.GetDatabase().UserJobs.FirstOrDefault(j => j.JobName == comboBoxJob.SelectedItem.ToString());
            if (job != null)
            {
                DataBaseClass.GetDatabase().Users.Add(new User() { Login = textBoxLogin.Text, Name = textBoxName.Text, Password = passwordBoxPass.Password, Surname = textBoxSurname.Text, Patronymic = textBoxPatronymic.Text, JobId = job.Id });
                DataBaseClass.GetDatabase().SaveChanges();
                MessageBox.Show("Успешно");
                Close();
            }
        }
    }
}
