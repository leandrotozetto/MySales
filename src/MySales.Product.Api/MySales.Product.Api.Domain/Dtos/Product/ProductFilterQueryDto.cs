//namespace MySales.Product.Api.Domain.Dtos.Product
//{
//    public class ProductFilterQueryDto : IFilter
//    {
//        /// <summary>
//        /// Name.
//        /// </summary>
//        public string Name { get; private set; }

//        /// <summary>
//        /// Status.
//        /// </summary>        
//        public bool Status { get; private set; }

//        /// <summary>
//        /// Current page.
//        /// </summary>
//        public string OrderBy { get; private set; }

//        /// <summary>
//        /// Current page.
//        /// </summary>
//        public int CurrentPage { get; private set; }

//        /// <summary>
//        /// Quantity of items per page.
//        /// </summary>
//        public int ItemsPerPage { get; private set; }

//        private ProductFilterQueryDto() { }

//        public static ProductFilterQueryDto New(string name, bool status, string orderby, int currentPage, int itemsPerPage)
//        {
//            return new ProductFilterQueryDto()
//            {
//                Name = name,
//                Status = status,
//                OrderBy = orderby,
//                CurrentPage = currentPage,
//                ItemsPerPage = itemsPerPage
//            };
//        }
//    }
//}
