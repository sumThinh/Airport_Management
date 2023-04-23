﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlTypes;

namespace DAL
{
    public class DAL_Customer
    {
        public bool AddCustomer(Customer customer) 
        {
            using(var db = new AirportManager())
            {
                db.Customers.Add(customer);
                if (db.SaveChanges() > 0)
                    return true;
                
                return false;
            }
        }

        public bool UpdateCustomer(Customer updated_customerr) { 
        
            using (var db = new AirportManager())
            {
                Customer current_customer = db.Customers.Find(updated_customerr.CustomerID);
                if (current_customer != null)
                {
                    db.Entry(current_customer).CurrentValues.SetValues(updated_customerr);
                    if (db.SaveChanges() >= 0)
                        return true;

                }
                return false;
            }
        }
        
        public bool DeleteCustomer(int customer_id)
        {
            using(var db = new AirportManager())
            {
                Customer deleted_customer = db.Customers.FirstOrDefault(cus => cus.CustomerID == customer_id);
                db.Customers.Remove(deleted_customer);
                if (db.SaveChanges() > 0)
                    return true;

                return false;
            }
            
        }

        public List<Customer> GetCustomerList()
        {
            List<Customer> list = new List<Customer>();
            using (var db = new AirportManager())
            {
                list = db.Customers.ToList();
            }

            return list;
        }

        public bool checkExistCustomer(int id, String nationalID, String telePhone)
        {
            using(var db = new AirportManager()) {
                Customer existCustomer = db.Customers.Where(cus => cus.CustomerID != id && cus.NationalID == nationalID && cus.TeleNumber == telePhone ).FirstOrDefault();
                if (existCustomer != null)
                    return true;
                return false;

            }
        }
    }
}
