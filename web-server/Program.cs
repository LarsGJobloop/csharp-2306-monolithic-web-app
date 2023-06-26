var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var todoes = new System.Web.HttpUtility();

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

  var todoes = new {
    body = new[]
    {
      new { Id = 0, Name = "Walk dog", IsComplete = false},
      new { Id = 1, Name = "Feed Shark", IsComplete = false},
      new { Id = 2, Name = "Remember towel", IsComplete = false},
    }
  };

  return Results.Ok(todoes);
});


// Starting up and exposing the server to our local network
app.Run();