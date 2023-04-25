using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Linq;
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
                list = db.Bill_Detail.Include(t => t.Flight).Include(t => t.Flight.Location).Include(t=>t.Flight.Location1).Include(t=>t.Flight.Bill_Detail).Include(t=>t.Customer).ToList();
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

        public bool RemoveTicket(int id)
        {
            using (var db = new AirportManager())
            {
                var targetedBill = (from d in db.Bill_Detail where d.BillID == id select d).SingleOrDefault();
                db.Bill_Detail.Remove(targetedBill);
                return db.SaveChanges() > 0;
            }
        }

        public bool UpdateTicket(Bill_Detail updated_tick)
        {
            using(var db = new AirportManager())
            {
                var cur_ticket = db.Bill_Detail.Find(updated_tick.BillID);
                if(cur_ticket != null)
                {
                    db.Entry(cur_ticket).CurrentValues.SetValues(updated_tick);

                    if (db.SaveChanges() > 0)
                        return true;
                }
            }
            return false;
        }

        public List<Bill_Detail> getTicketListByDate(DateTime date)
        {
            List<Bill_Detail> tickets = new List<Bill_Detail>();
            using (var db = new AirportManager())
            {
                tickets = db.Bill_Detail.Where(c => DbFunctions.TruncateTime(c.BookingDate) == date).ToList();
            }
            return tickets;
        }

        public List<Bill_Detail> getTicketListByMonthYear(int month, int year)
        {
            List<Bill_Detail> tickets = new List<Bill_Detail>();
            using (var db = new AirportManager())
            {
                tickets = db.Bill_Detail.Where(t => t.BookingDate.Value.Month == month && t.BookingDate.Value.Year == year).ToList();
            }
            return tickets;
        }
    }
}
