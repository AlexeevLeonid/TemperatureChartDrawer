using AutoMapper;
using FluentValidation;
using MediatR;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Application.Source.Requests;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Exceptions.ApplicationExceptions;
using TempArAn.Domain.Models.Record;
using TempArAn.Domain.Models.Source;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Source.Handlers
{
    public class CreateHTMLSourceCommandHandler : IRequestHandler<CreateHTMLSourceCommand, SourceResponse>
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<SourceBase> _validator;
        public CreateHTMLSourceCommandHandler(
            IApplicationUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<SourceBase> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<SourceResponse> Handle(CreateHTMLSourceCommand request, CancellationToken cancellationToken)
        {
            var newSource = new HTMLSource(
                request.Name, request.Url, request.Left, request.Right, request.User.Id, request.Interval);
            _validator.ValidateAndThrow(newSource);
            if (newSource.TryRecording() is SourceErrorRecord sourceErrorRecord)
                InvalidSourceHandle(newSource, sourceErrorRecord);
            await _unitOfWork.Sources.PostSourceAsync(newSource);
            return _mapper.Map<SourceResponse>(newSource);
        }

        private async static void InvalidSourceHandle(HTMLSource s, SourceErrorRecord sourceErrorRecord)
        {
            if (sourceErrorRecord.TypeSourceError == Domain.Enums.TypeSourceError.NotFound)
                throw new WrongSourceDetailsException("Page not found");

            try
            {
                var page = await s.GetPageAsync();
                var IndexLeft = page.IndexOf(s.Left);
                var IndexRight = page.IndexOf(s.Right, IndexLeft);
                var result = page.Substring(
                        IndexLeft + s.Left.Length,
                        IndexRight - IndexLeft - s.Left.Length).
                        Replace(" ", "").Replace(".", ",");
                throw new WrongSourceDetailsException($"Can't parce [{result}] to double");
            }
            catch (Exception ex)
            {
                throw new WrongSourceDetailsException($"Internal parce error: {ex.Message}");
            }
        }
    }
}
