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

namespace PZ1
{
    /// <summary>
    /// Interaction logic for Elipsa.xaml
    /// </summary>
    public partial class Elipsa : Window
    {
        private static double a;
        private static double b;

        public Elipsa(double a1, double b1)
        {
            a = a1;
            b = b1;

            InitializeComponent();
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Draw(object sender, RoutedEventArgs e)
        {
            double w, h; //width, heigth
            int bt; //border thickness

            if (double.TryParse(Height.Text, out h) && double.TryParse(Width.Text, out w) && int.TryParse(BorderThickness.Text, out bt))
            {
                if (h > 0 && w > 0 && bt >= 0)
                {
                    StackPanel sp = (StackPanel)FillColor.SelectedItem;
                    string boja = ((Label)sp.Children[1]).Content.ToString();
                    Ellipse ellipse = new Ellipse();
                    ellipse.Height = Int32.Parse(Height.Text);
                    ellipse.Width = Int32.Parse(Width.Text);
                    ellipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(boja));
                    sp = (StackPanel)BorderColor.SelectedItem;
                    boja = ((Label)sp.Children[1]).Content.ToString();
                    ellipse.StrokeThickness = Int32.Parse(BorderThickness.Text);
                    ellipse.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(boja));

                    ((MainWindow)Application.Current.MainWindow).canvas.Children.Add(ellipse);
                    Canvas.SetLeft(ellipse, a);
                    Canvas.SetTop(ellipse, b);

                    if(Komande.clear)
                    {
                        Komande.clear = false;
                        Komande.undo.Clear();
                    }
                    Komande.undo.Push(ellipse);
                    Komande.redo.Clear();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sirina i visina moraju biti veci od 0. Debljina 0 ili vise", "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Sirina i visina moraju biti veci od 0. Debljina 0 ili vise", "Error", MessageBoxButton.OK);
            }
        }
    }
}
