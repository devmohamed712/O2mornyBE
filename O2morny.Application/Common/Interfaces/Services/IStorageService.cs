namespace O2morny.Application.Common.Interfaces.Services
{
    public interface IStorageService
    {
        Task<string> UploadFile(Stream file, string fileExtension, string subFolderName, string entityId = "");

        void DeleteFile(string path);

        void DeleteDir(string path);
    }
}
