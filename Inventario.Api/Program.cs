using Dapper.Contrib.Extensions;
using Inventario.Api.DataAccess;
using Inventario.Api.DataAccess.Interfaces;
using Inventario.Api.Repositories;
using Inventario.Api.Repositories.Interfaces;
using Inventario.Api.Repositories.Interfecies;
using Inventario.Api.Services;
using Inventario.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<IProductCategoryRepository, InMemoryProductCategoryRepositoty>();

//lanzar la aplicacion con las interfaces , para cuando al momento de lanzarlo, el programa trabaje con ello.

builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IRegistroMaterialRepository, RegistroMaterialRepository>();
builder.Services.AddScoped<IRegistroMaterialService, RegistroMaterialService>();
builder.Services.AddScoped<IOrdenCompraRepository, OrdenCompraRepository>();
builder.Services.AddScoped<IOrdenCompraService, OrdenCompraService>(); 
builder.Services.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();
builder.Services.AddScoped<IDetallePedidoService, DetallePedidoService>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IDbContext, DbContext>();

var app = builder.Build();
//mapeo
SqlMapperExtensions.TableNameMapper = entityType =>
{
    var name = entityType.ToString();
    if (name.Contains("Inventario.Core.Entities."))
        name = name.Replace("Inventario.Core.Entities.", "");
    var letters = name.ToCharArray();
    letters[0] = char.ToUpper(letters[0]);
    return new string(letters);
};


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