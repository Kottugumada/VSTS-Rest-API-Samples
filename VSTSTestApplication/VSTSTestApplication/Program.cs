using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSTSUtils;
using static VSTSUtils.InitHelper;

namespace VSTSTestApplication
{
    class Program
    {
        static  IConfiguration _configuration = new VSTSUtils.Configuration();
        static void Main(string[] args)
        {
            WorkItems request = new WorkItems(_configuration);
         //   WorkItemPatchResponse.WorkItem updateLinkResponse = request.UpdateWorkItemUpdateLink("926","930");
           // WorkItemPatchResponse.WorkItem addLinkResponse = request.UpdateWorkItemAddLink("926", "930");
            WorkItemPatchResponse.WorkItem removeLinkresponse = request.UpdateWorkItemRemoveLink("930");
            Console.WriteLine(removeLinkresponse.url);
        }
    }
}
