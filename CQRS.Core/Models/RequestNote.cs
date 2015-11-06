namespace CQRS.Core.Models
{
    public class RequestNote
    {
        public int? RequestNoteId { get; set; }
        public int? RequestId { get; set; }
        public int? UserId { get; set; }
        public string Note { get; set; }

        public virtual Request Request { get; set; }
        public virtual User User { get; set; }
    }
}