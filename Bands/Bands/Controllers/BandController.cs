using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bands.Models;

namespace Bands.Controllers
{
    [Route("api/[controller]")]
    public class BandController : Controller
    {
        private readonly BandContext _context;

        public BandController(BandContext context)
        {
            _context = context;
            if (_context.Band.Count() == 0)
            {
                _context.Band.Add(new Band { BandName = "ROTNS", FormationYear = 2008, Members = 5, Style = "HxC" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Band> GetAll()
        {
            return _context.Band.ToList();
        }

        [HttpGet("{id}", Name = "GetBand")]
        public IActionResult GetById(int id)
        {
            var item = _context.Band.FirstOrDefault(t => t.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Band item)
        {
            if (item == null || item.Id != id)
                return BadRequest();

            var band = _context.Band.FirstOrDefault(t => t.Id == id);
            if (band == null)
                return NotFound();

            try
            {
            band.BandName = item.BandName;
            band.Style = item.Style;
            band.FormationYear = item.FormationYear;
            band.Members = item.Members;
            _context.Band.Update(band);
            _context.SaveChanges();
            return new NoContentResult();
            }catch(Exception e)
            {
                return BadRequest();
            }
        }
    
        [HttpPost]
        public IActionResult Create([FromBody]Band item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.Band.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetBand", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var band = _context.Band.FirstOrDefault(t => t.Id == id);
            if (band == null)
                return NotFound();

            _context.Band.Remove(band);
            _context.SaveChanges();
            return new NoContentResult();

        }
    }
}