using EcWebapi.Database;
using EcWebapi.Database.Table;
using EcWebapi.Repository;

namespace EcWebapi
{
    public class UnitOfWork(EcDbContext context,
                            GenericRepository<Member> memberRepository,
                            GenericRepository<MemberCaptcha> memberCaptchaRepository,
                            GenericRepository<ApiResponse> apiResponseRepository) : IDisposable
    {
        private readonly EcDbContext _context = context;

        public GenericRepository<Member> MemberRepository = memberRepository;

        public GenericRepository<MemberCaptcha> MemberCaptchaRepository = memberCaptchaRepository;

        public GenericRepository<ApiResponse> ApiResponseRepository = apiResponseRepository;

        private bool disposed = false;

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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