using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
    public class VesselController : Controller
    {
        private readonly WksDbContext _context;

        public VesselController(WksDbContext context)
        {
            _context = context;
        }

        // GET: Vessel
        public async Task<IActionResult> Index()
        {
              return _context.Vessels != null ? 
                          View(await _context.Vessels.ToListAsync()) :
                          Problem("Entity set 'WksDbContext.Vessels'  is null.");
        }


        [HttpPost]
        public IActionResult GetVessels()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var vesselData = (from tempVessel in _context.Vessels select tempVessel);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    vesselData = vesselData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    vesselData = vesselData.Where(m => m.VesselCode.Contains(searchValue)
                                                || m.VesselName.Contains(searchValue)
                                                || m.VesselType.Contains(searchValue));
                }
                recordsTotal = vesselData.Count();
                var data = vesselData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Vessel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vessels == null)
            {
                return NotFound();
            }

            var vessel = await _context.Vessels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vessel == null)
            {
                return NotFound();
            }

            return View(vessel);
        }

        // GET: Vessel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vessel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VesselCode,VesselType,VesselName,CreateDate")] Vessel vessel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vessel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vessel);
        }

        // GET: Vessel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vessels == null)
            {
                return NotFound();
            }

            var vessel = await _context.Vessels.FindAsync(id);
            if (vessel == null)
            {
                return NotFound();
            }
            return View(vessel);
        }

        // POST: Vessel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VesselCode,VesselType,VesselName,CreateDate")] Vessel vessel)
        {
            if (id != vessel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vessel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VesselExists(vessel.Id))
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
            return View(vessel);
        }

        // GET: Vessel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vessels == null)
            {
                return NotFound();
            }

            var vessel = await _context.Vessels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vessel == null)
            {
                return NotFound();
            }

            return View(vessel);
        }

        // POST: Vessel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vessels == null)
            {
                return Problem("Entity set 'WksDbContext.Vessels'  is null.");
            }
            var vessel = await _context.Vessels.FindAsync(id);
            if (vessel != null)
            {
                _context.Vessels.Remove(vessel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VesselExists(int id)
        {
          return (_context.Vessels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
