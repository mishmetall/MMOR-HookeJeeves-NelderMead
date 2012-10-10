using System;
using System.Collections.Generic;
using System.Linq;
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
using MMOR_2.Methods;
using MMOR_2.Function;

namespace MMOR_2
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Function.Vector start = new Function.Vector(2);
            start[0] = Double.Parse(vX1.Text);
            start[1] = Double.Parse(vX1.Text);
            HookeJeeves sol1 = new HookeJeeves(start);
            Function.Vector res = sol1.solve(new MyFunction(), Double.Parse(vPrecision.Text));
            DELETEMe = res[0] + "; " + res[1];
            NelderMead sol2 = new NelderMead(start);
            Function.Vector res2 = sol2.solve(new MyFunction(), Double.Parse(vPrecision.Text));
            DELETEMe +="\n" + res2[0] + "; " + res2[1];
            DataContext = this;
        }

        public string DELETEMe { get; set; }
    }
}
