using DTO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Ticket
    {
        public List<Bill_Detail> LoadBills()
        {
            var list = new List<Bill_Detail>();
            using (var db = new AirportManager())
                list = db.Bill_Detail.ToList();
            return list;
        }

        public bool AddTicket(Bill_Detail tick)
        {
            using (var db = new AirportManager())
            {
                db.Bill_Detail.Add(tick);
                return db.SaveChanges() > 0;
            }
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

        public bool RemoveTicket(int id)
        {
            using (var db = new AirportManager())
            {
                var targetedBill = (from d in db.Bill_Detail where d.BillID == id select d).SingleOrDefault();
                db.Bill_Detail.Remove(targetedBill);
                return db.SaveChanges() > 0;
            }
        }

        public List<Bill_Detail> getTicketListByDate(DateTime date) {
            List<Bill_Detail> tickets = new List<Bill_Detail>();
            using(var db = new AirportManager())
            {
                tickets = db.Bill_Detail.Where(c => DbFunctions.TruncateTime(c.BookingDate) == date).ToList();
            }
            return tickets; 
    }
}
