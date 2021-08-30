using System.Collections.ObjectModel;
using WpfGenesys.Models;

namespace WpfGenesys.Data
{
    public class ItemAttachments : ObservableCollection<ItemAttachment>
    {
        public ItemAttachments()
        {
            Add(new ItemAttachment { Name = "Balanced Hilt", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Duelist Cross Guard", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Explosive Missile", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Razor Edge", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Recurve Limbs", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Rune of Blades", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Runic Flame", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Runic Frost", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Runic Thunder", HardPointsNeeded = 2 });
            Add(new ItemAttachment { Name = "Rune of Severing", HardPointsNeeded = 2 });
            Add(new ItemAttachment { Name = "Serrated Edge", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Superior Weapon Customization", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Weighted Head", HardPointsNeeded = 1 });
            Add(new ItemAttachment { Name = "Ynfernael Corruption", HardPointsNeeded = 1 });
        }
    }

}
