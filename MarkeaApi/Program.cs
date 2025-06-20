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