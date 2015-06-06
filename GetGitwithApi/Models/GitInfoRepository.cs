using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LibGit2Sharp;

namespace GetGitwithApi.Models
{
    public class GitInfoRepository
    {
        // get global detail
        public GitInfo GetInfo(string repositoryURL)
        {
            GitInfo gitlist = new GitInfo();
            try
            {
                var tmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                CloneOptions myoption = new CloneOptions();
                myoption.IsBare = true;
                string path = Repository.Clone(repositoryURL, tmp, myoption);
                using (var Git = new Repository(path))
                {
                    gitlist.homePage = string.Format("{0} | {1} | {2}", Git.Head.Name, Git.Head.IsCurrentRepositoryHead, Git.Head.Remote.Url);
                    List<ObjectId> branchesId = Git.Branches.Select(b => b.Tip.Id).Distinct().ToList();
                    gitlist.contributorscount = Git.Commits.Select(c => c.Committer.Name).Distinct().Count();
                    foreach (var brancheid in branchesId)
                    {
                        MyBranch branch = new MyBranch();
                        branch.BranchId = brancheid;
                        branch.CountCommits = Git.Branches.FirstOrDefault(b => b.Tip.Id == brancheid).Commits.Count();
                        gitlist.branches.Add(branch);
                    }
                }
            }
            catch(Exception ex)
            {
            }
            
            return gitlist;
        }

        // get contributor chart
        public List<MyContributor> GetCChart(string repositoryURL)
        {
            List<MyContributor> contributorChartList = new List<MyContributor>();
            try
            {
                var tmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                CloneOptions myoption = new CloneOptions();
                myoption.IsBare = true;

                string path = Repository.Clone(repositoryURL, tmp, myoption);
                using (var Git = new Repository(path))
                {
                    List<string> contributors = Git.Commits.Select(c => c.Committer.Name).Distinct().ToList();
                    foreach (var contributor in contributors)
                    {
                        MyContributor mycontributor = new MyContributor();
                        mycontributor.Name = contributor;
                        mycontributor.CountCommits = Git.Commits.Where(c => c.Committer.Name == contributor).Count();
                        contributorChartList.Add(mycontributor);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return contributorChartList;
        }

        // get Branche chart
        public MyBranchChart GetBChart(string repositoryURL)
        {
            MyBranchChart branchChart = new MyBranchChart();
            try
            {
                var tmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                CloneOptions myoption = new CloneOptions();
                myoption.IsBare = true;

                string path = Repository.Clone(repositoryURL, tmp, myoption);
                using (var Git = new Repository(path))
                {
                    List<ObjectId> branchesId = Git.Branches.Select(b => b.Tip.Id).Distinct().ToList();
                    branchChart.Days = Git.Commits.Select(s => s.Committer.When.Date).Distinct().ToList();
                    foreach (var brancheid in branchesId)
                    {
                        branchChart.BranchesID.Add(brancheid.Sha);
                        List<int> cons = new List<int>();
                        foreach (var day in branchChart.Days)
                        {
                            int count = Git.Branches.FirstOrDefault(b => b.Tip.Id == brancheid).Commits.Where(c => c.Committer.When.Date == day).Count();
                            cons.Add(count);
                        }
                        branchChart.CountCommitsPerDay.Add(cons);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return branchChart;
        }
    }
}