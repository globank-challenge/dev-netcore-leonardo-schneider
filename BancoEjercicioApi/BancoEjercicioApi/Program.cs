using Microsoft.EntityFrameworkCore;
using BancoEjercicioApi.DataAccess;
using BancoEjercicioApi.DataAccess.UnitOfWork;
using BancoEjercicioApi.Services;
using BancoEjercicioApi.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Indica el motor y el conecction string de la db
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IClienteService), typeof(ClienteService));
builder.Services.AddScoped(typeof(ICuentaService), typeof(CuentaService));
builder.Services.AddScoped(typeof(IMovimientoService), typeof(MovimientoService));
builder.Services.AddScoped(typeof(IReportesService), typeof(ReportesService));


#region Manejo de Excepciones Custom

builder.Services.AddMvc(options =>
{
    options.Filters.Add(item: new HttpExceptionFilter());
});

#endregion Manejo de Excepciones Custom


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
