using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WpfTextWork.Model;

namespace WpfTextWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((Run)FirstParagraph.Inlines.FirstInline).Text = "Change test";

            Model1 db = new Model1();

            foreach (Category item in db.Category.ToList())
            {

                Run r = new Run();
                r.Text = item.Cat_Name;
                Paragraph p = new Paragraph();
                p.Inlines.Add(r);
                p.FontWeight = FontWeights.Bold;
                ListItem li = new ListItem(p);
                ListHistory.ListItems.Add(li);
            }


        }

        private void LoadDoc_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*";
            if (openFile.ShowDialog() == true)
            {
                TextRange documTextRange = new TextRange(MyrichTextBox.Document.ContentStart,
                    MyrichTextBox.Document.ContentEnd);
                using (FileStream fs = new FileStream(openFile.FileName, FileMode.Open))
                {
                    documTextRange.Load(fs, DataFormats.Rtf);
                }
            }
        }

        private void SaveDoc_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*";

            if (saveFile.ShowDialog() == true)
            {
                TextRange documTextRange = new TextRange(MyrichTextBox.Document.ContentStart,
                    MyrichTextBox.Document.ContentEnd);
                using (FileStream fs = File.Create(saveFile.FileName))
                {
                    documTextRange.Save(fs, DataFormats.Rtf);
                }
            }

        }
    }
}
