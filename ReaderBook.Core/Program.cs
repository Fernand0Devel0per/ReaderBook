using ReaderBook.Core.DAL;
using ReaderBook.Core.DAL.Interface;
using ReaderBook.Core.Data.Caching;
using ReaderBook.Core.Data.Caching.Interface;
using ReaderBook.Core.Data.Context;
using ReaderBook.Core.Data.Contexts.Interfaces;
using ReaderBook.Core.Helpers.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IReaderBookDBContext, ReaderBookDBContext>();
builder.Services.AddScoped<IBookDao, BookDao>();
builder.Services.AddScoped<IChachingService, ChachingService>();

builder.Services.AddStackExchangeRedisCacheFromConfiguration(builder.Configuration); 

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
