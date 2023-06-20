

namespace API.DTOs
{
    public class BasketDto
    {
        public int Id { get; set; }
        public string ByerId { get; set; }
        public List<BasketItemDto> Items { get; set; } 
      
    }
}