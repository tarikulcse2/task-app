using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Entities.Models
{
    [Table("Jobs")]
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string Location { get; set; }
        public int Notify { get; set; }
        public string Label { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int ModifierId { get; set; }
    }
}