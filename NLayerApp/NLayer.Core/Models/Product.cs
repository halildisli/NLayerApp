using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } 
        //Bu isimlendirme yapıldığında EF-Core bu property'nin Category entitysine ait bir Foreign key olduğunu anlıyor. Ancak Custom bir isimlendirme yapılırsa bunun ForeignKey olarak işaretlenmesi gerekmektedir. Aynı durum PrimaryKey (Id) içinde geçerlidir. 

        //Navigation Properties
        public Category? Category { get; set; }
        public ProductFeature? ProductFeature { get; set; }
    }
}
