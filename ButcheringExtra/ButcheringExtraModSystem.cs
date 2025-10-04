using Butchering;
using System;
using System.Text;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace butchering_patch;


class BlockEntityButcherHookFix : BlockEntityButcherHook
{

    public override void GetBlockInfo(IPlayer forPlayer, StringBuilder sb)
    {
        base.GetBlockInfo(forPlayer, sb);
        float num = GameMath.Clamp((1f - container.GetPerishRate() - 0.5f) * 3f, 0f, 1f);
        var perishRate = Math.Round(container.GetPerishRate(), 2);
        sb.AppendLine(Lang.Get("Stored food perish speed: {0}x", perishRate));
        var index = forPlayer.CurrentBlockSelection.SelectionBoxIndex;
        if (!this.inventory[index].Empty)
        {
            ItemStack itemstack = inventory[index].Itemstack;
            if (itemstack.Collectible.TransitionableProps != null && itemstack.Collectible.TransitionableProps.Length != 0)
            {

                sb.Append(BlockEntityShelf.PerishableInfoCompact(Api, inventory[index], num));
            }
            else
            {
                sb.AppendLine(itemstack.GetName());
            }
        }
        else sb.AppendLine("Empty");
    }
}

class BlockEntityButcherTableFix : BlockEntityButcherTable
{

    public override void GetBlockInfo(IPlayer forPlayer, StringBuilder sb)
    {
        base.GetBlockInfo(forPlayer, sb);
        float num = GameMath.Clamp((1f - container.GetPerishRate() - 0.5f) * 3f, 0f, 1f);


        sb.AppendLine();
        var index = forPlayer.CurrentBlockSelection.SelectionBoxIndex;
        if (!this.inventory[index].Empty)
        {
            ItemStack itemstack = inventory[index].Itemstack;
            if (itemstack.Collectible.TransitionableProps != null && itemstack.Collectible.TransitionableProps.Length != 0)
            {

                sb.Append(BlockEntityShelf.PerishableInfoCompact(Api, inventory[index], num));
            }
            else
            {
                sb.AppendLine(itemstack.GetName());
            }
        }
        else sb.AppendLine("Empty");
    }
}



public class butchering_patchModSystem : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        api.RegisterBlockEntityClass("ButcherHook", typeof(BlockEntityButcherHookFix));
        api.RegisterBlockEntityClass("ButcherTable", typeof(BlockEntityButcherTableFix));
    }
}
