using Bitinvest.Infra.Context;
using Bitinvest.Infra.Repositories;
using Bitinvest.Infra.Identity;
using Microsoft.EntityFrameworkCore;
using Bitinvest.App.Notifications;
using Bitinvest.App.Services;
using Bitinvest.App.Configurations;
using Bitinvest.App.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => opt.Filters.Add<LoggingFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddScoped<INotificator, Notificator>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IContaCorrenteReaisService, ContaCorrenteReaisService>();
builder.Services.AddScoped<IContaCriptoService, ContaCriptoService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddDbContext<BitInvestDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRepositories();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfig();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
