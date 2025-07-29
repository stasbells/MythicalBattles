using MythicalBattles.Shop.EquipmentShop;

namespace MythicalBattles
{
    public interface IVisitorAcceptor
    {
       public void Accept(IShopItemVisitor visitor);
    }
}
