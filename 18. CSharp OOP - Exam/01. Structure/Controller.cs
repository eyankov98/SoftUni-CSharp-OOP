using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private UserRepository users;
        private VehicleRepository vehicles;
        private RouteRepository routes;

        public Controller()
        {
            this.users = new UserRepository();
            this.vehicles = new VehicleRepository();
            this.routes = new RouteRepository();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            IUser user = this.users.FindById(drivingLicenseNumber); // take first user with userDrivingLicenseNumber wich is equal to given drivingLicenseNumber

            if (user != null) // If there is already a user with the same drivingLicenseNumber
            {
                return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }
            else // If there is not a user with the same drivingLicenseNumber
            {
                user = new User(firstName, lastName, drivingLicenseNumber); // create a new User

                this.users.AddModel(user); // Adding user in collection

                return string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
            }
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != nameof(PassengerCar) && // If the given vehicleTypeName is NOT presented as a valid Vehicle’s child class
                vehicleType != nameof(CargoVan))
            {
                return string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
            }

            IVehicle vehicle = this.vehicles.FindById(licensePlateNumber); // take first user with vehicleLicensePlateNumber wich is equal to given licensePlateNumber

            if (vehicle != null) // If there is already a vehicle with the same licensePlateNumber
            {
                return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
            }
            else //  If there is not already a vehicle with the same licensePlateNumber
            {
                if (vehicleType == nameof(PassengerCar)) // If the given vehicleTypeName is presented as a valid Vehicle’s child class
                {
                    vehicle = new PassengerCar(brand, model, licensePlateNumber); // create new vehicle
                }
                else if (vehicleType == nameof(CargoVan))
                {
                    vehicle = new CargoVan(brand, model, licensePlateNumber);
                }

                this.vehicles.AddModel(vehicle); // Adding vehicle in collection

                return string.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
            }
        }

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            int routeId = this.routes.GetAll().Count + 1; // creat routeId

            IRoute route = this.routes.GetAll().FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint); // take of all routes first route wich routeStartPoint is equal to given startPoint and routeEndPoint is equal to given endPoint

            if (route != null) // check if route with given startPoint and end point is already exist in collection 
            {
                if (route.Length == length) // and route.Length is equal to given length
                {
                    return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
                }
                else if (route.Length < length) // and route.Length is less then given length
                {
                    return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
                }
                else if (route.Length > length) // and route.Length is greater then given length
                {
                    route.LockRoute(); // lock the longer route
                }
            }

            // else If the above cases is not reached

            IRoute newRoute = new Route(startPoint, endPoint, length, routeId); // create new route

            this.routes.AddModel(newRoute); // adding new route in collection

            return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = this.users.FindById(drivingLicenseNumber); // take first user with userDrivingLicenseNumber wich is equal to given drivingLicenseNumber

            if (user.IsBlocked) // if user.IsBlocked is true
            {
                return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }

            IVehicle vehicle = this.vehicles.FindById(licensePlateNumber); // take first user with vehicleLicensePlateNumber wich is equal to given licensePlateNumber

            if (vehicle.IsDamaged) // if vehicle.IsDamaged is true
            {
                return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }

            IRoute route = this.routes.FindById(routeId); // take first route with routeId wich is equal to given routeId 

            if (route.IsLocked) // if route.IsLocked is true
            {
                return string.Format(OutputMessages.RouteLocked, routeId);
            }

            vehicle.Drive(route.Length); // Drive the specific vehicle on the specific route

            if (isAccidentHappened) // If the value of the parameter isAccidentHappened is true
            {
                vehicle.ChangeStatus(); // the IsDamaged status of the vehicle should be changed to true

                user.DecreaseRating(); // The Rating of the User who has rented the Vehicle should be decreased
            }
            else
            {
                user.IncreaseRating(); // increase the User’s Rating
            }

            return vehicle.ToString();
        }

        public string RepairVehicles(int count)
        {
            var damagedVehicles = this.vehicles.GetAll().Where(v => v.IsDamaged == true).OrderBy(v => v.Brand).ThenBy(v => v.Model); // The method should select only those vehicles from the VеhicleRepository, which are damaged. Order the selected vehicles alphabetically by their Brand, then alphabetically by their Model

            int countOfRepairedVehicles = 0; // make counter for RepairedVehicles

            if (damagedVehicles.Count() < count)
            {
                countOfRepairedVehicles = damagedVehicles.Count();
            }
            else
            {
                countOfRepairedVehicles = count;
            }

            var selectedVehicles = damagedVehicles.ToArray().Take(countOfRepairedVehicles);

            foreach (var vehicle in selectedVehicles)
            {
                vehicle.ChangeStatus(); // Each of the chosen vehicles will be repaired (IsDamaged == false)
                vehicle.Recharge();
            }

            return string.Format(OutputMessages.RepairedVehicles, countOfRepairedVehicles);
        }

        public string UsersReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("*** E-Drive-Rent ***");

            foreach (var user in this.users.GetAll().OrderByDescending(u => u.Rating).ThenBy(u => u.LastName).ThenBy(u => u.FirstName))
            {
                sb.AppendLine(user.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
