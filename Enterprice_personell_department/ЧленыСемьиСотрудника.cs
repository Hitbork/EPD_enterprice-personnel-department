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
    
    public partial class ЧленыСемьиСотрудника
    {
        public int id_ЧленаСемьи { get; set; }
        public int id_Сотрудника { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public int id_ВидаРодства { get; set; }
    
        public virtual ВидРодства ВидРодства { get; set; }
        public virtual Сотрудник Сотрудник { get; set; }
    }
}
