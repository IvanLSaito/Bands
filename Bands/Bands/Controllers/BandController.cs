using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bands.Models;

namespace Bands.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BandController : Controller
    {
        private readonly BandContext _context;

        public BandController(BandContext context)
        {
            _context = context;
            if (_context.BandItems.Count() == 0)
            {
                _context.BandItems.Add(new BandsItem { BandName = "ROTNS", FormationYear = 2008, Members = 5, Style = "HxC" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<BandsItem> GetAll()
        {
            return _context.BandItems.ToList();
        }

        [HttpGet("{id}", Name = "GetBand")]
        public IActionResult GetById(int id)
        {
            var item = _context.BandItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]BandsItem item)
        {
            if (item == null || item.Id != id)
                return BadRequest();

            var band = _context.BandItems.FirstOrDefault(t => t.Id == id);
            if (band == null)
                return NotFound();

            band.BandName = item.BandName;
            band.Style = item.Style;
            band.FormationYear = item.FormationYear;
            band.Members = item.Members;
            _context.BandItems.Update(band);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var band = _context.BandItems.FirstOrDefault(t => t.Id == id);
            if (band == null)
                return NotFound();

            _context.BandItems.Remove(band);
            _context.SaveChanges();
            return new NoContentResult();

        }
    }
}