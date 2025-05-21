using Amazon.S3;
using AWSPractice.Data;
using AWSPractice.Helper;
using AWSPractice.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string conectionString = builder.Configuration.GetConnectionString("MySql");
builder.Services.AddDbContext<PersonajesContext>(options =>
    options.UseMySQL(conectionString));
builder.Services.AddTransient<HelperBuckets>(); 
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<Repository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Personajes}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
