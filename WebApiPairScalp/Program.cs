using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IDbConnection>((s) =>
{
    IDbConnection conn = new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
    conn.Open();
    return conn;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5034", "http://82.97.241.41:5002", "http://api.chillacoin.ru", "https://api.chillacoin.ru");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });

});

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


app.UseCors();
app.MapControllers();

app.Run();


