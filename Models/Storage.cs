namespace seminar1._1.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
