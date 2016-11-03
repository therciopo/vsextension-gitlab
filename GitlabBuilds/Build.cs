using System;

namespace GitlabBuilds
{
    public class Build
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Ref { get; set; }
        public string Stage { get; set; }
        public string Name { get; set; }

        public Commit Commit { get; set; }

        public DateTime finished_at { get; set; }

        public Pipeline Pipeline { get; set; }

        public User User { get; set; }
    }
}