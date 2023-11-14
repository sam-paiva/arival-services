using ArivalBankServices.Core.Base;
using ArivalBankServices.Core.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace ArivalBankServices.Infra.DataAccess.Maps
{
    internal class EntitiesMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Entity>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id).SetIsRequired(true).SetIdGenerator(GuidGenerator.Instance);
                map.MapMember(x => x.CreationDate).SetIsRequired(true);
                map.SetIsRootClass(true);
            });

            BsonClassMap.RegisterClassMap<VerificationCode>(map =>
            {
                map.AutoMap();
                map.MapMember(c => c.CodeStatus).SetIsRequired(true);
                map.MapMember(c => c.PhoneNumber).SetIsRequired(true);
                map.MapMember(c => c.CountryCode).SetIsRequired(true);
                map.MapMember(c => c.ConfirmationCode).SetIsRequired(true);
                map.MapMember(c => c.CodeExpiryDate).SetIsRequired(true);
                map.SetIgnoreExtraElements(true);
                map.SetIsRootClass(true);
            });
        }
    }
}
