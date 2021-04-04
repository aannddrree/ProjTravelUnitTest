using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjTravelUnitTest.Api.Models;

    public class TravelContext : DbContext
    {

    public TravelContext(DbContextOptions<TravelContext> options) : base(options)
    {
    }

    public DbSet<Client> Client { get; set; }

}
