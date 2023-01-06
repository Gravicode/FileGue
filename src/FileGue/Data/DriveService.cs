using Redis.OM;
using FileGue.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System.Text;

namespace FileGue.Data
{
    public class DriveService
    {
        public Drive MyDrive { set; get; }
        IRedisTypedClient<Drive> db;
        public string Username { get; set; }
        public string KeyStr { get; set; }
        public bool IsInit { get; set; } = false;
        public DriveService()
        {
            using var redisManager = new PooledRedisClientManager(AppConstants.RedisCon);
            using var redis = redisManager.GetClient();
            var db = redis.As<Drive>();
            IsInit = false;
        }

        public void InitDrive(string Username)
        {
            this.KeyStr = $"Drive:{Username}";
            this.Username = Username;
            MyDrive = db.GetValue(KeyStr);
            if (MyDrive == null)
            {
                MyDrive = new() { CreatedDate = DateHelper.GetLocalTimeNow(), UserName = Username };
                Save();
            }
            IsInit = true;
        }

        void Save()
        {
            db.SetValue(KeyStr, MyDrive);
        }

        public bool DeleteFolder(DriveFolder Folder, string FolderUid)
        {
            var item = Folder.Folders.FirstOrDefault(x => x.UID == FolderUid);
            if (item != null)
            {
                return Folder.Folders.Remove(item);
            }
            return false;
        }

        public bool DeleteFile(DriveFolder Folder, string FileUid)
        {
            var item = Folder.Files.FirstOrDefault(x => x.UID == FileUid);
            if (item != null)
            {
                return Folder.Files.Remove(item);
            }
            return false;
        }
        public List<DriveFile> FindFiles(DriveFolder folder, string Keyword, string Extension)
        {
            if (!IsInit) return default;
            
            var files = SearchFiles(folder, Keyword, Extension);
            return files;
        }
        List<DriveFile> SearchFiles(DriveFolder currentFolder, string Keyword, string Extension = "")
        {
            var files = new List<DriveFile>();
            var query = string.IsNullOrEmpty(Keyword) ? currentFolder.Files :  currentFolder.Files.Where(x => x.Name.Contains(Keyword));
            if (!string.IsNullOrEmpty(Extension))
            {
                query = query.Where(x => x.Extension == Extension);
            }
            files = query.ToList();

            foreach (var folder in currentFolder.Folders)
            {
                var datas = SearchFiles(folder, Keyword, Extension);
                if (datas != null)
                    files.AddRange(datas);
            }

            return files;
        }
        public List<DriveFile> FindItem(DriveFile file)
        {
            if (!IsInit) return default;

            var files = SearchFile(MyDrive.RootFolder, file);
            return files;
        }

        public List<DriveFolder> FindItem(DriveFolder folder)
        {
            if (!IsInit) return default;

            var folders = SearchFolder(MyDrive.RootFolder, folder);
            return folders;
        }

        List<DriveFile> SearchFile(DriveFolder currentFolder, DriveFile findFile)
        {
            var files = new List<DriveFile>();
            foreach (var currentFile in currentFolder.Files)
            {
                if (currentFile == findFile)
                {
                    files.Add(currentFile);
                    return files;
                }
            }
            if (files.Count <= 0)
            {
                foreach (var folder in currentFolder.Folders)
                {
                    return SearchFile(folder, findFile);
                }
            }
            return files;
        }

        List<DriveFolder> SearchFolder(DriveFolder currentFolder, DriveFolder findFolder)
        {
            var folders = new List<DriveFolder>();

            foreach (var folder in currentFolder.Folders)
            {
                if (folder == findFolder)
                {
                    folders.Add(folder);
                    return folders;
                }
                return SearchFolder(folder, findFolder);
            }

            return folders;
        }


    }

}
