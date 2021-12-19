using FaceRecognition.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.FacePrediction;
using Services.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFaceRecognitionService _faceRecognitionService;
        private readonly IStorageService _storageService;

        public HomeController(ILogger<HomeController> logger,
                              IFaceRecognitionService faceRecognitionService,
                              IStorageService storageService)
        {
            _logger = logger;

            _faceRecognitionService = faceRecognitionService;
            _storageService = storageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UploadImage(ImageModel model)
        {
            if (model != null && model.Image != null)
            {
                var url = _storageService.UploadFile(model.Image);
                var res = _faceRecognitionService.GetFacePredictions(url);

                var result = res.OrderByDescending(x => x.Value).Take(3);

                return Json(new { success = true, message = "Image uploaded!", result });
            }

            return Json(new { success = false, message = "Error uploading image!" });
        }
    }
}
