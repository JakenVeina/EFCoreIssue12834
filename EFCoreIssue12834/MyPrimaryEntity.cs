using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreIssue12834
{
    public class MyPrimaryEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public PrimaryEntityType Type { get; set; }

        [Required]
        [ForeignKey(nameof(SecondaryEntity))]
        public long SecondaryEntityId { get; set; }

        [Required]
        public virtual MySecondaryEntity SecondaryEntity { get; set; }
    }
}
