using Nirvana.Models.Login;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nirvana.Models
{
    public class SettingContext : DbContext
    {
        public SettingContext() : base("DefaultConnection") { }

        public DbSet<Setting> Settings { get; set; }
    }
}
