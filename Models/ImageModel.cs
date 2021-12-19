using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.Models
{
    public class ImageModel
    {
        public IFormFile Image { get; set; }
    }
}
