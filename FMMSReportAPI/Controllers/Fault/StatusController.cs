using FMMSReportAPI.Interface;
using FMMSReportAPI.Models;
using FMMSReportAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace FMMSReportAPI.Controllers.Fault
{
    public class StatusController : ApiController
    {
        private readonly StatusQuery statusQuery = new StatusQuery();

        [HttpGet]
        [Route("api/statusReport/Download")]
        public HttpResponseMessage DownloadSurvey(StatusModel model)
        {
            statusQuery.GetStatusReport(model);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            string filepaths = HttpContext.Current.Server.MapPath($"/CSV/Status.csv");
            var fileBytes = File.ReadAllBytes(filepaths);
            var fileMemStream = new MemoryStream(fileBytes);
            result.Content = new StreamContent(fileMemStream);
            var headers = result.Content.Headers;
            headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            headers.ContentDisposition.FileName = filepaths;
            headers.ContentType = new MediaTypeHeaderValue("application/csv");
            headers.ContentLength = fileMemStream.Length;
            return result;

        }
    }
}
