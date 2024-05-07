using ApiProtection.StartupConfig;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching(); // the client can cache the date

// Rate limiting configuration
// We use Memory Cache for other things not only rate limiting
builder.Services.AddMemoryCache();

builder.AddRateLimitServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();
app.UseIpRateLimiting();

app.Run();
