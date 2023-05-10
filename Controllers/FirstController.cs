using App.Services;
using Microsoft.AspNetCore.Mvc;
namespace App.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly ProductService _ProductSevice;
        private IWebHostEnvironment Environment;
        //Khai báo phương thức khởi tạo FirstController để Inject
        public FirstController(ILogger<FirstController> logger, IWebHostEnvironment _environment, ProductService ProductSevice)
        {
            _logger = logger;
            Environment = _environment;
            _ProductSevice = ProductSevice;
        }
        public string Index()
        {
            _logger.LogInformation("Đây là dòng log 1");
            _logger.LogWarning("Thông báo warning");
            _logger.LogError("Thông báo warning");
            _logger.LogDebug("Thông báo Debug");
            _logger.LogCritical("Thông báo Critical");
            //  Console.WriteLine("Đây là dòng log");
            return "Tôi là Index";

        }
        public IActionResult Readme()
        {
            var content = "Xin chao cac ban!";
            return Content(content, "text/plain");
        }
        public void Nothing()
        {
            _logger.LogInformation("Đây là dòng log 2");
            Response.Headers.Add("Hello", "Xin chao cac ban");
        }

        public object Anything() => DateTime.Now;

        public IActionResult Price()
        {
            return Json(
                new
                {
                    productName = "Iphone 13 ProMax",
                    price = "17.500.000"
                }
            );
        }

        public IActionResult Bird()
        {
            string contentPath = this.Environment.ContentRootPath;
            string filePath = Path.Combine(contentPath, "Files", "Bird.jpg");
            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "image/jpg");
        }

        public IActionResult Redirect()
        {
            var url = Url.Action("Privacy", "Home");
            _logger.LogInformation("Đang chuyển hướng đến...");
            return LocalRedirect(url);
        }

        public IActionResult Google()
        {
            var url = "https://google.com";
            _logger.LogInformation("Đang chuyển hướng đến...");
            return Redirect(url);
        }
        public IActionResult HelloView(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = "Khách";
            }
            //-----------------------
            /* View(template) - template đường dẫn tuyệt đối tới .cshtml
               return View("/MyView/View1.cshtml");
             */

            /* View(template,model) - thêm model
               return View("/MyView/View1.cshtml",userName);
            */

            /* View2.cshtml --> /Views/First/View2.cshtml - Gọi tên Template
               return View("View2",userName);
            */

            /*View1.cshtml --> /Views/First/View1.cshtml - Gọi mặc định
              Model phải gọi kèm Object
            */
            return View("View3", userName);
        }

        [TempData]
        public string StatusMessage { get; set; }
        public IActionResult ViewProduct(int? id)
        {
            var product = _ProductSevice.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                // TempData["StatusMessage"] = "Sản phẩm không có!";
                StatusMessage = "Sản phẩm không có!";
                return Redirect(Url.Action("Index","Home"));
            }

            // ViewData["product"] = product;
            // ViewData["Title"] = product.Name;
            // return View("ViewProduct2");

            ViewBag.product = product;
            ViewBag.Title = product.Name;
            return View("ViewProduct3");
        }
        // ContentResult               | Content()
        // EmptyResult                 | new EmptyResult()
        // FileResult                  | File()
        // ForbidResult                | Forbid()
        // JsonResult                  | Json()
        // LocalRedirectResult         | LocalRedirect()
        // RedirectResult              | Redirect()
        // RedirectToActionResult      | RedirectToAction()
        // RedirectToPageResult        | RedirectToRoute()
        // RedirectToRouteResult       | RedirectToPage()
        // PartialViewResult           | PartialView()
        // ViewComponentResult         | ViewComponent()
        // StatusCodeResult            | StatusCode()
        // ViewResult                  | View()

    }
}