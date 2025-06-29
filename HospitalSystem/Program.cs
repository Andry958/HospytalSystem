using HospitalSystem.Models.DB;
using HospitalSystem.Models.Services;
using HospitalSystem.Models.Servies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<RegisterC_service>();
builder.Services.AddScoped<PatientEditorC_service>();
builder.Services.AddScoped<MedicationC_service>();
builder.Services.AddScoped<LoginC_service>();
builder.Services.AddScoped<AutoFillC_service>();
builder.Services.AddScoped<GetInformationC_service>();
builder.Services.AddScoped<DoctorsC_service>();

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
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
