using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using spp3P.Data;
using System;
using System.Data.SqlClient;

namespace spp3P;

class Program
{
    public ShopContext CreateDbContext(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var sqlConnectionBuilder = new SqlConnectionStringBuilder();
        sqlConnectionBuilder.ConnectionString = "";
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(sqlConnectionBuilder.ConnectionString));
    }

    static void Main(string[] args)
    {
        Program p = new Program();

        using (StudentContext sc = p.CreateDbContext(null))
        {
            sc.Database.Migrate();

            sc.Students.AddRange
            (
                new Student { Name = "Isaac Newton" },
                new Student { Name = "C.F. Gauss" },
                new Student { Name = "Albert Einstein" }
            );

            sc.SaveChanges();
        }
    }
}