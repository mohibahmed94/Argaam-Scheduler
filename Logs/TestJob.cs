namespace ArgaamSchedular.Logs
{
    public class TestJob
    {
        private readonly ILogger<TestJob> _logger;

        public TestJob(ILogger<TestJob> logger) => _logger = logger;


    }
}
