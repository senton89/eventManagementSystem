using System.Collections.Generic; // Добавлено для IEnumerable

namespace EventManagementSystem.APIs.Common;

public interface IFindManyInput<M, W>
{
    W? Where { get; set; }

    IEnumerable<string>? SortBy { get; set; }

    int? Skip { get; set; }

    int? Take { get; set; }
}
