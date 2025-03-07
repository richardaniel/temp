
using Microsoft.EntityFrameworkCore;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios.HashPassword;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
//using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;
using Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;
using Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes;
using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Infraestructure.Core.Entity.Standard;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.StrategyPais;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Transportistas;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Roles;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Notificaciones;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Dispositivos;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Helpers;



var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        corsBuilder =>
        {
            if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Staging"))
            {
                corsBuilder
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            }
            else
            {
                corsBuilder
                .WithOrigins("https://*.grupofarsiman.com", "https://*.grupofarsiman.io")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            }
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Domain
builder.Services.AddScoped<ColaboradorAppDomain>();
builder.Services.AddScoped<ColaboradorSucursalAppDomain>();
builder.Services.AddScoped<ValidateEmailDomain>();
builder.Services.AddScoped<AprobacionSolicitudAppDomain>();
builder.Services.AddScoped<SolicitudViajeAppDomain>();
builder.Services.AddScoped<TransportistaAppDomain>();
builder.Services.AddScoped<SucursalAppDomain>();
builder.Services.AddScoped<ViajeAppDomain>();


builder.Services.AddSingleton<MonedaStrategyFactory>();
builder.Services.AddScoped<IMonedaStrategy>(provider =>
{
    var factory = provider.GetRequiredService<MonedaStrategyFactory>();
    return factory.CreateStrategy();
});




//Infrastructure
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = true; 
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                   ValidAudience = builder.Configuration["JwtSettings:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),
               };
           });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GerenteOnly", policy => policy.RequireRole("Gerente de Tienda"));
});

//builder.Services.AddDbContext<SistemaTransporteContext>(options =>
//{
//   options.UseSqlServer(builder.Configuration.GetConnectionString("SistemaTransporte"));
//});











var isTesting = builder.Environment.IsEnvironment("Test");

if (!isTesting) // SOLO REGISTRAR SQL SERVER SI NO ESTAMOS EN MODO TEST
{
    builder.Services.AddDbContext<SistemaTransporteContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SistemaTransporte"));
    });

    builder.Services.AddScoped<IUnitOfWork>(serviceProvider =>
    {
        var dbContext = serviceProvider.GetRequiredService<SistemaTransporteContext>();
        return new UnitOfWork(dbContext);
    });
}









builder.Services.AddScoped<IUnitOfWork>(serviceProvider =>
{
    var dbContext = serviceProvider.GetRequiredService<SistemaTransporteContext>();
    return new UnitOfWork(dbContext);
});







// Application
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ColaboradorService>();
builder.Services.AddScoped<SucursalService>();
builder.Services.AddScoped<ColaboradorSucursalService>();
builder.Services.AddScoped<SolicitudViajeService>();
builder.Services.AddScoped<AprobacionSolicitudService>();
builder.Services.AddScoped<ViajeService>();
builder.Services.AddScoped<RolService>();
builder.Services.AddScoped<NotificacionService>();
builder.Services.AddScoped<DispositivoService>();

builder.Services.AddScoped<IGenericValidatorService, GenericValidatorService>();

builder.Services.AddScoped<RutasService>();
builder.Services.AddScoped<IHashPassword, HashPasswordService>();
builder.Services.AddHttpClient<LocationService>();



//Maps
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }