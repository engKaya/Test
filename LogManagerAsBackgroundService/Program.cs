using LogManagerAsBackgroundService.Extensions;
using LogManagerAsBackgroundService.Implements;
using LogManagerAsBackgroundService.Implements.Business;
using LogManagerAsBackgroundService.Interfaces.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<LogWorker>();   

builder.Services.AddSingleton<IBackgroundTaskQueue>(ctx => { 
    return new BackgroundTaskQueue();
});
builder.Services.AddSingleton<IBusinessService, BusinessService>();

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
