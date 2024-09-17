using Demo2.Context;
using Demo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2
{
    public static class PublicActions
    {
        public static IsajkinContext PublicContext = new IsajkinContext();
        public static List<Client> Clients = PublicContext.Clients.ToList();
    }
}
