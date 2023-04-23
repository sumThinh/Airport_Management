using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Ticket
    {
        DAL_Ticket dalTicket = new DAL_Ticket();
        public List<Bill_Detail> getTicketsList()
        {
            return dalTicket.getTicketsList();
        }

        public bool addTicket(Bill_Detail ticket)
        {
            return dalTicket.addTicket(ticket);
        }

        public bool updateTicket(Bill_Detail updated_ticket) 
        {
            return dalTicket.updateTicket(updated_ticket);
        }

        public bool deleteTicket(Bill_Detail deleted_ticket)
        {
            return dalTicket.deleteTicket(deleted_ticket);
        }
    }
}
