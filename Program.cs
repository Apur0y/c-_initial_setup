var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

//REST API 
app.MapGet("/", () => "Server is running");

app.MapGet("/user", () =>
{
    var response = new { message = "This is  a json Object", succces=true};

    return Results.Ok(response);
});

app.MapPost("/user", () =>
{
    return "Hi from POST";
});

app.MapPut("/user", () =>
{
    return "Hi from PUT";
});

app.MapDelete("/user", () =>
{
    return "Hi from DELETE";
});

var products = new List<Products>() {
    new Products("Apple", 299),
    new Products("Samsung", 399),
};

app.MapGet("/products", () =>
{
    return Results.Ok(products);
});

app.Run();


public record Products(string Name, decimal Price);

public record Category
{
     public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
}