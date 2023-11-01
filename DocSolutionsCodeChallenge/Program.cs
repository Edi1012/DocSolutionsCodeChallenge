using DocSolutionsCodeChallenge.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure the application to read the connection string from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");

// Register your EmployeeRepository as a service using the connection string from appsettings.json
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IEmployeeRepository>(s => new EmployeeRepository(connectionString));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
