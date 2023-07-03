using Integrated_Systems_Homework;
using ISH.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContextAndIdentity(builder.Configuration);
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerSecurity();

builder.Services.Configure(builder.Configuration);

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddStripeInfrastructure(builder.Configuration);
builder.Services.AddMvc();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dataContext.Database.Migrate();
    await ApplicationInitializer.GenerateRoles(scope.ServiceProvider);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("FrontendOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();