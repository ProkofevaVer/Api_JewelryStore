using Api_JewelryStore.DTO_Models;
using Api_JewelryStore.Models;
using Api_JewelryStore.Service_Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_JewelryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly DiplomDb3Context _context;
        private readonly CardItemService _cardItemService;
        public CartItemController(DiplomDb3Context context)
        {
            _context = context;
            _cardItemService = new CardItemService();
        }

        [HttpPost("{id}/status")]
        public async Task<ActionResult<CardItem>> UpdateStatusAsync(int id, [FromBody] UpdateStatusRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Status))
            {
                return BadRequest("Статус обязателен");
            }

            var updatedCardItem = await _cardItemService.UpdateStatusAsync(id, request.Status);
            if (updatedCardItem == null)
            {
                return NotFound($"CardItem with ID {id} not found.");
            }

            return Ok(updatedCardItem);
        }


        // GET: api/CartItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardItem>>> GetCartItem()
        {
            var cartItem = await _context.CardItems.ToListAsync();

            if (cartItem == null || !cartItem.Any())
            {
                return NotFound("No materials found.");
            }

            return Ok(cartItem);
        }



        // POST: api/cartitem
        [HttpPost("addCard")]
        public async Task<ActionResult<CardItem>> AddCartItem([FromBody] AddCartItemDto addCartItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Создание нового элемента корзины на основе DTO
            var cartItem = new CardItem
            {
                JewelrySizesItemId = addCartItemDto.JewelrySizesItemId,
                UserId = addCartItemDto.UserId,
                Status = addCartItemDto.Status,
                CardQuantity = addCartItemDto.CardQuantity,
                CardTotalPrice = addCartItemDto.CardTotalPrice, // Обратите внимание на правильное имя свойства
                CardDate = addCartItemDto.CardDate
            };

            // Добавление нового элемента в корзину
            _context.CardItems.Add(cartItem);
            await _context.SaveChangesAsync();

            // Возвращаем созданный элемент с его ID
            return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, cartItem); // Используйте cartItem.Id
        }

        // PUT: api/cartitem/updateStatus/{id}
        [HttpPut("updateStatus/{id}")]
        public async Task<ActionResult<CardItem>> UpdateCartItemStatus(int id, [FromBody] string newStatus)
        {
            // Находим элемент корзины по ID
            var cartItem = await _context.CardItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound($"Cart item with ID {id} not found.");
            }

            // Обновляем статус
            cartItem.Status = newStatus;

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            // Возвращаем обновленный элемент
            return Ok(cartItem);
        }


    }
}
