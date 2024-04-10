using MediatR;
using TempArAn.Domain.Requests;

namespace TempArAn.Application.Base
{
    public class RequestLoginDetails<TEntity> : IRequest<TEntity>
    {
        public LoginDetails LoginDetails { get; set; }

        protected RequestLoginDetails(LoginDetails loginDetails)
        {
            LoginDetails = loginDetails;
        }
    }
}
