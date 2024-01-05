using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count + 1; // creating boothId BoothRepository(booths.Models.Count) + 1

            Booth booth = new Booth(boothId, capacity);

            booths.AddModel(booth);

            return string.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            IDelicacy delicacy;

            if (delicacyTypeName != nameof(Gingerbread) && delicacyTypeName != nameof(Stolen)) // check if delicacyTypeName is not supported in application
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            if (booths.Models.Any(b => b.DelicacyMenu.Models.Any(dm => dm.Name == delicacyName))) // check if given delicacyName already exists in delicacy repository
            {
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            if (delicacyTypeName == nameof(Gingerbread)) // check if delicacyTypeName is supported in application and create new delicacy with the given delicacyName
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else // delicacyTypeName == nameof(Stolen)
            {
                 delicacy = new Stolen(delicacyName);
            }

            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId); // take first booth.BoothId where is equal to given boothId

            booth.DelicacyMenu.AddModel(delicacy); // added to the DelicacyMenu of the Booth with the given boothId

            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            ICocktail cocktail;

            if (cocktailTypeName != nameof(MulledWine) && cocktailTypeName != nameof(Hibernation)) // check if the given cocktailTypeName is not supported in the application
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            if (size != "Small" && size != "Middle" && size != "Large") // check if the given size is different from the supported in the application
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }

            if (booths.Models.Any(b => b.CocktailMenu.Models.Any(cm => cm.Name == cocktailName && cm.Size == size))) // check if cocktailName and size already exists in the cocktail repository
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            if (cocktailTypeName == nameof(MulledWine)) // check if cocktailTypeName is supported in application and create new cocktail with the given cocktailName and size
            {
                cocktail = new MulledWine(cocktailName, size); 
            }
            else // cocktailTypeName == nameof(Hibernation)
            {
                cocktail = new Hibernation(cocktailName, size); 
            }

            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId); // take first booth.BoothId where is equal to given boothId

            booth.CocktailMenu.AddModel(cocktail); // added to the CocktailMenu of the Booth with the given boothId

            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            var orderBooths = booths.Models.Where(b => b.IsReserved == false && b.Capacity >= countOfPeople).OrderBy(b => b.Capacity).ThenByDescending(b => b.BoothId); //Order all booths from the BoothRepository

            IBooth booth = orderBooths.FirstOrDefault(); // Take the first available Booth

            if (booth == null) // check if there is no such booth
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }
            else // check if an available Booth is found
            {
                booth.ChangeStatus(); // sets the IsReserved status to true

                return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
            }
        }

        public string TryOrder(int boothId, string order)
        {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            string[] orderArray = order.Split("/");

            bool isCocktail = false;
            
            string itemTypeName = orderArray[0];

            if (itemTypeName != nameof(Hibernation) &&
                itemTypeName != nameof(MulledWine) &&
                itemTypeName != nameof(Gingerbread) && 
                itemTypeName != nameof(Stolen))
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }

            string itemName = orderArray[1];

            if (!booth.DelicacyMenu.Models.Any(dm => dm.Name == itemName) &&
                !booth.CocktailMenu.Models.Any(cm => cm.Name == itemName))
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }

            int countOfOrderedPieces = int.Parse(orderArray[2]);

            if (itemTypeName == nameof(Hibernation) || itemTypeName == nameof(MulledWine))
            {
                isCocktail = true;
            }

            if (isCocktail)
            {
                string size = orderArray[3];

                ICocktail desiredCocktail = booth.CocktailMenu.Models.FirstOrDefault(c => c.GetType().Name == itemTypeName && c.Name == itemName && c.Size == size);

                if (desiredCocktail == null)
                {
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }
                else
                {
                    booth.UpdateCurrentBill(desiredCocktail.Price * countOfOrderedPieces);

                    return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countOfOrderedPieces, itemName);
                }
            }
            else
            {
                IDelicacy desiredDelicacy = booth.DelicacyMenu.Models.FirstOrDefault(d => d.GetType().Name == itemTypeName && d.Name == itemName);

                if (desiredDelicacy == null)
                {
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                }
                else
                {
                    booth.UpdateCurrentBill(desiredDelicacy.Price * countOfOrderedPieces);

                    return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countOfOrderedPieces, itemName);
                }
            }
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format(OutputMessages.GetBill, $"{booth.CurrentBill:F2}"));

            booth.Charge();

            booth.ChangeStatus();

            sb.AppendLine(string.Format(OutputMessages.BoothIsAvailable, boothId));

            return sb.ToString().TrimEnd();
        }

        public string BoothReport(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            return booth.ToString().TrimEnd();
        }
    }
}
