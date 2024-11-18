using System.ComponentModel.DataAnnotations;

namespace WROBoxLabelGeneration.Domain.Enums
{
    public enum BoxPackagingType
    {
        [Display(Name = "Not Found")]
        NotFound = 0,
        [Display(Name = "Everything in one box")] // 
        EverythingInOneBox = 1,
        [Display(Name = "Single SKU per box")]
        OneSkuPerBox = 2,
        [Display(Name = "Multiple SKU per box")]
        MultipleSkuPerBox = 3
    }
}
