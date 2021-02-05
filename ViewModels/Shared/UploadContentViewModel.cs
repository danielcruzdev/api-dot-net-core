using System.Collections.Generic;

namespace api_dot_net_core.ViewModels.Shared
{
    public class UploadContentViewModel
    {
        public UploadContentViewModel()
        {
            FilesWithErrors = new List<UploadErrorViewModel>();
            SuccessFiles = new List<MidiaContentViewModel>();
        }

        public IEnumerable<UploadErrorViewModel> FilesWithErrors { get; set; }
        public IEnumerable<MidiaContentViewModel> SuccessFiles { get; set; }
    }
}
