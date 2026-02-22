using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Application.DTOs.Trainers;

public sealed record UpdateTrainerRequest(
    string FullName,
    string Specialty
);
