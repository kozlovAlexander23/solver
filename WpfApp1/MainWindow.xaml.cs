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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Fx
        {
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
            public string Rez { get {
                    double D, X1, X2;
                    if (A == 0) return "Ввидите\nзначние A";
                    D = Math.Pow(B, 2) - 4 * A * C;
                    if (D > 0)
                    {
                        X1 = (-B + Math.Sqrt(D)) / (2 * A);
                        X2 = (-B - Math.Sqrt(D)) / (2 * A);
                        return "X1= " + Math.Round(X1, 3).ToString() + "\nX2= " + Math.Round(X2, 3).ToString();
                    }
                    else if (D == 0)
                    {
                        X1 = -B / (2 * A);
                        return "X= " + Math.Round(X1, 3).ToString();
                    }
                    else return "Ошибка";
                } }
        }
        static Fx s_Fx;
        public MainWindow()
        {
            InitializeComponent();
            s_Fx = new Fx();
            TextBoxA.DataContext = s_Fx;
            TextBoxB.DataContext = s_Fx;
            TextBoxC.DataContext = s_Fx;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxR.DataContext = null;
            TextBoxR.DataContext = s_Fx;
        }
    }
}