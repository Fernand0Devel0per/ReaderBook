using ReaderBook.Core.DAL;
using ReaderBook.Core.DAL.Interface;
using ReaderBook.Core.Data.Caching;
using ReaderBook.Core.Data.Caching.Interface;
using ReaderBook.Core.Data.Context;
using ReaderBook.Core.Data.Contexts.Interfaces;
using ReaderBook.Core.Domain.Book;
using ReaderBook.Core.Helpers.AutoMapper;
using ReaderBook.Core.Helpers.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddSingleton<IReaderBookDBContext, ReaderBookDBContext>();
//builder.Services.AddScoped<IBookDao<BookSchema>, BookDao>();
builder.Services.AddScoped(typeof(IBookDao<>), typeof(BookDao<>));

builder.Services.AddScoped<ICachingService, CachingService>();

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
