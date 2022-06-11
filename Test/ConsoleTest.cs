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
            const string json = "{\"args\":[\"M_TARGET_DIRECTORY\"],\"jobs\":[{\"type\":\"test\",\"tag\":[\"a\",\"b\"],\"params\":{\"from\":\"hello.txt\",\"to\":\"%SI_TARGET_DIRECTORY%\\\\hello.txt\"}},{\"type\":\"test2\",\"tag\":[\"a\",\"-b\"],\"params\":{\"a\":3,\"b\":5}}]}";
            #endregion
            var loader = new SIJSONLoader(typeof(TestJob), typeof(TestJob2));
            SIData data = loader.Load(json);
            var args = data.Args;
            var jobs = data.Jobs;

            CollectionAssert.AreEqual(new string[]{"M_TARGET_DIRECTORY"}, args);
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