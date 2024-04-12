namespace Models.Bug
{
    public class BugUpdateReq
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public  string Fixed {  get; set; }
    }
}
