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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PZ1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Queue<Point> polygonTacke = new Queue<Point>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void elipse_Checked(object sender, RoutedEventArgs e)
        {
            polygon.IsChecked = false;
            polygonTacke.Clear();
            Slika.IsChecked = false;
            rectangle.IsChecked = false;
        }

        private void rectangle_Checked(object sender, RoutedEventArgs e)
        {
            polygon.IsChecked = false;
            polygonTacke.Clear();
            Slika.IsChecked = false;
            elipse.IsChecked = false;
        }

        private void polygon_Unchecked(object sender, RoutedEventArgs e)
        {
            polygonTacke.Clear();
        }

        private void polygon_Checked(object sender, RoutedEventArgs e)
        {
            elipse.IsChecked = false;
            Slika.IsChecked = false;
            rectangle.IsChecked = false;
        }

        private void Slika_Checked(object sender, RoutedEventArgs e)
        {
            polygon.IsChecked = false;
            polygonTacke.Clear();
            elipse.IsChecked = false;
            rectangle.IsChecked = false;
        }

        private void Button_Undo(object sender, RoutedEventArgs e)
        {
            if (Komande.undo.Count != 0)
            {
                if (Komande.clear)
                {
                    while (Komande.undo.Count != 0)
                    {
                        UIElement tuple = Komande.undo.Pop();
                        canvas.Children.Add(tuple);
                    }

                    Komande.clear = false;
                }
                else
                {
                    UIElement tuple = Komande.undo.Pop();
                    canvas.Children.Remove(tuple);
                    Komande.redo.Push(tuple);
                }
            }
        }

        private void Button_Redo(object sender, RoutedEventArgs e)
        {
            if (Komande.redo.Count != 0)
            {
                UIElement tuple = Komande.redo.Pop();
                canvas.Children.Add(tuple);
                Komande.undo.Push(tuple);
            }
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            Komande.clear = true;
            Komande.undo.Clear();
            Komande.redo.Clear();
            foreach (UIElement ui in canvas.Children)
            {
                Point location = ui.PointToScreen(new Point(0, 0));
                Komande.undo.Push(ui);
            }
            canvas.Children.Clear();
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);
            double x = p.X;
            double y = p.Y;
            if (polygon.IsChecked == true && polygonTacke.Count > 1)
            {
                Poly pol = new Poly(polygonTacke, x, y);
                pol.ShowDialog();
            }
        }

        private void canvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            double Skratiti = ((Menu)docp.Children[0]).ActualHeight;
            Point p = e.GetPosition(this);
            double x = p.X;
            double y = p.Y;

            if (elipse.IsChecked == true)
            {
                Elipsa el = new Elipsa(x, y - Skratiti);
                el.ShowDialog();
            }
            else if (rectangle.IsChecked == true)
            {
                Recta r = new Recta(x, y - Skratiti);
                r.ShowDialog();
            }
            else if (polygon.IsChecked == true)
            {
                polygonTacke.Enqueue(new Point(x, y - Skratiti));
            }
            else if (Slika.IsChecked == true)
            {
                Slika s = new Slika(x, y - Skratiti);
                s.ShowDialog();
            }
        }
    }
}
