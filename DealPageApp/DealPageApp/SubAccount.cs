//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DealPageApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubAccount()
        {
            this.Operations = new HashSet<Operation>();
        }
    
        public int ID { get; set; }
        public Nullable<int> AccountPlanID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    
        public virtual AccountPlan AccountPlan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operation> Operations { get; set; }
    }
}