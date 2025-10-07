using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment.Application.Commons.Bases;

public class BaseFilters : BasePagination
{
    public int? NumFilter { get; set; }
    public string? TextFilter { get; set; }
    public string? StateFilter { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public bool? Download { get; set; }
}
