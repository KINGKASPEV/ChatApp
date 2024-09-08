namespace Chat_Service.Domain.Common
{
    public class Entity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateCreated { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? DateModified { get; set; }
    }
}
