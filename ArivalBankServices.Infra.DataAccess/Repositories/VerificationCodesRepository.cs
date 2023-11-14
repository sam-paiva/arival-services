using ArivalBankServices.Core.Domain;
using ArivalBankServices.Infra.DataAccess.Context;

namespace ArivalBankServices.Infra.DataAccess.Repositories
{
    public class VerificationCodesRepository : BaseRepository<VerificationCode>, IVerificationCodesRepository
    {
        public VerificationCodesRepository(IMongoContext context) : base(context)
        {
        }
    }
}
