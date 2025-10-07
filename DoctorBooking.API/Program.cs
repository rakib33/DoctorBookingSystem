using DoctorBooking.API.Extention;
using DoctorBooking.API.Middlewares;
using DoctorBooking.Application.Mappings;
using DoctorBooking.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.DatabaseConnectionConfig();
builder.RegisterDependency();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors("CorsPolicy");

// Add global exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
