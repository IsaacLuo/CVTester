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
using System.IO;

namespace CVTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.listBoxFileNames.Items.Add("1");
            this.listBoxFileNames.Items.Add("2");
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                this.fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.
                foreach(string fileName in this.fileNames)
                {
                    string mainFileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
                    this.listBoxFileNames.Items.Add(mainFileName);
                }
            }
        }

        private string[] fileNames;


        private void ListBoxFileNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            this.listBoxFileNames.Items.Add(e.AddedItems[0]);
        }
    }
}
