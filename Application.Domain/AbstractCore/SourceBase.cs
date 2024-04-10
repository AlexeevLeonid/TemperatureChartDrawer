using System.Text.Json.Serialization;
using TempArAn.Domain.Exceptions.SourceExeptions;
using TempArAn.Domain.Exceptions.SourceExeptions.HtmlSourceExceptions;
using TempArAn.Domain.Models.Record;

namespace TempArAn.Domain.AbstractCore
{
    public abstract class SourceBase
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public bool IsRecording { get; private set; }
        public string Name { get; private set; }
        public DateTime NextRecord { get; private set; }
        public int Interval { get; private set; }

        [JsonIgnore]
        public TimeSpan IntervalAsTimeSpan { get; private set; }
        public static TimeSpan ErrorInterval = TimeSpan.FromHours(1);
        public SourceBase(string name, int interval, Guid userId)
        {
            Name = name;
            IsRecording = true;
            UserId = userId;
            IntervalAsTimeSpan = TimeSpan.FromMinutes(interval);
            Interval = interval;
            Id = Guid.NewGuid();
            NextRecord = DateTime.MinValue;
        }

        /// <summary>
        /// implements value getting
        /// </summary>
        /// <returns></returns>
        protected abstract RecordBase GetValue();

        public RecordBase TryRecording()
        {
            RecordBase result;
            try
            {
                result = GetValue();
                if (result != null)
                    NextRecord = DateTime.Now.AddMinutes(Interval);
            }
            catch (Exception ex)
            {
                if (ex is SourceException se)
                    result = new SourceErrorRecord(Id, DateTime.Now, se);
                else result = new SourceErrorRecord(Id, DateTime.Now, 0);
                NextRecord = DateTime.Now.Add(ErrorInterval);
            }
            return result ?? throw new ArgumentNullException();
        }

        public void SetRecordingState(bool value)
        {
            IsRecording = value;
        }

        /// <summary>
        /// this user can change the source
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsProperty(IUser? user)
        {
            return user != null && UserId == user.Id;
        }

    }
}
