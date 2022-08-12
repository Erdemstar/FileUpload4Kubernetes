using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload4Kubernetes.Utility.FileUploadSettings
{
    public class FileUploadSettings : IFileUploadSettings
    {
        public string Path { get; set; }
    }

    public interface IFileUploadSettings
    {
        string Path { get; set; }
    }
}
