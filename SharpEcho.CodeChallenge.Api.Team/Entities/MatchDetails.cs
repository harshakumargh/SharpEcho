using System;
namespace SharpEcho.CodeChallenge.Api.Team.Entities
{
	public class Matches
	{
		public Matches()
		{
		}

		public long Id { get; set; }
        public DateTime Date { get; set; }
		public long Team1 { get; set; }
		public long Team2 { get; set; }
		public long Winner { get; set; }

		public string Validate()
		{
            if (string.IsNullOrEmpty(Convert.ToString(Date)))
            {
                return "Match Date is mandatory";
            }
            if (Team1 > 0)
			{
				return "Invalid team1 selected";
			}
            if (Team2 > 0)
            {
                return "Invalid team2 selected";
            }
			if(Winner != Team1 || Winner != Team2)
			{
                return "Invalid winner selected";
            }
			return string.Empty;
        }
	}
}

