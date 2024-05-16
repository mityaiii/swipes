using Swipes.Api.Hubs;
using Swipes.Api.Middlewares;
using Swipes.Bll.Extensions;
using Swipes.Dal.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddLogging();
services.AddControllers();

services.AddSignalR();

services
    .AddDalInfrastructure(builder.Configuration)
    .AddDataAccessExtensions()
    .AddServiceExtensions();

services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();

app.MapHub<LobbyHub>("/api/lobby");

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.MigrateUp();

app.Run();
