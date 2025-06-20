using System.Collections.Generic;

public class CheckoutRequestDto
{
    public int IdComprador { get; set; }
    public List<CartItemDto> Productos { get; set; } = new List<CartItemDto>();
}