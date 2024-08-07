using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YourAppNamespace.Models;

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
            try
            {
                return Ok(_mindMaps);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred while fetching all MindMaps.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MindMap> Get(int id)
        {
            try
            {
                var mindMap = _mindMaps.FirstOrDefault(x => x.Id == id);
                if (mindMap == null)
                {
                    return NotFound();
                }
                return Ok(mindMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred while retrieving specific MindMap.");
            }
        }

        [HttpPost]
        public ActionResult<MindMap> Create([FromBody]MindMap mindMap)
        {
            try
            {
                if (mindMap == null)
                {
                    return BadRequest("MindMap data is null");
                }
                
                mindMap.Id = _mindMaps.Any() ? _mindMaps.Max(x => x.Id) + 1 : 1;
                _mindMaps.Add(mindMap);
                return CreatedAtAction(nameof(Get), new { id = mindMap.Id }, mindMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the MindMap.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] MindMap mindMap)
        {
            try
            {
                if (mindMap == null)
                {
                    return BadRequest("MindMap data is null");
                }

                var existingMindMap = _mindMaps.FirstOrDefault(x => x.Id == id);
                if(existingMindMap == null)
                {
                    return NotFound();
                }
                
                existingMindMap.Name = mindMap.Name;
                existingMindMap.Content = mindMap.Content;

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the MindMap.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var mindMap = _mindMaps.FirstOrDefault(x => x.Id == id);
                if (mindMap == null)
                {
                    return NotFound();
                }

                _mindMaps.Remove(mindMap);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the MindMap.");
            }
        }
    }
}