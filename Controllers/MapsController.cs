using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YourAppNamespace.Models;
using System;

namespace YourAppNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MindMapsController : ControllerBase
    {
        private static List<MindMap> _mindMaps = new List<MindMap>();

        [HttpGet]
        public ActionResult<IEnumerable<MindMap>> GetAll()
        {
            return Ok(_mindMaps);
        }

        [HttpGet("{id}")]
        public ActionResult<MindMap> Get(int id)
        {
            var mindMap = _mindMaps.FirstOrDefault(x => x.Id == id);
            if (mindMap == null)
            {
                return NotFound();
            }
            return Ok(mindMap);
        }

        [HttpPost]
        public ActionResult<MindMap> Create(MindMap mindMap)
        {
            mindMap.Id = _mindMaps.Any() ? _mindMaps.Max(x => x.Id) + 1 : 1;
            _mindMaps.Add(mindMap);
            return CreatedAtAction(nameof(Get), new { id = mindMap.Id }, mindMap);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, MindMap mindMap)
        {
            var existingMindMap = _mindMaps.FirstOrDefault(x => x.Id == id);
            if (existingMindMap == null)
            {
                return NotFound();
            }

            existingMindMap.Name = mindMap.Name;
            existingMindMap.Content = mindMap.Content;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var mindMap = _mindMaps.FirstOrDefault(x => x.Id == id);
            if (mindMap == null)
            {
                return NotFound();
            }

            _mindMaps.Remove(mindMap);
            return NoContent();
        }
    }
}