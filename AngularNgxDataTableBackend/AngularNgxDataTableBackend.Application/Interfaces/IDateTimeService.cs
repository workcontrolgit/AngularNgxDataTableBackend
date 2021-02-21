using System;

namespace AngularNgxDataTableBackend.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
