using diegomoreno.Brq.bff;
using diegomoreno.Brq.Repository.Contexts.Entity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.RegisterServices();
builder.Services.AddDbContext<TrucksDbContext>(options =>
{
    if (connString != null) options.UseSqlServer(connString);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
