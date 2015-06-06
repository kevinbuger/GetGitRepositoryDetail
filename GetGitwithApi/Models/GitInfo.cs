using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibGit2Sharp;

namespace GetGitwithApi.Models
{
    public class GitInfo
    {
        public string homePage { get; set; }
        public int contributorscount { get; set; }
        private List<MyBranch> _branches = new List<MyBranch>();
        public List<MyBranch> branches{get { return _branches; }}
        //private List<MyContributor> _contributors = new List<MyContributor>();
        //public List<MyContributor> contributors{get { return _contributors; }}
    }

    public class MyBranch
    {
        public ObjectId BranchId { get; set; }
        public int CountCommits { get; set; }
    }

    public class MyBranchChart
    {
        private List<string> _branches = new List<string>();
        public List<string> BranchesID
        {
            get { return _branches; }
        }
        private List<DateTime> _days = new List<DateTime>();
        public List<DateTime> Days
        {
            get { return _days; }
            set { _days = value; }
        }
        private List<List<int>> _countcommitsPerDay = new List<List<int>>();
        public List<List<int>> CountCommitsPerDay
        {
            get { return _countcommitsPerDay; }
        }
       // public int CountCommitsPerDay { get; set; }
        
    }
    public class MyContributor
    {
        public string Name { get; set; }
        public int CountCommits { get; set; }
    }
}