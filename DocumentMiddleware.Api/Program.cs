using Microsoft.Extensions.FileProviders;
using DocumentMiddleware.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// MOVE THESE INTO EXTENSIONS
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
});


builder.Services
    .AddDbContext(builder.Configuration)
    .RegisterServices();

var app = builder.Build();

// Maps uploads folder to resources folder
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Resources"
});

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseAuth() lines go HERE!!!

app.UseAntiforgery();
app.RegisterAntiqueEndpoints();

app.Run();
