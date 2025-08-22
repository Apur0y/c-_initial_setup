var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}
app.UseHttpsRedirection();

//REST API 
app.MapGet("/", () => {
    return "Server is running";
});
app.MapGet("/user", () =>
{
    return "Hello From get";
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

app.Run();

