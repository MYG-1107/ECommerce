using ECommerce.API.Data.Seed;
using ECommerce.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseGlobalExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ClientCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await DataSeeder.SeedAsync(app.Services, app.Configuration);

app.Run();
