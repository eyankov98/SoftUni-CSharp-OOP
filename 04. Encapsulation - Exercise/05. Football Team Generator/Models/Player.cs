using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator.Models
{
    public class Player
    {
        private const string StatOutOfRangeExceptionMessage = "{0} should be between 0 and 100.";
        private const string NameEmptyExceptionMessage = "A name should not be empty.";

        private const int StatMinValue = 0;
        private const int StatMaxValue = 100;

        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Name = name;
            Endurance = endurance;
            Sprint = sprint;
            Dribble = dribble;
            Passing = passing;
            Shooting = shooting;
        }

        public string Name 
        { 
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(NameEmptyExceptionMessage);
                }

                name = value;
            }
        }

        public int Endurance
        { 
            get
            {
                return endurance;
            } 
            set
            {
                if (CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(StatOutOfRangeExceptionMessage, nameof(Endurance)));
                }

                endurance = value;
            } 
        }

        public int Sprint
        {
            get
            {
                return sprint;
            }
            set
            {
                if (CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(StatOutOfRangeExceptionMessage, nameof(Sprint)));
                }

                sprint = value;
            }
        }

        public int Dribble
        {
            get
            {
                return dribble;
            }
            set
            {
                if (CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(StatOutOfRangeExceptionMessage, nameof(Dribble)));
                }

                dribble = value;
            }
        }

        public int Passing
        {
            get
            {
                return passing;
            }
            set
            {
                if (CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(StatOutOfRangeExceptionMessage, nameof(Passing)));
                }

                passing = value;
            }
        }

        public int Shooting
        {
            get
            {
                return shooting;
            }
            set
            {
                if (CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(StatOutOfRangeExceptionMessage, nameof(Shooting)));
                }

                shooting = value;
            } 
        }

        public double Stats 
            => (Endurance + Sprint + Dribble + Passing + Shooting) / 5.0;

        private bool CheckStatValue(int value) 
            => value < StatMinValue || value > StatMaxValue;
    }
}