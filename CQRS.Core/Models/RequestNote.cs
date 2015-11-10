namespace CQRS.Core.Models
{
    public class RequestNote
    {
        public int? RequestNoteId { get; set; }
        public int? RequestId { get; set; }
        public int? UserId { get; set; }
        public string Note { get; set; }

        public Request Request { get; set; }
        public User User { get; set; }
    }
}