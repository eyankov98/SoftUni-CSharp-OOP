namespace FootballTeamGenerator;

using FootballTeamGenerator.Models;

public class StartUp
{
    static void Main(string[] args)
    {
        List<Team> teams = new List<Team>();

        string command = string.Empty;

        while ((command = Console.ReadLine()) != "END")
        {
            string[] inputInfoTeam = command
                .Split(";", StringSplitOptions.RemoveEmptyEntries);

            string inputCommand = inputInfoTeam[0];
            string teamName = inputInfoTeam[1];

            try
            {
                if (inputCommand == "Team")
                {
                    AddTeam(teamName, teams);
                }
                else if (inputCommand == "Add")
                {
                    string playerName = inputInfoTeam[2];
                    int endurance = int.Parse(inputInfoTeam[3]);
                    int sprint = int.Parse(inputInfoTeam[4]);
                    int dribble = int.Parse(inputInfoTeam[5]);
                    int passing = int.Parse(inputInfoTeam[6]);
                    int shooting = int.Parse(inputInfoTeam[7]);


                    AddPLayer(
                        teamName,
                        playerName,
                        endurance,
                        sprint,
                        dribble,
                        passing, 
                        shooting,
                        teams);
                }
                else if (inputCommand == "Remove")
                {
                    string playerName = inputInfoTeam[2];

                    RemovePlayer(teamName, playerName, teams);
                }
                else if (inputCommand == "Rating")
                {
                    PrintRating(teamName, teams);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void AddTeam(string name, List<Team> teams)
            => teams.Add(new Team(name));

        static void AddPLayer(
            string teamName, 
            string playerName, 
            int endurance, 
            int sprint, 
            int dribble, 
            int passing, 
            int shooting,
            List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);

            if (team is null)
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }

            Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);

            team.AddPlayer(player);
        }

        static void RemovePlayer(string teamName, string playerName, List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);

            if (team is null)
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }

            team.RemovePlayer(playerName);
        }

        static void PrintRating(string teamName, List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);

            if (team is null)
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }

            Console.WriteLine($"{teamName} - {team.Rating:f0}");
        } 
    }
}