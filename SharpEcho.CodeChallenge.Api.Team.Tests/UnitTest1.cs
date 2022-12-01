using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEcho.CodeChallenge.Data;
using SharpEcho.CodeChallenge.Api.Team.Controllers;

namespace SharpEcho.CodeChallenge.Api.Team.Tests
{
    [TestClass]
    public class TeamUnitTests
    {
        IRepository Repository = new GenericRepository(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SharpEcho"));

        [TestMethod]
        public void GetTeamByName_ShouldReturnCorrectTeam()
        {
            var controller = new TeamsController(Repository);

            var team = new Entities.Team
            {
                Name = "Houston Cougars"
            };

            var result = controller.GetTeamByName(team.Name).Value;

            if (result == null)
            {
                controller.Post(team);
                result = controller.GetTeamByName(team.Name).Value;
            }

            Assert.AreEqual(team.Name, result.Name);
        }

        [TestMethod]
        public void GetTeamByName_ShouldNotReturnTeam()
        {
            var controller = new TeamsController(Repository);

            var team = new Entities.Team
            {
                Name = Guid.NewGuid().ToString()
            };

            var result = controller.GetTeamByName(team.Name).Value;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void AddMatch_ShouldReturnSuccess()
        {
            var controller = new TeamsController(Repository);

            var match = new Entities.MatchDetails
            {
                Team1 = 1,
                Team2 = 2,
                Winner = 2,
                Date = DateTime.Now
            };

            var result = controller.AddMatch(match);
            Assert.IsNotNull(result.Result); 
        }

        [TestMethod]
        public void AddMatch_ShouldReturnFailure_InvalidInput()
        {
            var controller = new TeamsController(Repository);

            var match = new Entities.MatchDetails
            {
                Team1 = 0,
                Team2 = 2,
                Winner = 2,
                Date = DateTime.Now
            };

            var result = controller.AddMatch(match);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void GetMatchUps_ShouldReturnCorrectResponse()
        {
            var controller = new TeamsController(Repository);

            var team = new Entities.MatchUps
            {
                Team1 = 1,
                Team2 = 2
            };

            var result = controller.GetMatchUps(team.Team1, team.Team2).Value;

            Assert.IsTrue(result.Team1 >= 0);
        }

        [TestMethod]
        public void GetMatchUps_ShouldNotReturnEmptyResponse()
        {
            var controller = new TeamsController(Repository);

            var team = new Entities.MatchUps
            {
                Team1 = 0,
                Team2 = 2
            };

            var result = controller.GetMatchUps(team.Team1, team.Team2).Value;

            Assert.IsNull(result);
        }
    }
}
