using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIConsole;

namespace Test
{
    [TestClass]
    public class ConsoleTest
    {
        class TestJob : SIJob
        {
            public const string Code = "test";

            public string From { get; set; }
            public string To { get; set; }
        }

        class Test2Job : SIJob
        {
            public const string Code = "test2";
        }

        [TestMethod]
        public void JSONLoadTest1()
        {
            const string json = "[{\"type\":\"test\",\"params\":{\"from\":\"hello.txt\",\"to\":\"%SI_TARGET_DIRECTORY%\\\\hello.txt\"}}]";

            var loader = new SIJSONLoader();
            loader.Load(json, typeof(TestJob));
            var jobs = loader.ToJobs();

            Assert.AreEqual(1, jobs.Length);

            var testJob = (TestJob)jobs[0];

            Assert.AreEqual(testJob.From, "hello.txt");
            Assert.AreEqual(testJob.To, "%SI_TARGET_DIRECTORY%\\hello.txt");
        }
    }
}