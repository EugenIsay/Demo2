using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.IO;
using System;
using Avalonia.Media.Imaging;
using Demo2.Models;
using System.Linq;

namespace Demo2;

public partial class EditWindow : Window
{
    int index = -1;
    public string FileToCopy = "";
    public string FileName = "";
    public string OldFile = "";
    public EditWindow()
    {
        InitializeComponent();
        Gender.ItemsSource = PublicActions.PublicContext.Genders.Select(g => g.Name);
    }
    public EditWindow(int i)
    {
        InitializeComponent();
        ImageButton.Content = "Изменить картинку";
        Gender.ItemsSource = PublicActions.PublicContext.Genders.Select(g => g.Name);

        Client client = PublicActions.Clients.ToList().First(c => c.Id == i);
        Id.Text = client.Id.ToString();
        Lastname.Text = client.Lastname;
        Firstname.Text = client.Firstname;
        Patronim.Text = client.Patronymic;
        Email.Text = client.Email;
        Phone.Text = client.Phone;
        BDay.SelectedDate = DateTimeOffset.Parse(client.Birthday.ToString());
        Gender.SelectedIndex = PublicActions.PublicContext.Genders.ToList().IndexOf(PublicActions.PublicContext.Genders.ToList().First(g => g.Code == client.Gendercode));
        OldFile = client.Photopath;
        Image.Source = new Bitmap(Environment.CurrentDirectory + "/" + OldFile);
    }
    private async void AddImage(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        var TopLevel = await openFileDialog.ShowAsync(this);
        FileToCopy = string.Join("", TopLevel);
        if (FileToCopy != "")
        {
            int pos = FileToCopy.LastIndexOf('.');
            string str = FileToCopy.Substring(pos, FileToCopy.Length - pos);
            Image.Source = new Bitmap(FileToCopy);
            FileName = System.Guid.NewGuid().ToString() + str;
        }
        ImageButton.Content = "Изменить картинку";
    }

    private void Comfirm(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(Lastname.Text) || string.IsNullOrEmpty(Firstname.Text) 
            || string.IsNullOrEmpty(Patronim.Text) || string.IsNullOrEmpty(Email.Text) 
            || string.IsNullOrEmpty(Phone.Text)|| Gender.SelectedIndex == null || !BDay.SelectedDate.HasValue )
        if (index != -1)
        {
            if (PublicActions.PublicContext.Clients.ToList().Where(t => t.Phone == Phone.Text).Count() != 0)
            {
                return;
            }
            Client client = new Client() { Lastname = Lastname.Text, Firstname = Firstname.Text, 
                Patronymic = Patronim.Text, Email = Email.Text, Phone = Phone.Text, Birthday = DateOnly.Parse(BDay.SelectedDate.ToString()), 
                Gendercode = PublicActions.PublicContext.Genders.ToList().First(g => g.Code == Gender.SelectedIndex).Code, Photopath = $"ClientsPhotos/{FileName}"
            };
            PublicActions.PublicContext.Clients.Add(client);
            File.Copy(FileToCopy, $"{Environment.CurrentDirectory}/ClientsPhotos/{FileName}");
            PublicActions.PublicContext.SaveChanges();
        }
        else
        {
            Client client = PublicActions.Clients.ToList().First(c => c.Id == index);
            client.Lastname = Lastname.Text;
            client.Firstname = Firstname.Text;
            client.Patronymic = Patronim.Text;
            client.Email = Email.Text;
            client.Phone = Phone.Text;
            client.Birthday = DateOnly.Parse(BDay.SelectedDate.ToString());
            client.Gendercode = PublicActions.PublicContext.Genders.ToList().First(g => g.Code == Gender.SelectedIndex).Code;
            client.Photopath = $"ClientsPhotos/{FileName}";
            if (FileToCopy != "")
            {
                File.Copy(FileToCopy, $"{Environment.CurrentDirectory}/ClientsPhotos/{FileName}");
                FileInfo file = new FileInfo(OldFile);
                file.Delete();
            }
            PublicActions.PublicContext.Clients.Update(client);
            PublicActions.PublicContext.SaveChanges();
        }
        new MainWindow().Show();
        this.Close();
    }
}