var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serving static files (HTML, CSS, JS, JPG, etc)

app.UseStaticFiles();

// Returning the index html, so the user does not have to specify: www.ourpage.com/index.html
app.MapGet("/", () => {
  return Results.File("index.html", "text/html");
});
// Redirecting to base URL if the user tries to go to www.ourpage.com/index.html
app.MapGet("/index.html", () => Results.Redirect("/"));


// Small API for our application
app.MapGet("/todoes", () => {
  return Results.Json(new {todoes = "All the todoes"});
});


// Starting up and exposing the server to our local network
app.Run();