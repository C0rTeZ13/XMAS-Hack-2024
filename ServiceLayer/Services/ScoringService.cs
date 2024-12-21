using ServiceLayer.Models;
using ServiceLayer.Models.Parsing;

namespace ServiceLayer.Services
{
    public class ScoringService
    {
        private readonly VbankcenterParserService _parserService;

        public ScoringService(VbankcenterParserService parserService)
        {
            _parserService = parserService;
        }

        public async Task<ScoringResponce> GetScoringRequestAsync(ScoringRequest request)
        {
            CompanyInfo? info = await _parserService.GetСompanyInfoAsync(request.INN);

            CompanyDto company = new CompanyDto()
            {
                EmployeeCount = request.EmployeeCount ?? info?.EmemployeeCount,
                FormWithdrawalTurnover = request.FormWithdrawalTurnover,
                INN = request.INN,
                NegativeInformation = request.NegativeInformation,
                OKVED = request.OKVED ?? info?.ActivityCode,
                OKVEDCount = request.OKVEDCount,
                RecommendedDealLimit = info?.RecommendedDealLimit,
                RegistrarServices = request.RegistrarServices,
                ReliabilityRating = info?.ReliabilityRating,
                Revenue = request.Revenue ?? info?.Revenue,
                Scammers = request.Scammers,
                TaxationSystem = request.TaxationSystem,
                TaxBurden = request.TaxBurden,
                WithdrawalTurnover = request.WithdrawalTurnover,
                ZSK = request.ZSK,
            };

            ScoringResponce responce = new ScoringResponce()
            {
                Allow = true
            };
            return responce;
        }
    }
}
