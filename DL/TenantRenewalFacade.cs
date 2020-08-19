using Dapper;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public static class TenantRenewalFacade
    {
        public static int Create(TenantRenewal tenantRenewal)
        {
            DynamicParameters dParams = GenerateDynamicParameter(tenantRenewal);
            return DataAccessHelper.SaveData<TenantRenewal>("spInsertTenantRenewal", dParams);
        }

        public static int Update(TenantRenewal tenantRenewal)
        {
            DynamicParameters dParams = GenerateDynamicParameter(tenantRenewal);
            return DataAccessHelper.SaveData<TenantRenewal>("spUpdateTenantRenewalById", dParams);
        }

        private static DynamicParameters GenerateDynamicParameter(TenantRenewal tenantRenewal)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@id", tenantRenewal.Id);
            dParams.Add("@tenantId", tenantRenewal.TenantId);
            dParams.Add("@name", tenantRenewal.Name);
            dParams.Add("@area", tenantRenewal.Area);
            dParams.Add("@leasingStatus", tenantRenewal.LeasingStatus);
            dParams.Add("@expiryDate", tenantRenewal.ExpiryDate);
            //dParams.Add("@renewalRequirements", tenantRenewal.RenewalRequirements);
            dParams.Add("@contractSignedNotarized", tenantRenewal.ContractSignedNotarized);
            dParams.Add("@balancesPaid", tenantRenewal.BalancesPaid);
            dParams.Add("@renewalSigned", tenantRenewal.RenewalSigned);
            dParams.Add("@underperformer", tenantRenewal.Underperformer);
            dParams.Add("@category", tenantRenewal.Category);
            dParams.Add("@categorySpecialist", tenantRenewal.CategorySpecialist);
            dParams.Add("@underperformerYears", tenantRenewal.UnderperformerYears);
            dParams.Add("@underperformerMonth", tenantRenewal.UnderperformerMonth);
            dParams.Add("@currentRentBase", tenantRenewal.CurrentRentBase);
            dParams.Add("@currentMGR", tenantRenewal.CurrentMGR);
            dParams.Add("@historicalPerformanceYear1", tenantRenewal.HistoricalPerformanceYear1);
            dParams.Add("@aveSalesPerSQMYear1", tenantRenewal.AveSalesPerSQMYear1);
            dParams.Add("@aveEffectiveRentYear1", tenantRenewal.AveEffectiveRentYear1);
            dParams.Add("@historicalPerformanceYear2", tenantRenewal.HistoricalPerformanceYear2);
            dParams.Add("@aveSalesPerSQMYear2", tenantRenewal.AveSalesPerSQMYear2);
            dParams.Add("@aveEffectiveRentYear2", tenantRenewal.AveEffectiveRentYear2);
            dParams.Add("@historicalPerformanceYear3", tenantRenewal.HistoricalPerformanceYear3);
            dParams.Add("@aveSalesPerSQMYear3", tenantRenewal.AveSalesPerSQMYear3);
            dParams.Add("@aveEffectiveRentYear3", tenantRenewal.AveEffectiveRentYear3);
            dParams.Add("@historicalPerformanceYear4", tenantRenewal.HistoricalPerformanceYear4);
            dParams.Add("@aveSalesPerSQMYear4", tenantRenewal.AveSalesPerSQMYear4);
            dParams.Add("@aveEffectiveRentYear4", tenantRenewal.AveEffectiveRentYear4);
            dParams.Add("@leasingCategorySpecialistRemarks", tenantRenewal.LeasingCategorySpecialistRemarks);
            dParams.Add("@leasingReviewDone", tenantRenewal.LeasingReviewDone);
            dParams.Add("@leasingHeadRemarks", tenantRenewal.LeasingHeadRemarks);
            dParams.Add("@leasingHeadVerified", tenantRenewal.LeasingHeadVerified);
            dParams.Add("@designStatus", tenantRenewal.DesignStatus);
            dParams.Add("@forMajorRenovation", tenantRenewal.ForMajorRenovation);
            dParams.Add("@designRemarks", tenantRenewal.DesignRemarks);
            dParams.Add("@designReviewDone", tenantRenewal.DesignReviewDone);
            dParams.Add("@designHeadVerified", tenantRenewal.DesignHeadVerified);
            dParams.Add("@operationStatus", tenantRenewal.OperationStatus);
            dParams.Add("@operationFacilitiesRemarks", tenantRenewal.OperationFacilitiesRemarks);
            dParams.Add("@operationFacilitiesReviewDone", tenantRenewal.OperationFacilitiesReviewDone);
            dParams.Add("@operationTenancyRemarks", tenantRenewal.OperationTenancyRemarks);
            dParams.Add("@operationsTenancyReviewDone", tenantRenewal.OperationsTenancyReviewDone);
            dParams.Add("@operationHouseKeepingRemarks", tenantRenewal.OperationHouseKeepingRemarks);
            dParams.Add("@operationHouseKeepingReviewDone", tenantRenewal.OperationHouseKeepingReviewDone);
            dParams.Add("@operationSecurityRemarks", tenantRenewal.OperationSecurityRemarks);
            dParams.Add("@operationSecurityReviewDone", tenantRenewal.OperationSecurityReviewDone);
            dParams.Add("@operationsHeadVerified", tenantRenewal.OperationsHeadVerified);
            dParams.Add("@iTStatus", tenantRenewal.ITStatus);
            dParams.Add("@pOSSystemAgreement", tenantRenewal.POSSystemAgreement);
            dParams.Add("@dailySalesReporting", tenantRenewal.DailySalesReporting);
            dParams.Add("@iTRemarks", tenantRenewal.ITRemarks);
            dParams.Add("@iTReviewDone", tenantRenewal.ITReviewDone);
            dParams.Add("@iTHeadVerified", tenantRenewal.ITHeadVerified);
            dParams.Add("@financeStatus", tenantRenewal.FinanceStatus);
            dParams.Add("@unpaidCharges", tenantRenewal.UnpaidCharges);
            dParams.Add("@advanceRentPaid", tenantRenewal.AdvanceRentPaid);
            dParams.Add("@securityDepositPaid", tenantRenewal.SecurityDepositPaid);
            dParams.Add("@financeReviewDone", tenantRenewal.FinanceReviewDone);
            dParams.Add("@financeHeadVerified", tenantRenewal.FinanceHeadVerified);
            dParams.Add("@statusStatus", tenantRenewal.StatusStatus);
            dParams.Add("@auditRequirements", tenantRenewal.AuditRequirements);
            dParams.Add("@auditRemarks", tenantRenewal.AuditRemarks);
            dParams.Add("@auditReviewDone", tenantRenewal.AuditReviewDone);
            dParams.Add("@auditHeadVerified", tenantRenewal.AuditHeadVerified);
            dParams.Add("@vPGMStatus", tenantRenewal.VPGMStatus);
            dParams.Add("@vPGMRemarks", tenantRenewal.VPGMRemarks);
            dParams.Add("@vPGMHeadVerified", tenantRenewal.VPGMHeadVerified);
            dParams.Add("@checklistStatus", tenantRenewal.ChecklistStatus);
            dParams.Add("@reviewDate", tenantRenewal.ReviewDate);
            dParams.Add("@reviewDoneBy", tenantRenewal.ReviewDoneBy);
            dParams.Add("@leasingHeadVerifiedDate", tenantRenewal.LeasingHeadVerifiedDate);
            dParams.Add("@leasingHeadVerifiedBy", tenantRenewal.LeasingHeadVerifiedBy);
            dParams.Add("@designReviewDoneDate", tenantRenewal.DesignReviewDoneDate);
            dParams.Add("@designReviewDoneBy", tenantRenewal.DesignReviewDoneBy);
            dParams.Add("@designHeadVerifiedDate", tenantRenewal.DesignHeadVerifiedDate);
            dParams.Add("@designHeadVerifiedBy", tenantRenewal.DesignHeadVerifiedBy);
            dParams.Add("@operationFacilitiesReviewDoneDate", tenantRenewal.OperationFacilitiesReviewDoneDate);
            dParams.Add("@operationFacilitiesReviewDoneBy", tenantRenewal.OperationFacilitiesReviewDoneBy);
            dParams.Add("@operationsTenancyReviewDoneDate", tenantRenewal.OperationsTenancyReviewDoneDate);
            dParams.Add("@operationsTenancyReviewDoneBy", tenantRenewal.OperationsTenancyReviewDoneBy);
            dParams.Add("@operationHouseKeepingReviewDoneDate", tenantRenewal.OperationHouseKeepingReviewDoneDate);
            dParams.Add("@operationHouseKeepingReviewDoneBy", tenantRenewal.OperationHouseKeepingReviewDoneBy);
            dParams.Add("@operationSecurityReviewDoneDate", tenantRenewal.OperationSecurityReviewDoneDate);
            dParams.Add("@operationSecurityReviewDoneBy", tenantRenewal.OperationSecurityReviewDoneBy);
            dParams.Add("@operationsHeadVerifiedDate", tenantRenewal.OperationsHeadVerifiedDate);
            dParams.Add("@operationsHeadVerifiedBy", tenantRenewal.OperationsHeadVerifiedBy);
            dParams.Add("@iTReviewDoneDate", tenantRenewal.ITReviewDoneDate);
            dParams.Add("@iTReviewDoneBy", tenantRenewal.ITReviewDoneBy);
            dParams.Add("@iTHeadVerifiedDate", tenantRenewal.ITHeadVerifiedDate);
            dParams.Add("@iTHeadVerifiedBy", tenantRenewal.ITHeadVerifiedBy);
            dParams.Add("@financeReviewDoneDate", tenantRenewal.FinanceReviewDoneDate);
            dParams.Add("@financeReviewDoneBy", tenantRenewal.FinanceReviewDoneBy);
            dParams.Add("@auditReviewDoneDate", tenantRenewal.AuditReviewDoneDate);
            dParams.Add("@auditReviewDoneBy", tenantRenewal.AuditReviewDoneBy);
            dParams.Add("@auditHeadVerifiedDate", tenantRenewal.AuditHeadVerifiedDate);
            dParams.Add("@auditHeadVerifiedBy", tenantRenewal.AuditHeadVerifiedBy);
            dParams.Add("@vPGMHeadVerifiedDate", tenantRenewal.VPGMHeadVerifiedDate);
            dParams.Add("@vPGMHeadVerifiedBy", tenantRenewal.VPGMHeadVerifiedBy);
            dParams.Add("@auditCMSRSubmissions", tenantRenewal.AuditCMSRSubmissions);
            dParams.Add("@auditTextFileFormat", tenantRenewal.AuditTextFileFormat);
            return dParams;
        }

        public static List<TenantRenewal> GetAllTenantRenewal()
        {
            return DataAccessHelper.LoadData<TenantRenewal>("spGetTenantRenewal");
        }

        public static TenantRenewal GetTenantRenewalById(int Id)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", Id);
            return DataAccessHelper.LoadData<TenantRenewal>("spGetTenantRenewalById", dParam).FirstOrDefault();
        }

        public static List<TenantRenewal> GetTenantRenewalByCheckListStatus(string status)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@checkListStatus", status);
            return DataAccessHelper.LoadData<TenantRenewal>("spGetTenantRenewalByCheckListStatus", dParam);
        }

        public static int GetTenantRenewalByCheckListStatusCount(string checkListStatus)
        {
            var dParams = new DynamicParameters();
            dParams.Add("checklistStatus", checkListStatus);
            dParams.Add("@statusCount", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            return DataAccessHelper.ReturnInt("spGetTenantRenewalByCheckListStatusCount", dParams).Get<int>("@statusCount");
        }
    }
}
