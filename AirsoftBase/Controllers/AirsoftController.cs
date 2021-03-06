﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirsoftBase.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AirsoftBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirsoftController : ControllerBase
    {

        private readonly Context ctxt;
        public AirsoftController(Context ctxt)
        {
            this.ctxt = ctxt;
        }

        [HttpGet]
        
        public ActionResult<AirsoftGun[]> Get()
        {
            if (this.ctxt.Airsofts == null)
            {
                return NotFound("no data found");
                
            }

            return this.ctxt.Airsofts.ToArray();
        }
         

        [HttpGet]
        [Route("{id}")]
        public ActionResult<AirsoftGun> Details(int id) {

            var result = ctxt.Airsofts.FirstOrDefault(c => c.Id == id);
            if (result != null)
            {
                return (result);

            }
            else return NotFound($"Id {id} not found");
        }
       

        [HttpPost]
        public ActionResult<AirsoftGun> Create([FromBody] AirsoftGun NewAirsoft)
        {
            ctxt.Airsofts.Add(NewAirsoft);
            ctxt.SaveChanges();

            return Created("", NewAirsoft);
        }



        [HttpPut]
        public IActionResult Update([FromBody] AirsoftGun _airsoft)
        {
            var item = ctxt.Airsofts.Find(_airsoft.Id);

            if (item == null)
            {
                return NotFound();
            }
            item.Brand = _airsoft.Brand;
            item.Type = _airsoft.Type;
            item.Name = _airsoft.Name;
            item.MagazineSize = _airsoft.MagazineSize;
            item.FPS = _airsoft.FPS;
            item.Caliber = _airsoft.Caliber;
            item.Joule = _airsoft.Joule;
            item.GearBox = _airsoft.GearBox;
            item.Lenght = _airsoft.Lenght;
            item.Weight = _airsoft.Weight;
            item.Battery = _airsoft.Battery;
            item.Description = _airsoft.Description;

            ctxt.SaveChanges();

            return Ok(item);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<AirsoftGun>> Delete(int id)
        {
            var result = await ctxt.Airsofts.FindAsync(id);
            if (result == null)
            {
                return NotFound($"Id {id} not found");
            }

            ctxt.Airsofts.Remove(result);
            await ctxt.SaveChangesAsync();

            return result;
        }

       
    }
}
