using Log_system.Data.Model;
using Log_system.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Log_system.Controllers
{
    [Route("get-log-data")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogRepository _repository;
        public ErrorController(ILogRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("errors/{HotelId:int}")]
        public IActionResult GetErrors(int HotelId)
        {
            List<LogData> Errors = _repository.FindErrorsById(HotelId);
            return Ok(Errors);
        }

        [HttpGet]
        [Route("errors/{StartDate:DateTime}/{EndDate:DateTime}")]
        public IActionResult GetErrors(DateTime StartDate, DateTime EndDate)
        {
            List<LogData> Errors = _repository.FindErrorsByDate(StartDate, EndDate);
            return Ok(Errors);
        }

        [HttpGet]
        [Route("errors/{HotelId:int}/{StartDate:DateTime}/{EndDate:DateTime}")]
        public IActionResult GetErrors(int HotelId, DateTime StartDate, DateTime EndDate)
        {
            List<LogData> Errors = _repository.FindErrorsByIdAndDate(HotelId, StartDate, EndDate);
            return Ok(Errors);
        }
    }
}