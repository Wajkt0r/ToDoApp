using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) 
        { 

        }
        
        public DbSet<ToDoTask> Tasks { get; set; }
    }
}
