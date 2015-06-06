﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetGitwithApi.Models;
using LibGit2Sharp;
using System.Web;

namespace GetGitwithApi.Controllers
{
    public class BranchesController : ApiController
    {
        public MyBranchChart GetBranchesChart()
        {
            GitInfoRepository gRep = new GitInfoRepository();
            string repositoryURL = HttpContext.Current.Request.QueryString["rUrl"];
            return gRep.GetBChart(repositoryURL);
        }
    }
}
