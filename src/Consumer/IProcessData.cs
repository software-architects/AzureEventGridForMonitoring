using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public interface IProcessData
    {
        Task Process(SimpleMessage message);
    }
}
