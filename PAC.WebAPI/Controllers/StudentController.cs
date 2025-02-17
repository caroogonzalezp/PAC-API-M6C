﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PAC.Domain;
using PAC.IBusinessLogic;

namespace PAC.WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentLogic _studentLogic;

        public StudentController(IStudentLogic studentLogic)
        {
            this._studentLogic = studentLogic;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(_studentLogic.GetStudents());
        }

        [HttpGet( "{id}")]

        public object GetStudentById(int studentId)
        {
            Student student = _studentLogic.GetStudentById(studentId);
            if (student != null) return Ok(student);
            else return NotFound("Student not found");
        }
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult InsertStudent(Student student)
        {
            if (student == null) return BadRequest("The student is null");
            else
            {
                _studentLogic.InsertStudents(student);
                return Ok();
            } 
        }
    }
}
