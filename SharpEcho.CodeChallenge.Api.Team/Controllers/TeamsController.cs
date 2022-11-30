using Microsoft.AspNetCore.Mvc;
using SharpEcho.CodeChallenge.Data;
using System.Linq;

namespace SharpEcho.CodeChallenge.Api.Team.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : GenericController<Entities.Team>
    {
        public TeamsController(IRepository repository) : base(repository)
        {
        }

        [HttpGet("GetTeamByName")]
        public virtual ActionResult<Entities.Team> GetTeamByName (string name)
        {
            var result = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = name });
            if (result != null && result.Count() > 0)
            {
                return result.First();
            }
            return NotFound();
        }

        [HttpPost("RecordMatch")]
        public virtual ActionResult<Entities.Matches> RecordMatch([FromBody] Entities.Matches match)
        {
            if (!string.IsNullOrEmpty(match.Validate()))
            {
                var id = Repository.Insert(match);
                var updatedEntity = match as dynamic;
                updatedEntity.Id = id;

                return Created(match.GetType().Name + '/' + id.ToString(), updatedEntity);
            }
            return BadRequest(match.Validate());
        }

        [HttpGet("GetMatchUps")]
        public virtual ActionResult<Entities.MatchUps> GetMatchUps(int team1, int team2)
        {
            string query = "SELECT SUM(team1wins) AS Team1, SUM(team2wins) AS Team2 FROM ((SELECT team1, team2,(CASE WHEN winner = @Team1 THEN 1 ELSE 0 END) as team1wins,(CASE WHEN winner = @Team2 THEN 1 ELSE 0 END) as team2wins FROM Matches WHERE (team1 =@Team1 and team2 = @Team2 ) OR (team1 =@Team2 and team2 = @Team1))) t";
            var result = Repository.Query<Entities.MatchUps>(query, new { Team1 = team1, Team2 = team2 });
            if (result != null && result.Count() > 0)
            {
                return result.First();
            }
            return NotFound();
        }
    }
}
