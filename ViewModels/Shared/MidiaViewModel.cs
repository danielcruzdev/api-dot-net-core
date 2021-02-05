namespace api_dot_net_core.ViewModels.Shared
{

    public class MidiaViewModel
    {
        public MidiaViewModel()
        {
            ContentType = "image/jpeg";
        }

        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }

}
