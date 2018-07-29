using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreIssue12834
{
    public class MySecondaryEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public SecondaryEntityType Type { get; set; }

        [ForeignKey(nameof(PrimaryEntity))]
        public long? PrimaryEntityId { get; set; }

        public virtual MyPrimaryEntity PrimaryEntity { get; set; }
    }
}
