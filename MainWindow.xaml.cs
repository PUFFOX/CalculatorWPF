using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data; //математика

namespace CalculatorWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentExpression = ""; // Поле для хранения текущего выражения

        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement el in MainGrid.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)((Button)e.OriginalSource).Content; //читається контент кнопки з преобразованієм

            if (str == "CE")
            {
                //resultLabel.Text = "";
                //memoryLabel.Text = "";
                ClearCurrentEntry();
            }

            else if (str == "=")
            {
                //string value = new DataTable().Compute(resultLabel.Text, null).ToString(); //рішає через строку
                //resultLabel.Text = value;

                EvaluateExpression();
            }

            else if (str == "C")
            {
                ClearAll();
            }

            else if (str == "<")
            {
                RemoveLastCharacter();
            }

            else if (str == ".")
            {
                AddDecimalPoint();
            }

            else
            {
                AppendToCurrentEntry(str);
            }


        }

        private void ClearCurrentEntry()
        {
            resultLabel.Text = "";
        }

        private void ClearAll()
        {
            resultLabel.Text = "";
            memoryLabel.Text = "";
            currentExpression = "";
        }

        private void RemoveLastCharacter()
        {
            if (resultLabel.Text.Length > 0)
            {
                resultLabel.Text = resultLabel.Text.Substring(0, resultLabel.Text.Length - 1);
                currentExpression = resultLabel.Text;
            }
        }

        private void AddDecimalPoint()
        {
            if (!resultLabel.Text.Contains("."))
            {
                resultLabel.Text += ".";
                currentExpression += ".";
            }
        }

        private void AppendToCurrentEntry(string str)
        {
            resultLabel.Text += str;
            currentExpression += str;
        }

        private void EvaluateExpression()
        {
            try
            {
                DataTable dataTable = new DataTable();
                var result = dataTable.Compute(currentExpression, null);
                resultLabel.Text = result.ToString();
                memoryLabel.Text = currentExpression;
                currentExpression = result.ToString();
            }
            catch (Exception ex)
            {
                resultLabel.Text = "Error";
                memoryLabel.Text = ex.Message;
            }
        }
    }
}
