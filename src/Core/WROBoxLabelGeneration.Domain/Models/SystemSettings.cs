using System.ComponentModel.DataAnnotations;

namespace WROBoxLabelGeneration.Models
{
    public class SystemSettings
    {
        public int SettingId { get; set; }
        public string SettingName { get; set; }
        public bool Enabled { get; set; }
        public string Value { get; set; }
    }
}
