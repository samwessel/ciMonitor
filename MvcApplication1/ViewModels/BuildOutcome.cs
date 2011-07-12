namespace ciMonitor.ViewModels
{
    public class BuildOutcome
    {
        public BuildOutcome(string name, Status status)
        {
            Name = name;
            Status = status;
        }

        public string Name { get; private set; }
        public Status Status { get; private set; }
    }
}