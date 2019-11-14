using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Domain;

namespace TODO.FRM
{
    public class ToDoContext : DbContext
    {
        public ToDoContext() : base("DefaultConnection")
        {

        }

        public override int SaveChanges()
        {
            return base.SaveChanges();

        }
        public DbSet<User> Users { get; set; }
        public DbSet<BoardCard> Boards { get; set; }
    }

}
