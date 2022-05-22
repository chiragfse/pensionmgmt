using PensionerDetailsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailsService.Services
{
    public interface IPensionerDataservice
    {
        List<PensionerDetail> GetPensionerDetails();
    }
}
