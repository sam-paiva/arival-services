using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace ArivalBankServices.Infra.DataAccess.Context
{
    public class MongoContext : IMongoContext
    {
        private readonly ILogger<MongoContext> _logger;

        private IMongoDatabase? Database { get; set; }
        public IClientSessionHandle? Session { get; set; }
        public MongoClient? MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        private readonly IConfiguration _configuration;

        public MongoContext(IConfiguration configuration, ILogger<MongoContext> logger)
        {
            _configuration = configuration;

            _commands = new List<Func<Task>>();
            _logger = logger;
        }

        public async Task<int> SaveChanges()
        {
            ConfigureMongo();

            using (Session = await MongoClient!.StartSessionAsync())
            {
                Session.StartTransaction();
                try
                {
                    await Task.WhenAll(_commands.Select(x => x()));
                    await Session.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    Session.AbortTransaction();
                    _logger.LogError(ex, "Error when trying to execute repositories commands");
                    throw;
                }
                Session.Dispose();
            }
            return _commands.Count;
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            // Configure mongo (You can inject the config, just to simplify)
            MongoClient = new MongoClient(_configuration["ConnectionStrings:DefaultConnection"]);

            Database = MongoClient.GetDatabase(_configuration["ConnectionStrings:DatabaseName"]);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database!.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func) => _commands.Add(func);
    }
}
