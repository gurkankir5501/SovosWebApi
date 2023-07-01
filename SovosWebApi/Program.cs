
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;
using SovosWebApi.Core.MailServer;
using SovosWebApi.Core.Repositories;
using SovosWebApi.Core.Validator;
using SovosWebApi.JobSchedulers;
using SovosWebApi.Repository.Repositories;
using SovosWebApi.Repository.RepositoryContext;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//test
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.IgnoreNullValues = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.
    SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("Conn").Value,
        x => x.MigrationsAssembly(typeof(Context).Assembly.GetName().Name));
    options.EnableSensitiveDataLogging(true);

});


builder.Services.AddScoped(typeof(IInvoiceHeaderRepository), typeof(InvoiceHeaderRepository));
builder.Services.AddScoped(typeof(IInvoiceLineRepository), typeof(InvoiceLineRepository));
builder.Services.AddScoped(typeof(IMailer), typeof(Mailer));
builder.Services.AddSingleton(typeof(IJobScheduler), typeof(JobScheduler));
builder.Services.AddValidatorsFromAssembly(Assembly.Load(typeof(InvoiceHeaderValidator).Assembly.GetName().Name?.ToString()));


// Serilog yapýlandýrmasý
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(new JsonFormatter(), "Logger/log.json", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddLogging(s =>
{
    //s.AddConsole().AddConfiguration();
    s.ClearProviders();
    s.AddSerilog();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Context>();
    if (!dbContext.Database.CanConnect())
        dbContext.Database.Migrate();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
