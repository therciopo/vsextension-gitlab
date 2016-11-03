using System;

namespace GitlabBuilds
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime last_activity_at { get; set; }
        public NameSpace NameSpace { get; set; }

        public Uri Avatar_url { get; set; }

    }
}