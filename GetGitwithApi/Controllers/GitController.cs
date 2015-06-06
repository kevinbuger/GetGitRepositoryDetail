using System;
using System.Collections.Generic;
//using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetGitwithApi.Models;
using LibGit2Sharp;
using System.Web;
using GetGitwithApi.Helper;
namespace GetGitwithApi.Controllers
{
    public class GitController : ApiController
    {
        public GitInfo GetGitInfo()
        {
            GitInfoRepository gRep = new GitInfoRepository();
            string repositoryURL = HttpContext.Current.Request.QueryString["rUrl"];
            return gRep.GetInfo(repositoryURL);
        }
    }
}
