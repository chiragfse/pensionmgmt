using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PensionerDetailsService.Controllers;
using PensionerDetailsService.Services;
using PensionerDetailsService.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;

namespace PensionerDetailServiceTests
{
    public class PensionerDetailControllerTests
    {
        private readonly Mock<IPensionerDataservice> _pensionerDataService;
        private readonly Mock<ILogger<PensionerDetailController>> _logger = new Mock<ILogger<PensionerDetailController>>();
        
        public PensionerDetailControllerTests()
        {
            _pensionerDataService = new Mock<IPensionerDataservice>();
            _logger = new Mock<ILogger<PensionerDetailController>>();
            
        }
        

        [Test]
        public void PensionerDetailByAadhar_ReturnsPensionerDetails()
        {
            
            var PensionList = new List<PensionerDetail>();
            PensionList.Add(new PensionerDetail()
            {
                Name = "Ajay",
                DateOfBirth = DateTime.Now,
                PAN = "KSMOC9374L",
                AadharNumber = Convert.ToInt64(791714947214),
                SalaryEarned = Convert.ToInt64(436986),
                Allowances = Convert.ToInt64(19292),
                PensionType = (PensionTypes)Enum.Parse(typeof(PensionTypes),"0"),
                AccountNumber = Convert.ToInt64(968557297810),
                BankName = "Indusland Bank Ltd.",
                BankType = (BankTypes)Enum.Parse(typeof(BankTypes),"1")
            });

            _pensionerDataService.Setup(x => x.GetPensionerDetails())
                .Returns(PensionList);
            
            var controller = new PensionerDetailController(_pensionerDataService.Object, _logger.Object);
            var returnPensionerDetail = controller.PensionerDetailByAadhaar(791714947214);
            var result = (OkObjectResult)returnPensionerDetail.Result;
            var resultValue = (returnPensionerDetail.Result as OkObjectResult).Value as PensionerDetail;
            


            Assert.IsNotNull(controller);
            Assert.IsNotNull(returnPensionerDetail);
            Assert.AreEqual(200,result.StatusCode);
            Assert.AreEqual(PensionList[0], result.Value);
            Assert.AreEqual("Ajay", resultValue.Name);
            
        }
    }
}