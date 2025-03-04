﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Properties.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeeController : ControllerBase
    {
        static List<employee> employees=new List<employee>() { 
        
            new employee(){Id=1,Name="gila" ,Age=25, Experiance=5},
            new employee(){Id=2,Name="hila" ,Age=35, Experiance=10},
            new employee(){Id=3,Name="chana",Age=27, Experiance=7},
            new employee(){Id=4,Name="dina",Age=40,Experiance=20}

        }
            ;  



       
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(employees);
        }

       
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                employee e = employees.First(e => e.Id == id);
                return Ok(e);
            }
            catch {
                return NotFound("Id is not valid");
            }
          
        }

        //// GET api/<employeeController>/find
        //[HttpGet("find")]
        //public List<employee> FindEm(string query)
        //{
        //    return query;
        //}

        
        [HttpPost]
        public void Post([FromBody] employee e)
        {
            e.Id = employees[employees.Count-1].Id+1;
            employees.Add(e);
        }

        [HttpPost("createDataSave/{path}")]
        public IActionResult post(string path)
        {
            if (!path.Contains(".txt"))
                return BadRequest("you shoiuld provide a txt file");
            try
            {
                using (StreamWriter write = new StreamWriter(path))
                {
                    foreach (employee e in employees)
                    {
                        write.WriteLine();
                        write.Write("employee" + e.Id);
                        write.Write(", name: " + e.Name);
                        write.Write(", age: " + e.Age);
                        write.Write(", experiance: " + e.Experiance);
                    }
                    return Ok("success");
                }

            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }


        }

        // PUT api/<employeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] employee e)
        {
           int Index=employees.FindIndex(e=>e.Id==id);
            employees[Index].Id = e.Id;
            employees[Index].Name = e.Name;
            employees[Index].Age = e.Age;
            employees[Index].Experiance = e.Experiance;
        }

        // DELETE api/<employeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           employee e= employees.FirstOrDefault(e => e.Id == id);
           employees.Remove(e);
        }
    }
}
