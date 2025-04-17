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
using static WpfApp1.MainWindow;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int hg;
        double hf = 32, hx, hy;
        //double verh, niz, h=1;
        double[] Y;
        double centrX;
        double centrY;
        int scrol;
        int razmer = 0;
        double razmerf = 1;
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
        public class Grafic
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public double N { get; set; }
        }
        static Grafic s_Grafic;
        static Fx s_Fx;
        public MainWindow()
        {
            InitializeComponent();
            s_Fx = new Fx();
            s_Grafic = new Grafic();
            TextBoxA.DataContext = s_Fx;
            TextBoxB.DataContext = s_Fx;
            TextBoxC.DataContext = s_Fx;
            TextBoxMin.DataContext = s_Grafic;
            TextBoxMax.DataContext = s_Grafic;
            TextBoxN.DataContext = s_Grafic;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxR.DataContext = null;
            TextBoxR.DataContext = s_Fx;
            if (s_Grafic.Min< s_Grafic.Max&& s_Grafic.N > 0)
            {
                Y = new double[((int)((s_Grafic.Max - s_Grafic.Min) / s_Grafic.N))];
                for (int i = 0; i< Y.Length;i++) {

                    Y[i] = s_Fx.A * Math.Pow(s_Grafic.Min + (s_Grafic.N * i), 2) + s_Fx.B * (s_Grafic.Min + (s_Grafic.N * i)) + s_Fx.C;
                }
                Loginc(null, null);
            }
            else
            {
                grid_znachX.Children.Clear();
                grid_znachY.Children.Clear();
                gridGraf.Children.Clear();
                TextBlock text = new TextBlock();
                text.TextAlignment = TextAlignment.Center;
                text.Text = "Параметры заданы\nнекорректно";
                gridGraf.Children.Add(text);
            }
        }
        private void Loginc(object sender, EventArgs e)
        {
            centrX = (s_Grafic.Min) + ((s_Grafic.Max - s_Grafic.Min) / 2);//всё дело в центровке правь его
            centrY = Math.Round((Y.Min()) + ((Y.Max() - Y.Min()) / 2));
            wf();
        }
        private void wf()
        {
            hg = 32 + scrol;
            centrX *= hg;
            centrY *= hg;
            //------------------------------------------------------------------------------------------------------------------------------------------------
            grid_znachX.Children.Clear();
            grid_znachY.Children.Clear();
            gridGraf.Children.Clear();
            Ellipse t = new Ellipse();
            t.Width = 20;
            t.Height = 20;
            t.Stroke = Brushes.Red;
            gridGraf.Children.Add(t);
            Line a = new Line();
            a.X1 = 0;
            a.X2 = gridGraf.ActualWidth;
            a.Y1 = gridGraf.ActualHeight / 2;
            a.Y2 = gridGraf.ActualHeight / 2;
            a.Stroke = Brushes.Black;
            TextBlock text = new TextBlock();
            text.Margin = new Thickness(0, gridGraf.ActualHeight / 2 - 9, 0, 0);
            text.VerticalAlignment = VerticalAlignment.Top;
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.Height = 16;
            text.Width = 25;
            text.Text = Convert.ToString(/*Math.Round*/(centrY / hg));
            grid_znachY.Children.Add(text);
            gridGraf.Children.Add(a);
            a = new Line();
            a.X1 = gridGraf.ActualWidth / 2;
            a.X2 = gridGraf.ActualWidth / 2;
            a.Y1 = 0;
            a.Stroke = Brushes.Black;
            a.Y2 = gridGraf.ActualHeight;
            text = new TextBlock();
            text.Margin = new Thickness(gridGraf.ActualWidth / 2 - 9, 0, 0, 0);
            text.VerticalAlignment = VerticalAlignment.Top;
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.Height = 16;
            text.Width = 25;
            text.Text = Convert.ToString(/*Math.Round*/(centrX / hg));
            grid_znachX.Children.Add(text);
            gridGraf.Children.Add(a);
            for (int i = hg, j = 1; i < gridGraf.ActualHeight / 2; i += hg, j++)
            {
                text = new TextBlock();
                text.Margin = new Thickness(0, gridGraf.ActualHeight / 2 - i - 9, 0, 0);
                text.VerticalAlignment = VerticalAlignment.Top;
                text.HorizontalAlignment = HorizontalAlignment.Left;
                text.Height = 16;
                text.Width = 25;
                text.Text = Convert.ToString(/*Math.Round*/(centrY / hg) + Math.Pow(2, razmer) * j);
                grid_znachY.Children.Add(text);
                a = new Line();
                a.X1 = 0;
                a.X2 = gridGraf.ActualWidth;
                a.Y1 = gridGraf.ActualHeight / 2 - i;
                a.Y2 = gridGraf.ActualHeight / 2 - i;
                a.Stroke = Brushes.Black;
                gridGraf.Children.Add(a);
                text = new TextBlock();
                text.Margin = new Thickness(0, gridGraf.ActualHeight / 2 + i - 9, 0, 0);
                text.VerticalAlignment = VerticalAlignment.Top;
                text.HorizontalAlignment = HorizontalAlignment.Left;
                text.Height = 15;
                text.Width = 25;
                text.Text = Convert.ToString(/*Math.Round*/(centrY / hg) - Math.Pow(2, razmer) * j);
                grid_znachY.Children.Add(text);
                a = new Line();
                a.X1 = 0;
                a.X2 = gridGraf.ActualWidth;
                a.Y1 = gridGraf.ActualHeight / 2 + i /*- hx*/;
                a.Y2 = gridGraf.ActualHeight / 2 + i /*- hx*/;
                a.Stroke = Brushes.Black;
                gridGraf.Children.Add(a);
            }
            for (int i = hg, j = 1; i < gridGraf.ActualWidth / 2; i += hg, j++)
            {
                text = new TextBlock();
                text.Margin = new Thickness(gridGraf.ActualWidth / 2 + i - 9, 0, 0, 0);
                text.VerticalAlignment = VerticalAlignment.Top;
                text.HorizontalAlignment = HorizontalAlignment.Left;
                text.Height = 16;
                text.Width = 25;
                text.Text = Convert.ToString(/*Math.Round*/(centrX / hg) + Math.Pow(2, razmer) * j);
                grid_znachX.Children.Add(text);
                a = new Line();
                a.X1 = gridGraf.ActualWidth / 2 - i;
                a.X2 = gridGraf.ActualWidth / 2 - i;
                a.Y1 = 0;
                a.Y2 = gridGraf.ActualHeight;
                a.Stroke = Brushes.Black;
                gridGraf.Children.Add(a);
                text = new TextBlock();
                text.Margin = new Thickness(gridGraf.ActualWidth / 2 - i - 9, 0, 0, 0);
                text.VerticalAlignment = VerticalAlignment.Top;
                text.HorizontalAlignment = HorizontalAlignment.Left;
                text.Height = 15;
                text.Width = 25;
                text.Text = Convert.ToString(/*Math.Round*/(centrX / hg) - Math.Pow(2, razmer) * j);
                grid_znachX.Children.Add(text);
                a = new Line();
                a.X1 = gridGraf.ActualWidth / 2 + i;
                a.X2 = gridGraf.ActualWidth / 2 + i;
                a.Y1 = 0;
                a.Y2 = gridGraf.ActualHeight;
                a.Stroke = Brushes.Black;
                gridGraf.Children.Add(a);
            }
            Polyline horArr = new Polyline();
            horArr.Points = new PointCollection();
            double _y, _x;
            for (double x = s_Grafic.Min, i = 0; i < Y.Length; x += s_Grafic.N, i += 1)
            {
                _x = (((x * hf) + gridGraf.ActualWidth / 2 - ((((centrX) / hg)) * hf)));
                _y= (((-Y[(int)i] * hf) + gridGraf.ActualHeight / 2 + ((((centrY) / hg)) * hf)));
                //if (_x > 0 /*&& _y > 0*/) 
                horArr.Points.Add(new Point(_x, _y));
            }
            horArr.Stroke = Brushes.Green;
            gridGraf.Children.Add(horArr);
            horArr = new Polyline();
            horArr.Points = new PointCollection();
            horArr.Stroke = Brushes.Red;
            gridGraf.Children.Add(horArr);
            centrX /= hg;
            centrY /= hg;
        }
        private void grid_MouseWheel(object sender, EventArgs q)
        {
            MouseWheelEventArgs e = q as MouseWheelEventArgs;
            if (e.Delta < 0)
            {
                scrol += -1;
                hf += -razmerf;
            }
            else
            {
                scrol += 1;
                hf += razmerf;
            }
            if (scrol > 31)//31
            {
                scrol = 0;
                razmer--;
                razmerf *= 2;//2
            }
            else if (scrol < -15)//-15
            {
                scrol = 0;
                razmer++;
                razmerf *= 0.5;//0.5
            }
            wf();
        }
        private void grid_MouseMove(object sender, EventArgs q)
        {
            MouseEventArgs e = q as MouseEventArgs;
            Point pozicia = e.GetPosition(sender as MainWindow);
            pozicia.X = Math.Round(pozicia.X - (gridGraf.ActualWidth / 2) - (hg / 2) - 4 /*- (scrol / 2)*/-220);
            pozicia.Y = -Math.Round(pozicia.Y - (gridGraf.ActualHeight / 2) /*- (hg / 2)*/ - 7 /*+ (scrol / 2)*/-45);
            hy = pozicia.Y;
            int cx = 0, cy = 0;
            if ((pozicia.X > hg - 3 - (scrol / 2) || pozicia.X < -hg + 3 + (scrol / 2)) || (pozicia.Y > hg - 3 - (scrol * 2) || pozicia.Y < -hg + 3 + (scrol * 2)))
            {
                if (pozicia.X > 0)
                {
                    while (pozicia.X > hg / 2)
                    {
                        cx++;
                        pozicia.X -= hg;
                    }
                }
                else
                {
                    while (pozicia.X < -hg / 2)
                    {
                        cx--;
                        pozicia.X += hg;
                    }
                }
                if (pozicia.Y > 0)
                {
                    while (pozicia.Y > hg / 2)
                    {
                        cy++;
                        pozicia.Y -= hg;
                    }
                }
                else
                {
                    while (pozicia.Y < -hg / 2)
                    {
                        cy--;
                        pozicia.Y += hg;
                    }
                }
            }
            hx = pozicia.X;
            centrX += Math.Pow(2, razmer) * cx;
            centrY += Math.Pow(2, razmer) * cy;
            wf();
        }

    }
}