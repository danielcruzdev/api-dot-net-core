using System;

namespace api_dot_net_core.Entities.Shared
{
    public class Midia
    {
        public Midia()
        {
            Guid = System.Guid.NewGuid();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public Guid? Guid { get; set; }
        public string ContentType { get; set; }
    }
}
