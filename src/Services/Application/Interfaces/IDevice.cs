using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDevice
    {
        public string[] ComPorts { get; set; }
        public Task<IEnumerable<string>> GetComPortNames();
    }
}
