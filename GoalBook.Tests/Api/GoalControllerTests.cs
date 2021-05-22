using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using NUnit.Framework;

using GoalBook.Api;
using GoalBook.Core.Domain.Entities;

namespace GoalBook.Tests.Api
{
    [TestFixture]
    public class GoalControllerTests
    {
        private const string HostUri = "https://localhost:37903/api/goal";

        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = GetHttpClient();
        }

        [Test]
        public async Task GetGoalsTest()
        {
            var requestUri = $"{HostUri}/get-all";
            var expected = HttpStatusCode.OK;

            using (var response = await _client.GetAsync(requestUri))
            {
                var actual = response.StatusCode;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(" ", HttpStatusCode.BadRequest)]
        [TestCase("335f7107-2819-4b91-868d-37f556c2e7b6", HttpStatusCode.NoContent)]
        [TestCase("f00d312b-9c69-4f96-8af4-83f2edc2b6f4", HttpStatusCode.OK)]
        public async Task GetGoalExpectIdTests(string goalId, HttpStatusCode expected)
        {
            var requestUri = $"{HostUri}/get?id={goalId}";

            using (var response = await _client.GetAsync(requestUri))
            {
                var actual = response.StatusCode;
                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        public async Task CreateGoalTest()
        {
            var requestUri = $"{HostUri}/create";
            var expected = HttpStatusCode.Created;

            using (var response = await _client.PostAsJsonAsync(requestUri, CreateGoal(" ")))
            {
                var actual = response.StatusCode;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(" ", HttpStatusCode.BadRequest)]
        public async Task UpdateGoalTests(string goalId, HttpStatusCode expected)
        {
            var requestUri = $"{HostUri}/update";
            var requestPostUri = $"{HostUri}/create";

            await _client.PostAsJsonAsync(requestPostUri, CreateGoal(goalId));


            using (var response = await _client.PutAsJsonAsync(requestUri, CreateGoal(goalId)))
            {
                var actual = response.StatusCode;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(" ", HttpStatusCode.BadRequest)]
        [TestCase("335f7107-2819-4b91-868d-37f575c2e7b6", HttpStatusCode.NoContent)]
        public async Task DeleteGoalExpectIdTests(string goalId, HttpStatusCode expected)
        {
            var requestUri = $"{HostUri}/delete?id={goalId}";
            var requestPostUri = $"{HostUri}/create";

            await _client.PostAsJsonAsync(requestPostUri, CreateGoal(goalId));

            using (var response = await _client.DeleteAsync(requestUri))
            {
                var actual = response.StatusCode;
                Assert.AreEqual(expected, actual);
            }
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        private HttpClient GetHttpClient()
        {
            return new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseConfiguration(GetConfiguration())
                .UseStartup<Startup>())
                .CreateClient();
        }

        private IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private Goal CreateGoal(string id)
        {
            return Goal.Create()
                .SetTitle(" ")
                .SetDescription(" ")
                .SetDate(DateTime.Now, null)
                .Build();
        }
    }
}
