using System;

namespace MQFlux.Models
{
    public struct BusyBuffingResult
    {
        public BusyBuffingResult(bool isBusyBuffing)
        {
            IsBusyBuffing = isBusyBuffing;
            Timestamp = DateTime.UtcNow;
        }

        public bool IsBusyBuffing { get; }
        public DateTime Timestamp { get; }
    }
}
