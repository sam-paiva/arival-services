using ArivalBankServices.Core.Base;
using MediatR;
using MediatR.Pipeline;
using OperationResult;

namespace ArivalBankServices.Application.Pipelines
{
    public class PostProcessorPipeline<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostProcessorPipeline(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Process(TRequest request, TResponse result, CancellationToken cancellationToken)
        {
            bool saveChanges = true;
            if (ResultCaster.UsesOperationResult<TResponse>())
            {
                dynamic response = result!;
                saveChanges = response.IsSuccess;
            }

            if (saveChanges)
            {
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
