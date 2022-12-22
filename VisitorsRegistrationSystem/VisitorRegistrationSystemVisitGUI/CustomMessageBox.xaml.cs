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

namespace VisitorRegistrationSystemVisitGUI
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public new string Title { get; set; }
        public string Message { get; set; }

        public CustomMessageBox(string title, string message)
        {
            InitializeComponent();
            Title = title;
            Message = message;
        }
    }
}
