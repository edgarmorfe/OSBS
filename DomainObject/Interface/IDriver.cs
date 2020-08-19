using System;

namespace DomainObject
{
    public interface IDriver
    {
        int Id { get; set; }
        string Photo { get; set; }
        string UserName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        string FirstName { get; set; }
        string PhoneNumber { get; set; }
        DateTime DateOfBirth { get; set; }
        string Address { get; set; }
        DateTime LicenseExpiry { get; set; }
        bool Status { get; set; }
    }
}