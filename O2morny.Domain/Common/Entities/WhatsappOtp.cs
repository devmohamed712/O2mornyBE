using O2morny.Domain.Common.Enums;

namespace O2morny.Domain.Common.Entities
{
    public class WhatsappOtp
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Code { get; set; }

        public DateTime ExpireAt { get; set; }

        public bool IsUsed { get; set; }

        public DateTime CreatedAt { get; set; }

        public OtpStatus Status { get; set; } 
    }
}
