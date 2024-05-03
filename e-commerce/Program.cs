using ecommerce.Business.Service;
using ecommerce.Business.Service.Interface;
using ecommerce.Data;
using ecommerce.Data.Repositories;
using ecommerce.Data.Repositories.Interface;

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
builder.Services.AddTransient<IWishRepository, WishRepository>();
builder.Services.AddTransient<IWishListRepository, WishListRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

// Link services
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IProductListService, ProductListService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IRateService, RateService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IWishService, WishService>();
builder.Services.AddTransient<IWishListService, WishListService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

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
