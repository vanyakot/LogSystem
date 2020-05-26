using Log_system.Data.Model;
using Log_system.Model;
using Log_system.Services;
using Log_system.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Log_system.Controllers
{
    [Route("get-log-data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IErrorRepository _errorRepository;
        private readonly IWarningRepository _warningRepository;
        private readonly ChartHandler _chart;
        public DataController(IErrorRepository errorRepository, IWarningRepository warningRepository, ChartHandler chart)
        {
            _errorRepository = errorRepository;
            _warningRepository = warningRepository;
            _chart = chart;
        }

        [HttpGet]
        [Route("errors/{StartDate:DateTime}/{EndDate:DateTime}")]
        public IActionResult GetErrors(DateTime StartDate, DateTime EndDate)
        {
            List<LogError> Errors = _errorRepository.FindErrorsByDate(StartDate, EndDate);
            return Ok(Errors);
        }

        [HttpGet]
        [Route("errors/{HotelId:int}/{StartDate:DateTime}/{EndDate:DateTime}")]
        public IActionResult GetErrors(int HotelId, DateTime StartDate, DateTime EndDate)
        {
            List<LogError> Errors = _errorRepository.FindErrorsByIdAndDate(HotelId, StartDate, EndDate);
            return Ok(Errors);
        }

        [HttpGet]
        [Route("warnings/{StartDate:DateTime}/{EndDate:DateTime}")]
        public IActionResult GetWarnings(DateTime StartDate, DateTime EndDate)
        {
            List<LogWarning> Warnings = _warningRepository.FindWarningsByDate(StartDate, EndDate);
            return Ok(Warnings);
        }

        [HttpGet]
        [Route("warnings/{HotelId:int}/{StartDate:DateTime}/{EndDate:DateTime}")]
        public IActionResult GetWarnings(int HotelId, DateTime StartDate, DateTime EndDate)
        {
            List<LogWarning> Warnings = _warningRepository.FindWarningsByIdAndDate(HotelId, StartDate, EndDate);
            return Ok(Warnings);
        }

        [HttpGet]
        [Route("errors/charts/{StartDate:DateTime}/{EndDate:DateTime}")]
        public IActionResult GetDataErrorCharts(DateTime StartDate, DateTime EndDate)
        {
            return Ok(_chart.CreatePointErrorChart(StartDate, EndDate));
        }

        [HttpGet]
        [Route("warnings/charts/{StartDate:DateTime}/{EndDate:DateTime}")]
        public IActionResult GetDataWarningCharts(DateTime StartDate, DateTime EndDate)
        {
            return Ok(_chart.CreatePointWarningChart(StartDate, EndDate));
        }
    }
}