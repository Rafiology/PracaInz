using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZgloszeniaIT.Models;
using ZgloszeniaIT.Data;

namespace ZgloszeniaIT.Services
{
    public interface IApplicationRepository
    {
        void AssignReport(Guid Id, string userName);
        void CloseReport(Guid Id, string userName, string solution);
        ReportViewModel GetReport(Guid Id);
        void AddReportDTO(ReportViewModel reportDTO);
        void UpdateReportDTO(ReportViewModel reportDTO);
        List<ReportViewModel> GetReports(string userName, string operatorName, bool closed);
        void AddReport(Report report);
        void AddComment(Comment comment, string userName);
        void UpdateReport(Report report);
        void DeleteReport(Report report);
        List<Status> GetStatus();
        List<Priority> GetPriority();
        List<Category> GetCategory();
        void AddAttachment(string path, Guid IdReport);
    }
}
