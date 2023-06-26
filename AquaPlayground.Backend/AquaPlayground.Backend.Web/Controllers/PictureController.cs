namespace AquaPlayground.Backend.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/pictures")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetPicture(string id)
        {
            byte[] pictureData = GetPictureData(id);

            if (pictureData is null)
            {
                return NotFound();
            }

            string contentType = "image/jpeg";

            return File(pictureData, contentType);
        }

        private byte[] GetPictureData(string id)
        {
            try
            {
                if (Guid.TryParse(id, out var result))
                {
                    string filePath = $"Pictures/{id}.jpg";

                    byte[] pictureData = System.IO.File.ReadAllBytes(filePath);

                    return pictureData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
