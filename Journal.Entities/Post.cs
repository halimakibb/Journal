using System.Collections.Generic;

namespace Journal.Entities
{
    public class Post : BaseEntity
    {
        public long PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int MoodPercent { get; set; }
        public long UserId { get; set; }
        public User Author { get; set; }
        public List<Tag> Tags { get; set; }
    }
}