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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebBrowserNameSpace
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Создать приложение, которое имитирует работу браузера. Предусмотреть вкладки и их добавление, а также строку ввода урл.

		public MainWindow()
		{
			InitializeComponent();
		}

		private void tabControlLoaded(object sender, RoutedEventArgs e) // Инициализация tabControl при запуске программы
		{
			tabControl = (sender as TabControl);
		}

		TabItem tabItem; // Находится здесь для удобства доступности методов

		private void searchByTextButtonClick(object sender, RoutedEventArgs e)
		{
			try
			{
				WebBrowser browser = new WebBrowser();
				browser.Navigate($"https://www.google.com/search?q={searchByText.Text}");

				if(searchByText.Text == "")
				{
					//searchByText.Text = "Главная страница";
					MessageBox.Show("Введите текст"); // Либо же выдать ошибку о невозможности ввода пустой строки
					return;
				}

				tabItem = new TabItem
				{
					Height = 20,
					Width = 80,
					Header = searchByText.Text,
					Content = (browser as WebBrowser)
				};

				tabItem.MouseDoubleClick += tabItemMouseDoubleClick;	// Добавление действия на двойной клик по вкладке

				tabControl.Items.Add(tabItem);	//	Добавление вкладки в tabControl

				tabItem.IsSelected = true;  // Переключение на новую вкладку

				searchByText.Text = "";	// Удобнее работать при очищении строки поиска
			}
			catch
			{
				MessageBox.Show("Не представляю как, но вам удалось ввести неверный поисковой запрос.");
			}
		}

		private void searchByUrlButtonClick(object sender, RoutedEventArgs e)
		{
			try
			{
				WebBrowser browser = new WebBrowser();
				browser.Navigate(searchByUrl.Text);

				tabItem = new TabItem
				{
					Height = 20,
					Width = 80,
					Header = "Url Tab",
					Content = (browser as WebBrowser)
				};

				tabItem.MouseDoubleClick += tabItemMouseDoubleClick;

				tabControl.Items.Add(tabItem);

				tabItem.IsSelected = true;

				searchByUrl.Text = "";
			}
			catch
			{
				MessageBox.Show("Введен неверный URL адрес");
			}
		}

		private void tabItemMouseDoubleClick(object sender, MouseButtonEventArgs e) // Реализовал через двойной клик так как вижу это наиболее лаконичным и простым
		{
			tabControl.Items.Remove(tabControl.SelectedItem);
		}
	}
}
