using Aspose.CAD;
using Aspose.CAD.ImageOptions;
using Microsoft.AspNetCore.Mvc;

namespace ConversionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        [HttpPost("convertFromDWGToImage")]
        public async Task<ActionResult> Post(IFormFile file)
        {
            string outputfile = "output.png";

            var bytes = await file.GetBytes();

            //FormFileExtensions.SaveByteArrayToFileWithBinaryWriter(bytes, file.FileName);

            //  var license = new License();
            //license.SetLicense("license.lic");

            var stream = new MemoryStream(bytes);

            using (Aspose.CAD.Image image = Aspose.CAD.Image.Load(stream))
            {
                // Create an instance of CadRasterizationOptions
                CadRasterizationOptions rasterizationOptions = new CadRasterizationOptions();

                // Set page width & height
                rasterizationOptions.PageWidth = 1200;
                rasterizationOptions.PageHeight = 1200;

                // Create an instance of JpegOptions for the resultant image
                ImageOptionsBase options = new JpegOptions();

                // Set rasterization options
                options.VectorRasterizationOptions = rasterizationOptions;

                // CAD to JPG
                image.Save(outputfile, options);
            }

            byte[] binaryImage = await System.IO.File.ReadAllBytesAsync(outputfile);

            return File(binaryImage, "image/png");
        }
    }
}