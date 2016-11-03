
using System;
using System.Collections.Generic;

namespace GitlabBuilds
{
    public class Pipeline
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public string Ref { get; set; }

        public string Commit { get; set; }

        public string Commit_id { get; set; }

        public DateTime Finished_at { get; set; }

        public string UserName { get; set; }

        public Uri ImageFilePath { get; set; }

        public IEnumerable<Build> Builds { get; set; }
    }
}