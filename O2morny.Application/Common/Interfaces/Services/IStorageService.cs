namespace O2morny.Application.Common.Interfaces.Services
{
    public interface IStorageService
    {
        Task<string> UploadFile(Stream file, string fileExtension, string subFolderName, string entityId = "");

        void DeleteDir(string path);
    }
}
