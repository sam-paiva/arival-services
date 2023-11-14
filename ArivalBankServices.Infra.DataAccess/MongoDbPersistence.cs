using ArivalBankServices.Infra.DataAccess.Maps;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace ArivalBankServices.Infra.DataAccess
{
    public class MongoDbPersistence
    {
        public static void Configure()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            EntitiesMap.Configure();

            // Conventions
            var pack = new ConventionPack
                {
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfDefaultConvention(true),
                };
            ConventionRegistry.Register("Solution Conventions", pack, t => true);
        }
    }
}
