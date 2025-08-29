using Microsoft.AspNetCore.Mvc;

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

var products = new List<Products>() {
    new Products("Apple", 299),
    new Products("Samsung", 399),
};

app.MapGet("/api/categories", ([FromQuery] string searchValue="") =>
{
    Console.WriteLine($"{searchValue}");
    if (!string.IsNullOrEmpty(searchValue))
    {
       var res= categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
            return Results.Ok(res);
    }
    
    return Results.Ok(categories);
});

app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
{
    if (string.IsNullOrEmpty(categoryData.Name))
    {
        return Results.BadRequest("Catefory name required and not be empty");
    }
    // Console.WriteLine($"This is my bojdy: {categoryData}");
    var category = new Category
    {
        CategoryId = Guid.NewGuid(),
        Name =categoryData.Name,
        Description=categoryData.Description,
        CreatedAt = DateTime.Now,
    };
    categories.Add(category);
    return Results.Created($"/api/categories/{category.CategoryId}", category);

}); 

app.MapDelete("/api/categories/{categoryId:guid}", (Guid categoryId) =>
{
    var foundCategory = categories.FirstOrDefault(Category => Category.CategoryId == categoryId);
    Console.WriteLine(foundCategory);
    if (foundCategory is null)
    {
        return Results.NotFound("Category with this id no not");
    }
    categories.Remove(foundCategory);
    return Results.NoContent();
}); 

app.MapPut("/api/categories/{categoryId}", (Guid categoryId, [FromBody] Category categoryData) =>
{
    var foundCategory = categories.FirstOrDefault(Category => Category.CategoryId == categoryId);
    Console.WriteLine(foundCategory);
    if (foundCategory is null)
    {
        return Results.NotFound("Category with this id not found");
    }
    if (categoryData is null)
    {
        return Results.BadRequest("CategoryData is missing!");
    }
    if (!string.IsNullOrEmpty(categoryData.Name))
    {
        if (categoryData.Name.Length > 2)
        {

            foundCategory.Name = categoryData.Name;
        }
        else
        {
           return Results.BadRequest("Name must be at least two charecter");
        }
    }
    if (!string.IsNullOrEmpty(categoryData.Description))
    {
        foundCategory.Description = categoryData.Description;
    }
    return Results.Ok(foundCategory);
}); 

app.Run();


public record Products(string Name, decimal Price);

public record Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
}