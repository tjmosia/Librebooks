using System.ComponentModel.DataAnnotations;

using Librebooks.Models.Entity.SystemSpace;

namespace Librebooks.Areas.Admin.Models.BusinessSectorModels
{
    public class BusinessSectorModel
    {
        [Required(ErrorMessage = "Sector name is required.")]
        public string? Name { get; set; }
    }

    public class BusinessSectorEditModel : BusinessSectorModel
    {
        [Required(ErrorMessage = "Sector id is required.")]
        public int Id { get; set; }
    }

    public class BusinessSectorDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public static BusinessSectorDTO MapFromBusinessSector (BusinessSector sector)
        {
            return new BusinessSectorDTO
            {
                Id = sector.Id,
                Name = sector.Name
            };
        }
    }
}