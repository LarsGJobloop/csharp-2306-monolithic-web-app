using Microsoft.AspNetCore.Mvc;

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


// The Web API of our application
// Return a JSON object
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

// Parsing JSON Object recived from the frontend
app.MapPut("/todoitems/{id}", (int id, [FromBody] Todo todo ) => {
  Console.WriteLine(todo.Name);

  return Results.Accepted();
});


// Starting up and exposing the server to our local network
app.Run();

// The Todo Model (what data it contains)
public class Todo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}