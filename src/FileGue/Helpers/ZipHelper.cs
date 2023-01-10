﻿using Aspose.Zip;
using Aspose.Zip.Saving;
using FileGue.Models;
using System.Text;

namespace FileGue.Helpers
{
    public class ZipHelper
    {
        public static async Task<byte[]> CreateZipFromLinks(List<DriveFile> Files, string BaseUrl = null)
        {
            var client = new HttpClient();
            var newZipFile = Path.GetTempFileName() + ".zip";
            // Create FileStream for output ZIP archive
            using (FileStream zipFile = File.Open(newZipFile, FileMode.Create))
            {
                using (var archive = new Archive())
                {
                    foreach (var file in Files)
                    {
                        var url = string.IsNullOrEmpty(BaseUrl) ? file.FileUrl : BaseUrl + file.FileUrl;
                        var datas = await client.GetByteArrayAsync(url);
                        // File to be added to archive
                        MemoryStream source1 = new MemoryStream(datas);
                        // File to be added to archive
                        // Add files to the archive
                        archive.CreateEntry(file.Name, source1);
                        // ZIP the files
                    }
                    archive.Save(zipFile, new ArchiveSaveOptions() { Encoding = Encoding.ASCII, ArchiveComment = $"{Files.Count} files are compressed in this archive" });
                }
            }
            if (File.Exists(newZipFile))
            {
                return File.ReadAllBytes(newZipFile);
            }
            return null;
        }
    }
}
