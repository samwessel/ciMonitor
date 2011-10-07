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

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof (BuildOutcome)) 
                return false;
            return Equals((BuildOutcome) obj);
        }

        public bool Equals(BuildOutcome other)
        {
            return Equals(other.Name, Name) 
                && other.BuildNumber == BuildNumber 
                && Equals(other.Status, Status);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Name != null ? Name.GetHashCode() : 0);
                result = (result*397) ^ BuildNumber;
                result = (result*397) ^ (Status != null ? Status.GetHashCode() : 0);
                return result;
            }
        }
    }
}