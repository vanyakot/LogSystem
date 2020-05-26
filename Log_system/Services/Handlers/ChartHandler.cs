using Log_system.Data.Model;
using Log_system.Model;
using Log_system.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Log_system.Services
{
    public class ChartHandler
    {
        private readonly IErrorRepository _errorRepository;
        private readonly IWarningRepository _warningRepository;
        public ChartHandler(IErrorRepository errorRepository, IWarningRepository warningRepository)
        {
            _errorRepository = errorRepository;
            _warningRepository = warningRepository;

        }

        public Dictionary<string, int> CreatePointErrorChart(DateTime StartDate, DateTime EndDate)
        {
            Dictionary<string, int> Points =  new Dictionary<string, int>();

            if (EndDate.Subtract(StartDate).Days <= 1)
            {
                Points = CreateErrorDictionaryHours(StartDate, EndDate);
            }
            else if (EndDate.Subtract(StartDate).Days <= 31)
            {
                Points = CreateErrorDictionaryDays(StartDate, EndDate);
            }
            else if (EndDate.Subtract(StartDate).Days <= 365)
            {
                Points = CreateErrorDictionaryMonths(StartDate, EndDate);
            }

            return Points;

        }

        private Dictionary<string, int> CreateErrorDictionaryHours(DateTime StartDate, DateTime EndDate)
        {
            Dictionary<string, int> ErrorsDictionary = new Dictionary<string, int>();
            List<LogError> Errors = _errorRepository.FindErrorsByDate(StartDate, EndDate);
            while (StartDate < EndDate)
            {
                int currentErrors = Errors.Where(error => error.CreationDateTime >= StartDate && error.CreationDateTime <= StartDate.AddHours(1)).Count();
                string currentTime = StartDate.ToString("HH:mm");
                ErrorsDictionary.Add(currentTime, currentErrors);
                StartDate = StartDate.AddHours(1);
            }

            return ErrorsDictionary;
        }

        private Dictionary<string, int> CreateErrorDictionaryDays(DateTime StartDate, DateTime EndDate)
        {
            Dictionary<string, int> ErrorsDictionary = new Dictionary<string, int>();
            List<LogError> Errors = _errorRepository.FindErrorsByDate(StartDate, EndDate);
            while (StartDate < EndDate)
            {
                int currentErrors = Errors.Where(error => error.CreationDateTime >= StartDate && error.CreationDateTime <= StartDate.AddDays(1)).Count();
                string currentDay = StartDate.ToString("dd MMMM");
                ErrorsDictionary.Add(currentDay, currentErrors);
                StartDate = StartDate.AddDays(1);
            }

            return ErrorsDictionary;
        }

        private Dictionary<string, int> CreateErrorDictionaryMonths(DateTime StartDate, DateTime EndDate)
        {
            Dictionary<string, int> ErrorsDictionary = new Dictionary<string, int>();
            List<LogError> Errors = _errorRepository.FindErrorsByDate(StartDate, EndDate);
            while (StartDate < EndDate)
            {
                int currentErrors = Errors.Where(error => error.CreationDateTime >= StartDate && error.CreationDateTime <= StartDate.AddMonths(1)).Count();
                string currentDay = StartDate.ToString("MMMM yyyy");
                ErrorsDictionary.Add(currentDay, currentErrors);
                StartDate = StartDate.AddMonths(1);
            }

            return ErrorsDictionary;
        }

        public Dictionary<string, int> CreatePointWarningChart(DateTime StartDate, DateTime EndDate)
        {
            Dictionary<string, int> Points = new Dictionary<string, int>();

            if (EndDate.Subtract(StartDate).Days <= 1)
            {
                Points = CreateWarningDictionaryHours(StartDate, EndDate);
            }
            else if (EndDate.Subtract(StartDate).Days <= 31)
            {
                Points = CreateWarningDictionaryDays(StartDate, EndDate);
            }
            else if (EndDate.Subtract(StartDate).Days <= 365)
            {
                Points = CreateWarningDictionaryMonths(StartDate, EndDate);
            }

            return Points;

        }

        private Dictionary<string, int> CreateWarningDictionaryHours(DateTime StartDate, DateTime EndDate)
        {
            Dictionary<string, int> WarningsDictionary = new Dictionary<string, int>();
            List<LogWarning> Warnings = _warningRepository.FindWarningsByDate(StartDate, EndDate);
            while (StartDate < EndDate)
            {
                int currentWarnings = Warnings.Where(warning => warning.CreationDateTime >= StartDate && warning.CreationDateTime <= StartDate.AddHours(1)).Count();
                string currentTime = StartDate.ToString("HH:mm");
                WarningsDictionary.Add(currentTime, currentWarnings);
                StartDate = StartDate.AddHours(1);
            }

            return WarningsDictionary;
        }

        private Dictionary<string, int> CreateWarningDictionaryDays(DateTime StartDate, DateTime EndDate)
        {
            Dictionary<string, int> WarningsDictionary = new Dictionary<string, int>();
            List<LogWarning> Warnings = _warningRepository.FindWarningsByDate(StartDate, EndDate);
            while (StartDate < EndDate)
            {
                int currentWarnings = Warnings.Where(warning => warning.CreationDateTime >= StartDate && warning.CreationDateTime <= StartDate.AddDays(1)).Count();
                string currentDay = StartDate.ToString("dd MMMM");
                WarningsDictionary.Add(currentDay, currentWarnings);
                StartDate = StartDate.AddDays(1);
            }

            return WarningsDictionary;
        }

        private Dictionary<string, int> CreateWarningDictionaryMonths(DateTime StartDate, DateTime EndDate)
        {
            Dictionary<string, int> WarningsDictionary = new Dictionary<string, int>();
            List<LogWarning> Warnings = _warningRepository.FindWarningsByDate(StartDate, EndDate);
            while (StartDate < EndDate)
            {
                int currentWarnings = Warnings.Where(warning => warning.CreationDateTime >= StartDate && warning.CreationDateTime <= StartDate.AddMonths(1)).Count();
                string currentDay = StartDate.ToString("MMMM yyyy");
                WarningsDictionary.Add(currentDay, currentWarnings);
                StartDate = StartDate.AddMonths(1);
            }

            return WarningsDictionary;
        }
    }
}
