using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpApiIntegration.FetchFromV2.GroupModel
{
    public class GroupsApiResponse
    {
        public string ApiVersion { get; set; }
        public GroupsData Data { get; set; }
        //public ApiError Error { get; set; }
    }
}
