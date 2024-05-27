using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Models.Payment.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace E_commercial_Web_RESTAPI.Data
{
    public class ApplicationDBcontext : DbContext
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options)
        {


        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Payment> payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            

        }
    }
}
