using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class TenantRenewalView
    {
        public int Id { get; set; }
        [Display(Name = "Tenant Id")]
        public string TenantId { get; set; }
        public DateTime Expiry { get; set; }
        [Display(Name = "My Property")]
        public int MyProperty { get; set; }
        public string Name { get; set; }
        public decimal Area { get; set; }
        [Display(Name = "Leasing Status")]
        public string LeasingStatus { get; set; }
        public string LeasingStatusString { get; set; }
        [Display(Name = "Renewal Requirements")]
        public string RenewalRequirements { get; set; }
        [Display(Name= "Contract Signed and Notarized")]
        public bool ContractSignedNotarized { get; set; }
        public bool BalancesPaid { get; set; }
        public bool RenewalSigned { get; set; }
        public bool Underperformer { get; set; }
        public string Category { get; set; }
        [Display(Name = "Category Specialist")]
        public string CategorySpecialist { get; set; }
        [Display(Name = "Underformer Years")]
        public int UnderperformerYears { get; set; }
        [Display(Name = "Underperformer Month")]
        public int UnderperformerMonth { get; set; }
        [Display(Name = "Current Rent Base")]
        public decimal CurrentRentBase { get; set; }
        [Display(Name = "Current MGR")]
        public string CurrentMGR { get; set; }
        [Display(Name = "Historical Performance Year 1")]
        public int HistoricalPerformanceYear1 { get; set; }
        [Display(Name = "Ave Sales Per SQM Year 1")]
        public decimal AveSalesPerSQMYear1 { get; set; }
        [Display(Name = "Ave Effective Rent Year 1")]
        public decimal AveEffectiveRentYear1 { get; set; }
        [Display(Name = "Historical Performance Year 2")]
        public int HistoricalPerformanceYear2 { get; set; }
        [Display(Name = "Ave Sales Per SQM Year 2")]
        public decimal AveSalesPerSQMYear2 { get; set; }
        [Display(Name = "Ave Effective Rent Year 2")]
        public decimal AveEffectiveRentYear2 { get; set; }
        [Display(Name = "Historical Performance Year 3")]
        public int HistoricalPerformanceYear3 { get; set; }
        [Display(Name = "Ave Sales Per SQM Year 3")]
        public decimal AveSalesPerSQMYear3 { get; set; }
        [Display(Name = "Ave Effective Rent Year 3")]
        public decimal AveEffectiveRentYear3 { get; set; }
        [Display(Name = "Historical Performance Year 4")]
        public int HistoricalPerformanceYear4 { get; set; }
        [Display(Name = "Ave Sales Per SQM Year 4")]
        public decimal AveSalesPerSQMYear4 { get; set; }
        [Display(Name = "Ave Effective Rent Year 4")]
        public decimal AveEffectiveRentYear4 { get; set; }
        [Display(Name = "Leasing Category Specialist Remarks")]
        public string LeasingCategorySpecialistRemarks { get; set; }
        [Display(Name = "Leasing Review Done")]
        public bool LeasingReviewDone { get; set; }
        [Display(Name = "Leasing Head Remarks")]
        public string LeasingHeadRemarks { get; set; }
        [Display(Name = "Leasing Head Verified")]
        public bool LeasingHeadVerified { get; set; }
        [Display(Name = "Design Status")]
        public string DesignStatus { get; set; }
        public string DesignStatusString { get; set; }
        [Display(Name = "For Major Renovation")]
        public bool ForMajorRenovation { get; set; }
        [Display(Name = "Design Remarks")]
        public string DesignRemarks { get; set; }
        [Display(Name = "Design Review Done")]
        public bool DesignReviewDone { get; set; }
        [Display(Name = "Design Head Verified")]
        public bool DesignHeadVerified { get; set; }
        [Display(Name = "Operation Status")]
        public string OperationStatus { get; set; }
        public string OperationStatusString { get; set; }
        [Display(Name = "Operation Facilities Remarks")]
        public string OperationFacilitiesRemarks { get; set; }
        public string OperationFacilitiesReviewerName { get; set; }
        [Display(Name = "Operation Facilities Review Done")]
        public bool OperationFacilitiesReviewDone { get; set; }
        [Display(Name = "Operation Tenancy Remarks")]
        public string OperationTenancyRemarks { get; set; }
        [Display(Name = "Operation Tenancy Review Done")]
        public bool OperationsTenancyReviewDone { get; set; }
        public string OperationTenancyReviewerName { get; set; }
        [Display(Name = "Operation House Keeping Remarks")]
        public string OperationHouseKeepingRemarks { get; set; }
        [Display(Name = "Operation House Keeping Review Done")]
        public bool OperationHouseKeepingReviewDone { get; set; }
        public string OperationHouseKeepingReviewerName { get; set; }
        [Display(Name = "Operation Secuirty Remarks")]
        public string OperationSecurityRemarks { get; set; }
        [Display(Name = "Operation Security Review Done")]
        public bool OperationSecurityReviewDone { get; set; }
        public string OperationSecurityReviewerName { get; set; }
        [Display(Name = "Operation Head Verified")]
        public bool OperationsHeadVerified { get; set; }
        [Display(Name = "IT Status")]
        public string ITStatus { get; set; }
        public string ITStatusString { get; set; }
        [Display(Name = "POS System Agreement")]
        public string POSSystemAgreement { get; set; }
        [Display(Name = "Daily Sales Reporting")]
        public string DailySalesReporting { get; set; }
        [Display(Name = "IT Remarks")]
        public string ITRemarks { get; set; }
        [Display(Name = "IT Review Done")]
        public bool ITReviewDone { get; set; }
        [Display(Name = "IT Head Verified")]
        public bool ITHeadVerified { get; set; }
        [Display(Name = "Finance Status")]
        public string FinanceStatus { get; set; }
        public string FinanceStatusString { get; set; }
        [Display(Name = "Unpaid Charges")]
        public decimal UnpaidCharges { get; set; }
        [Display(Name = "Advance Rent Paid")]
        public decimal AdvanceRentPaid { get; set; }
        [Display(Name = "Security Deposit Paid")]
        public decimal SecurityDepositPaid { get; set; }
        [Display(Name = "Finance Review Done")]
        public bool FinanceReviewDone { get; set; }
        [Display(Name = "Finance Head Verified")]
        public bool FinanceHeadVerified { get; set; }
        [Display(Name = "Audit Status")]
        public string AuditStatus { get; set; }
        public string AuditStatusString { get; set; }
        [Display(Name = "Audit Requirements")]
        public string AuditRequirements { get; set; }
        [Display(Name = "Audit Remarks")]
        public string AuditRemarks { get; set; }
        [Display(Name = "Audit Review Done")]
        public bool AuditReviewDone { get; set; }
        [Display(Name = "Audit Head Verified")]
        public bool AuditHeadVerified { get; set; }
        [Display(Name = "VP/GM Status")]
        public string VPGMStatus { get; set; }
        public string VPGMStatusString { get; set; }
        [Display(Name = "VP/GM Remarks")]
        public string VPGMRemarks { get; set; }
        [Display(Name = "VP/GM Head Verified")]
        public bool VPGMHeadVerified { get; set; }
        [Display(Name = "Checklist Status")]
        public string ChecklistStatus { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewDoneBy { get; set; }
        public DateTime LeasingHeadVerifiedDate { get; set; }
        public string LeasingHeadVerifiedBy { get; set; }
        public DateTime DesignReviewDoneDate { get; set; }
        public string DesignReviewDoneBy { get; set; }
        public DateTime DesignHeadVerifiedDate { get; set; }
        public string DesignHeadVerifiedBy { get; set; }
        public DateTime OperationFacilitiesReviewDoneDate { get; set; }
        public string OperationFacilitiesReviewDoneBy { get; set; }
        public DateTime OperationsTenancyReviewDoneDate { get; set; }
        public string OperationsTenancyReviewDoneBy { get; set; }
        public DateTime OperationHouseKeepingReviewDoneDate { get; set; }
        public string OperationHouseKeepingReviewDoneBy { get; set; }
        public DateTime OperationSecurityReviewDoneDate { get; set; }
        public string OperationSecurityReviewDoneBy { get; set; }
        public DateTime OperationsHeadVerifiedDate { get; set; }
        public string OperationsHeadVerifiedBy { get; set; }
        public DateTime ITReviewDoneDate { get; set; }
        public string ITReviewDoneBy { get; set; }
        public DateTime ITHeadVerifiedDate { get; set; }
        public string ITHeadVerifiedBy { get; set; }
        public DateTime FinanceReviewDoneDate { get; set; }
        public string FinanceReviewDoneBy { get; set; }
        public DateTime FinanceVerifiedDoneDate { get; set; }
        public string FinanceVerifiedDoneBy { get; set; }
        public DateTime AuditReviewDoneDate { get; set; }
        public string AuditReviewDoneBy { get; set; }
        public DateTime AuditHeadVerifiedDate { get; set; }
        public string AuditHeadVerifiedBy { get; set; }
        public DateTime VPGMHeadVerifiedDate { get; set; }
        public string VPGMHeadVerifiedBy { get; set; }
        [Display(Name = "CMRS Submissions")]
        public bool AuditCMSRSubmissions { get; set; }
        [Display(Name="Text File Format")]
        public bool AuditTextFileFormat { get; set; }
        public string LeasingReviewerName { get; set; }
        public string LeasingVerifierName { get; set; }
        public string DesignReviewerName { get; set; }
        public string DesignVerifierName { get; set; }
        public string OperationReviewerName { get; set; }
        public string OperationVerifierName { get; set; }
        public string ITReviewerName { get; set; }
        public string ITVerifierName { get; set; }
        public string FinanceReviewerName { get; set; }
        public string FinanceVerifierName { get; set; }
        public string AuditReviewerName { get; set; }
        public string AuditVerifierName { get; set; }
        public string VPGMVerifierName { get; set; }
        public double Progress { get; set; }
        public List<Status> Statuses { get; set; }
    }

    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}