namespace Gym.Infrastructure.Repositories;

public class MemberRepository : GenericRepository<Member>, IMemberRepository
{
    public MemberRepository(GymDbContext context) : base(context)  { }

    // MemberRepository He Can Use the _context  Because He is Protected in the GenericRepository .
    // Foucus on The Order of Applying The Filters and Operations in The Search Method : 
                                            //WHERE
                                            //  ↓
                                            //COUNT
                                            //  ↓
                                            //ORDER
                                            //  ↓
                                            //PAGINATION
                                            //  ↓
                                            //SELECT

    public async Task<PagedResult<MemberListItem>> SearchAsync(MemberQueryRequest request,CancellationToken ct = default)
    {
        // 1. Build the query And Start with the base query (All Members)
        var query = _context.Members.AsNoTracking().AsQueryable();



        // 2. Apply Search Term Filter 
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.Trim().ToLower();

            query = query.Where(m => m.FullName.ToLower().Contains(searchTerm) ||
                                     m.Email.ToLower().Contains(searchTerm)    ||
                                     m.Phone.Contains(searchTerm));
        }



        // 3. Apply Status Filter
        if (request.Status.HasValue)
        {
            query = query.Where( m => m.Status == request.Status.Value);
        }



        // 4. Apply Membership Plan Filter
        if (request.MembershipPlanId.HasValue)
        {
            query = query.Where( m => m.MembershipPlanId == request.MembershipPlanId.Value);
        }



        // 5. Get Total Count Before Pagination
        var totalCount = await query.CountAsync(ct);



        // 6. Apply Pagination + Projection to MemberListItem
        var members = await query
                    .OrderBy(m => m.FullName)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(m => new MemberListItem(
                        m.Id,
                        m.FullName,
                        m.Phone,
                        m.Status,
                        m.MembershipPlan.Type,
                        m.Package != null ? m.Package.Name : null,
                        m.MembershipEndDate
                    ))
                    .ToListAsync(ct);



        // 7. Return Paged Result
        return new PagedResult<MemberListItem>(
               members,
               request.PageNumber,
               request.PageSize,
               totalCount
        );
    }
}