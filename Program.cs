using App.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    //     • {0} - Action Name
    //     • {1} - Controller Name
    //     • {2} - Area Name
    //     • RazorViewEngine.ViewExtension - .cshtml        

    // Tìm thêm View ở /MyView/ControllerName/ActionName.cshtml 
    options.ViewLocationFormats.Add("/MyView/{0}" + RazorViewEngine.ViewExtension);

});
// builder.Services.AddScoped<> // Hết 1 phiên làm việc tạo 1 đối tượng mới
// builder.Services.AddTransient<> // Mỗi lần truy vấn tạo 1 đối tượng mới 
// Tạo duy nhất 1 đối tượng (3 kiểu viết):
//  builder.Services.AddSingleton<ProductService>(); 
//  builder.Services.AddSingleton(typeof(ProductService)); 
//  builder.Services.AddSingleton(typeof(ProductService),typeof(ProductService)); 

 builder.Services.AddSingleton<ProductService,ProductService>(); // Services: ProductService, Object/Kế thừa object: ProductService

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();//Xac dinh danh tinh 
app.UseAuthorization(); //Xac dinh quyen truy cap

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
