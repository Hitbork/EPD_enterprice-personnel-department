//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Enterprice_personell_department
{
    using System;
    using System.Collections.Generic;
    
    public partial class Договор
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Договор()
        {
            this.Сотрудник = new HashSet<Сотрудник>();
        }
    
        public int id_Договора { get; set; }
        public System.DateTime ДатаСоставления { get; set; }
        public System.DateTime ДатаПриема { get; set; }
        public Nullable<System.DateTime> ДатаУвольнение { get; set; }
        public int id_Работы { get; set; }
    
        public virtual ВидРаботы ВидРаботы { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Сотрудник> Сотрудник { get; set; }
    }
}
