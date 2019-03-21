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
        private string[] fileNames;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                this.fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                this.listBoxFileNames.Items.Clear();
                for(int i=0;i<this.fileNames.Length;i++)
                {
                    string mainFileName = System.IO.Path.GetFileNameWithoutExtension(this.fileNames[i]);
                    this.listBoxFileNames.Items.Add(mainFileName);
                }
            }
        }

        private void ListBoxFileNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            int index = this.listBoxFileNames.Items.IndexOf(e.AddedItems[0]);
            string filename = this.fileNames[index];
            try
            {
                ExperimentResult result = RunSubProcess.run(filename);
                this.textBlock.Text = System.IO.File.ReadAllText(result.textFileName);
                BitmapImage src = new BitmapImage(new Uri(result.pictureFileName, UriKind.RelativeOrAbsolute));
                

                this.image.Source = src;
            }
            catch(Exception exception)
            {
                this.textBlock.Text = exception.Message;
            }
        }
    }
}
