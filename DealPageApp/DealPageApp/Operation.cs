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
    
    public partial class Operation
    {
        public int ID { get; set; }
        public Nullable<int> DealID { get; set; }
        public Nullable<int> SubAccountID { get; set; }
        public string Number { get; set; }
        public Nullable<System.DateTime> OperationDate { get; set; }
        public Nullable<int> OperationType { get; set; }
        public Nullable<int> OperationSum { get; set; }
        public Nullable<int> SaldoInput { get; set; }
        public Nullable<int> SaldoOutput { get; set; }
    
        public virtual SubAccount SubAccount { get; set; }
    }
}
