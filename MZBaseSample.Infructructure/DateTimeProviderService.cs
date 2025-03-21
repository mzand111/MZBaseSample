using MZBase.Infrastructure;

namespace MZBaseSample.Infrastructure
{
    public class DateTimeProviderService : IDateTimeProviderService
    {
        public DateTime GetNow()
        {
            return DateTime.UtcNow;
        }
    }
}
