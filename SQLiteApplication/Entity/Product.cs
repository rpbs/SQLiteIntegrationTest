using System.ComponentModel.DataAnnotations;

namespace SQLiteApplication.Entity
{
    public class Product(string description, double value)
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; } = description;
        public double Value { get; set; } = value;
    }
}
