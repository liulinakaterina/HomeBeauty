using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeBeauty.Data;
using HomeBeauty.Entities;
using HomeBeauty.Models;

namespace HomeBeauty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public CareProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CareProducts
        [HttpGet]
        public IEnumerable<CareProductModel> GetCareProducts()
        {
            var careProductModels = _context.CareProducts.Select(x =>
                new CareProductModel()
                {
                    CareProductId = x.CareProductId,
                    Name = x.Name,
                    Description = x.Description,
                    Compounds = x.Compounds.Select(c => 
                        new CompoundModel()
                        {
                            CompoundId = c.CompoundId,
                            ChemicalId = c.ChemicalId,
                            Amount = c.Amount,
                            Unit = c.Unit
                        }).ToList()
                }).ToList();
            return careProductModels;
        }

        // GET: api/CareProducts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCareProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var careProduct = await _context.CareProducts.FindAsync(id);

            var careProductModel = new CareProductModel()
            {
                CareProductId = careProduct.CareProductId,
                Compounds = careProduct.Compounds.Select(x =>
                    new CompoundModel()
                    {
                        CompoundId = x.CompoundId,
                        ChemicalId = x.ChemicalId,
                        Amount = x.Amount,
                        Unit = x.Unit
                    }).ToList(),
                Description = careProduct.Description,
                Name = careProduct.Name
            };

            if (careProductModel == null)
            {
                return NotFound();
            }

            return Ok(careProductModel);
        }

        // PUT: api/CareProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCareProduct([FromRoute] int id, [FromBody] CareProductModel careProductModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != careProductModel.CareProductId)
            {
                return BadRequest();
            }
            var careProduct = await _context.CareProducts.FindAsync(careProductModel.CareProductId);
            careProduct.Name = careProductModel.Name;
            careProduct.Description = careProductModel.Description;

            _context.Entry(careProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CareProducts
        [HttpPost]
        public async Task<IActionResult> PostCareProduct([FromBody] CareProductModel careProductModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var compounds = careProductModel.Compounds.Select(x =>
                new Compound()
                {
                    CompoundId = x.CompoundId,
                    ChemicalId = x.ChemicalId,
                    Amount = x.Amount,
                    CareProductId = careProductModel.CareProductId,
                    Unit = x.Unit
                }
            ).ToList();

            var careProduct = new CareProduct()
            {
                CareProductId = careProductModel.CareProductId,
                Description = careProductModel.Description,
                Name = careProductModel.Name,
                Compounds = compounds
            };
            foreach(var compound in compounds)
            {
                _context.Compounds.Add(compound);
            }
            _context.CareProducts.Add(careProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCareProduct", new { id = careProductModel.CareProductId }, careProduct);
        }

        // DELETE: api/CareProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCareProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var careProduct = await _context.CareProducts.FindAsync(id);
            if (careProduct == null)
            {
                return NotFound();
            }

            _context.CareProducts.Remove(careProduct);
            await _context.SaveChangesAsync();

            return Ok(careProduct);
        }

        private bool CareProductExists(int id)
        {
            return _context.CareProducts.Any(e => e.CareProductId == id);
        }
    }
}