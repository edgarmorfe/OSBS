using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Controllers
{
    public static class Computation
    {
        public static double ProgressPercentage(TenantRenewal tenantRenewal)
        {
            int completedSteps = 0;
            if (tenantRenewal.LeasingReviewDone)
                completedSteps += 1;
            if (tenantRenewal.LeasingHeadVerified)
                completedSteps += 1;
            if (tenantRenewal.DesignReviewDone)
                completedSteps += 1;
            if (tenantRenewal.DesignHeadVerified)
                completedSteps += 1;
            if (tenantRenewal.OperationFacilitiesReviewDone)
                completedSteps += 1;
            if (tenantRenewal.OperationsTenancyReviewDone)
                completedSteps += 1;
            if (tenantRenewal.OperationHouseKeepingReviewDone)
                completedSteps += 1;
            if (tenantRenewal.OperationSecurityReviewDone)
                completedSteps += 1;
            if (tenantRenewal.OperationsHeadVerified)
                completedSteps += 1;
            if (tenantRenewal.ITReviewDone)
                completedSteps += 1;
            if (tenantRenewal.ITHeadVerified)
                completedSteps += 1;
            if (tenantRenewal.FinanceReviewDone)
                completedSteps += 1;
            if (tenantRenewal.FinanceHeadVerified)
                completedSteps += 1;
            if (tenantRenewal.AuditReviewDone)
                completedSteps += 1;
            if (tenantRenewal.AuditHeadVerified)
                completedSteps += 1;
            if (tenantRenewal.VPGMHeadVerified)
                completedSteps += 1;
            double result = ((double)completedSteps / 16) * 100;
            return Math.Round(result, 0);   
        }
    }
}