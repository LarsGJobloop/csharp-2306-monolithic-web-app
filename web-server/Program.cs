var builder = WebApplication.CreateBuilder(args);

// Setup the JSON De-/Serializer

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

app.MapPut("/todoitems/{id}", async (int id, HttpContent content) => {
  Console.WriteLine($"Toggling status of todo: {id}");

  Todo? jsonPayload = await content.ReadFromJsonAsync<Todo>();

  if (jsonPayload == null) {
    Console.WriteLine("Help");
  }

  Console.WriteLine(jsonPayload);

  return Results.Ok();
});


// Starting up and exposing the server to our local network
app.Run();

public class Todo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}