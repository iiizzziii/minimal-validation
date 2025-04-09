using Microsoft.AspNetCore.Mvc;
using minimal_validation.Filters;
using minimal_validation.Models;
using QueryString = minimal_validation.Models.QueryString;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/get", ([AsParameters] QueryString qString) => 
    Results.Ok((object?)qString)).AddEndpointFilter<ValidationFilter<QueryString>>();

app.MapPost("/post", ([FromBody] RequestBody body) => 
    Results.Ok((object?)body)).AddEndpointFilter<ValidationFilter<RequestBody>>();

app.Run();
