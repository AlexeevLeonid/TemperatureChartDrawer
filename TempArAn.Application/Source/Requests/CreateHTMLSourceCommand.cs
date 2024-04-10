using MediatR;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Requests;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.Source.Requests
{
    public class CreateHTMLSourceCommand : IRequest<SourceResponse>
    {
        public string Url { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
        public string Name { get; set; }

        public IUser User;
        public int Interval { get; set; }
        public CreateHTMLSourceCommand
            (CreateHtmlSourceDetails details, IUser user)
        {
            Url = details.Url;
            Left = details.Left;
            Right = details.Right;
            Name = details.Name;
            Interval = details.Interval;
            User = user;
        }
    }
}
