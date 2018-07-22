using Ford.Tracker.Api.DTO.Interfaces;

namespace Ford.Tracker.Api.DTO
{
    public class GlobalPositioningSystemCreateRequest : IDataTransferObject
    {
        public string GlobalPositioningData { get; set; }
    }
}
