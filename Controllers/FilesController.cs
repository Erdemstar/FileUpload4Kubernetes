using FileUpload4Kubernetes.Entity.File;
using FileUpload4Kubernetes.Repository.File;
using FileUpload4Kubernetes.Utility.FileUploadSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload4Kubernetes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IOptions<FileUploadSettings> _fileUploadSettings;

        private readonly FileRepository _fileRepository;

        public FilesController(IOptions<FileUploadSettings> fileUploadSettings, FileRepository fileRepository)
        {
            _fileUploadSettings = fileUploadSettings;
            _fileRepository = fileRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFile()
        {
            var result = await _fileRepository.GetFile();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            string FileName = file.FileName;
            string FilePath = _fileUploadSettings.Value.Path + FileName;
            try
            {
                using (var localFile = System.IO.File.OpenWrite(FilePath))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }

            _fileRepository.CreateFile( new FileEntity() { FileName = FileName, FilePath = FilePath });

            return Ok("File upload");
        }
    }
}
