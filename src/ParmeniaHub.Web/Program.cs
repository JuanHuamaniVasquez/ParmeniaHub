using ParmeniaHub.Infrastructure;
using ParmeniaHub.Application.Convocatorias.Crear;
using ParmeniaHub.Application.Convocatorias.Listar;
using ParmeniaHub.Application.Convocatorias.Obtener;
using ParmeniaHub.Application.Convocatorias.Publicar;
using ParmeniaHub.Application.Entregables;
using ParmeniaHub.Application.Herramientas;
using ParmeniaHub.Application.Postulaciones;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<CrearConvocatoriaService>();
builder.Services.AddScoped<ListarConvocatoriasService>();
builder.Services.AddScoped<ObtenerConvocatoriaService>();
builder.Services.AddScoped<PublicarConvocatoriaService>();
builder.Services.AddScoped<CrearPostulacionService>();
builder.Services.AddScoped<ListarPostulacionesService>();
builder.Services.AddScoped<ObtenerPostulacionService>();
builder.Services.AddScoped<AvanzarPostulacionService>();
builder.Services.AddScoped<CrearEntregableService>();
builder.Services.AddScoped<ListarEntregablesService>();
builder.Services.AddScoped<ListarEntregablesPorPostulacionService>();
builder.Services.AddScoped<CambiarEstadoEntregableService>();
builder.Services.AddScoped<EvaluarAvancePostulacionService>();
builder.Services.AddScoped<CalcularProgresoProyectoService>();

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
