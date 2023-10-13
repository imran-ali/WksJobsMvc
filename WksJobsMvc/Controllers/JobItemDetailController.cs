using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WksJobsMvc.Data;
using WksJobsMvc.Models;

namespace WksJobsMvc.Controllers
{
    [Authorize]
    public class JobItemDetailController : Controller
    {
        private readonly WksDbContext _context;

        public JobItemDetailController(WksDbContext context)
        {
            _context = context;
        }

        // GET: JobItemDetail
        public async Task<IActionResult> Index()
        {
            //var wksDbContext = _context.JobItemDetails.Include(j => j.Item).Include(j => j.Job);
            //return View(await wksDbContext.ToListAsync());
            return View();
        }

        // GET: JobItemDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobItemDetails == null)
            {
                return NotFound();
            }

            var jobItemDetail = await _context.JobItemDetails
                //.Include(j => j.Item)
                //.Include(j => j.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobItemDetail == null)
            {
                return NotFound();
            }

            return View(jobItemDetail);
        }

        // GET: JobItemDetail/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "ItemCode");
            ViewData["JobId"] = new SelectList(_context.Jobs, "ID", "JobCode");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public String RespondToThis([Bind("Id,ItemCode,ItemQty,ItemRate,ItemNetTotal,JobId,ItemId")] JobItemDetail jobItemDetai)
        {

            bool exists = (from jobItemDetailss in _context.JobItemDetails  where jobItemDetailss.ItemId == jobItemDetai.ItemId && jobItemDetailss.JobId == jobItemDetai.JobId select jobItemDetailss).Any();

            if (exists == true)
            {
                var jobcode = (from jobmaster in _context.Jobs where jobmaster.ID == jobItemDetai.JobId select jobmaster.JobCode).FirstOrDefault();
                return "Err: Item already exists in the job (" + jobcode + "). Delete it first and then re-enter.";
            }
            else
            {
                if (jobItemDetai.ItemRate <= 0)
                {
                    return "Err: Item rate zero cannot be utilized.";

                }
                else
                {
                    _context.Add(jobItemDetai);
                    _context.SaveChanges();
                    return "Msg: Saved";
                }
            }
            
        }


        // POST: JobItemDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemCode,ItemQty,ItemRate,ItemNetTotal,JobId,ItemId")] JobItemDetail jobItemDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobItemDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "ItemCode", jobItemDetail.ItemId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "ID", "JobCode", jobItemDetail.JobId);
            return View(jobItemDetail);
        }

        // GET: JobItemDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobItemDetails == null)
            {
                return NotFound();
            }

            var jobItemDetail = await _context.JobItemDetails.FindAsync(id);
            if (jobItemDetail == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "ItemCode", jobItemDetail.ItemId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "ID", "JobCode", jobItemDetail.JobId);
            return View(jobItemDetail);
        }

        // POST: JobItemDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemCode,ItemQty,ItemRate,ItemNetTotal,JobId,ItemId")] JobItemDetail jobItemDetail)
        {
            if (id != jobItemDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobItemDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobItemDetailExists(jobItemDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "ItemCode", jobItemDetail.ItemId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "ID", "JobCode", jobItemDetail.JobId);
            return View(jobItemDetail);
        }

        // GET: JobItemDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobItemDetails == null)
            {
                return NotFound();
            }

            var jobItemDetail = await _context.JobItemDetails
                //.Include(j => j.Item)
                //.Include(j => j.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobItemDetail == null)
            {
                return NotFound();
            }

            return View(jobItemDetail);
        }

        // POST: JobItemDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobItemDetails == null)
            {
                return Problem("Entity set 'WksDbContext.JobItemDetails'  is null.");
            }
            var jobItemDetail = await _context.JobItemDetails.FindAsync(id);
            if (jobItemDetail != null)
            {
                _context.JobItemDetails.Remove(jobItemDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobItemDetailExists(int id)
        {
          return (_context.JobItemDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
