using MilitaryElite.Core.Interfaces;
using MilitaryElite.Enums;
using MilitaryElite.Models;
using MilitaryElite.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Core
{
    public class Engine : IEngine
    {
        private Dictionary<int, ISoldier> soldiers;

        public Engine()
        {
            soldiers = new Dictionary<int, ISoldier>();
        }

        public void Run()
        {
            string input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                try
                {
                    string[] inputInfo = input
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    Console.WriteLine(ProcessInput(inputInfo));
                }
                catch (Exception ex)
                {
                }
            }
        }

        private string ProcessInput(string[] inputInfo)
        {
            string soldierType = inputInfo[0];
            int id = int.Parse(inputInfo[1]);
            string firstName = inputInfo[2];
            string lastName = inputInfo[3];

            ISoldier soldier = null;

            if (soldierType == "Private")
            {
                decimal salary = decimal.Parse(inputInfo[4]);

                soldier = GetPrivate(id, firstName, lastName, salary);
            }
            else if (soldierType == "LieutenantGeneral")
            {
                soldier = GetLieutenantGeneral(id, firstName, lastName, inputInfo);
            }
            else if (soldierType == "Engineer")
            {
                soldier = GetEngineer(id, firstName, lastName, inputInfo);
            }
            else if (soldierType == "Commando")
            {
                soldier = GetCommando(id, firstName, lastName, inputInfo);
            }
            else if (soldierType == "Spy")
            {
                int codeNumber = int.Parse(inputInfo[4]);

                soldier = GetSpy(id, firstName, lastName, codeNumber);
            }

            soldiers.Add(id, soldier);

            return soldier.ToString();
        }

        private ISoldier GetPrivate(int id, string firstName, string lastName, decimal salary) 
            => new Private(id, firstName, lastName, salary);

        private ISoldier GetLieutenantGeneral(int id, string firstName, string lastName, string[] inputInfo)
        {
            decimal salary = decimal.Parse(inputInfo[4]);

            List<IPrivate> privates = new List<IPrivate>();

            for (int i = 5; i < inputInfo.Length; i++)
            {
                int soldierId = int.Parse(inputInfo[i]);
                IPrivate soldier = (IPrivate)soldiers[soldierId];
                privates.Add(soldier);
            }

            return new LieutenantGeneral(id, firstName, lastName, salary, privates);
        }

        private ISoldier GetEngineer(int id, string firstName, string lastName, string[] inputInfo)
        {
            decimal salary = decimal.Parse(inputInfo[4]);
            string engineerCorps = inputInfo[5];

            bool isValidCorps = Enum.TryParse<Corps>(engineerCorps, out Corps corps);

            if (!isValidCorps)
            {
                throw new Exception();
            }

            List<IRepair> repairs = new List<IRepair>();

            for (int i = 6; i < inputInfo.Length; i += 2)
            {
                string partName = inputInfo[i];
                int hoursWorked = int.Parse(inputInfo[i + 1]);

                IRepair repair = new Repair(partName, hoursWorked);

                repairs.Add(repair);
            }

            return new Engineer(id, firstName, lastName, salary, corps, repairs);
        }

        private ISoldier GetCommando(int id, string firstName, string lastName, string[] inputInfo)
        {
            decimal salary = decimal.Parse(inputInfo[4]);
            string commandoCorps = inputInfo[5];

            bool isValidCorps = Enum.TryParse<Corps>(commandoCorps, out Corps corps);

            if (!isValidCorps)
            {
                throw new Exception();
            }

            List<IMission> missions = new List<IMission>();

            for (int i = 6; i < inputInfo.Length; i += 2)
            {
                string missionName = inputInfo[i];
                string missionState = inputInfo[i + 1];

                bool isValidMissionState = Enum.TryParse<State>(missionState, out State state);

                if (!isValidMissionState)
                {
                    continue;
                }

                IMission mission = new Mission(missionName, state);

                missions.Add(mission);
            }

            return new Commando(id, firstName, lastName, salary, corps, missions);
        }

        private ISoldier GetSpy(int id, string firstName, string lastName, int codeNumber)
            => new Spy(id, firstName, lastName, codeNumber);
    }
}
