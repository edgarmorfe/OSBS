namespace DomainObject
{
    public interface IEmployee
    {
        string empNo { get; set; }
        string empFName { get; set; }
        string empMName { get; set; }
        string empLName { get; set; }
        string empCorpEmail { get; set; }
        string empMobile { get; set; }
        string coName { get; set; }
        string teamName { get; set; }
        string empLoginID { get; set; }
        string supEmpNo { get; set; }
        string supEmpFName { get; set; }
        string supEmpMName { get; set; }
        string supEmpLName { get; set; }
        string supMobileNo { get; set; }
        string supEmail { get; set; }
    }
}