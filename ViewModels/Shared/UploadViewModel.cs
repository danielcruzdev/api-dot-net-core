using Microsoft.AspNetCore.Http;
using System;

namespace api_dot_net_core.ViewModels.Shared
{
    public class UploadViewModel
    {
        public Guid Id { get; set; }
        public IFormFile File { get; set; }

    }
}
