using Microsoft.Extensions.Logging;
using PensionerDetailsService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailsService.Services
{
    public class PensionerDataService : IPensionerDataservice
    {
        private static List<PensionerDetail> _pensionerDetails = null;
        private static ILogger<PensionerDataService> _logger;
        public PensionerDataService(ILogger<PensionerDataService> logger)
        {
            _logger = logger;
        }
        public List<PensionerDetail> GetPensionerDetails()
        {
            _logger.LogInformation("GetPensionerDetails method started");

            try
            {
                if (_pensionerDetails != null && _pensionerDetails.Any())
                {
                    return _pensionerDetails;
                }

                _pensionerDetails = new List<PensionerDetail>();
                using (StreamReader sr = new StreamReader("PensionerDetailsData.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        _pensionerDetails.Add(new PensionerDetail()
                        {
                            Name = values[0],
                            DateOfBirth = Convert.ToDateTime(values[1]),
                            PAN = values[2],
                            AadharNumber = Convert.ToInt64(values[3]),
                            SalaryEarned = Convert.ToInt64(values[4]),
                            Allowances = Convert.ToInt64(values[5]),
                            PensionType = (PensionTypes)Enum.Parse(typeof(PensionTypes), values[6]),
                            AccountNumber = Convert.ToInt64(values[7]),
                            BankName = values[8],
                            BankType = (BankTypes)Enum.Parse(typeof(BankTypes), values[9])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            return _pensionerDetails;
        }


    }
}
