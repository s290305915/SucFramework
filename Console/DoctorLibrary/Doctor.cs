using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctorLibrary
{
    public class Doctor
    {
        public string Name { get; set; } = "SUCHI";
        public int Age { get; set; } = 18;
    }

    public class DoctorProvider : IDoctor
    {
        public static Doctor _doctor = new Doctor();
        public DoctorProvider()
        {
        }
        public Doctor GetDoctor()
        {
            return _doctor;
        }

        public int GetDoctorAge()
        {
            return _doctor.Age;
        }

        public string GetDoctorName()
        {
            return _doctor.Name;
        }
    }

}
