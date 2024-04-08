using ecommerce.Business.Service;
using ecommerce.Data;
using ecommerce.Data.Repositories;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins(
                "http://localhost:4200",
                "http://127.0.0.1:4200"
            );
        });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>();

// Link repositories
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<IProductListRepository, ProductListRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IRateRepository, RateRepository>();
builder.Services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

// Link services
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IProductListService, ProductListService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IRateService, RateService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
