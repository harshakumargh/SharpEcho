using System;
namespace SharpEcho.CodeChallenge.Api.Team.Entities
{
	public class MatchDetails
	{
		public MatchDetails()
		{
		}

		public long Id { get; set; }
        public DateTime Date { get; set; }
		public long Team1 { get; set; }
		public long Team2 { get; set; }
		public long Winner { get; set; }

		public bool IsInputValid()
		{
            if (string.IsNullOrEmpty(Convert.ToString(Date)))
            {
				return false;
            }
            if (Team1 < 1)
			{
				return false;
			}
            if (Team2 < 1)
            {
				return false;
            }
			if(Winner != Team1 && Winner != Team2)
			{
                return false;
            }
			return true;
        }
	}
}

