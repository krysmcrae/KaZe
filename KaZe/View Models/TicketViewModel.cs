using KaZe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaZe.View_Models
{
    public class TicketViewModel
    {
        public Ticket ticket { get; set; }
        public ApplicationUser projMan { get; set; }
        public TicketComment newComment { get; set; }
        public TicketViewModel()
        {

        }
    }
}