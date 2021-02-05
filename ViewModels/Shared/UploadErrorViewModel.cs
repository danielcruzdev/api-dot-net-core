using System;

namespace api_dot_net_core.ViewModels.Shared
{
    public class UploadErrorViewModel
    {
        public Guid? Id { get; set; }
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
    }
}
