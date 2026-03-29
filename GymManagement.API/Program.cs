using GymManagement.API.Services;
using GymManagement.Core.Interfaces;
using GymManagement.Infrastructure.Data;
using GymManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using GymManagement.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure CORS for Angular Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular default port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure Entity Framework Core with InMemory
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseInMemoryDatabase("GymDb"));

// Dependency Injection
builder.Services.AddScoped<IGymRepository, GymRepository>();
builder.Services.AddScoped<IGymService, GymService>();

var app = builder.Build();

// Seed Sample Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GymDbContext>();
    SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseCors("AllowAngular");

// Important for routing
app.UseRouting();

app.MapControllers();

app.Run();

void SeedData(GymDbContext context)
{
    if (context.Trainers.Any()) return;

    var trainers = new List<Trainer>
    {
        new Trainer { Name = "Michael Jordan", Specialization = "Strength & Conditioning" },
        new Trainer { Name = "Serena Williams", Specialization = "Flexibility & Yoga" },
        new Trainer { Name = "Arnold Schwarzenegger", Specialization = "Bodybuilding" }
    };
    context.Trainers.AddRange(trainers);

    var plans = new List<Plan>
    {
        new Plan { PlanName = "3 Months", DurationMonths = 3, Price = 80 },
        new Plan { PlanName = "6 Months", DurationMonths = 6, Price = 150 },
        new Plan { PlanName = "9 Months", DurationMonths = 9, Price = 220 },
        new Plan { PlanName = "12 Months", DurationMonths = 12, Price = 280 }
    };
    context.Plans.AddRange(plans);
    context.SaveChanges();

    var members = new List<Member>
    {
        new Member { Name = "John Smith", Age = 28, Phone = "555-0101", JoinDate = DateTime.UtcNow.AddMonths(-2), PlanId = plans[0].PlanId, TrainerId = trainers[0].TrainerId },
        new Member { Name = "Emma Watson", Age = 24, Phone = "555-0102", JoinDate = DateTime.UtcNow.AddMonths(-1), PlanId = plans[1].PlanId, TrainerId = trainers[1].TrainerId },
        new Member { Name = "Robert Downey", Age = 35, Phone = "555-0103", JoinDate = DateTime.UtcNow.AddMonths(-3), PlanId = plans[2].PlanId, TrainerId = trainers[2].TrainerId },
        new Member { Name = "Scarlett Johansson", Age = 29, Phone = "555-0104", JoinDate = DateTime.UtcNow.AddDays(-15), PlanId = plans[3].PlanId, TrainerId = trainers[0].TrainerId },
        new Member { Name = "Chris Evans", Age = 31, Phone = "555-0105", JoinDate = DateTime.UtcNow.AddDays(-5), PlanId = plans[0].PlanId, TrainerId = trainers[2].TrainerId }
    };
    context.Members.AddRange(members);
    context.SaveChanges();
}
