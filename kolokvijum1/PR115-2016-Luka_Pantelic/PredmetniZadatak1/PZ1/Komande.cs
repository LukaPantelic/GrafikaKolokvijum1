using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PZ1
{
    public class Komande
    {
        public static Stack<UIElement> undo = new Stack<UIElement>();
        public static Stack<UIElement> redo = new Stack<UIElement>();
        public static bool clear = false;
    }
}
