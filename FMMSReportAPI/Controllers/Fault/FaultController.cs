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
    public class FaultController : ApiController
    {
        private readonly FaultQuery faultQuery = new FaultQuery();


        [HttpGet]
        [Route("api/faultReport/Download")]
        public HttpResponseMessage DownloadSurvey(FaultModel model)
        {
            faultQuery.GetFaultReport(model);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            string filepaths = HttpContext.Current.Server.MapPath($"/CSV/Fault.csv");
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
