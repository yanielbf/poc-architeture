using System.ComponentModel.DataAnnotations;

namespace WROBoxLabelGeneration.Domain.Enums
{
    public enum SystemSettingsType
    {
        [Display(Name = "NewWROBoxLabelFormatEnabled")]
        NewWROBoxLabelFormatEnabled = 1,

        [Display(Name = "FcSupportPhoneNumber")]
        FcSupportPhoneNumber = 2,

        [Display(Name = "WROLabelPurchase")]
        WROLabelPurchase = 3
    }
}
