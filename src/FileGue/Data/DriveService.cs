using Redis.OM;
using FileGue.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System.Text;
using FileGue.Helpers;
using ServiceStack.Model;

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
            db = redis.As<Drive>();
            IsInit = false;
        }

        public void InitDrive(string Username)
        {
            this.Username = Username;
            Refresh();
            if (MyDrive == null)
            {
                //init with first drive
                MyDrive = new() { CreatedDate = DateHelper.GetLocalTimeNow(), UserName = Username, RootFolder = new DriveFolder() { CreatedDate = DateHelper.GetLocalTimeNow(), Favorite = true, Files = new(), Folders = new() { new DriveFolder() { CreatedDate = DateHelper.GetLocalTimeNow(), UpdatedDate = DateHelper.GetLocalTimeNow(), Favorite=true, Files = new(), Folders= new(), IsRoot=false, Name = "My Files", Path = "/MyFiles", Size=0, UID = UIDHelper.CreateNewUID()  } }, IsRoot = true, Name = "My Documents",  Path = "/", Size=0, UID = UIDHelper.CreateNewUID(), UpdatedDate = DateHelper.GetLocalTimeNow() } };
                Save();
            }
            IsInit = true;
        }

        public long GetUsedSize()
        {
            if (!IsInit) return -1;
            return GetSize(MyDrive.RootFolder);
        }

        public long GetSize(DriveFolder folder)
        {
            long size = 0;
            foreach(var file in folder.Files)
            {
                size += file.Size;
            }
            foreach(var subfolder in folder.Folders)
            {
                if(subfolder.Files.Count>0)
                    size += GetSize(subfolder);
            }
            return size;
        }

        public void Refresh()
        {
            this.KeyStr = $"Drive:{Username}";
            MyDrive = db.GetValue(KeyStr);
        }
        public void Save()
        {
            db.SetValue(KeyStr, MyDrive);
        }
        public DriveFolder GetFolder(string FolderUid)
        {
            var find = TraceFolder(MyDrive.RootFolder, FolderUid);
            return find;
        }

        DriveFolder TraceFolder(DriveFolder folder,string FolderUid)
        {
            if (folder.UID == FolderUid) return folder;
            foreach(var subfolder in folder.Folders)
            {
                var find = TraceFolder(subfolder, FolderUid);
                if (find != null) return find;
            }
            return null;
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
