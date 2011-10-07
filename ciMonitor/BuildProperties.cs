namespace ciMonitor
{
    public class BuildProperties
    {
        public BuildProperties(int buildNumber, Status status, bool isBuilding)
        {
            BuildNumber = buildNumber;
            Status = status;
            IsBuilding = isBuilding;
        }

        public int BuildNumber { get; private set; }
        public Status Status { get; private set; }
        public bool IsBuilding { get; private set; }
    }
}