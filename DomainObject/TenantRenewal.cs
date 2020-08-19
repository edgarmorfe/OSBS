using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class TenantRenewal
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int MyProperty { get; set; }
        public string Name { get; set; }
        public decimal Area { get; set; }
        public string LeasingStatus { get; set; }
        public string RenewalRequirements { get; set; }
        public bool ContractSignedNotarized { get; set; }
        public bool BalancesPaid { get; set; }
        public bool RenewalSigned { get; set; }
        public bool Underperformer { get; set; }
        public string Category { get; set; }
        public string CategorySpecialist { get; set; }
        public int UnderperformerYears { get; set; }
        public int UnderperformerMonth { get; set; }
        public decimal CurrentRentBase { get; set; }
        public string CurrentMGR { get; set; }
        public int HistoricalPerformanceYear1 { get; set; }
        public decimal AveSalesPerSQMYear1 { get; set; }
        public decimal AveEffectiveRentYear1 { get; set; }
        public int HistoricalPerformanceYear2 { get; set; }
        public decimal AveSalesPerSQMYear2 { get; set; }
        public decimal AveEffectiveRentYear2 { get; set; }
        public int HistoricalPerformanceYear3 { get; set; }
        public decimal AveSalesPerSQMYear3 { get; set; }
        public decimal AveEffectiveRentYear3 { get; set; }
        public int HistoricalPerformanceYear4 { get; set; }
        public decimal AveSalesPerSQMYear4 { get; set; }
        public decimal AveEffectiveRentYear4 { get; set; }
        public string LeasingCategorySpecialistRemarks { get; set; }
        public bool LeasingReviewDone { get; set; }
        public string LeasingHeadRemarks { get; set; }
        public bool LeasingHeadVerified { get; set; }
        public string DesignStatus { get; set; }
        public bool ForMajorRenovation { get; set; }
        public string DesignRemarks { get; set; }
        public bool DesignReviewDone { get; set; }
        public bool DesignHeadVerified { get; set; }
        public string OperationStatus { get; set; }
        public string OperationFacilitiesRemarks { get; set; }
        public bool OperationFacilitiesReviewDone { get; set; }
        public string OperationTenancyRemarks { get; set; }
        public bool OperationsTenancyReviewDone { get; set; }
        public string OperationHouseKeepingRemarks { get; set; }
        public bool OperationHouseKeepingReviewDone { get; set; }
        public string OperationSecurityRemarks { get; set; }
        public bool OperationSecurityReviewDone { get; set; }
        public bool OperationsHeadVerified { get; set; }
        public string ITStatus { get; set; }
        public string POSSystemAgreement { get; set; }
        public string DailySalesReporting { get; set; }
        public string ITRemarks { get; set; }
        public bool ITReviewDone { get; set; }
        public bool ITHeadVerified { get; set; }
        public string FinanceStatus { get; set; }
        public decimal UnpaidCharges { get; set; }
        public decimal AdvanceRentPaid { get; set; }
        public decimal SecurityDepositPaid { get; set; }
        public bool FinanceReviewDone { get; set; }
        public bool FinanceHeadVerified { get; set; }
        public string StatusStatus { get; set; }
        public string AuditRequirements { get; set; }
        public string AuditRemarks { get; set; }
        public bool AuditReviewDone { get; set; }
        public bool AuditHeadVerified { get; set; }
        public string VPGMStatus { get; set; }
        public string VPGMRemarks { get; set; }
        public bool VPGMHeadVerified { get; set; }
        public string ChecklistStatus { get; set; }

        public DateTime ReviewDate {get; set;}
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
        public DateTime AuditReviewDoneDate { get; set; }
        public string AuditReviewDoneBy { get; set; }
        public DateTime AuditHeadVerifiedDate { get; set; }
        public string AuditHeadVerifiedBy { get; set; }
        public DateTime VPGMHeadVerifiedDate { get; set; }
        public string VPGMHeadVerifiedBy { get; set; }
        public bool AuditCMSRSubmissions { get; set; }
        public bool AuditTextFileFormat { get; set; }
    }
}
