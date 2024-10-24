namespace src.DTO.Menu.Request
{
    public class UpdateMenuRequest
    {
        public string menuName { get; set; }
        public string menuCode { get; set; }
        public string menuType { get; set; }
        public bool isAvailable { get; set; }
        public int price { get; set; }
    }
}