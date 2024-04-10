using MediatR;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Application.Source.Requests;
using TempArAn.Domain.Exceptions.ApplicationExceptions;

namespace TempArAn.Application.Source.Handlers
{
    public class DeleteSourceCommandHandler : IRequestHandler<DeleteSourceCommand, bool>
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public DeleteSourceCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
        {
            var source = await _unitOfWork.Sources.GetSourceAsync(request.Guid);
            if (!source.IsProperty(request.User)) throw new AccessDeniedException("Access denied");
            await _unitOfWork.Sources.DeleteSourceAsync(request.Guid);
            return true;
        }
    }
}
