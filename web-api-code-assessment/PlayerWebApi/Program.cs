// /////////////////////////////////////////////////////////////////////////////
// PLEASE DO NOT RENAME OR REMOVE ANY OF THE CODE BELOW. 
// YOU CAN ADD YOUR CODE TO THIS FILE TO EXTEND THE FEATURES TO USE THEM IN YOUR WORK.
// /////////////////////////////////////////////////////////////////////////////
using Microsoft.EntityFrameworkCore;
using PlayerWebApi.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PlayerDbContext>(options => options.UseInMemoryDatabase("PlayerWebApiDb"));

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    PlayerDbContext dbContext = scope.ServiceProvider.GetRequiredService<PlayerDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseRouting();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Player API V1");
    options.RoutePrefix = string.Empty;
});

app.Run();