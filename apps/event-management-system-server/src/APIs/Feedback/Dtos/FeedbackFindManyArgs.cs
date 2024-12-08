using EventManagementSystem.APIs.Common;
using EventManagementSystem.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class FeedbackFindManyArgs : FindManyInput<Feedback, FeedbackWhereInput> { }
