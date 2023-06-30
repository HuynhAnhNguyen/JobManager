﻿using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobManager.Areas.Admin.Pages.Task
{
    public class DeleteModel : TaskPageModel
    {
        public DeleteModel(ApplicationDbContext context, INotyfService notyf, ILogger<TaskPageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }

        public DuAn duAn { get; set; }
        public IActionResult OnGet() => RedirectToPage("./Index");

        public async Task<IActionResult> OnPostAsync(string taskid)
        {
            if (taskid == null)
            {
                _notyf.Error("Không tìm thấy dự án có mã " + taskid, 3);
                return RedirectToPage("./Index");
            }

            duAn = await _context.DuAn.Where(x => x.MaDuAn == taskid).FirstOrDefaultAsync();

            if (duAn == null)
            {
                _notyf.Error("Không tìm thấy dự án có mã " + taskid, 3);
                return RedirectToPage("./Index");
            }

            _context.Update(duAn);
            duAn.Deleted = true;
            await _context.SaveChangesAsync();

            _notyf.Success("Xóa dự án thành công", 3);

            return RedirectToPage("./Index");


        }
    }
}
