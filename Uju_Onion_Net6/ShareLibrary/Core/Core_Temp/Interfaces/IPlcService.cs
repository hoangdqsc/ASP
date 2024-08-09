using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Temp.Interfaces
{
    public interface IPlcService
    {
        bool Connect();
        int GetMachineRuntime();
        float GetTemperature();
        void Disconnect();
        void SaveMachineData();
    }
}
