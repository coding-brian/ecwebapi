using EcWebapi.Database;
using EcWebapi.Database.Table;

namespace EcWebapi.Repository
{
    public class MemberRepository(EcDbContext context) : GenericRepository<Member>(context)
    {
        public new async Task CreateAsync(Member dto)
        {
            dto.Id = Guid.NewGuid();
            await CreateAsync(dto);
        }
    }
}