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

List<Category> categories = new List<Category>(); 

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

app.MapPost("/api/categories", () =>
{
    var category = new Category
    {
        CategoryId = Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"
),
        Name = "Samsung",
        Description = "This the best phone",
        CreatedAt = DateTime.Now,
    };
    categories.Add(category);
    return Results.Created($"/api/categories/{category.CategoryId}", category);
}); 

app.MapDelete("/api/categories", () =>
{
    var foundCategory = categories.FirstOrDefault(Category => Category.CategoryId == Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"
));
    if (foundCategory is null)
    {
        return Results.NotFound("Category with this id no not");
    }
    categories.Remove(foundCategory);
    return Results.NoContent();
}); 

app.MapPut("/api/categories", () =>
{
    var foundCategory = categories.FirstOrDefault(Category => Category.CategoryId == Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301"
));
    if (foundCategory is null)
    {
        return Results.NotFound("Category with this id no not");
    }
         foundCategory.Name = "Updated Name";
         foundCategory.Description = "Updated Description";
    return Results.NoContent();
}); 

app.Run();


public record Products(string Name, decimal Price);

public record Category
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    
}