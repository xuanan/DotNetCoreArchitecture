using DotNetCore.AspNetCore;
using DotNetCore.Objects;
using DotNetCoreArchitecture.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Web
{
    [ApiController]
    [RouteController]
    public class FilesController : ControllerBase
    {
        public FilesController
        (
            IHostingEnvironment environment,
            IFileProvider fileProvider,
            IFileService fileService
        )
        {
            Directory = Path.Combine(environment.ContentRootPath, "Files");
            FileProvider = fileProvider;
            FileService = fileService;
        }

        private string Directory { get; }

        private IFileProvider FileProvider { get; }

        private IFileService FileService { get; }

        [DisableRequestSizeLimit]
        [HttpPost]
        public Task<IEnumerable<FileBinary>> AddAsync()
        {
            return FileService.AddAsync(Directory, Request.Files());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SelectAsync(Guid id)
        {
            var file = await FileService.SelectAsync(Directory, id);

            file.ContentType = FileProvider.GetContentType(file.ContentType);

            return File(file.Bytes, file.ContentType, file.Name);
        }
    }
}
