using DL;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class TenantRenewalBL
    {
        public static int Create(TenantRenewal tenantRenewal)
        {
            return TenantRenewalFacade.Create(tenantRenewal);
        }
        public static int Update(TenantRenewal tenantRenewal)
        {
            return TenantRenewalFacade.Update(tenantRenewal);
        }
        public static TenantRenewal GetTenantRenewalById(int Id)
        {
            return TenantRenewalFacade.GetTenantRenewalById(Id);
        }
        public static List<TenantRenewal> GetTenantRenewalByCheckListStatus(string status)
        {
            return TenantRenewalFacade.GetTenantRenewalByCheckListStatus(status);
        }
        public static List<TenantRenewal> GetAllTenantRenewal()
        {
            return TenantRenewalFacade.GetAllTenantRenewal();
        }
        public static int GetTenantRenewalByCheckListStatusCount(string checkListStatus)
        {
            return TenantRenewalFacade.GetTenantRenewalByCheckListStatusCount(checkListStatus);
        }
    }
}
