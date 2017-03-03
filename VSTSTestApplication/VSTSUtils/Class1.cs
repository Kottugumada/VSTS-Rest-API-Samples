using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VSTSUtils
{
    public static class InitHelper
    {
        public class Configuration : IConfiguration
        {
            public string UriString { get; set; }
            public string CollectionId { get; set; }
            public string PersonalAccessToken { get; set; }
            public string Project { get; set; }
            public string Team { get; set; }
            public string MoveToProject { get; set; }
            public string Query { get; set; }
            public string Identity { get; set; }
            public string WorkItemIds { get; set; }
            public string WorkItemId { get; set; }
            public string ProcessId { get; set; }
            public string PickListId { get; set; }
            public string QueryId { get; set; }
            public string FilePath { get; set; }
            public string GitRepositoryId { get; set; }
        }
        public static IConfiguration GetConfiguration(IConfiguration configuration)
        {
            configuration.CollectionId = ConfigurationSettings.AppSettings["appsetting.collectionid"].ToString();
            configuration.PersonalAccessToken = ConfigurationSettings.AppSettings["appsetting.pat"].ToString();
            configuration.Project = ConfigurationSettings.AppSettings["appsetting.project"].ToString();
            configuration.Team = ConfigurationSettings.AppSettings["appsetting.team"].ToString();
            configuration.MoveToProject = ConfigurationSettings.AppSettings["appsetting.movetoproject"].ToString();
            configuration.Query = ConfigurationSettings.AppSettings["appsetting.query"].ToString();
            configuration.Identity = ConfigurationSettings.AppSettings["appsetting.identity"].ToString();
            configuration.UriString = ConfigurationSettings.AppSettings["appsetting.uri"].ToString();
            configuration.WorkItemIds = ConfigurationSettings.AppSettings["appsetting.workitemids"].ToString();
            configuration.WorkItemId = ConfigurationSettings.AppSettings["appsetting.workitemid"].ToString();
            configuration.ProcessId = ConfigurationSettings.AppSettings["appsetting.processid"].ToString();
            configuration.PickListId = ConfigurationSettings.AppSettings["appsetting.picklistid"].ToString();
            configuration.QueryId = ConfigurationSettings.AppSettings["appsetting.queryid"].ToString();
            configuration.FilePath = ConfigurationSettings.AppSettings["appsetting.filepath"].ToString();
            configuration.GitRepositoryId = ConfigurationSettings.AppSettings["appsetting.git.repositoryid"].ToString();

            return configuration;
        }
        public interface IConfiguration
        {
            string CollectionId { get; set; }
            string PersonalAccessToken { get; set; }
            string Project { get; set; }
            string Team { get; set; }
            string MoveToProject { get; set; }
            string UriString { get; set; }
            string Query { get; set; }
            string Identity { get; set; }
            string WorkItemIds { get; set; }
            string WorkItemId { get; set; }
            string ProcessId { get; set; }
            string PickListId { get; set; }
            string QueryId { get; set; }
            string FilePath { get; set; }
            string GitRepositoryId { get; set; }
        }
        public class WorkItems
        {
            readonly IConfiguration _configuration;
            readonly string _credentials;
            private const string VSTSInstance = "https://somesite-tst.visualstudio.com/";
            private const string VSTSPAT = "flkpkmtn7g6vh22ewijh2ptavisgdqvmp4mh6o726ploqidhu";

            public WorkItems(IConfiguration configuration)
            {
                _configuration = configuration;
                _configuration.PersonalAccessToken = VSTSPAT;
                _credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", _configuration.PersonalAccessToken)));
            }


            public WorkItemPatchResponse.WorkItem UpdateWorkItemUpdateLink(string id, string linkToId)
            {
                WorkItemPatchResponse.WorkItem viewModel = new WorkItemPatchResponse.WorkItem();
                Object[] patchDocument = new Object[1];

                patchDocument[0] = new
                {
                    op = "add",
                    path = "/relations/-",
                    value = new
                    {
                        rel = "System.LinkTypes.Related",
                        url = VSTSInstance + "/_apis/wit/workitems/" + linkToId,
                        attributes = new
                        {
                            comment = "Making a new link for the relation"
                        }
                    }
                };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credentials);

                    // serialize the fields array into a json string          
                    var patchValue = new StringContent(JsonConvert.SerializeObject(patchDocument), Encoding.UTF8, "application/json-patch+json"); // mediaType needs to be application/json-patch+json for a patch call

                    var method = new HttpMethod("PATCH");
                    var request = new HttpRequestMessage(method, VSTSInstance + "_apis/wit/workitems/" + id + "?api-version=3.0-preview") { Content = patchValue };
                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        viewModel = response.Content.ReadAsAsync<WorkItemPatchResponse.WorkItem>().Result;
                    }

                    viewModel.HttpStatusCode = response.StatusCode;

                    return viewModel;
                }
            }

            public WorkItemPatchResponse.WorkItem UpdateWorkItemAddLink(string id, string linkToId)
            {
                WorkItemPatchResponse.WorkItem viewModel = new WorkItemPatchResponse.WorkItem();
                Object[] patchDocument = new Object[1];

                patchDocument[0] = new
                {
                    op = "add",
                    path = "/relations/-",
                    value = new
                    {
                        rel = "System.LinkTypes.Related",
                        url = VSTSInstance + "/_apis/wit/workitems/" + linkToId,
                        attributes = new
                        {
                            comment = "Making a new link for the relation"
                        }
                    }
                };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credentials);

                    // serialize the fields array into a json string          
                    var patchValue = new StringContent(JsonConvert.SerializeObject(patchDocument), Encoding.UTF8, "application/json-patch+json"); // mediaType needs to be application/json-patch+json for a patch call

                    var method = new HttpMethod("PATCH");
                    var request = new HttpRequestMessage(method, VSTSInstance + "_apis/wit/workitems/" + id + "?api-version=3.0-preview") { Content = patchValue };
                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        viewModel = response.Content.ReadAsAsync<WorkItemPatchResponse.WorkItem>().Result;
                    }

                    viewModel.HttpStatusCode = response.StatusCode;

                    return viewModel;
                }
            }

            public WorkItemPatchResponse.WorkItem UpdateWorkItemRemoveLink(string id)
            {
                WorkItemPatchResponse.WorkItem viewModel = new WorkItemPatchResponse.WorkItem();
                Object[] patchDocument = new Object[2];

                patchDocument[0] = new { op = "test", path = "/rev", value = "1" };
                patchDocument[1] = new
                {
                    op = "remove",
                    path = "/relations/0",
                };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credentials);

                    // serialize the fields array into a json string          
                    var patchValue = new StringContent(JsonConvert.SerializeObject(patchDocument), Encoding.UTF8, "application/json-patch+json"); // mediaType needs to be application/json-patch+json for a patch call

                    var method = new HttpMethod("PATCH");
                    var request = new HttpRequestMessage(method, VSTSInstance + "_apis/wit/workitems/" + id + "?api-version=2.2") { Content = patchValue };
                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        viewModel = response.Content.ReadAsAsync<WorkItemPatchResponse.WorkItem>().Result;
                    }

                    viewModel.HttpStatusCode = response.StatusCode;

                    return viewModel;
                }
            }
        }
    }
}
