using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Domain.Entities
{
    //add common properties to this class that you want to be inherited by all entities in the domain layer.
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(150)]
        public String CreatedBy { get; set; } 
        public DateTime? LastModifiedDate { get; set; }

        [StringLength(150)]
        public String UpdatedBy { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }

    //donot add properties/fields/methods to this class. Do that in the above class.
    public class BaseEntity : BaseEntity<int> { }
}
