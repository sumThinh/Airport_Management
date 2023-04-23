using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Ticket
    {
        public List<Bill_Detail> getTicketsList()
        {
            List<Bill_Detail> tickets = new List<Bill_Detail>();
            using(var db = new AirportManager()){
                tickets = db.Bill_Detail.Include("Customer").ToList();
            }
            return tickets;

        }

        public bool addTicket(Bill_Detail ticket)
        {
            using(var db = new AirportManager())
            {
                db.Bill_Detail.Add(ticket);
                if (db.SaveChanges() > 0) return true;
            }
            return false;
        }

        public bool updateTicket(Bill_Detail updated_ticket)
        {
            using(var db = new AirportManager())
            {
                Bill_Detail current_ticket = db.Bill_Detail.Find(updated_ticket.BillID);
                if (current_ticket != null)
                {
                    db.Entry(current_ticket).CurrentValues.SetValues(updated_ticket);
                    db.SaveChanges();

                    return true;
                }

            }
            return true;
        }

        public bool deleteTicket(Bill_Detail deleted_ticket) 
        {
            return true;
        }
    }
}
