using EcWebapi.Database;
using EcWebapi.Repository;

namespace EcWebapi
{
    public class UnitOfWork(EcDbContext context, MemberRepository memberRepository) : IDisposable
    {
        private readonly EcDbContext _context = context;

        public MemberRepository MemberRepository = memberRepository;

        private bool disposed = false;

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}