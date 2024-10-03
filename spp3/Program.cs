using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using spp3.Data;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionBuilder = new SqlConnectionStringBuilder();
sqlConnectionBuilder.ConnectionString = "Server=127.0.0.1,1433;Initial Catalog=TradeCompany(CourseWork);User Id=sa;Password=Pa55w0rd!;TrustServerCertificate=true";
builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(sqlConnectionBuilder.ConnectionString));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore //убрать повторения
);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
