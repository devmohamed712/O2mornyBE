using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Services;

namespace O2morny.Infrastructure.Services
{
    public class StorageService : IStorageService
    {
        public async Task<string> UploadFile(Stream file, string fileExtension, string subFolderName, string entityId = "")
        {
            if (file == null)
                throw new BadRequestException("File is required");

            fileExtension = fileExtension.Trim('.').ToLowerInvariant();

            HashSet<string> allowedTypes = new(StringComparer.OrdinalIgnoreCase) { "pdf", "txt", "docx", "xlsx", "pptx", "jpg", "jpe", "png", "jpeg", "webp" };

            if (!allowedTypes.Contains(fileExtension))
                throw new BadRequestException("Invalid file extension");

            var folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                subFolderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string time = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            var fileName = !string.IsNullOrWhiteSpace(entityId)
                ? $"{entityId}_{time}_{Guid.NewGuid()}"
                : $"{time}_{Guid.NewGuid()}";

            if (fileName.Length > 120)
                fileName = fileName[..120];

            var finalFileName = $"{fileName}.{fileExtension}";

            var filePath = Path.Combine(folderPath, finalFileName);

            using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            return finalFileName;
        }

        public void DeleteDir(string path)
        {
            bool exists = System.IO.Directory.Exists(path);
            if (exists)
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(path);
                di.Delete(true);
            }
        }
    }
}
