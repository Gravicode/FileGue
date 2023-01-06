using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Pdf.Content.Objects;
using Redis.OM;
using Redis.OM.Searching;
using FileGue.Data;
using FileGue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace FileGue.Data
{
    public class DriveService 
    {
        //FileGueDB db;
        RedisConnectionProvider provider;
        //IRedisCollection<Drive> db;
        UserProfileService UserSvc;
        Drive MyDrive;
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
            }
            IsInit = true;
        }

        void Save()
        {
            db.SetValue(KeyStr, MyDrive);
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

        List<DriveFile> SearchFile(DriveFolder currentFolder,DriveFile findFile)
        {
            var files = new List<DriveFile>();
            foreach (var currentFile in currentFolder.Files)
            {
                if (currentFile == findFile)
                {
                    files.Add(currentFile);
                }
            }
            if (files.Count <= 0) 
            {
                foreach(var folder in currentFolder.Folders)
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
                return SearchFolder(folder, findFolder);
            }
            
            return folders;
        }
       
       
    }

}
