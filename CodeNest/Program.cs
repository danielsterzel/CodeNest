using CodeNest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// we add a database context in order to allow for access to the database and allowing for modifying data in the database
// connection string is what allows the EF framework connection to database
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Database")));

// Registers an in-memory cache service.
// Required for session state and optional for caching data.
// Stores data in server RAM (not persistent, not shared across servers).
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // HTTP is a stateless protocol. Each request is independent and the server doesn't remember anything about previous requests
    // Sometimes we want to maintain a state between the user and the HTTP server.
    // Cookies are used to create a unique session ID in the client's browser which is sent to the server with each request
    // This allows to associate the user with user-specific data stored on the server side(e.g. logged-in user info, shopping cart, etc.)
    
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true; // --> Denies JS access to Cookies. Prevents some malicious attacks.
    options.Cookie.IsEssential = true;
});




static void CreateAdmin(AppDbContext context)
{
    if (!context.Users.Any(u => u.Role == "Admin"))
    {
        var hasher = new PasswordHasher<User>();
        
        var admin = new User()
        {
            UserName = "Admin",
            FirstName = "admin",
            LastName = "admin",
            Email = "admin@gmail.com",
            Phone = "111111111",
            Role = "Admin",
            Password = ""
        };

        admin.Password = hasher.HashPassword(admin, "qwerty13");
        context.Users.Add(admin);
        context.SaveChanges();
    }
    
    
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    CreateAdmin(dbContext);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
