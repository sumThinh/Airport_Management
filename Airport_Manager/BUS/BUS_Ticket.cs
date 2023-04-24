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

        public List<Bill_Detail> getTicketList()
        {
            return dalTicket.getTicketsList();
        }
        public List<Bill_Detail> getTicketListByDate(DateTime date)
        {
            return dalTicket.getTicketListByDate(date);
        }
    }
}
