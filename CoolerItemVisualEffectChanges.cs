using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using MonoMod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace ExtraUtility
{

    [ExtendsFromMod("CoolerItemVisualEffect")] // Guarantee load only if mod present
    public class CoolerItemVisualEffectChanges : ILoadable
    {
        List<Tuple<MethodInfo, ILContext.Manipulator>> ilChanges = new();

        public bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CoolerItemVisualEffect", out _) && false;
        }

        public void Load(Mod mod)
        {
            /*
            List<Type> modItems = new List<Type> // Type of ModItem
            {
                // Manually list items
                typeof(CoolerItemVisualEffect.Weapons.FirstZenith),
                typeof(CoolerItemVisualEffect.Weapons.LivingWoodSword)
            };
            */

            //var modItems = ContentSamples.ItemsByType.Values.Where(i => i.ModItem?.Mod.Name == "CoolerItemVisualEffect").Select(i => i.ModItem.GetType()); // Not loaded yet!
            var modItems = ModLoader.GetMod("CoolerItemVisualEffect").GetContent<ModItem>().Select(i => i.GetType());

            foreach (var modItem in modItems)
            {
                var method = modItem.GetMethod(nameof(ModItem.CanUseItem));
                if (method != null)
                {
                    if (method.GetRealDeclaringType() == modItem)
                    {
                        ilChanges.Add(Tuple.Create(method, new ILContext.Manipulator(ilEditCanUseItem)));
                    }
                }
            }
            foreach (var ilChange in ilChanges)
            {
                HookEndpointManager.Modify(ilChange.Item1, ilChange.Item2);
            }
        }

        private void ilEditCanUseItem(ILContext il)
        {
            var cur = new ILCursor(il);
            if (!cur.TryGotoNext(MoveType.After,
                i => i.MatchLdfld<Player>(nameof(Player.name)),
                i => i.MatchLdstr(""), // (out _) in case you want to bypass any name, might cause problems and require looping if edited in the future
                i => i.MatchCall<string>("op_Equality")
            ))
                return;
            cur.EmitDelegate((bool isEmptyPlayer) =>
            {
                // Potentially have a config file and return isEmptyPlayer to preserve behaviour when config is not on
                return true; // Fake the player to have always empty name
            });
        }

        public void Unload()
        {
            foreach (var ilChange in ilChanges)
            {
                HookEndpointManager.Unmodify(ilChange.Item1, ilChange.Item2);
            }
            ilChanges.Clear();
        }
    }
}
