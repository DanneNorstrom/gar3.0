using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gar3._0.Data;
using gar3._0.Models;
using System.ComponentModel.DataAnnotations;

namespace gar3._0.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly gar3_0Context _context;

        public ParkedVehiclesController(gar3_0Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.ParkedVehicle.ToListAsync());
        }*/

        public async Task<IActionResult> Overview()
        {
            var vehicles = await _context.ParkedVehicle.ToListAsync();
            List<OverviewViewModel> ve = new List<OverviewViewModel>();
            OverviewViewModel vehicle;

            foreach (var v in vehicles)
            {
                vehicle = new OverviewViewModel();

                vehicle.Vehicletype = v.Vehicletype;
                vehicle.Regnr = v.Regnr;
                vehicle.Arrival = v.Arrival;

                ve.Add(vehicle);
            }

            return View(ve);
        }

        public async Task<IActionResult> Reciept(string id)
        {
            var vehicles = await _context.ParkedVehicle.ToListAsync();
            List<RecieptViewModel> rec = new List<RecieptViewModel>();

            int c = 0;

            foreach(var v in vehicles)
            {
                if (v.Regnr == id) break;
                c++;
            }

            RecieptViewModel r = new RecieptViewModel();
            r.Regnr = vehicles[c].Regnr;
            r.RegnrFormatted = "= " + r.Regnr;
            r.Arrival = vehicles[c].Arrival;
            r.ArrivalFormatted = "= " + r.Arrival.ToString();
            r.Departure = DateTime.Now;
            r.DepartureFormatted = "= " + r.Departure;
            TimeSpan ts = r.Departure - r.Arrival;
            r.Parkedhours = (int)ts.TotalHours + 1;
            r.ParkedhoursFormatted = "= " + r.Parkedhours;
            r.RatePerHourFormatted = "= " + r.RatePerHour.ToString() + "kr";
            r.Fee = r.Parkedhours * r.RatePerHour;
            r.FeeFormatted = "= " + r.Fee + "kr";

            rec.Add(r);
          
            return View(rec);
        }

        public async Task<IActionResult> Search(string regnr){

            //var v = await _context.Product.ToListAsync();
            var v = await _context.ParkedVehicle.ToListAsync();

            //List<Product> produ = new List<Product>();
            List<ParkedVehicle> pveh = new List<ParkedVehicle>();

            //Product p;
            ParkedVehicle p;

            foreach (var veh in v)
            {
                if (veh.Regnr == regnr)
                {
                    p = new ParkedVehicle();

                    p.Vehicletype = veh.Vehicletype;
                    p.Regnr = veh.Regnr;
                    p.Color = veh.Color;
                    p.Brand = veh.Brand;
                    p.Model = veh.Model;
                    p.Numofwheels = veh.Numofwheels;
                    p.Arrival = veh.Arrival;
                    pveh.Add(p);
                }
            }

            return View(pveh);
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Regnr == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Checkin
        public IActionResult Checkin()
        {
            return View();
        }

        // POST: ParkedVehicles/Checkin
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkin([Bind("Vehicletype,Regnr,Color,Brand,Model,Numofwheels,Arrivaltime")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(parkedVehicle);
                    await _context.SaveChangesAsync();
                }

                catch (Exception ex)
                {
                    return RedirectToAction(nameof(Status), new {msg = "Check In Fail!" });
                }

                return RedirectToAction(nameof(Status), new { msg = "Check In Success!" });
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Status), new { msg = "Edit Fail!" });
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Status), new { msg = "Edit Fail!" });

            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Vehicletype,Regnr,Color,Brand,Model,Numofwheels,Arrivaltime")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Regnr)
            {
                //return NotFound();
                return RedirectToAction(nameof(Status), new { msg = "Edit Fail!" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }

                //catch (DbUpdateConcurrencyException)
                catch(Exception ex)
                {
                    /*if (!ParkedVehicleExists(parkedVehicle.Regnr))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }*/

                    return RedirectToAction(nameof(Status), new { msg = "Edit Fail!" });
                }

                return RedirectToAction(nameof(Status), new { msg = "Edit Success!" });
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Checkout/5
        public async Task<IActionResult> Checkout(string id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Status), new { msg = "Check out Fail!" });
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Regnr == id);
            if (parkedVehicle == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Status), new { msg = "Check Out Fail!" });
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Checkout/5
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutConfirmed(string id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle != null)
            {
                _context.ParkedVehicle.Remove(parkedVehicle);
            }

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                return RedirectToAction(nameof(Status), new { msg = "Check Out Fail!" });
            }

            return RedirectToAction(nameof(Status), new { msg = "Check Out Success!" });
        }

        private bool ParkedVehicleExists(string id)
        {
            return _context.ParkedVehicle.Any(e => e.Regnr == id);
        }

        public ActionResult Status(string msg)
        {
            List<StatusViewModel> er = new List<StatusViewModel>();

            StatusViewModel e = new StatusViewModel();
            e.Message = msg;
            er.Add(e);
            return View(er);
        }
    }
}
