using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo2.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateOnly? Birthday { get; set; }

    public DateTime Registrationdate { get; set; }

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public char Gendercode { get; set; }

    public string Gender { get => PublicActions.PublicContext.Genders.ToList().FirstOrDefault(g => g.Code == Gendercode).Name; }

    public string? Photopath { get; set; }

    public Bitmap Image
    {
        get
        {
            try
            {
                return new Bitmap($"{Environment.CurrentDirectory}\\{Photopath}");
            }
            catch
            {
                return new Bitmap($"{Environment.CurrentDirectory}\\ClientsPhotos\\stock_photo.png");
            }
        }
    }

    public virtual ICollection<Clientservice> Clientservices { get; set; } = new List<Clientservice>();

    public virtual Gender GendercodeNavigation { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
