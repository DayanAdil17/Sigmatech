using Sigmatech.Entities.Base;

namespace src.Entities.Menu
{
    public class MenuEntity : BaseEntity
    {
        public string menuName { get; set; }
        public string menuCode { get; set; }
        public string menuType { get; set; }
        public bool isAvailable { get; set; }
        public int price { get; set; }
    }
}