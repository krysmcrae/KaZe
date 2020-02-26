using System;

namespace KaZe.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Ticket Ticket { get; set; }

    }
}