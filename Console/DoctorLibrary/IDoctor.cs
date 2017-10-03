using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctorLibrary
{
    public interface IDoctor
    {
        string GetDoctorName();
        int GetDoctorAge();

        Doctor GetDoctor();
    }
}
