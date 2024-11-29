using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.External_Integrations.Interfaces
{
    public interface IAirlineService
    {
        Task<string?> GetAirlaneNameAsync(string airlineIcaoCode);
    }
}
