using Demo2.Context;
using Demo2.Models;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Demo2
{
    public static class PublicActions
    {
        public static IsajkinContext PublicContext = new IsajkinContext();
        public static List<Client> Clients = PublicContext.Clients.Include(x=>x.Tags).ToList();
        public static List<String> Genders = new List<string>() { "Все типы" }.Concat(PublicContext.Genders.ToList().Select(g => g.Name).ToList()).ToList();
        public static string ShowmClientAmount = $"{Clients.Count}/{PublicContext.Clients.ToList().Count()}";
        public static void ClientsActions(int s, int f, string srch)
        {
            Clients.Clear();
            Clients = PublicContext.Clients.Include(x => x.Tags).ToList();

            switch (s)
            {
                case 0:
                    Clients = Clients.OrderBy(c => c.Id).ToList();
                    break;
                case 1:
                    Clients = Clients.OrderBy(c => c.Lastname).ToList();
                    break;
                case 3:
                    Clients = Clients.OrderByDescending(c => c.Amount).ToList();
                    break;
                case 2:
                    Clients = Clients.Where(s => s.LastService != "Нет").OrderByDescending(c => DateTime.Parse(c.LastService)).ToList().Concat(Clients.Where(s => s.LastService == "Нет")).ToList();
                    break;

            }
            if (f != 0)
            {
                Clients = Clients.Where(c => c.Gendercode == PublicContext.Genders.ToList().FirstOrDefault(g => g.Name == Genders[f]).Code).ToList();
            }
            List<string> words = srch.Split(' ').ToList();
            foreach (string word in words)
            {
                Clients = Clients.Where(c => c.Firstname.ToLower().Contains(word.ToLower()) || c.Lastname.ToLower().Contains(word.ToLower()) || c.Patronymic.ToLower().Contains(word.ToLower()) ||
                c.Phone.ToLower().Contains(word.ToLower()) || c.Email.ToLower().Contains(word.ToLower())).ToList();
            }
            ShowmClientAmount = $"{Clients.Count}/{PublicContext.Clients.ToList().Count()}";

        }

    }
}
