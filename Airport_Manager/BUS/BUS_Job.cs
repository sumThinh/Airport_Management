using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Job
    {
        public int AddJob(Job job)
        {
            DAL_Job dalJob = new DAL_Job();
            if (dalJob.CheckExitEmployee(job.EmployeeID.Value) == true && dalJob.CheckExitFlight(job.FlightID.Value) == true)
            {
                if(dalJob.AddJob(job)== true) 
                    return 1;   // EXITED Employee AND Fight
                else
                    return -1;
            }
            else
            {
                if (dalJob.CheckExitEmployee(job.EmployeeID.Value) == true)
                    return 2;   // NOT EXIT FLIGHTID
                if (dalJob.CheckExitFlight(job.FlightID.Value) == true)
                    return 3;   // NOT EXIT EMPLOYEEID

            }
            return 0;
        }

        public int UpdateJob(Job job)
        {
            DAL_Job dalJob = new DAL_Job();
            if (dalJob.CheckExitJob(job) == true)
            {
                if (dalJob.UpdateJob(job) == true)
                    return 1;
                else
                    return 0;
            }
            else
                return 2;

        }
    }
}
