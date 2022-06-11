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
            public string Return { get; set; }

            public override void Execute()
            {
                Return = "Hello World";
                Progress = 1;
            }
        }

        class TestJob2 : SIJob
        {
            public const string Code = "test2";

            public int A { get; set; }
            public int B { get; set; }
            public int Return { get; set; }

            public override void Execute()
            {
                Return = A + B;
                Progress = 1;
            }
        }

        [TestMethod]
        public void JSONLoadTest1()
        {
            #region json
            const string json = "[{\"type\":\"test\",\"params\":{\"from\":\"hello.txt\",\"to\":\"%SI_TARGET_DIRECTORY%\\\\hello.txt\"}},{\"type\":\"test2\",\"params\":{\"a\":3,\"b\":5}}]";
            #endregion
            var loader = new SIJSONLoader();
            loader.Load(json, typeof(TestJob), typeof(TestJob2));
            var jobs = loader.ToJobs();

            Assert.AreEqual(2, jobs.Length);

            var testJob = (TestJob)jobs[0];

            Assert.AreEqual(testJob.From, "hello.txt");
            Assert.AreEqual(testJob.To, "%SI_TARGET_DIRECTORY%\\hello.txt");

            var testJob2 = (TestJob2)jobs[1];

            Assert.AreEqual(testJob2.A, 3);
            Assert.AreEqual(testJob2.B, 5);

            var executor = new SIJobExecutor(jobs);
            executor.ExecuteAll();
            Assert.AreEqual(testJob.Return, "Hello World");
            Assert.AreEqual(testJob2.Return, 8);
            Assert.AreEqual(testJob2.Progress, 1);
        }
    }
}