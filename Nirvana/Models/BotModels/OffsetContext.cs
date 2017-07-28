using System.Data.Entity;

namespace Nirvana.Models.BotModels
{
    public class OffsetContext : DbContext
    {
        public OffsetContext() : base("DefaultConnection") { }

        public DbSet<Offset> Offsets { get; set; }
    }
}
