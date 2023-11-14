using ArivalBankServices.Application.Commands.SendConfirmationCode;
using ArivalBankServices.Application.Config;
using ArivalBankServices.Application.Pipelines;
using ArivalBankServices.Core.Base;
using ArivalBankServices.Core.Domain;
using ArivalBankServices.Infra.DataAccess;
using ArivalBankServices.Infra.DataAccess.Context;
using ArivalBankServices.Infra.DataAccess.Repositories;
using FluentValidation;
using Hydra.Application.Pipelines;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArivalBankServices.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            MongoDbPersistence.Configure();
            RegisterRepositories(services);

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<SendConfirmationCodeCommand>();

                config.AddOpenBehavior(typeof(FailFastRequestValidator<,>));
                config.AddOpenRequestPostProcessor(typeof(PostProcessorPipeline<,>));
            });
            services.Configure<CodeConfiguration>(configuration.GetSection(nameof(CodeConfiguration)));
            services.AddValidatorsFromAssembly(typeof(SendConfirmationCodeCommand).Assembly);

        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Entity>, BaseRepository<Entity>>();
            services.AddScoped<IVerificationCodesRepository, VerificationCodesRepository>();
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}