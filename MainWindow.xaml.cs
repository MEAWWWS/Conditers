using ConfectioneryFactoryDll;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Conditer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = passwordBoxPass.Password;
            User user = DataBaseClass.GetDatabase().Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                WindowConditer window = new WindowConditer();
                window.ShowDialog();                
            }
            else
            {
                MessageBox.Show("Неправильные логин или пароль");
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            WindowRegister window = new WindowRegister();
            window.ShowDialog();
        }
    }
}