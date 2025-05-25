using Api_JewelryStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_JewelryStore.Service_Client
{
    public class CardItemService
    {
        public CardItemService()
        {
        }

        public async Task<CardItem> UpdateStatusAsync(int id, string status)
        {
            using (var context = new DiplomDb3Context())
            {
                var cardItem = await context.CardItems.FindAsync(id);
                if (cardItem == null) return null;

                cardItem.Status = status;
                await context.SaveChangesAsync();
                return cardItem;
            }
        }
    }
}
