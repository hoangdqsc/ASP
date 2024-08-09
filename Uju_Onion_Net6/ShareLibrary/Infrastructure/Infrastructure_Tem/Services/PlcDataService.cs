
using Core_Temp.Interfaces;
using Microsoft.Extensions.Hosting;


namespace Infrastructure_Temp.Services
{
      
        public class PlcDataService : IHostedService, IDisposable
        {
            private readonly IPlcService _plcService;
            private Timer _timer;

            public PlcDataService(IPlcService plcService)
            {
                _plcService = plcService;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
                return Task.CompletedTask;
            }

            private void DoWork(object state)
            {
                if (_plcService.Connect())
                {
                    _plcService.SaveMachineData();
                    _plcService.Disconnect();
                }
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                _timer?.Change(Timeout.Infinite, 0);
                return Task.CompletedTask;
            }

            public void Dispose()
            {
                _timer?.Dispose();
            }
        }
  

}
