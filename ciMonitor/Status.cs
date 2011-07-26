namespace ciMonitor
{
    public class Status
    {
        private readonly string _status;

        public static Status Unknown()
        {
            return new Status("unknown");
        }

        public static Status Success()
        {
            return new Status("success");
        }

        public static Status Fail()
        {
            return new Status("fail");
        }

        private Status(string status)
        {
            _status = status;
        }

        public override string ToString()
        {
            return _status;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Status))
                return false;
            return ((Status)obj)._status == _status;
        }

        public bool Equals(Status other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._status, _status);
        }

        public override int GetHashCode()
        {
            return (_status != null ? _status.GetHashCode() : 0);
        }
    }
}