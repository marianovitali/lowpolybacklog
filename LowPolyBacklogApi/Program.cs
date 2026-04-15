using LowPolyBacklogApi.Data;
using LowPolyBacklogApi.Repositories.Implementations;
using LowPolyBacklogApi.Repositories.Interfaces;
using LowPolyBacklogApi.Services.Implementations;
using LowPolyBacklogApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var licenseKey = builder.Configuration["AutoMapper:LicenseKey"];

builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = licenseKey;
}, typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Low Poly API",
        Version = "v1",
        Description = "API personal para gestión de backlog de juegos de PS1."
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("name=DefaultConnection"));

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IBacklogRepository, BacklogRepository>();
builder.Services.AddScoped<IBacklogService, BacklogService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

//PIPELINE

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();


app.UseCors("AllowAll");
app.MapControllers();


app.Run();
