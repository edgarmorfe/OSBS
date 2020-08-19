namespace DomainObject
{
    public interface IReservationTransaction
    {
        int Id { get; set; }
        int ReservationId { get; set; }
        int TransactionId { get; set; }
    }
}