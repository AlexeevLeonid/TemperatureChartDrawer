using MediatR;

namespace TempArAn.Application.User.Requests
{
    public class IsNotTakenQuery : IRequest<bool>
    {
        public string Name { get; set; }
        public IsNotTakenQuery(string name)
        {
            Name = name;
        }
    }
}
