//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace проект_wpf_4_курс
{
    using System;
    using System.Collections.Generic;
    
    public partial class usersimage
    {
        public int id { get; set; }
        public int id_user { get; set; }
        public string path { get; set; }
        public byte[] image { get; set; }
        public bool avatar { get; set; }
    
        public virtual users users { get; set; }
    }
}
