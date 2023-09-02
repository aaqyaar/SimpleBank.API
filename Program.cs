using Microsoft.AspNetCore.StaticFiles;
using Serilog;
using SimpleBank.API;
using SimpleBank.API.DbContexts;
using SimpleBank.API.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().WriteTo.File("logs/branch.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();

// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers((options) => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<BranchDataStore>();
builder.Services.AddDbContext<BranchContext>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());    
#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
