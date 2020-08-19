namespace DomainObject
{
    public interface IShuttle
    {
        string Coding { get; set; }
        int Id { get; set; }
        string PlateNo { get; set; }
        int SeatCount { get; set; }
        string Status { get; set; }
        int SchedId { get; set; }
        int SsdId { get; set; }
    }
}