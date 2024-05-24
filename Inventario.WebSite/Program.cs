using Inventario.WebSite.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5094);
    options.ListenAnyIP(7031, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IRegistroMaterialService, RegistroMaterialService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IDetallePedidoService, DetallePedidoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IOrdenCompraService, OrdenCompraService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();