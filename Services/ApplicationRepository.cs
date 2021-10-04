using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZgloszeniaIT.Models;
using ZgloszeniaIT.Data;
using Microsoft.EntityFrameworkCore;

namespace ZgloszeniaIT.Services
{
    public class ApplicationRepository: IApplicationRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserRepository _userRepository;

        public ApplicationRepository(ApplicationDbContext db, IUserRepository userRepository)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _userRepository = userRepository;
        }
        public ReportViewModel GetReport(Guid Id)
        {
            if (Id == Guid.Empty) throw new ArgumentNullException(nameof(Id));
            var item = _db.Reports.FirstOrDefault(x => x.IdReport == Id);
            var user = _userRepository.GetApplicationUserAsync(item.IdUser).Result;
            ReportViewModel reportDTO = new ReportViewModel();

            reportDTO.IdReport = item.IdReport;
            reportDTO.User = user.FirstName + " " + user.LastName;
            if(item.IdOperator == Guid.Empty)
            {
                reportDTO.Operator = "(brak)";
            }
            else
            {
                var op = _userRepository.GetApplicationUserAsync(item.IdOperator).Result;
                reportDTO.Operator = op.FirstName + " " + op.LastName;
            }
            reportDTO.Title = item.Title;
            reportDTO.Description = item.Description;
            reportDTO.AddDate = item.AddDate;
            reportDTO.SolutionDesc = item.SolutionDesc;
            var priority = _db.Priority.FindAsync(item.IdPriority).Result;
            reportDTO.Priority = priority.PriorityDesc;
            var status = _db.Status.FindAsync(item.IdStatus).Result;
            reportDTO.Status = status.StatusDesc;
            var cat = _db.Categories.FindAsync(item.IdCategory).Result;
            reportDTO.Category = cat.CategoryName;
            var att = _db.Attachments.Where(x => (x.IdReport == item.IdReport)).ToList();
            reportDTO.AttachmentList = att;
            var com = _db.Comments.Where(x => (x.IdReport == item.IdReport)).OrderByDescending(i => i.AddDate).ToList();
            foreach (var i in com)
            {
                var author = _userRepository.GetApplicationUserAsync(i.AuthorId).Result;
                i.Author = author.FirstName + " " + author.LastName;
            }
            reportDTO.CommentList = com;

            return reportDTO;
        }

        public List<ReportViewModel> GetReports(string userName, string operatorName, bool closed)
        {
            var reportList = (List<Report>)null;
            var reportDTOList = new List<ReportViewModel>();
            //var reportList = _db.Reports.ToList();
            if (userName != null)
            {
                var user = _userRepository.GetApplicationUserByName(userName).Result;
                reportList = _db.Reports.Where(x => (x.IdUser == user.Id)).ToList();
            }
            else if (operatorName != null)
            {
                var user = _userRepository.GetApplicationUserByName(operatorName).Result;
                reportList = _db.Reports.Where(x => x.IdOperator == user.Id && x.IdStatus != Guid.Parse("d5fa120e-8879-4a13-89f9-c8cf0d59f772")).ToList();
            }
            else if (closed == true)
            {
                reportList = _db.Reports.Where(x => x.IdStatus == Guid.Parse("d5fa120e-8879-4a13-89f9-c8cf0d59f772")).ToList();
            }
            else 
            {
                reportList = _db.Reports.Where(x => x.IdStatus != Guid.Parse("d5fa120e-8879-4a13-89f9-c8cf0d59f772")).ToList();
            }
            foreach(var item in reportList)
            {
                ReportViewModel reportDTO = new ReportViewModel();
                var user = _userRepository.GetApplicationUserAsync(item.IdUser).Result;
                reportDTO.IdReport = item.IdReport;
                reportDTO.User = user.FirstName + " " + user.LastName;
                if (item.IdOperator == Guid.Empty)
                {
                    reportDTO.Operator = "(brak)";
                }
                else
                {
                    var op = _userRepository.GetApplicationUserAsync(item.IdOperator).Result;
                    reportDTO.Operator = op.FirstName + " " + op.LastName;
                }
                reportDTO.Title = item.Title;
                reportDTO.Description = item.Description;
                reportDTO.AddDate = item.AddDate;
                reportDTO.SolutionDesc = item.SolutionDesc;
                var priority = _db.Priority.FindAsync(item.IdPriority).Result;
                reportDTO.Priority = priority.PriorityDesc;
                var status = _db.Status.FindAsync(item.IdStatus).Result;
                reportDTO.Status = status.StatusDesc;
                var cat = _db.Categories.FindAsync(item.IdCategory).Result;
                reportDTO.Category = cat.CategoryName;
                var att = _db.Attachments.Where(x => (x.IdReport == item.IdReport)).ToList();
                reportDTO.AttachmentList = att;
                var com = _db.Comments.Where(x => (x.IdReport == item.IdReport)).ToList();
                reportDTO.CommentList = com;

                reportDTOList.Add(reportDTO);
            }
            return reportDTOList;
        }
        public void AddReportDTO(ReportViewModel reportDTO)
        {
            var user = _userRepository.GetApplicationUserByName(reportDTO.User).Result;
            
            Report report = new Report();
            report.IdReport = reportDTO.IdReport;
            report.IdUser = user.Id;
            report.IdOperator = Guid.Empty;
            report.IdStatus = Guid.Parse(reportDTO.Status);
            report.IdCategory = Guid.Parse(reportDTO.Category);
            report.IdPriority = Guid.Parse(reportDTO.Priority);
            report.AddDate = reportDTO.AddDate;
            report.Title = reportDTO.Title;
            report.Description = reportDTO.Description;

            AddReport(report);
            if(reportDTO.FilePath != null)
            {
                AddAttachment(reportDTO.FilePath, report.IdReport);
            }
        }

        public void AddAttachment(string path, Guid IdReport)
        {
            Attachment attachment = new Attachment();
            attachment.IdAttachment = Guid.NewGuid();
            attachment.IdReport = IdReport;
            attachment.AddDate = DateTime.Now;
            attachment.AttachmentPath = path;
            _db.Attachments.Add(attachment);
            _db.SaveChanges();
        }

        public void AssignReport(Guid Id, string userName)
        {
            Report report = _db.Reports.FirstOrDefault(x => x.IdReport == Id);
            var user = _userRepository.GetApplicationUserByName(userName).Result;
            report.IdOperator = user.Id;
            report.IdStatus = Guid.Parse("77d08315-e2b5-4533-8219-90155c495ec9");
            UpdateReport(report);
        }

        public void CloseReport(Guid Id, string userName, string solution)
        {
            Report report = _db.Reports.FirstOrDefault(x => x.IdReport == Id);
            var user = _userRepository.GetApplicationUserByName(userName).Result;
            report.IdOperator = user.Id;
            report.SolutionDesc = solution;
            report.IdStatus = Guid.Parse("d5fa120e-8879-4a13-89f9-c8cf0d59f772");
            UpdateReport(report);
        }

        public void UpdateReportDTO(ReportViewModel reportDTO)
        {
            var rep = _db.Reports.AsNoTracking().FirstOrDefault(x => x.IdReport == reportDTO.IdReport);
            Report report = new Report();
            report.IdReport = reportDTO.IdReport;
            report.IdUser = rep.IdUser;
            if (reportDTO.Operator == null)
            {
                report.IdOperator = Guid.Empty;
            }
            else
            {
                report.IdOperator = rep.IdOperator;
            }
            report.IdStatus = _db.Status.FirstOrDefault(x => (x.StatusDesc == reportDTO.Status)).IdStatus;
            report.IdCategory = Guid.Parse(reportDTO.Category);
            report.IdPriority = Guid.Parse(reportDTO.Priority);
            report.AddDate = reportDTO.AddDate;
            report.Title = reportDTO.Title;
            report.Description = reportDTO.Description;

            UpdateReport(report);
        }

        public void AddReport(Report report)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));
            _db.Reports.Add(report);
            _db.SaveChanges();
        }

        public void AddComment(Comment comment, string userName)
        {
            var user = _userRepository.GetApplicationUserByName(userName).Result;
            comment.AuthorId = user.Id;
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }

        public void UpdateReport(Report report)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));
            _db.Reports.Update(report);
            _db.SaveChanges();
        }

        public void DeleteReport(Report report)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));
            _db.Reports.Remove(report);
        }
        public List<Status> GetStatus()
        {
            return _db.Status.ToList();    
        }
        public List<Priority> GetPriority()
        {
            return _db.Priority.ToList();
        }
        public List<Category> GetCategory()
        {
            return _db.Categories.ToList();
        }
    }
}
