namespace DomainObject
{
    public interface ITransaction
    {
        decimal Amount { get; set; }
        decimal Balance { get; set; }
        string CreatedBy { get; set; }
        string Description { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string OrNo { get; set; }
        string UserName { get; set; }
    }
}