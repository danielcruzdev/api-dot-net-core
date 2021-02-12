using System;

namespace api_dot_net_core.ViewModels.Shared
{
    public class MidiaContentViewModel
    {
        public MidiaContentViewModel()
        {
            Guid = System.Guid.NewGuid();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public Guid? Guid { get; set; }
    }
}
