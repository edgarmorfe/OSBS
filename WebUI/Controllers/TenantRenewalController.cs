using BL;
using DomainObject;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebUI.Attributes;
using WebUI.Models;

namespace WebUI.Controllers
{
    [SessionExpire]
    [Authorize]

    public class TenantRenewalController : Controller
    {
        // GET: TenantRenewal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update(int id)
        {
            var _tenantDO = TenantRenewalBL.GetTenantRenewalById(id);
            var _progress = Computation.ProgressPercentage(_tenantDO);
            // populate statuses
            var _statuses = GenerateStatus();

            TenantRenewalView _tenantmodel = PopulateTenantRenewalView(_tenantDO, _progress, _statuses);
            return View(_tenantmodel);
        }

        private TenantRenewalView PopulateTenantRenewalView(TenantRenewal _tenantDO, double _progress, List<Status> _statuses)
        {
            TenantRenewalView _tenantmodel = new TenantRenewalView()
            {
                Id = _tenantDO.Id,
                TenantId = _tenantDO.TenantId,
                Expiry = _tenantDO.ExpiryDate,
                MyProperty = _tenantDO.MyProperty,
                Name = _tenantDO.Name,
                Area = _tenantDO.Area,
                LeasingStatus = _tenantDO.LeasingStatus,
                LeasingStatusString = ConvertStatusToString(_tenantDO.LeasingStatus),
                //RenewalRequirements = _tenantDO.RenewalRequirements,
                ContractSignedNotarized = _tenantDO.ContractSignedNotarized,
                BalancesPaid = _tenantDO.BalancesPaid,
                RenewalSigned = _tenantDO.RenewalSigned,
                Underperformer = _tenantDO.Underperformer,
                Category = _tenantDO.Category,
                CategorySpecialist = _tenantDO.CategorySpecialist,
                UnderperformerYears = _tenantDO.UnderperformerYears,
                UnderperformerMonth = _tenantDO.UnderperformerMonth,
                CurrentRentBase = _tenantDO.CurrentRentBase,
                CurrentMGR = _tenantDO.CurrentMGR,
                HistoricalPerformanceYear1 = _tenantDO.HistoricalPerformanceYear1,
                AveSalesPerSQMYear1 = _tenantDO.AveSalesPerSQMYear1,
                AveEffectiveRentYear1 = _tenantDO.AveEffectiveRentYear1,
                HistoricalPerformanceYear2 = _tenantDO.HistoricalPerformanceYear2,
                AveSalesPerSQMYear2 = _tenantDO.AveSalesPerSQMYear2,
                AveEffectiveRentYear2 = _tenantDO.AveEffectiveRentYear2,
                HistoricalPerformanceYear3 = _tenantDO.HistoricalPerformanceYear3,
                AveSalesPerSQMYear3 = _tenantDO.AveSalesPerSQMYear3,
                AveEffectiveRentYear3 = _tenantDO.AveEffectiveRentYear3,
                HistoricalPerformanceYear4 = _tenantDO.HistoricalPerformanceYear4,
                AveSalesPerSQMYear4 = _tenantDO.AveSalesPerSQMYear4,
                AveEffectiveRentYear4 = _tenantDO.AveEffectiveRentYear4,
                LeasingCategorySpecialistRemarks = _tenantDO.LeasingCategorySpecialistRemarks,
                LeasingReviewDone = _tenantDO.LeasingReviewDone,
                LeasingHeadRemarks = _tenantDO.LeasingHeadRemarks,
                LeasingHeadVerified = _tenantDO.LeasingHeadVerified,
                DesignStatus = _tenantDO.DesignStatus,
                DesignStatusString = ConvertStatusToString(_tenantDO.DesignStatus),
                ForMajorRenovation = _tenantDO.ForMajorRenovation,
                DesignRemarks = _tenantDO.DesignRemarks,
                DesignReviewDone = _tenantDO.DesignReviewDone,
                DesignHeadVerified = _tenantDO.DesignHeadVerified,
                OperationStatus = _tenantDO.OperationStatus,
                OperationStatusString = ConvertStatusToString(_tenantDO.OperationStatus),
                OperationFacilitiesRemarks = _tenantDO.OperationFacilitiesRemarks,
                OperationFacilitiesReviewDone = _tenantDO.OperationFacilitiesReviewDone,
                OperationTenancyRemarks = _tenantDO.OperationTenancyRemarks,
                OperationsTenancyReviewDone = _tenantDO.OperationsTenancyReviewDone,
                OperationHouseKeepingRemarks = _tenantDO.OperationHouseKeepingRemarks,
                OperationHouseKeepingReviewDone = _tenantDO.OperationHouseKeepingReviewDone,
                OperationSecurityRemarks = _tenantDO.OperationSecurityRemarks,
                OperationSecurityReviewDone = _tenantDO.OperationSecurityReviewDone,
                OperationsHeadVerified = _tenantDO.OperationsHeadVerified,
                ITStatus = _tenantDO.ITStatus,
                ITStatusString = ConvertStatusToString(_tenantDO.ITStatus),
                POSSystemAgreement = _tenantDO.POSSystemAgreement,
                DailySalesReporting = _tenantDO.DailySalesReporting,
                ITRemarks = _tenantDO.ITRemarks,
                ITReviewDone = _tenantDO.ITReviewDone,
                ITHeadVerified = _tenantDO.ITHeadVerified,
                FinanceStatus = _tenantDO.FinanceStatus,
                FinanceStatusString = ConvertStatusToString(_tenantDO.FinanceStatus),
                UnpaidCharges = _tenantDO.UnpaidCharges,
                AdvanceRentPaid = _tenantDO.AdvanceRentPaid,
                SecurityDepositPaid = _tenantDO.SecurityDepositPaid,
                FinanceReviewDone = _tenantDO.FinanceReviewDone,
                FinanceHeadVerified = _tenantDO.FinanceHeadVerified,
                AuditStatus = _tenantDO.StatusStatus,
                AuditStatusString = ConvertStatusToString(_tenantDO.StatusStatus),
                AuditRequirements = _tenantDO.AuditRequirements,
                AuditRemarks = _tenantDO.AuditRemarks,
                AuditReviewDone = _tenantDO.AuditReviewDone,
                AuditHeadVerified = _tenantDO.AuditHeadVerified,
                VPGMStatus = _tenantDO.VPGMStatus,
                VPGMStatusString = ConvertStatusToString(_tenantDO.VPGMStatus),
                VPGMRemarks = _tenantDO.VPGMRemarks,
                VPGMHeadVerified = _tenantDO.VPGMHeadVerified,
                ChecklistStatus = _tenantDO.ChecklistStatus,
                AuditCMSRSubmissions = _tenantDO.AuditCMSRSubmissions,
                AuditTextFileFormat = _tenantDO.AuditTextFileFormat,
                LeasingHeadVerifiedDate = _tenantDO.LeasingHeadVerifiedDate,
                LeasingHeadVerifiedBy = _tenantDO.LeasingHeadVerifiedBy,
                ReviewDate = _tenantDO.ReviewDate,
                ReviewDoneBy = _tenantDO.ReviewDoneBy,
                DesignReviewDoneDate = _tenantDO.DesignReviewDoneDate,
                DesignReviewDoneBy = _tenantDO.DesignReviewDoneBy,
                DesignHeadVerifiedDate = _tenantDO.DesignHeadVerifiedDate,
                DesignHeadVerifiedBy = _tenantDO.DesignHeadVerifiedBy,
                OperationFacilitiesReviewDoneDate = _tenantDO.OperationFacilitiesReviewDoneDate,
                OperationFacilitiesReviewDoneBy = _tenantDO.OperationFacilitiesReviewDoneBy,
                OperationsTenancyReviewDoneDate = _tenantDO.OperationsTenancyReviewDoneDate,
                OperationsTenancyReviewDoneBy = _tenantDO.OperationsTenancyReviewDoneBy,
                OperationHouseKeepingReviewDoneDate = _tenantDO.OperationHouseKeepingReviewDoneDate,
                OperationHouseKeepingReviewDoneBy = _tenantDO.OperationHouseKeepingReviewDoneBy,
                OperationSecurityReviewDoneDate = _tenantDO.OperationSecurityReviewDoneDate,
                OperationSecurityReviewDoneBy = _tenantDO.OperationSecurityReviewDoneBy,
                OperationsHeadVerifiedDate = _tenantDO.OperationsHeadVerifiedDate,
                OperationsHeadVerifiedBy = _tenantDO.OperationsHeadVerifiedBy,
                ITReviewDoneDate = _tenantDO.ITReviewDoneDate,
                ITReviewDoneBy = _tenantDO.ITReviewDoneBy,
                ITHeadVerifiedDate = _tenantDO.ITHeadVerifiedDate,
                ITHeadVerifiedBy = _tenantDO.ITHeadVerifiedBy,
                FinanceReviewDoneDate = _tenantDO.FinanceReviewDoneDate,
                FinanceReviewDoneBy = _tenantDO.FinanceReviewDoneBy,
                FinanceVerifiedDoneDate = _tenantDO.FinanceReviewDoneDate,
                FinanceVerifiedDoneBy = _tenantDO.FinanceReviewDoneBy,
                AuditReviewDoneDate = _tenantDO.AuditReviewDoneDate,
                AuditReviewDoneBy = _tenantDO.AuditReviewDoneBy,
                AuditHeadVerifiedDate = _tenantDO.AuditHeadVerifiedDate,
                AuditHeadVerifiedBy = _tenantDO.AuditHeadVerifiedBy,
                VPGMHeadVerifiedDate = _tenantDO.VPGMHeadVerifiedDate,
                VPGMHeadVerifiedBy = _tenantDO.VPGMHeadVerifiedBy,
                Progress = _progress,
                Statuses = _statuses
            };
            return _tenantmodel;
        }

        private List<Status> GenerateStatus()
        {
            List<Status> statuses = new List<Status>()
            {
                new Status(){ Id = 0, Name = "Completed" },
                new Status(){ Id = 1, Name = "For Renewal"},
                new Status(){ Id = 2, Name = "On Hold"}
            };
            return statuses;
        }

        [HttpPost]
        public ActionResult Update(TenantRenewalView tenantRenewalView)
        {
            TenantRenewal _tenantDO = new TenantRenewal()
            {
                Id = tenantRenewalView.Id,
                TenantId = tenantRenewalView.TenantId,
                ExpiryDate = tenantRenewalView.Expiry,
                MyProperty = tenantRenewalView.MyProperty,
                Name = tenantRenewalView.Name,
                Area = tenantRenewalView.Area,
                LeasingStatus = tenantRenewalView.LeasingStatus,
                //RenewalRequirements = _tenantDO.RenewalRequirements,
                ContractSignedNotarized = tenantRenewalView.ContractSignedNotarized,
                BalancesPaid = tenantRenewalView.BalancesPaid,
                RenewalSigned = tenantRenewalView.RenewalSigned,
                Underperformer = tenantRenewalView.Underperformer,
                Category = tenantRenewalView.Category,
                CategorySpecialist = tenantRenewalView.CategorySpecialist,
                UnderperformerYears = tenantRenewalView.UnderperformerYears,
                UnderperformerMonth = tenantRenewalView.UnderperformerMonth,
                CurrentRentBase = tenantRenewalView.CurrentRentBase,
                CurrentMGR = tenantRenewalView.CurrentMGR,
                HistoricalPerformanceYear1 = tenantRenewalView.HistoricalPerformanceYear1,
                AveSalesPerSQMYear1 = tenantRenewalView.AveSalesPerSQMYear1,
                AveEffectiveRentYear1 = tenantRenewalView.AveEffectiveRentYear1,
                HistoricalPerformanceYear2 = tenantRenewalView.HistoricalPerformanceYear2,
                AveSalesPerSQMYear2 = tenantRenewalView.AveSalesPerSQMYear2,
                AveEffectiveRentYear2 = tenantRenewalView.AveEffectiveRentYear2,
                HistoricalPerformanceYear3 = tenantRenewalView.HistoricalPerformanceYear3,
                AveSalesPerSQMYear3 = tenantRenewalView.AveSalesPerSQMYear3,
                AveEffectiveRentYear3 = tenantRenewalView.AveEffectiveRentYear3,
                HistoricalPerformanceYear4 = tenantRenewalView.HistoricalPerformanceYear4,
                AveSalesPerSQMYear4 = tenantRenewalView.AveSalesPerSQMYear4,
                AveEffectiveRentYear4 = tenantRenewalView.AveEffectiveRentYear4,
                LeasingCategorySpecialistRemarks = tenantRenewalView.LeasingCategorySpecialistRemarks,
                LeasingReviewDone = tenantRenewalView.LeasingReviewDone,
                ReviewDoneBy = tenantRenewalView.LeasingReviewDone == true ? UserInfo.Username : null,
                ReviewDate = tenantRenewalView.LeasingReviewDone == true ? DateTime.Now : new DateTime(1753, 1, 1),
                LeasingHeadRemarks = tenantRenewalView.LeasingHeadRemarks,
                LeasingHeadVerified = tenantRenewalView.LeasingHeadVerified,
                LeasingHeadVerifiedBy = tenantRenewalView.LeasingHeadVerified == true ? UserInfo.Username : null,
                LeasingHeadVerifiedDate = tenantRenewalView.LeasingHeadVerified == true ? DateTime.Now : new DateTime(1753, 1, 1),
                AuditHeadVerifiedBy = tenantRenewalView.LeasingHeadVerified == true ? UserInfo.Username : null,
                AuditHeadVerifiedDate = tenantRenewalView.LeasingHeadVerified == true ? DateTime.Now : new DateTime(1753, 1, 1),
                DesignStatus = tenantRenewalView.DesignStatus,
                ForMajorRenovation = tenantRenewalView.ForMajorRenovation,
                DesignRemarks = tenantRenewalView.DesignRemarks,
                DesignReviewDone = tenantRenewalView.DesignReviewDone,
                DesignReviewDoneBy = tenantRenewalView.DesignReviewDone == true ? UserInfo.Username : null,
                DesignReviewDoneDate = tenantRenewalView.DesignReviewDone == true ? DateTime.Now : new DateTime(1753, 1, 1),
                DesignHeadVerified = tenantRenewalView.DesignHeadVerified,
                DesignHeadVerifiedBy = tenantRenewalView.DesignHeadVerified == true ? UserInfo.Username : null,
                DesignHeadVerifiedDate = tenantRenewalView.DesignHeadVerified == true ? DateTime.Now : new DateTime(1753, 1, 1),
                OperationStatus = tenantRenewalView.OperationStatus,
                OperationFacilitiesRemarks = tenantRenewalView.OperationFacilitiesRemarks,
                OperationFacilitiesReviewDone = tenantRenewalView.OperationFacilitiesReviewDone,
                OperationFacilitiesReviewDoneBy = tenantRenewalView.OperationFacilitiesReviewDone == true ? UserInfo.Username : null,
                OperationFacilitiesReviewDoneDate = tenantRenewalView.OperationFacilitiesReviewDone == true ? DateTime.Now : new DateTime(1753, 1, 1),
                OperationTenancyRemarks = tenantRenewalView.OperationTenancyRemarks,
                OperationsTenancyReviewDone = tenantRenewalView.OperationsTenancyReviewDone,
                OperationsTenancyReviewDoneBy = tenantRenewalView.OperationsTenancyReviewDone == true ? UserInfo.Username : null,
                OperationsTenancyReviewDoneDate = tenantRenewalView.OperationsTenancyReviewDone == true ? DateTime.Now : new DateTime(1753, 1, 1),
                OperationHouseKeepingRemarks = tenantRenewalView.OperationHouseKeepingRemarks,
                OperationHouseKeepingReviewDone = tenantRenewalView.OperationHouseKeepingReviewDone,
                OperationHouseKeepingReviewDoneBy = tenantRenewalView.OperationHouseKeepingReviewDone == true ? UserInfo.Username : null,
                OperationHouseKeepingReviewDoneDate = tenantRenewalView.OperationHouseKeepingReviewDone == true ? DateTime.Now : new DateTime(1753, 1, 1),
                OperationSecurityRemarks = tenantRenewalView.OperationSecurityRemarks,
                OperationSecurityReviewDone = tenantRenewalView.OperationSecurityReviewDone,
                OperationSecurityReviewDoneBy = tenantRenewalView.OperationSecurityReviewDone == true ? UserInfo.Username : null,
                OperationSecurityReviewDoneDate = tenantRenewalView.OperationSecurityReviewDone == true ? DateTime.Now : new DateTime(1753, 1, 1),
                OperationsHeadVerified = tenantRenewalView.OperationsHeadVerified,
                OperationsHeadVerifiedBy = tenantRenewalView.OperationsHeadVerified == true ? UserInfo.Username : null,
                OperationsHeadVerifiedDate = tenantRenewalView.OperationsHeadVerified == true ? DateTime.Now : new DateTime(1753, 1, 1),
                ITStatus = tenantRenewalView.ITStatus,
                POSSystemAgreement = tenantRenewalView.POSSystemAgreement,
                DailySalesReporting = tenantRenewalView.DailySalesReporting,
                ITRemarks = tenantRenewalView.ITRemarks,
                ITReviewDone = tenantRenewalView.ITReviewDone,
                ITReviewDoneBy = tenantRenewalView.ITReviewDone == true ? UserInfo.Username : null,
                ITReviewDoneDate = tenantRenewalView.ITReviewDone == true ? DateTime.Now : new DateTime(1753, 1, 1),
                ITHeadVerified = tenantRenewalView.ITHeadVerified,
                ITHeadVerifiedBy = tenantRenewalView.ITHeadVerified == true ? UserInfo.UserId : null,
                ITHeadVerifiedDate = tenantRenewalView.ITHeadVerified == true ? DateTime.Now : new DateTime(1753, 1, 1),
                FinanceStatus = tenantRenewalView.FinanceStatus,
                UnpaidCharges = tenantRenewalView.UnpaidCharges,
                AdvanceRentPaid = tenantRenewalView.AdvanceRentPaid,
                SecurityDepositPaid = tenantRenewalView.SecurityDepositPaid,
                FinanceReviewDone = tenantRenewalView.FinanceReviewDone,
                FinanceReviewDoneBy = tenantRenewalView.FinanceReviewDone == true ? UserInfo.Username : null,
                FinanceReviewDoneDate = tenantRenewalView.FinanceReviewDone == true ? DateTime.Now : new DateTime(1753, 1, 1),
                FinanceHeadVerified = tenantRenewalView.FinanceHeadVerified,
                StatusStatus = tenantRenewalView.AuditStatus,
                AuditRequirements = tenantRenewalView.AuditRequirements,
                AuditRemarks = tenantRenewalView.AuditRemarks,
                AuditReviewDone = tenantRenewalView.AuditReviewDone,
                AuditReviewDoneBy = tenantRenewalView.AuditReviewDone == true ? UserInfo.Username : null,
                AuditReviewDoneDate = tenantRenewalView.AuditReviewDone == true ? DateTime.Now : new DateTime(1753, 1, 1),
                AuditHeadVerified = tenantRenewalView.AuditHeadVerified,
                VPGMStatus = tenantRenewalView.VPGMStatus,
                VPGMRemarks = tenantRenewalView.VPGMRemarks,
                VPGMHeadVerified = tenantRenewalView.VPGMHeadVerified,
                VPGMHeadVerifiedBy = tenantRenewalView.VPGMHeadVerified == true ? UserInfo.Username : null,
                VPGMHeadVerifiedDate = tenantRenewalView.VPGMHeadVerified == true ? DateTime.Now : new DateTime(1753, 1, 1),
                AuditCMSRSubmissions = tenantRenewalView.AuditCMSRSubmissions,
                AuditTextFileFormat = tenantRenewalView.AuditTextFileFormat,
                ChecklistStatus = tenantRenewalView.ChecklistStatus
            };
            var progress = Computation.ProgressPercentage(_tenantDO);
            _tenantDO.ChecklistStatus = PopulateChecklistStatus(tenantRenewalView, progress);
            TenantRenewalBL.Update(_tenantDO);
            TempData["message"] = "Saved Successful";
            return RedirectToAction("Update", new { id = _tenantDO.Id });
        }

        public ActionResult all(string filter)
        {
            var _tenantViews = new List<TenantRenewalView>();
            var _tenants = new List<TenantRenewal>();
            if (filter.Equals("all"))
                _tenants = TenantRenewalBL.GetAllTenantRenewal();
            else
                _tenants = TenantRenewalBL.GetTenantRenewalByCheckListStatus(filter);

            if (!User.IsInRole("admin") && User.IsInRole("VP/GM Head"))
                _tenants = _tenants.Where(t => t.VPGMHeadVerified == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Audit Head Approver"))
                _tenants = _tenants.Where(t => t.AuditHeadVerified == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Audit Reviewer"))
                _tenants = _tenants.Where(t => t.AuditReviewDone == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Finance Head Approver"))
                _tenants = _tenants.Where(t => t.FinanceHeadVerified == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Finance Reviewer"))
                _tenants = _tenants.Where(t => t.FinanceReviewDone == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("IT Head Approver"))
                _tenants = _tenants.Where(t => t.ITHeadVerified == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("IT Reviewer"))
                _tenants = _tenants.Where(t => t.ITReviewDone == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Operations Head Approver"))
                _tenants = _tenants.Where(t => t.OperationsHeadVerified == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Operation Security Reviewer"))
                _tenants = _tenants.Where(t => t.OperationSecurityReviewDone).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Operation House Keeping Reviewer"))
                _tenants = _tenants.Where(t => t.OperationHouseKeepingReviewDone == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Operations Tenancy Reviewer"))
                _tenants = _tenants.Where(t => t.OperationsTenancyReviewDone == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Operation Facilities Reviewer"))
                _tenants = _tenants.Where(t => t.OperationFacilitiesReviewDone == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Design Head Approver"))
                _tenants = _tenants.Where(t => t.DesignHeadVerified == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Design Reviewer"))
                _tenants = _tenants.Where(t => t.DesignReviewDone == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Leasing Head Approver"))
                _tenants = _tenants.Where(t => t.LeasingHeadVerified == false).ToList();
            else if (!User.IsInRole("admin") && User.IsInRole("Leasing Reviewer"))
                _tenants = _tenants.Where(t => t.LeasingReviewDone == false).ToList();

            foreach (TenantRenewal _tenant in _tenants)
            {
                var _tenantView = new TenantRenewalView()

                {
                    Id = _tenant.Id,
                    TenantId = _tenant.TenantId,
                    Area = _tenant.Area,
                    CategorySpecialist = _tenant.CategorySpecialist,
                    Expiry = _tenant.ExpiryDate,
                    ChecklistStatus = _tenant.ChecklistStatus,
                    Name = _tenant.Name,
                    Category = _tenant.Category
                };
                _tenantViews.Add(_tenantView);
            }

            StringBuilder reserveStr = GenerateTenantTable(_tenantViews);
            TempData["reseveList"] = reserveStr;
            return View();
        }

        public ActionResult filterByStatus(string status)
        {
            var _tenantViews = new List<TenantRenewalView>();
            List<TenantRenewal> _tenants = TenantRenewalBL.GetTenantRenewalByCheckListStatus(status);

            foreach (TenantRenewal _tenant in _tenants)
            {
                var _tenantView = new TenantRenewalView()

                {
                    Id = _tenant.Id,
                    TenantId = _tenant.TenantId,
                    Area = _tenant.Area,
                    CategorySpecialist = _tenant.CategorySpecialist,
                    Expiry = _tenant.ExpiryDate,
                    LeasingStatus = _tenant.LeasingStatus,
                    Name = _tenant.Name,
                    Category = _tenant.Category
                };
                _tenantViews.Add(_tenantView);
            }

            StringBuilder reserveStr = GenerateTenantTable(_tenantViews);
            TempData["reseveList"] = reserveStr;
            return View();
        }

        public ActionResult PrintableForm(int id)
        {
            var _tenantDO = TenantRenewalBL.GetTenantRenewalById(id);
            var _progress = Computation.ProgressPercentage(_tenantDO);
            // populate statuses
            var _statuses = GenerateStatus();

            TenantRenewalView _tenantmodel = PopulateTenantRenewalView(_tenantDO, _progress, _statuses);
            if (!_tenantmodel.ReviewDate.Year.Equals(1753))
                _tenantmodel.LeasingReviewerName = ReportGeneratName(_tenantmodel.ReviewDoneBy);
            if (!_tenantmodel.LeasingHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.LeasingVerifierName = ReportGeneratName(_tenantmodel.LeasingHeadVerifiedBy);
            if (!_tenantmodel.DesignReviewDoneDate.Year.Equals(1753))
                _tenantmodel.DesignReviewerName = ReportGeneratName(_tenantmodel.DesignReviewDoneBy);
            if (!_tenantmodel.DesignHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.DesignVerifierName = ReportGeneratName(_tenantmodel.DesignHeadVerifiedBy);
            if (!_tenantmodel.OperationFacilitiesReviewDoneDate.Year.Equals(1753))
                _tenantmodel.OperationFacilitiesReviewerName = ReportGeneratName(_tenantmodel.OperationFacilitiesReviewDoneBy);
            if (!_tenantmodel.OperationsTenancyReviewDoneDate.Year.Equals(1753))
                _tenantmodel.OperationTenancyReviewerName = ReportGeneratName(_tenantmodel.OperationsTenancyReviewDoneBy);
            if (!_tenantmodel.OperationHouseKeepingReviewDoneDate.Year.Equals(1753))
                _tenantmodel.OperationHouseKeepingReviewerName = ReportGeneratName(_tenantmodel.OperationHouseKeepingReviewDoneBy);
            if (!_tenantmodel.OperationSecurityReviewDoneDate.Year.Equals(1753))
                _tenantmodel.OperationSecurityReviewerName = ReportGeneratName(_tenantmodel.OperationSecurityReviewDoneBy);
            if (!_tenantmodel.OperationsHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.OperationVerifierName = ReportGeneratName(_tenantmodel.OperationsHeadVerifiedBy);
            if (!_tenantmodel.ITReviewDoneDate.Year.Equals(1753))
                _tenantmodel.ITReviewerName = ReportGeneratName(_tenantmodel.ITReviewDoneBy);
            if (!_tenantmodel.ITHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.ITVerifierName = ReportGeneratName(_tenantmodel.ITHeadVerifiedBy);
            if (!_tenantmodel.FinanceReviewDoneDate.Year.Equals(1753))
                _tenantmodel.FinanceReviewerName = ReportGeneratName(_tenantmodel.FinanceReviewDoneBy);
            if (!_tenantmodel.FinanceVerifiedDoneDate.Year.Equals(1753))
                _tenantmodel.FinanceVerifierName = ReportGeneratName(_tenantmodel.FinanceVerifiedDoneBy);
            if (!_tenantmodel.AuditReviewDoneDate.Year.Equals(1753))
                _tenantmodel.AuditReviewerName = ReportGeneratName(_tenantmodel.AuditReviewDoneBy);
            if (!_tenantmodel.AuditHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.AuditVerifierName = ReportGeneratName(_tenantmodel.AuditHeadVerifiedBy);
            if (!_tenantmodel.VPGMHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.VPGMVerifierName = ReportGeneratName(_tenantmodel.VPGMHeadVerifiedBy);
            return View(_tenantmodel);
        }

        private static string ReportGeneratName(string loginName)
        {
            var emp = EmployeeBL.GetEmployeeMasterInfoByLoginId(loginName).FirstOrDefault();
            return $"{emp.empFName} {emp.empLName}";
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PrintableForm(TenantRenewalView tenant)
        {
            var _tenantDO = TenantRenewalBL.GetTenantRenewalById(tenant.Id);
            var _progress = Computation.ProgressPercentage(_tenantDO);
            // populate statuses
            var _statuses = GenerateStatus();

            TenantRenewalView _tenantmodel = PopulateTenantRenewalView(_tenantDO, _progress, _statuses);
            if (!_tenantmodel.ReviewDate.Year.Equals(1753))
                _tenantmodel.LeasingReviewerName = ReportGeneratName(_tenantmodel.ReviewDoneBy);
            if (!_tenantmodel.LeasingHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.LeasingVerifierName = ReportGeneratName(_tenantmodel.LeasingHeadVerifiedBy);
            if (!_tenantmodel.DesignReviewDoneDate.Year.Equals(1753))
                _tenantmodel.DesignReviewerName = ReportGeneratName(_tenantmodel.DesignReviewDoneBy);
            if (!_tenantmodel.DesignHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.DesignVerifierName = ReportGeneratName(_tenantmodel.DesignHeadVerifiedBy);
            if (!_tenantmodel.OperationFacilitiesReviewDoneDate.Year.Equals(1753))
                _tenantmodel.OperationFacilitiesReviewerName = ReportGeneratName(_tenantmodel.OperationFacilitiesReviewDoneBy);
            if (!_tenantmodel.OperationsTenancyReviewDoneDate.Year.Equals(1753))
                _tenantmodel.OperationTenancyReviewerName = ReportGeneratName(_tenantmodel.OperationsTenancyReviewDoneBy);
            if (!_tenantmodel.OperationHouseKeepingReviewDoneDate.Year.Equals(1753))
                _tenantmodel.OperationHouseKeepingReviewerName = ReportGeneratName(_tenantmodel.OperationHouseKeepingReviewDoneBy);
            if (!_tenantmodel.OperationSecurityReviewDoneDate.Year.Equals(1753))
                _tenantmodel.OperationSecurityReviewerName = ReportGeneratName(_tenantmodel.OperationSecurityReviewDoneBy);
            if (!_tenantmodel.OperationsHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.OperationVerifierName = ReportGeneratName(_tenantmodel.OperationsHeadVerifiedBy);
            if (!_tenantmodel.ITReviewDoneDate.Year.Equals(1753))
                _tenantmodel.ITReviewerName = ReportGeneratName(_tenantmodel.ITReviewDoneBy);
            if (!_tenantmodel.ITHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.ITVerifierName = ReportGeneratName(_tenantmodel.ITHeadVerifiedBy);
            if (!_tenantmodel.FinanceReviewDoneDate.Year.Equals(1753))
                _tenantmodel.FinanceReviewerName = ReportGeneratName(_tenantmodel.FinanceReviewDoneBy);
            if (!_tenantmodel.FinanceVerifiedDoneDate.Year.Equals(1753))
                _tenantmodel.FinanceVerifierName = ReportGeneratName(_tenantmodel.FinanceVerifiedDoneBy);
            if (!_tenantmodel.AuditReviewDoneDate.Year.Equals(1753))
                _tenantmodel.AuditReviewerName = ReportGeneratName(_tenantmodel.AuditReviewDoneBy);
            if (!_tenantmodel.AuditHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.AuditVerifierName = ReportGeneratName(_tenantmodel.AuditHeadVerifiedBy);
            if (!_tenantmodel.VPGMHeadVerifiedDate.Year.Equals(1753))
                _tenantmodel.VPGMVerifierName = ReportGeneratName(_tenantmodel.VPGMHeadVerifiedBy);
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();
            string data = string.Empty;

            data = System.IO.File.ReadAllText(@"C:\Users\edgarm\source\repos\OSBS - Copy (4)\WebUI\Controllers\TenantFormTemplate.html");
            data = data.Replace("#LEASINGSTATUS#", _tenantmodel.LeasingStatusString);
            data = data.Replace("#LEASINGHEADAPPROVEDDATE#", _tenantmodel.LeasingHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#LEASINGREVIEWDATE#", _tenantmodel.ReviewDate.ToShortDateString());
            data = data.Replace("#LEASINGREVIEWERNAME#", _tenantmodel.LeasingReviewerName);
            data = data.Replace("#LEASINGCATEGORYSPECIALISTREMARKS#", _tenantmodel.LeasingCategorySpecialistRemarks);
            data = data.Replace("#LEASINGHEADREMARKS#", _tenantmodel.LeasingHeadRemarks);
            data = data.Replace("#VERIFIERNAME#", _tenantmodel.LeasingVerifierName);
            data = data.Replace("#DESIGNVERIFIERDATE#", _tenantmodel.DesignHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#DESIGNSTATUS#", _tenantmodel.DesignStatusString);
            data = data.Replace("#DESIGNFORRENOVATION#", _tenantmodel.ForMajorRenovation.ToString());
            data = data.Replace("#DESIGNREVIEWREMARKS#", _tenantmodel.ForMajorRenovation.ToString());
            data = data.Replace("#DESIGNREVIEWDATE#", _tenantmodel.DesignReviewDoneDate.ToShortDateString());
            data = data.Replace("#DESIGNREVIEWNAME#", _tenantmodel.DesignReviewerName);
            data = data.Replace("#DESIGNVERIFIEDDATE#", _tenantmodel.DesignHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#DESIGNVERIFIEDNAME#", _tenantmodel.DesignVerifierName);
            data = data.Replace("#OPERATIONVERIFIEDDATE#", _tenantmodel.OperationsHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#OPERATIONSTATUS#", _tenantmodel.OperationStatusString);
            data = data.Replace("#OPERATIONFACILITIESREMARKS#", _tenantmodel.OperationFacilitiesRemarks);
            data = data.Replace("#OPERATIONTENANCYREMARKS#", _tenantmodel.OperationTenancyRemarks);
            data = data.Replace("#OPERATIONHOUSEKEEPINGREMARKS#", _tenantmodel.OperationHouseKeepingRemarks);
            data = data.Replace("#OPERATIONFACILITIESREVIEWDATE#", _tenantmodel.OperationFacilitiesReviewDoneDate.ToShortDateString());
            data = data.Replace("#OPERATIONFACILITIESREVIEWNAME#", _tenantmodel.OperationFacilitiesReviewerName);
            data = data.Replace("#OPERATIONTENANCYREVIEWDATE#", _tenantmodel.OperationsTenancyReviewDoneDate.ToShortDateString());
            data = data.Replace("#OPERATIONTENANCYREVIEWNAME#", _tenantmodel.OperationTenancyReviewerName);
            data = data.Replace("#OPERATIONHOUSEKEEPINGREVIEWDATE#", _tenantmodel.OperationHouseKeepingReviewDoneDate.ToShortDateString());
            data = data.Replace("#OPERATIONHOUSEKEEPINGREVIEWNAME#", _tenantmodel.OperationHouseKeepingReviewerName);
            data = data.Replace("#OPERATIONSECURITYREVIEWDATE#", _tenantmodel.OperationSecurityReviewDoneDate.ToShortDateString());
            data = data.Replace("#OPERATIONSECURITYREVIEWNAME#", _tenantmodel.OperationSecurityReviewerName);
            data = data.Replace("#OPERATIONVERIFIEDDATE#", _tenantmodel.OperationsHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#OPERATIONVERIFIEDREVIEWNAME#", _tenantmodel.OperationVerifierName);
            data = data.Replace("#OPERATIONSECURITYREMARKS#", _tenantmodel.OperationSecurityRemarks);
            data = data.Replace("#ITVERIFIEDDATE#", _tenantmodel.ITHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#ITSTATUS#", _tenantmodel.ITStatusString);
            data = data.Replace("#ITREVIEWERDATE#", _tenantmodel.ITReviewDoneDate.ToShortDateString());
            data = data.Replace("#ITREVIEWERNAME#", _tenantmodel.ITReviewerName);
            data = data.Replace("#ITVERIFIERDATE#", _tenantmodel.ITHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#ITVERIFIERNAME#", _tenantmodel.ITVerifierName);
            data = data.Replace("#POSSYSTEMAGREEMENT#", _tenantmodel.POSSystemAgreement);
            data = data.Replace("#DAILYSALESREPORTING#", _tenantmodel.DailySalesReporting);
            data = data.Replace("#ITREMARKS#", _tenantmodel.ITRemarks);
            data = data.Replace("#FINANCEHEADVERIFIEDDATE#", _tenantmodel.FinanceVerifiedDoneDate.ToShortDateString());
            data = data.Replace("#FINANCESTATUS#", _tenantmodel.FinanceStatusString);
            data = data.Replace("#FINANCEUNPAIDCHARGES#", _tenantmodel.UnpaidCharges.ToString());
            data = data.Replace("#FINANCEUNPAIDRENT#", _tenantmodel.AdvanceRentPaid.ToString());
            data = data.Replace("#FINANCESECURITYDEPOSIT#", _tenantmodel.SecurityDepositPaid.ToString());
            data = data.Replace("#FINANCEREVIEWERDATE#", _tenantmodel.FinanceReviewDoneDate.ToShortDateString());
            data = data.Replace("#FINANCEREVIEWERNAME#", _tenantmodel.FinanceReviewerName);
            data = data.Replace("#FINANCEVERIFIERDATE#", _tenantmodel.FinanceVerifiedDoneDate.ToShortDateString());
            data = data.Replace("#FINANCEVERIFIERDATE#", _tenantmodel.FinanceVerifierName);
            data = data.Replace("#AUDITHEADVERIFIEDDATE#", _tenantmodel.AuditHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#AUDITSTATUS#", _tenantmodel.AuditStatusString);
            data = data.Replace("#AUDITREMARKS#", _tenantmodel.AuditRemarks);
            data = data.Replace("#AUDITREVIEWERDATE#", _tenantmodel.AuditReviewDoneDate.ToShortDateString());
            data = data.Replace("#AUDITREVIEWERNAME#", _tenantmodel.AuditReviewerName);
            data = data.Replace("#AUDITVERIFIERDATE#", _tenantmodel.AuditHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#AUDITVERIFIERNAME#", _tenantmodel.AuditVerifierName);
            data = data.Replace("#VPGMHEADVERIFIEDDATE#", _tenantmodel.VPGMHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#VPGMSTATUS#", _tenantmodel.VPGMStatusString);
            data = data.Replace("#VPGMREMARKS#", _tenantmodel.VPGMRemarks);
            data = data.Replace("#VPGMVERIFIERDATE#", _tenantmodel.VPGMHeadVerifiedDate.ToShortDateString());
            data = data.Replace("#VPGMVERIFIERNAME#", _tenantmodel.VPGMVerifierName);
            PdfDocument doc = converter.ConvertHtmlString(data);

            // save pdf document
            doc.Save(System.Web.HttpContext.Current.Response, false, "Sample.pdf");

            doc.Close();
            return null;
        }

        [HttpGet]
        public JsonResult GetTenantRenewalByCheckListStatusCount()
        {
            var checklistStatusCount = TenantRenewalBL.GetTenantRenewalByCheckListStatusCount("On Progress");
            return Json(checklistStatusCount, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTenantRenewalByCheckListStatusOnHoldCount()
        {
            var checklistStatusCount = TenantRenewalBL.GetTenantRenewalByCheckListStatusCount("On Hold");
            return Json(checklistStatusCount, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTenantRenewalByCheckListStatusCompletedCount()
        {
            var checklistStatusCount = TenantRenewalBL.GetTenantRenewalByCheckListStatusCount("Completed");
            return Json(checklistStatusCount, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTenantRenewalByCheckListStatusDelayedCount()
        {
            var checklistStatusCount = TenantRenewalBL.GetTenantRenewalByCheckListStatusCount("Delayed");
            return Json(checklistStatusCount, JsonRequestBehavior.AllowGet);
        }

        private StringBuilder GenerateTenantTable(List<TenantRenewalView> tenantsModel)
        {
            StringBuilder reservesStr = new StringBuilder();
            foreach (TenantRenewalView _tenant in tenantsModel)
            {
                string badgeColor = _tenant.ChecklistStatus != "On Hold" ? "green" : "red";
                reservesStr.Append($"[\"{_tenant.Id}\",\"{_tenant.TenantId}\",\"{_tenant.Name}\", \"{_tenant.Category}\", \"{_tenant.CategorySpecialist}\", \"{_tenant.Expiry.ToShortDateString()}\", " +
                     $"'<span class=\"new badge {badgeColor}\" data-badge-caption=\"\">{_tenant.ChecklistStatus}</span>', " +
                    $"'<a href=\"/tenantrenewal/update/{_tenant.Id}\"><i class=\"material-icons\">edit</i></a>', " +
                    $"'<a href=\"/tenantrenewal/printableform/{_tenant.Id}\"><i class=\"material-icons\">print</i></a>'],");
            }
            if (tenantsModel.Count > 0)
                reservesStr.Remove(reservesStr.Length - 1, 1);
            return reservesStr;
        }

        protected string PopulateChecklistStatus(TenantRenewalView tenantRenewal, double progress)
        {
            var checkListStatus = "On Progress";

            if (tenantRenewal.LeasingStatus == "0" || tenantRenewal.DesignStatus == "0" || tenantRenewal.OperationStatus == "0"
                || tenantRenewal.ITStatus == "0" || tenantRenewal.FinanceStatus == "0" || tenantRenewal.AuditStatus == "0" || tenantRenewal.VPGMStatus == "0")
                checkListStatus = "On Hold";
            if (progress.Equals(100) && checkListStatus != "On Hold")
                checkListStatus = "Completed";
            return checkListStatus;
        }
        protected string ConvertStatusToString(string statusId)
        {
            return statusId == "1" ? "For Renewal" : "On Hold";
        }
    }
}