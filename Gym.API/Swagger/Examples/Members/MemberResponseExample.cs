
namespace Gym.API.Swagger.Examples.Members;

public class MemberResponseExample : IExamplesProvider<MemberResponse>
{
    public MemberResponse GetExamples()
    {
        return new MemberResponse(
            Id: 1,
            FullName: "Yousseif Dawoud",
            Phone: "01012345678",
            Email: "yousseif@test.com",
            MembershipStartDate: new DateOnly(2026, 1, 1),
            MembershipEndDate: new DateOnly(2026, 12, 31),
            MembershipPlanId: 1,
            MembershipPlanName: "Gold Plan",
            Status: "Active",
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: null
        );
    }
}