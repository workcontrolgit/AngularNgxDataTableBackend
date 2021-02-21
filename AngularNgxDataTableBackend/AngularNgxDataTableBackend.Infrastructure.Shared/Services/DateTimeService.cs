using AngularNgxDataTableBackend.Application.Interfaces;
using System;

namespace AngularNgxDataTableBackend.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
