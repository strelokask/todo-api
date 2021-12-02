using Microsoft.EntityFrameworkCore;
using Todo.Core;

namespace Todo.DAL
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
              : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
