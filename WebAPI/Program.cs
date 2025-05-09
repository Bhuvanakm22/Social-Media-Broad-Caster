using WebAPI.Middleware;
using BoardCasterWebAPI.Model;
var builder = WebApplication.CreateBuilder(args);


//Register IHttpClientFactory (best practice for managing HttpClient instances)
builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
//});

//Add cors services
builder.Services.AddCors(
    options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    });

var app = builder.Build();

//Custom middleware to track error
app.UseExceptionHandleMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
app.UseSwaggerUI();
//app.UseSwaggerUI(
//    c=>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo web API");
//        c.RoutePrefix = string.Empty;
//    });
}

app.UseHttpsRedirection();

//Routing
app.UseRouting();
//Auth
app.UseAuthorization();
app.UseAuthentication();


  



//app.UseEndpoints(endpoints => endpoints.MapGet("/", 
//    async context => await context.Response.WriteAsync("Hello World"))
//);

//Controller base
app.MapControllers();

app.Run();
