using ParmeniaHub.Infrastructure;
using ParmeniaHub.Application.Convocatorias.Crear;
using ParmeniaHub.Application.Convocatorias.Listar;
using ParmeniaHub.Application.Convocatorias.Obtener;
using ParmeniaHub.Application.Convocatorias.Publicar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<CrearConvocatoriaService>();
builder.Services.AddScoped<ListarConvocatoriasService>();
builder.Services.AddScoped<ObtenerConvocatoriaService>();
builder.Services.AddScoped<PublicarConvocatoriaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
