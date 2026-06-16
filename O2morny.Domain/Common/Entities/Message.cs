
using O2morny.Domain.Common.Interfaces;

namespace O2morny.Domain.Common.Entities
{
    public class Message : IEntity<int>, IAuditable, ISoftDelete
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
