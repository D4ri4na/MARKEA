using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", app =>
    {
        app.WithOrigins("http://localhost:8080")
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<MongoDbService>();

builder.Services.AddScoped<ProductoRepositorio>();
builder.Services.AddScoped<ProductoService>();

builder.Services.AddScoped<VentaRepositorio>();
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<RepositorioUsuario>();
builder.Services.AddSingleton<ServiciosUsuarios>();


var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("en-US") };

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(localizationOptions);
app.UseMiddleware<ErrorHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();

app.UseCors("AllowReactApp"); 

app.UseAuthorization();

app.MapControllers();

app.Run();