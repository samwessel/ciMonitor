namespace ciMonitor.ViewModels
{
    public class BuildOutcome
    {
        public BuildOutcome(string name, int buildNumber, Status status)
        {
            Name = name;
            BuildNumber = buildNumber;
            Status = status;
        }

        public string Name { get; private set; }
        public int BuildNumber { get; private set; }
        public Status Status { get; private set; }
    }
}