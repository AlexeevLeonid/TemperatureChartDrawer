using AutoMapper;
using TempAnAr.Persistence.Context;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Domain.Models.Record;

namespace TempAnAr.Persistence.Implementations
{
    public class ApplicationUnitOfWork : IApplicationUnitOfWork
    {
        private readonly ApplicationContext _context;

        public ApplicationUnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        public ApplicationUnitOfWork(MockApplicationContext context)
        {
            _context = context;
        }


        public IUserRepository Users => new UserRepository(_context);
        public IRecordRepository<DoubleRecord> DoubleRecords =>
            new DoubleRecordRepository(_context) ??
            throw new NullReferenceException();
        public IRecordRepository<SourceErrorRecord> ErrorRecords =>
            new SourceErrorRecordRepository(_context) ??
            throw new NullReferenceException();
        public IRecordRepository<TemperatureDataSetRecords> DataRecords =>
            new DataRecordRepository(_context) ??
            throw new NullReferenceException();
        public ISourceRepository Sources =>
            new HTMLSourceRepository(_context) ??
            throw new NullReferenceException();

        public async void Save() => await _context.SaveChangesAsync();

    }
}
