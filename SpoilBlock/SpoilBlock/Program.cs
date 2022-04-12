using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpoilBlock.DAL.Abstract;
using SpoilBlock.DAL.Concrete;
using SpoilBlock.Data;
using SpoilBlock.Models;
using SpoilBlock.DAL.Abstract;
using SpoilBlock.DAL.Concrete;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IWoopUserMediumRepository, WoopUserMediumRepository>();
builder.Services.AddScoped<IWoopuserRepository, WoopuserRepository>();


builder.Services.AddSingleton<IAPIKeyAccessor>(k => new APIKeyAccessor(builder.Configuration["IMDbServiceApiKey"]));
//builder.Services.AddSingleton<IAPIKeyAccessor>(k => new APIKeyAccessor(builder.Configuration["IMDbToken:IMDbNewShowsApiKey"]));


//var connectionString = builder.Configuration.GetConnectionString("WOOPServerConnection"); builder.Services.AddDbContext<WOOPDbContext>(options =>
//    options.UseSqlServer(connectionString));

//var identityConnectionString = builder.Configuration.GetConnectionString("WOOPIdServerConnection"); builder.Services.AddDbContext<IdentityDbContext>(options =>
//     options.UseSqlServer(identityConnectionString)); builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//      .AddEntityFrameworkStores<IdentityDbContext>();

//Local connection string
var connectionString = builder.Configuration.GetConnectionString("WOOPServerConnection"); builder.Services.AddDbContext<WOOPDbContext>(options =>
    options.UseSqlServer(connectionString));

var identityConnectionString = builder.Configuration.GetConnectionString("WOOPIdentityServerConnection"); builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(identityConnectionString)); builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
     .AddEntityFrameworkStores<IdentityDbContext>();
//Local connection string

builder.Services.AddScoped<IAddMediaService, AddMediaService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IWoopuserRepository, WoopuserRepository>();
builder.Services.AddScoped<IWoopUserMediumRepository, WoopUserMediumRepository>();
builder.Services.AddScoped<IMediumRepository, MediumRepository>();
builder.Services.AddScoped<DbContext, WOOPDbContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IIMDbSearchService, IMDbSearchService>();
builder.Services.AddScoped<IIMDbNewShowsService, IMDbNewShowsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();

                });




app.Run();
