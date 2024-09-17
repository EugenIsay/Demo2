using Avalonia.Controls;
using System.Linq;

namespace Demo2
{
    public partial class MainWindow : Window
    {
        int page = 0;
        int shownAmount;
        public MainWindow()
        {
            InitializeComponent();
            Amount.SelectedIndex = 0;
        }
        public void ChangePage()
        {
            ClientsList.ItemsSource = PublicActions.Clients.OrderBy(c => c.Id).Skip((page * shownAmount)).Take(shownAmount);
            if (page == 0)
            {
                Back.IsEnabled = false;
            }
            else
            {
                Back.IsEnabled = true;
            }
            if ((page + 1) * shownAmount > PublicActions.Clients.Count - 1)
            {
                Forward.IsEnabled = false;
            }
            else
            {
                Forward.IsEnabled = true;
            }
        }

        private void PageButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Name == "Back")
            {
                page--;
            }
            else
            {
                page++;
            }
            ChangePage();
        }

        private void ShownAmounChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0: shownAmount = 10; break;
                case 1: shownAmount = 50; break;
                case 2: shownAmount = 200; break;
            }
            page = 0;
            ChangePage();
        }
    }
}