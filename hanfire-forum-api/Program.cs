using HanfireForum.Data.DataServices;
using HanfireForum.Data.EntityFramework;
using Hangfire;
using Hangfire.Redis.StackExchange;
using HangfireForum.Services.PaymentService;
using HangfireForum.Services.PaymentSubmissionService;
using HangfireForum.Services.SuspenseTransferService;
using HangfireForum.Services.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPaymentDataService, PaymentDataService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPaymentValidationService, PaymentValidationService>();
builder.Services.AddScoped<ISuspenseTransferService, SuspenseTransferService>();
builder.Services.AddScoped<IPaymentSubmissionService, PaymentSubmissionService>();

builder.Services.AddHangfire(configuration =>
{
    configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseRedisStorage(
            builder.Configuration.GetConnectionString("Redis")!);
});

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
