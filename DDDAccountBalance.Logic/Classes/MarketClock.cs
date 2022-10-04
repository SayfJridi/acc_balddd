using NodaTime;

namespace DDDAccountBalance.Logic.Classes
{
    public class TimeProvider : IClock
    {
        public TimeProvider(DateTime time)
        {
            DateTime now = time; 
        }
        public Instant GetCurrentInstant()
        {
            return new 
        }
    }
}
