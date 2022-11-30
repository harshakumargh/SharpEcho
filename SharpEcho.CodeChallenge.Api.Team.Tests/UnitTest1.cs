using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEcho.CodeChallenge.Api.Team.Controllers;
using SharpEcho.CodeChallenge.Data;
using System;

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
    }
}
