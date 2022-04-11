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

namespace MacCalculator
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

        /// <summary>
        /// Used for storing the first operand
        /// </summary>
        double lastNumber = 0;

        /// <summary>
        /// Used for storing the second operand
        /// </summary>
        double newNumber = 0;

        /// <summary>
        /// Used for storing the current mathematical operation
        /// </summary>
        SelectedOperator selectedOperator;

        /// <summary>
        /// Event handlerer which will handle adding numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (lblResult.Content.ToString()!.Length >= 10)
                return;

            int selectedNumber = 0;
            selectedNumber = GetSelectedNumber(sender);

            if (lblResult.Content.ToString() == "0")
            {
                lblResult.Content = selectedNumber.ToString();
            }
            else
            {
                lblResult.Content += selectedNumber.ToString();
            }
        }

        
        /// <summary>
        /// Checks if dot (coma) alredy exists and handles adding it if it doesn't exist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DotButton_Click(object sender, RoutedEventArgs e)
        {
            if(lblResult.Content.ToString()!.Contains(",") || lblResult.Content.ToString()!.Length >= 10)
            {
                return;
            }
            lblResult.Content += ",";
        }

        /// <summary>
        /// Clears the labele and sets all variable to 0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAC_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = "0";
            lastNumber = 0;
            newNumber = 0;
        }

        /// <summary>
        /// Negates the current number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNegative_Click(object sender, RoutedEventArgs e)
        {
            if(lblResult.Content.ToString() == "0")
            {
                return;
            }

            double temp = 0;

            temp = double.Parse(lblResult.Content.ToString());

            // Checks if there is enough place in the label for the number
            if (lblResult.Content.ToString()!.Length >= 10)
            {
                // if the number is positive and the length is 10 it will drop the last number to make place for the minus
                if(temp > 0)
                {
                    lblResult.Content = (-temp).ToString().Substring(0, 10);
                }
                // If the number is negative it will just negate it
                else
                {
                    lblResult.Content = (-temp).ToString();
                }
            }
            else 
            { 
                lblResult.Content = (-temp).ToString();
            }
        }

        /// <summary>
        /// Gives percentage of the current number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPercentage_Click(object sender, RoutedEventArgs e)
        {
            if(lblResult.Content.ToString() == "0" || lblResult.Content.ToString()!.Length >= 10)
            {
                return;
            }

            double temp = double.Parse(lblResult.Content.ToString());

            lblResult.Content = (temp / 100).ToString();
        }

        /// <summary>
        /// Determenes which operation was selected and clears the label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button)!.Content.ToString()!.Contains("+"))
            {
                selectedOperator = SelectedOperator.Addition;
            }
            else if((sender as Button)!.Content.ToString()!.Contains("-"))
            {
                selectedOperator = SelectedOperator.Subtraction;
            }
            else if((sender as Button)!.Content.ToString()!.Contains("*"))
            {
                selectedOperator = SelectedOperator.Multiplication;
            }
            else if((sender as Button)!.Content.ToString()!.Contains("/"))
            {
                selectedOperator = SelectedOperator.Division;
            }

            lastNumber = double.Parse(lblResult.Content.ToString());

            lblResult.Content = "0";
        }

        /// <summary>
        /// Does the calculation based on the selected operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEqual_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(lblResult.Content.ToString(), out newNumber))
            {
                return;
            }

            string calculationResult = "0";

            switch (selectedOperator)
            {
                case SelectedOperator.Addition:
                    {
                        calculationResult = (lastNumber + newNumber).ToString();
                        break;
                    }
                case SelectedOperator.Subtraction:
                    {
                        calculationResult = (lastNumber - newNumber).ToString();
                        break;
                    }
                case SelectedOperator.Multiplication:
                    {
                        calculationResult = (lastNumber * newNumber).ToString();
                        break;
                    }
                case SelectedOperator.Division:
                    {
                        if (newNumber == 0)
                        {
                            MessageBox.Show("Cannot divide by zero", "Invalid operation", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }

                        calculationResult = (lastNumber / newNumber).ToString();
                        break;
                    }
                default:
                    {
                        calculationResult = "0";
                        break;
                    }
            }
            lblResult.Content = (calculationResult.Length >= 10) ? calculationResult.Substring(0, 10) : calculationResult;
        }

        /// <summary>
        /// Removes _ before the number and parses it
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private static int GetSelectedNumber(object sender)
        {
            int selectedNumber;
            if ((sender as Button)!.Content!.ToString()!.Contains("_"))
            {
                selectedNumber = int.Parse((sender as Button)!.Content!.ToString()!.Substring(1));
            }
            else
            {
                selectedNumber = int.Parse((sender as Button)!.Content!.ToString());
            }

            return selectedNumber;
        }
    }
}
