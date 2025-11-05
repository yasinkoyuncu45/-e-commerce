namespace MarketHup.Models.MVVM
{
    public class MainPageModel
    {
        public List<Product> SliderProducts { get; set; }// slider ürünler
        public Product Productofday { get; set; }// günün ürünü ürünü
        public List<Product> NewProducts { get; set; }// yeni ürün
        public List<Product>? SpecialProducts { get; set; }// özel

        public List<Product>? DiscountedProducts { get; set; } //indirimli
        public List<Product>? HighlightedProducts { get; set; }//öne cıkanlar
        public List<Product>? TopsellerProducts { get; set; }//cok satanlar
        public List<Product>? StarProducts { get; set; }//yıldızlı ürünler
        public List<Product>? OpportunityProducts { get; set; }//fırsat satanlar
        public List<Product>? NoTableProducts { get; set; }// dikkat çeken
        public List<Product>? PopulerProducts { get; set; }//popüler
        public List<Product>? CategoryPage { get; set; }//Categori sol liste

        public List<Product>? SupplierPage { get; set; }//marka  alt liste

        public int TotalProductCount { get; set; } // <-- yeni eklendi
        public Product? Productdetails { get; set; }// günün ürünü ürünü







        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }

        public List<Product>? RelatedProducts { get; set; }//buna bakanlar
    }
}
