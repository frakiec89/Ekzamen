using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Ekzamen
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Question> _queryables = new List<Question>();

        public MainWindow()
        {
            InitializeComponent();
            tbCountVarContent.Text = "файл не открыт";
            tbCountVar.Text = "1";
        }

        private void btOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                Microsoft.Win32.OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Текстовый документ (.txt)|*.txt|Все файлы|*.*";
                if (  open.ShowDialog()== true)
                {
                    _queryables = Algoritm.GetQuestions(open.FileName);
                    tbCountContent.Text = _queryables.Count.ToString();
                    tbCountVarContent.Text = ((int) _queryables.Count / 2).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                tbPathVar.Text = dialog.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Algoritm.SaveVar(tbPathVar.Text, Convert.ToInt32(tbCountVar.Text),
                    Convert.ToInt32(tbCountVarContent.Text), _queryables);

                if
                    (
                MessageBox.Show("Операция выполнена - окрыть папку с файлами? " + tbPathVar.Text,
                    "Сообщение", MessageBoxButton.YesNo)== MessageBoxResult.Yes)
                {
                    Process.Start(tbPathVar.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
