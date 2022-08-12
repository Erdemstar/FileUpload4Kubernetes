using FileUpload4Kubernetes.Entity.File;
using FileUpload4Kubernetes.Utility.DatabaseSettings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload4Kubernetes.Repository.File
{
    public class FileRepository
    {
        private readonly IMongoCollection<FileEntity> _file;

        public FileRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _file = database.GetCollection<FileEntity>("File");
        }

        public async Task<List<FileEntity>> GetFile()
        {
            return await _file.Find(t => true).ToListAsync();
        }

        public FileEntity CreateFile(FileEntity file)
        {
            _file.InsertOneAsync(file);
            return file;
        }



    }
}
