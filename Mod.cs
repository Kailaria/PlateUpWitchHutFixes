using KitchenData;
using KitchenLib;
using KitchenLib.Logging;
using KitchenLib.References;
using KitchenLib.Utils;
using KitchenMods;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KitchenLogger = KitchenLib.Logging.KitchenLogger;

namespace KitchenWitchHutPatch
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "kailaria.witchhutpatch";
        public const string MOD_NAME = "Witch Hut Patch";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "Kailaria";
        public const string MOD_GAMEVERSION = ">=1.2.0";

        internal static KitchenLogger Logger;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            Logger.LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
            FixMissingEnchantments();
            AddEnchantmentPanel();
        }

        protected override void OnUpdate()
        {
        }
        
        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            Logger = InitLogger();
        }

        private void FixMissingEnchantments()
        {
            Logger.LogInfo("Fixing Missing Enchantments");

            var prepStationAppliance = (Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.PrepStation);
            var picklingStationAppliance = (Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.PicklingStation);

            if (!prepStationAppliance.Enchantments.Contains(picklingStationAppliance))
            {
                prepStationAppliance.Enchantments.Add(picklingStationAppliance);
            }
            Logger.LogInfo("Done Fixing Missing Enchantments");
        }

        private void AddEnchantmentPanel()
        {
            Logger.LogInfo("[AddEnchantmentPanel] Adding Missing Enchantable Tags");
            List<Appliance> enchantableAppliances = new List<Appliance>();
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.Countertop));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.Hob));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.SinkNormal));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.PrepStation));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.TrayStand));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.Belt));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.Bin));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.MopBucket));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.Dumbwaiter));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.PlateStack));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.FlowerPot));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.SharpKnifeProvider));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.RollingPinProvider));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.ClipboardStand));
            enchantableAppliances.Add((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.ScrubbingBrushProvider));

            if (GameData.Main.GlobalLocalisation.Text.TryGetValue("Enchantable", out string enchantableStr))
            {
                foreach (Appliance appliance in enchantableAppliances)
                {
                    Logger.LogInfo($"[AddEnchantmentPanel] Adding tag for {appliance.Name}");
                    appliance.Info.Get(Locale.English).Tags.Add(enchantableStr);
                }
            }
            else
            {
                Logger.LogWarning("[AddEnchantmentPanel] Unable to find \"Enchantable\" localization.");
            }
            Logger.LogInfo("[AddEnchantmentPanel] Done Adding Enchantable Tags");
        }
    }
}