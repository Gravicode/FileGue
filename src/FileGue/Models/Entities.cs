using FileGue.Data;
using GemBox.Document;
using Microsoft.CodeAnalysis;
using Redis.OM.Modeling;
using ServiceStack.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Reflection;

namespace FileGue.Models
{
    #region redis model
    [Table("share_link")]
    //[Document(StorageType = StorageType.Json)]
    public class ShareLink
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string LinkUid { set; get; }

        public string OwnerUsername { get; set; }

        public List<DriveFolder> Folders { get; set; }
        public List<DriveFile> Files { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<ShareInfo> Access { get; set; }
    }
    [Table("storage_info")]
    [Document(StorageType = StorageType.Json)]
    public class StorageInfo
    {
        [RedisIdField][Indexed] public string Id { get; set; }
        [Indexed(Sortable = true)]

        public string Username { set; get; }
        [Indexed(Sortable = true)]

        public long TotalSize { set; get; } = AppConstants.DefaultStorageSize;

        [Indexed(Sortable = true)]

        public long UsedSize { set; get; } = 0;

        [Indexed(Sortable = true)]
        public string PackageName { set; get; } = "Default";

        [Indexed(Sortable = true)]

        public DateTime CreatedDate { set; get; } 

        public long GetFreeSpace()
        {
            return TotalSize - UsedSize;
        }
    }
        [Table("page_view")]
    [Document(StorageType = StorageType.Json)]
    public class PageView
    {
        [RedisIdField][Indexed] public string Id { get; set; }
        [Indexed(Sortable = true)]

        public string PageName { set; get; }
        [Indexed(Sortable = true)]
        public string PageUrl { set; get; }
        [Indexed(Sortable = true)]
        public UserProfile User { set; get; }
        [Indexed(Sortable = true)]
        public string Agent { set; get; }
        [Indexed(Sortable = true)]
        public DateTime HitDate { set; get; }
    }

    [Table("cache_data")]
    [Document(StorageType = StorageType.Json)]
    public class CacheData
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        [RedisIdField][Indexed] public string Id { get; set; }
        [Indexed(Sortable = true)]
        public string Username { get; set; }
        [Indexed(Sortable = true)]
        public DateTime LastUpdate { get; set; }
        [Indexed(Sortable = true)]
        public string DataString { get; set; }
        [Indexed(Sortable = true)]
        public string KeyString { get; set; }
    }
    [Table("drive")]
    //[Document(StorageType = StorageType.Json)]
    public class Drive
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        //[RedisIdField][Indexed] public string Id { get; set; }
        //[Indexed(Sortable = true)]
        public DateTime CreatedDate { set; get; }
        //[Indexed(Sortable = true)]        
        public string UserName { set; get; }
        //[Indexed(Sortable = true)]        
        public DriveFolder RootFolder { get; set; } = new() { IsRoot = true };

    }
    [Table("notification")]
    [Document(StorageType = StorageType.Json)]
    public class Notification
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        [RedisIdField][Indexed] public string Id { get; set; }
        [Indexed(Sortable = true)]
        public DateTime CreatedDate { set; get; }
        [Indexed(Sortable = true)]
        //[ForeignKey(nameof(User)), Column(Order = 0)]
        public string UserName { set; get; }
        [Indexed(Sortable = true)]
        public UserProfile User { set; get; }
        [Indexed(Sortable = true)]
        public string Action { set; get; }
        [Indexed(Sortable = true)]
        public string LinkUrl { set; get; }
        [Indexed(Sortable = true)]
        public string LinkDesc { set; get; }
        [Indexed(Sortable = true)]
        public string Message { set; get; }
        [Indexed(Sortable = true)]
        public bool IsRead { set; get; } = false;
    }
    public enum LogCategory
    {
        Info, Error, Warning
    }
    [Table("logs")]
    [Document(StorageType = StorageType.Json)]
    public class Log
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        [RedisIdField][Indexed] public string Id { get; set; }
        [Indexed(Sortable = true)]
        public string CreatedBy { get; set; }
        [Indexed(Sortable = true)]
        public DateTime LogDate { get; set; }
        [Indexed(Sortable = true)]
        public string Message { get; set; }
        [Indexed(Sortable = true)]
        public LogCategory Category { get; set; }
    }

    [Table("userprofile")]
    [Document(StorageType = StorageType.Json)]
    public class UserProfile
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        [RedisIdField][Indexed] public string Id { get; set; }
        [Indexed(Sortable = true)]
        public string Username { get; set; }
        [Indexed(Sortable = true)]
        public string Password { get; set; }
        [Indexed(Sortable = true)]
        public string FullName { get; set; }
        [Indexed(Sortable = true)]
        public string? Phone { get; set; }
        [Indexed(Sortable = true)]
        public string? Email { get; set; }
        [Indexed(Sortable = true)]
        public string? Address { get; set; }
        [Indexed(Sortable = true)]
        public string? KTP { get; set; }
        [Indexed(Sortable = true)]
        public string? PicUrl { get; set; }
        [Indexed(Sortable = true)]
        public bool Aktif { get; set; } = true;
        [Indexed(Sortable = true)]
        public string? Daerah { get; set; }
        [Indexed(Sortable = true)]
        public string? Desa { get; set; }
        [Indexed(Sortable = true)]
        public string? Kelompok { get; set; }
        [Indexed(Sortable = true)]
        public Char Gender { get; set; } = 'N';
        [Indexed(Sortable = true)]
        public Roles Role { set; get; } = Roles.User;
        [Indexed(Sortable = true)]
        public DateTime CreatedDate { get; set; }
        [Indexed(Sortable = true)]
        public double? Longitude { get; set; }
        [Indexed(Sortable = true)]
        public double? Latitude { get; set; }
        [Indexed(Sortable = true)]
        public string? FirstName { set; get; }
        [Indexed(Sortable = true)]
        public string? LastName { set; get; }
        [Indexed(Sortable = true)]
        public string? AboutMe { set; get; }
        [Indexed(Sortable = true)]
        public DateTime? BirthDate { set; get; }
        [Indexed(Sortable = true)]
        public string? Website { set; get; }
        [Indexed(Sortable = true)]
        public string? Facebook { set; get; }
        [Indexed(Sortable = true)]
        public string? Twitter { set; get; }
        [Indexed(Sortable = true)]
        public string? Instagram { set; get; }
        [Indexed(Sortable = true)]
        public string? LinkedIn { set; get; }
        [Indexed(Sortable = true)]
        
        public string? City { set; get; }
      
      

        [Indexed(Sortable = true)]
        public bool TwoFactor { set; get; }

        [Indexed(Sortable = true)]
        public bool AlertIntruder { set; get; }

    }

    public enum Roles { Admin, User, Pengurus, Unknown }
    #endregion
    #region helpers model

    public class GroupedFile
    {
        public string GroupName { get; set; }
        public List<DriveFile> Files { get; set; }
    }
    public class NewCommands
    {
        public const string CreateFolder = "CreateFolder";
        public const string UploadFiles = "UploadFiles";
        public const string UploadFolder = "UploadFolder";
    }
        public class SearchItem
    {
        public DriveFolder ParentFolder { get; set; }
        public DriveFile File { get; set; }
    }
    public class FileTypes
    {
        public const string Image = "Image";
        public const string Audio = "Audio";
        public const string Video = "Video";
        public const string Compressed = "Compressed";
        public const string Word = "Word";
        public const string Excel = "Excel";
        public const string PowerPoint = "PowerPoint";
        public const string Pdf = "Pdf";
        public const string Text = "Text";

        public static string GetFileType(string FileName)
        {
            var ext = Path.GetExtension(FileName);
            switch (ext)
            {
                case ".bmp":
                case ".jpg":
                case ".gif":
                case ".png":
                case ".jpeg":
                    return Image;
                    break;
                case ".doc":
                case ".docx":
                    return Word;
                    break;
                case ".xls":
                case ".xlsx":
                    return Excel;
                    break;
                case ".ppt":
                case ".pptx":
                    return PowerPoint;
                    break;
                case ".pdf":
                    return Pdf;
                    break;
                case ".mp3":
                case ".wav":
                case ".flac":
                case ".mid":
                    return Audio;
                    break;
                case ".mp4":
                case ".avi":
                case ".mpeg":
                case ".mpg":
                case ".wmv":
                    return Video;
                    break;
                case ".zip":
                case ".rar":
                case ".tar":
                case ".tar.gz":
                case ".7z":
                    return Compressed;
                    break;
                case ".txt":
                case ".rtf":
                    return Text;
                    break;


            }
            return "Unknown";
        }
    }


    public class DriveFolder
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public long Size { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsRoot { get; set; } = false;
        public bool Favorite { get; set; } = false;
        public string Path { get; set; }
        public List<DriveFile> Files { get; set; } = new();
        public List<DriveFolder> Folders { get; set; } = new();
    }

    public class DriveFile
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public string Owner { get; set; }
        public bool Favorite { get; set; } = false;
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string FileUrl { get; set; }
        public string ParentFolderUid { set; get; }
    }
    public class PageViewMonth
    {
        public string Month { get; set; }
        public int Hit { get; set; }

    }
    public class StorageObject
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string FileUrl { get; set; }
        public string ContentType { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? LastAccess { get; set; }
    }
    public class StorageSetting
    {
        public string EndpointUrl { get; set; } = "https://is3.cloudhost.id";
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; } = "USWest1";
        public string Bucket { get; set; }
        public string BaseUrl { get; set; }
        public bool Ssl { get; set; } = true;
        public StorageSetting()
        {

        }
        public StorageSetting(string Endpoint, string Accesskey, string Secretkey, string Region, string Bucket)
        {
            this.EndpointUrl = Endpoint;
            this.AccessKey = Accesskey;
            this.SecretKey = Secretkey;
            this.Region = Region;
            this.Bucket = Bucket;
            GenerateBaseUrl();
        }
        public void GenerateBaseUrl()
        {
            this.BaseUrl = EndpointUrl + "/{bucket}/{key}";
        }
    }
    public class OutputCls
    {
        public OutputCls()
        {
            this.IsSucceed = false;
            this.Message = "";
        }
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsSucceed { get; set; }
    }
    public enum ShareAccess { Read, Write }
    public class ShareInfo
    {
        public List<string> Usernames { get; set; }
        public ShareAccess Access { get; set; }
    }
    #endregion






}
