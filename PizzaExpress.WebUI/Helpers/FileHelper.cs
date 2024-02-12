using PizzaExpress.WebUI.Interfaces;

namespace PizzaExpress.WebUI.Helpers
{
    public class FileHelper : IFileHelper
    {
        IWebHostEnvironment _env;

        public FileHelper(IWebHostEnvironment webHost)
        {
            _env = webHost;
        }
        public void DeleteFile(string imgUrl)
        {
            //delete existing file
            if (File.Exists(_env.WebRootPath + imgUrl))
            {
                File.Delete(_env.WebRootPath + imgUrl);
            }
        }

        public string UploadFile(IFormFile file)
        {
            var uploads = Path.Combine(_env.WebRootPath, "images");
            bool exists = Directory.Exists(uploads);
            if (!exists)
                Directory.CreateDirectory(uploads);

            //saving file
            var fileName = GenerateFileName(file.FileName);
            var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
            file.CopyToAsync(fileStream);

            return "/images/" + fileName;
        }
        private string GenerateFileName(string fileName)
        {
            string[] strName = fileName.Split('.');
            string strFileName = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }
    }
}
