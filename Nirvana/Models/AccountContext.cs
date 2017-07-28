using System.Data.Entity;
using Nirvana.Models;

namespace Nirvana.Models
{
    public class AccountContext : DbContext
    {
        public AccountContext() : base("DefaultConnection") { }

        public DbSet<Login.Account> Accounts { get; set; }
    }
}
