using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Application.DTOs.Common;

public sealed record PagedResponse<T>(
    IReadOnlyList<T> Items,
    int Page,
    int PageSize,
    int TotalCount
);
