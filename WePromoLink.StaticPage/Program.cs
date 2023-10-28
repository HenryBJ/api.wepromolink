var builder = WebApplication.CreateBuilder(args);

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

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapGet("/", (HttpContext httpContext) =>
{
    var subdomain = httpContext.Request.Host.Host;
    return $"Welcome to Static Page - {DateTime.Now.ToShortDateString()}  Subdomain: {subdomain}";
});

// app.MapControllers();

app.Run();
