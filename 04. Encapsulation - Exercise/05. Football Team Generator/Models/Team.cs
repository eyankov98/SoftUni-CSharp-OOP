﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator.Models
{
    public class Team
    {
        private const string NameEmptyExceptionMessage = "A name should not be empty.";
        private const string PlayerNotFoundExceptionMessage = "Player {0} is not in {1} team.";

        private string name;
        private List<Player> players;

        public Team(string name)
        {
            Name = name;
            players = new List<Player>();
        }

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(NameEmptyExceptionMessage);
                }

                name = value;
            }
        }
        public double Rating 
        { 
            get
            {
                if (players.Any())
                {
                    return players.Average(p => p.Stats);
                }

                return 0;
            }
        }

        public void AddPlayer(Player player) 
            => players.Add(player);

        public void RemovePlayer(string playerName)
        {
            Player player = players.FirstOrDefault(p => p.Name == playerName);

            if (player is null)
            {
                throw new ArgumentException(string.Format(PlayerNotFoundExceptionMessage, playerName, Name));
            }

            players.Remove(player);
        }
    }
}