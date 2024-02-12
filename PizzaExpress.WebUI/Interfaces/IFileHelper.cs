namespace PizzaExpress.WebUI.Interfaces
{
    public interface IFileHelper
    {
        void DeleteFile(string imgUrl);

        String UploadFile(IFormFile file);
    }
}
