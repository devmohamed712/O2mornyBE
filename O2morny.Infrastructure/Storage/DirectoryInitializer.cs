
namespace O2morny.Infrastructure.Storage
{
    public static class DirectoryInitializer
    {
        private static readonly string RootPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        private static readonly string[] SubFolders = new[]
        {
            "ProfilesImages",
            "NationalIdsImages",
        };

        public static void EnsureUploadDirectoriesExist()
        {
            if (!Directory.Exists(RootPath))
            {
                Directory.CreateDirectory(RootPath);
            }

            foreach (var folder in SubFolders)
            {
                string fullPath = Path.Combine(RootPath, folder);
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
            }
        }
    }
}
