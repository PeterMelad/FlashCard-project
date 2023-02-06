using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class FlashCardController : ControllerBase
    {
        private readonly Icontext db;
        public FlashCardController(Icontext db)
        {
            this.db = db;
        }
        [HttpGet]
        public  IActionResult GetData()
        {
            return Ok(db.GetAllData());
        }
       //[Authorize(Roles ="Teacher")]
        [HttpPost]
        public IActionResult CreateFlashCard(FlashCard flashCard)
        {
            if (!ModelState.IsValid) return BadRequest();
            db.CreateCard(flashCard);
            return Ok();
        }
       // [Authorize(Roles = "Teacher")]
        [HttpPut]
        public IActionResult UpdateFlashCard(FlashCard flashCard)
        {
            if (!ModelState.IsValid) return BadRequest();
           if(!db.UpdateCard(flashCard)) return BadRequest();
            return Ok();
        }
       // [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public IActionResult DeleteFlashCard(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
           if(!db.DeleteCard(id)) return BadRequest();
            return Ok();
        }
        //[Authorize(Roles = "Teacher")]
        [HttpDelete("DeleteSpecificQuestion/{id}")]
        public IActionResult DeleteSpecificQuestion(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
           if(!db.DeleteSpecificQuestion(id)) return BadRequest();
            return Ok();
        }
       // [Authorize(Roles = "Teacher")]
        [HttpPost("AddSpecificQuestion")]
        public IActionResult AddSpecificQuestion(ModelQuestion quest)
        {
            if (!ModelState.IsValid) return BadRequest();
            db.AddSpecificQuestion(quest);
            return Ok();
        }
       // [Authorize(Roles = "Teacher")]
        [HttpGet("{id}")]
        public IActionResult DetailedFlashCard(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var FC = db.DetailedFlashCard(id);
            if (FC == null) return NotFound();
            return Ok(FC);
        }
    }
}
