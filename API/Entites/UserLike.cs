namespace API.Entites
{
    public class UserLike
    {
        public Appuser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public Appuser LikedUser { get; set; }
        public int LikedUserId{get;set;}
    }
}