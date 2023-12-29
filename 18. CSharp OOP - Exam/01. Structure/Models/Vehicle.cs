using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private string licensePlateNumber;

        public Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            this.Brand = brand;
            this.Model = model;
            this.MaxMileage = maxMileage;
            this.LicensePlateNumber = licensePlateNumber;
            this.BatteryLevel = 100;
            this.IsDamaged = false;
        }

        public string Brand
        {
            get => this.brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BrandNull));
                }

                this.brand = value;
            }
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ModelNull));
                }

                this.model = value;
            }
        }

        public double MaxMileage { get; private set; }

        public string LicensePlateNumber
        {
            get => this.licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.LicenceNumberRequired));
                }

                this.licensePlateNumber = value;
            }
        }

        public int BatteryLevel { get; private set; }

        public bool IsDamaged { get; private set; }

        public void Drive(double mileage)
        {
            double percentageMileage = Math.Round((mileage / this.MaxMileage) * 100);

            this.BatteryLevel -= (int)percentageMileage;

            if (this.GetType().Name == nameof(CargoVan))
            {
                this.BatteryLevel -= 5;
            }
            
        }

        public void Recharge()
        {
            this.BatteryLevel = 100;
        }

        public void ChangeStatus()
        {
            if (!IsDamaged)
            {
                this.IsDamaged = true;
            }
            else
            {
                this.IsDamaged = false;
            }
        }

        public override string ToString()
        {
            string status = string.Empty;

            if (this.IsDamaged)
            {
                status = "damaged";
            }
            else
            {
                status = "OK";
            }

            return $"{this.Brand} {this.Model} License plate: {this.LicensePlateNumber} Battery: {this.BatteryLevel}% Status: {status}";
        }
    }
}
