using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.StudentModels
{
    internal class StudentsApiResponse
    {
        public string ApiVersion { get; set; }
        public StudentsData Data { get; set; }
        public ApiError Error { get; set; }
    }
}
