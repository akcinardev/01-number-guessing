using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumberGuessing
{
	public partial class MainWindow : Window
	{
		int correctNumber;
		public MainWindow()
		{
			InitializeComponent();
			correctNumber = CreateRandomNumber();
		}

		private bool IsUserGuessCorrect(int userGuess, int correctNumber)
		{
			return userGuess == correctNumber;
		}

		private int CreateRandomNumber()
		{
			Random rnd = new Random();
			int num = rnd.Next(0, 11);
			return num;
		}

		private void GuessButton_Clicked(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(guessedNumTbox.Text))
			{
				int userGuess = Int32.Parse(guessedNumTbox.Text);
				if (IsUserGuessCorrect(userGuess, correctNumber))
				{
					ShowResult("correct");
				}
				else
				{
					ShowResult("false");
				}
			}
			else
			{
				resultLabel.Content = "Enter a valid number!";
				resultLabel.Background = Brushes.Orange;
			}
		}

		private void NumberValidationTbox(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void StartButton_Clicked(object sender, RoutedEventArgs e)
		{
			startButton.Visibility = Visibility.Hidden;
			guessingStackPanel.Visibility = Visibility.Visible;
		}

		private void ShowResult(string result)
		{
			if (result == "correct")
			{
				resultLabel.Content = $"Your guess is true. The number is: {guessedNumTbox.Text}";
				resultLabel.Background = Brushes.GreenYellow;
			}
			else if (result == "false")
			{
				resultLabel.Content = $"Your guess is false. The number was: {correctNumber}";
				resultLabel.Background = Brushes.OrangeRed;
			}
			resultLabel.Visibility = Visibility.Visible;
			retryButton.Visibility = Visibility.Visible;
			guessButton.IsEnabled = false;
		}

		private void RetryButton_Clicked(object sender, RoutedEventArgs e)
		{
			guessedNumTbox.Clear();
			guessButton.IsEnabled = true;
			retryButton.Visibility = Visibility.Hidden;
			resultLabel.Content = "";
			resultLabel.Visibility = Visibility.Hidden;
			correctNumber = CreateRandomNumber();
		}
	}
}