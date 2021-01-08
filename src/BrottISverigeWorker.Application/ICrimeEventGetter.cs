using BrottISverigeWorker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BrottISverigeWorker.Application
{
    public interface ICrimeEventGetter
    {
        Task<IEnumerable<PoliceEvent>> GetPoliceEventsAsync();
    }
}
