using MediatR;
using PersonSrv.API.Services;
using PersonSrv.Application.Behaviors;
using PersonSrv.Domain.Repositories;
using PersonSrv.Infrastructure.Data;
using PersonSrv.Infrastructure.Repositories;
using Serilog;
using System.Reflection;
using FluentValidation;

using PersonSrv.API.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using PersonSrv.Application;
using PersonSrv.Infrastructure;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    //HTTPS for gRPC(HTTP / 2)

   options.ListenLocalhost(7097, o =>
   {
       o.Protocols = HttpProtocols.Http2;
       //o.UseHttps(); // Use development certificate
   });

    //HTTP for browser(HTTP / 1.1)
   options.ListenLocalhost(5097, o =>
   {
       o.Protocols = HttpProtocols.Http1;
   });
});

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

Log.Information("Starting gRPC server...");

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<GlobalErrorInterceptor>();
});

//builder.Services.AddScoped<PersonGrpcService>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.MapGrpcService<PersonGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");


app.Run();
