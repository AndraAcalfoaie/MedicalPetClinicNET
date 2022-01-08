using System;
using System.Collections.Generic;

namespace Services.Models.Appointment
{
    public class DoctorSchedule
    {
        public List<BusyInterval> BusyIntervals { get; set; }
    }

    public class BusyInterval
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
