namespace Librebooks.Areas.Admin.Models
{
    public class TaxTypeModel
    {
        public virtual string? Name { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual bool System { get; set; }
        public virtual string? Group { get; set; }
    }

    public class TaxTypeUpdateModel : TaxTypeModel
    {

    }

    public class TaxTypeDTO
    {

    }
}
