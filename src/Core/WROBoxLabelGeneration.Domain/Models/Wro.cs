using System.Text.RegularExpressions;
using WROBoxLabelGeneration.Domain.Enums;
using WROBoxLabelGeneration.SharedKernel;
using WROBoxLabelGeneration.SharedKernel.Extensions;

namespace WROBoxLabelGeneration.Models
{
    public class Wro
    {
        public int RequestID { get; private set; }
        
        public DateTime InsertDate { get; private set; }
        
        public DateTime ExpectedDateOfArrival { get; private set; }
        
        public int? PackageType { get; private set; }
        
        public BoxPackagingType? BoxPackagingType { get; private set; }
        
        public string? BoxLabelURL { get; private set; }
        
        public int UserId { get; private set; }
        
        public int FulfillmentCenterId { get; private set; }
        
        public User User { get; private set; }
        
        public FulfillmentCenter FulfillmentCenter { get; private set; }
        
        public WroOriginAddress WroOriginAddress { get; private set; }
        
        public ICollection<WroInventoryDetail> WroInventoryDetails { get; private set; } = new List<WroInventoryDetail>();
        
        public List<Box> Boxes { get; private set; } = new List<Box>();
        
        public bool HasOriginAddress { get { return WroOriginAddress != null; }}
        
        public List<int> PackingDetailIdsFromBoxes { 
            get { 
                return Boxes.Where(box => box.PackagingDetailsId != null).Select(box => box.PackagingDetailsId!.Value).ToList(); 
            } 
        }
        
        public string BoxLabelFileName { get { return $"{RequestID}_BoxLabel.pdf"; } }
        
        public string DestinationAddressFirstLineForWroBoxLabel {
            get {
                return FulfillmentCenter != null && FulfillmentCenter.FulfillmentCenterTypeId != 3 ? FulfillmentCenter.CenterName : "ShipBob, Inc.";
            }
        }
        
        public string DestinationAddressSecondLineForWroBoxLabel
        {
            get {
                return FulfillmentCenter != null ? FulfillmentCenter.StreetAddress1 : string.Empty;
            }
        }
        
        public string DestinationAddressThirdLineForWroBoxLabel
        {
            get
            {
                return FulfillmentCenter != null ? FulfillmentCenter.StreetAddress2 : string.Empty;
            }
        }
        
        public string DestinationAddressFourthLineForWroBoxLabel
        {
            get
            {
                string line = FulfillmentCenter != null ? $"{FulfillmentCenter.State} {FulfillmentCenter.ZipCode}" : string.Empty;

                if (FulfillmentCenter !=null  && FulfillmentCenter.City != null)
                {
                    line = $"{FulfillmentCenter.City.Name} {line}";
                }
                return line;
            }
        }
        
        public string DestinationPhoneForWroBoxLabel { get { return FulfillmentCenter != null ? FulfillmentCenter.Phone : string.Empty; } }
        
        public string DestinationEmailForWroBoxLabel { get { return FulfillmentCenter != null ? FulfillmentCenter.Email : string.Empty; } }
        
        public string UserCompanyForWroBoxLabel { get { return User != null ? User.CompanyName : string.Empty; } }
        
        public string UserEmailForWroBoxLabel { get { return User != null ? User.Email : string.Empty; } }
        
        public string BoxPackagingTypeName { get { return BoxPackagingType != null ? BoxPackagingType.Value.GetDisplayName() : string.Empty; } }
        
        public bool HasExpectedDateOfArrival { 
            get { 
                return ExpectedDateOfArrival.ToString(ConstantStrings.FormatDate) != DateTime.MinValue.Date.ToString(ConstantStrings.FormatDate);
            }
        }
        
        public string ExpectedDateOfArrivalForLabel {
            get { 
                return ExpectedDateOfArrival.ToString(ConstantStrings.FormatDate); 
            } 
        }
        
        public string InsertDateForLabel
        {
            get
            {
                return InsertDate.ToString(ConstantStrings.FormatDate);
            }
        }

        private Wro() {}

        public void SetBoxes()
        {
            var boxes = WroInventoryDetails.Select(wroid =>
            {
                var boxNumber = wroid.WroInventoryPackagings.Select(wroip => wroip.WroPackagingDetail.BoxNumber).ToList();
                return (wroid, boxNumber).ToTuple();
            }).ToList();

            var boxList = new Dictionary<int, List<WroInventoryDetail>>();

            boxes.ForEach(box =>
            {
                var wroid = box.Item1;
                box.Item2.ForEach(boxNumber =>
                {
                    List<WroInventoryDetail> existing;
                    if (!boxList.TryGetValue(boxNumber, out existing))
                    {
                        existing = new List<WroInventoryDetail>();
                        boxList[boxNumber] = existing;
                    }
                    existing.Add(wroid);
                });
            });

            var finalBoxList = boxList.Select(woridList =>
            {
                var boxNumber = woridList.Key;
                var products = woridList.Value.Distinct().Select(wroid =>
                {
                    var inventory = wroid.Inventory;
                    var warehouseReceivingOrderInventoryPackaging = wroid.WroInventoryPackagings.Where(wroip => wroip.WroPackagingDetail.BoxNumber == boxNumber);
                    var itemQuantity = warehouseReceivingOrderInventoryPackaging.Sum(wroip => wroip.ItemQuantity);
                    return (wroid.InventoryId, inventory.ItemName, wroid.LotNumber, wroid.LotDate, itemQuantity);
                }).ToList();
                return new Box(boxNumber, products);
            }).ToList();

            var boxPackagingDetailsInfo = WroInventoryDetails.Select(wroid => wroid.WroInventoryPackagings.Select(wroip => { return new { wroip.WroPackagingDetail.BoxNumber, wroip.WroPackagingDetail.Id }; }).ToList()).SelectMany(x => x).ToList();

            foreach (var box in finalBoxList)
            {
                var boxPackagingDetails = boxPackagingDetailsInfo.Find(pdi => pdi.BoxNumber == box.Number);
                if (boxPackagingDetails != null)
                {
                    var updatedBox = box with { PackagingDetailsId = boxPackagingDetails.Id };
                    Boxes.Add(updatedBox);
                }
            }

            WroInventoryDetails = new List<WroInventoryDetail>();
        }

        public void UpdateShippingLabelUrlForBox(int packagingDetailsId, string shippingLabelUrl)
        {

            var boxIndex = Boxes.FindIndex(box => box.PackagingDetailsId == packagingDetailsId);
            if (boxIndex != -1)
            {
                Boxes[boxIndex] = Boxes[boxIndex] with { ShippingLabelUrl = shippingLabelUrl };
            }  
        }

        public void UpdateBoxLabelURL(string boxLabelUrl)
        {
            BoxLabelURL = boxLabelUrl;
        }
    }

    public record Box(int Number, List<(int ProductId, string ProductName, string LotNumber, DateTime? ExpirationDate, int Quantity)> Products, int? PackagingDetailsId = null, string? ShippingLabelUrl = null);
}
