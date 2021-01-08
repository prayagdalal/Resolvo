using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class QnAContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Badge> Badge { get; set; }
        public DbSet<Forum> Forum { get; set; }
        public DbSet<Thread> Thread { get; set; }
        public DbSet<ForumTags> ForumTags { get; set; }
        public DbSet<UserBadges> UserBadges { get; set; }
        public DbSet<UserTags> UserTags { get; set; }
        public DbSet<Points> Points { get; set; }
        public DbSet<ForumVotes> ForumVotes { get; set; }
        public DbSet<ThreadVotes> ThreadVotes { get; set; }
        public QnAContext()
        {

        }
    }
}