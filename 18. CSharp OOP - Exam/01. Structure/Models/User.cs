using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        private string firstName;
        private string lastName;
        private string drivingLicenseNumber;

        public User (string firstName, string lastName, string drivingLicenseNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DrivingLicenseNumber = drivingLicenseNumber;
            this.Rating = 0;
            this.IsBlocked = false;
        }

        public string FirstName
        {
            get => this.firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.FirstNameNull));
                }

                this.firstName = value;
            }
        }

        public string LastName
        {
            get => this.lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.LastNameNull));
                }

                this.lastName = value;
            }
        }

        public double Rating { get; private set; }

        public string DrivingLicenseNumber
        {
            get => this.drivingLicenseNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.DrivingLicenseRequired));
                }

                this.drivingLicenseNumber = value;
            }
        }

        public bool IsBlocked { get; private set; }

        public void IncreaseRating()
        {
            this.Rating += 0.5;

            if (this.Rating > 10)
            {
                this.Rating = 10;
            }
        }

        public void DecreaseRating()
        {
            this.Rating -= 2;

            if (this.Rating < 0)
            {
                this.Rating = 0;

                this.IsBlocked = true;
            }
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} Driving license: {this.DrivingLicenseNumber} Rating: {this.Rating}";
        }
    }
}
