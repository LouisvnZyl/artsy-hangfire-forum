using HanfireForum.Data.DataServices;

namespace HangfireForum.Services.SuspenseTransferService
{
    public class SuspenseTransferService: ISuspenseTransferService
    {
        private readonly IPaymentDataService _dataService;

        public SuspenseTransferService(IPaymentDataService dataService)
        {
            _dataService = dataService;
        }
    }
}
