using FBV.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBV.Domain.Entities
{
    public class Membership
    {
        public int Id { get; set; }

        public MembershipType MembershipTypeId { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        #region Navigation properties

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        #endregion Navigation properties
    }
}
