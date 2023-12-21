using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Team : ITeam
    {
        private string name;

        private List<IPlayer> players;

        public Team(string name)
        {
            this.Name = name;
            this.PointsEarned = 0;

            players = new List<IPlayer>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.TeamNameNull));
                }

                this.name = value;
            }
        }

        public int PointsEarned { get; private set; }

        public double OverallRating
        {
            get
            {
                if (!this.players.Any())
                {
                    return 0;
                }

                double totalRating = this.players.Sum(p => p.Rating);

                double averageRating = totalRating / this.players.Count;

                Math.Round(averageRating, 2);

                return averageRating;
            }
        }

        public IReadOnlyCollection<IPlayer> Players => this.players;

        public void SignContract(IPlayer player)
        {
            this.players.Add(player);
        }

        public void Win()
        {
            this.PointsEarned += 3;

            foreach (var player in this.players)
            {
                player.IncreaseRating();
            }
        }

        public void Lose()
        {
            foreach (var player in this.players)
            {
                player.DecreaseRating();
            }
        }

        public void Draw()
        {
            this.PointsEarned += 1;

            foreach (var player in this.players)
            {
                if (player.GetType().Name == nameof(Goalkeeper))
                {
                    player.IncreaseRating();
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Team: {this.Name} Points: {this.PointsEarned}");
            sb.AppendLine($"--Overall rating: {this.OverallRating}");
            sb.Append("--Players: ");
            if (this.players.Any())
            {
                sb.Append(string.Join(", ", this.players.Select(p => p.Name)));
            }
            else
            {
                sb.Append("none");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
