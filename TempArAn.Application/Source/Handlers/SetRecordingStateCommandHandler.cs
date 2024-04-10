using MediatR;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Application.Source.Requests;
using TempArAn.Domain.Exceptions.ApplicationExceptions;

namespace TempArAn.Application.Source.Handlers
{
    internal class SetRecordingStateCommandHandler : IRequestHandler<SetRecordingStateCommand, bool>
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public SetRecordingStateCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(SetRecordingStateCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Sources.GetSourceAsync(request.Guid);
            if (!result.IsProperty(request.User))
                throw new AccessDeniedException("This source is not avialible");
            await _unitOfWork.Sources.SetRecordingStateAsync(request.Guid, request.Value);
            return true;
        }
    }
}
