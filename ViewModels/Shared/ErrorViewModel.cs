namespace api_dot_net_core.ViewModels.Shared
{
    public class ErrorViewModel
    {
        public string Instance { get; set; }
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Method { get; set; }
        public bool IsExpected { get; set; }
    }
}
