using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GreetingsIntegrationTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async System.Threading.Tasks.Task HaveGreetingsData_ReturnNotFound()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            using (var server = new HttpServer(config))
            {
                var client = new HttpClient(server);
                string url = "http://localhost:5000/api/Greetings/";
                using (var response = await client.GetAsync(url))
                {
                    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                }
            }
        }
    }
}