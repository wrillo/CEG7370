using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    internal class Emp
    {
        string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        double hoursWorked;

        public double HoursWorked
        {
            get { return hoursWorked; }
            set { hoursWorked = value;  }
        }
        double hourlyPay;

        public double HourlyPay
        {
            get { return hourlyPay; }
            set { hourlyPay = value; } 
        }

        public double Compute()
        {
            if (hoursWorked <= 40)
            {
                return hourlyPay * hoursWorked;
            }
            else
            {
                return (hourlyPay*40) + (hoursWorked-40)*(hourlyPay*1.5);
            }
        }

    }
}
