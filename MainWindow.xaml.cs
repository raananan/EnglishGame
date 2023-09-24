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
using Path = System.IO.Path;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Runtime.Serialization;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace EnglishProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XSSFWorkbook workbook = new XSSFWorkbook(File.Open(path, FileMode.Open));
        static string path = Path.Combine("Grammar.xlsx");
        static string en = "";
        static int row = 2;
        static int words_counter = 0;
        static string words_clue;
        public void MyFunction()
        {

            

            var sheet = workbook.GetSheetAt(0);
            int rows = sheet.LastRowNum;
            UserSentence.Text = "Insert text here...";

            try
            {
                CheckSentence.Content = "לחץ לבדיקת התשובה";
                CheckSentence.Background = Brushes.Beige;
                

                OriginSentence.Text = sheet.GetRow(row).GetCell(1).ToString();
               en = sheet.GetRow(row).GetCell(2).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            MyFunction();
        }

        private void CheckSentence_Click(object sender, RoutedEventArgs e)
        {
            if (en==UserSentence.Text)
            {         
                CheckSentence.Content = "תשובה נכונה";
                CheckSentence.Background = Brushes.GreenYellow;
            }
            else
            {               
                CheckSentence.Content = "תשובה שגויה";
                CheckSentence.Background = Brushes.Red;               
            }
        }
        private void next_question_Click(object sender, RoutedEventArgs e)
        {
            row++;
            words_counter = 0;
            words_clue = null;
            MyFunction();
        }

        private void prev_question_Click(object sender, RoutedEventArgs e)
        {
            if(row==2)
               {
                 MyFunction();
               }
            else
               row--;
               MyFunction();
        }

        private void clue_button_Click(object sender, RoutedEventArgs e)
        {
                var sheet = workbook.GetSheetAt(0);
                string[] words = sheet.GetRow(row).GetCell(2).ToString().Split(' ');

            if (!(string.IsNullOrEmpty(UserSentence.Text)))
                UserSentence.Text = null;
            if (words.Length > words_counter)
            {
                words_clue += words[words_counter++];
                //UserSentence.Text += words[words_counter++];
            }
            if(words_counter<words.Length)
            {
                words_clue += " ";
            }
            UserSentence.Text = words_clue;
        }

        private void UserSentence_MouseEnter(object sender, MouseEventArgs e)
        {
            if (UserSentence.Text == "Insert text here...")
            {
                UserSentence.Text = null;
            }
        }
    }
}
