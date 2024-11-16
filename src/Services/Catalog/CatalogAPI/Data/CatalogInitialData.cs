using Marten.Schema;

namespace CatalogAPI.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var sess = store.LightweightSession();

            if (await sess.Query<Product>().AnyAsync())
                return;

            sess.Store<Product>(GetPreConfiguredProducts());

            await sess.SaveChangesAsync();


        }

        public static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
        {
            new Product
    {
        Id = Guid.NewGuid(),
        Name = "Apple iPhone 13",
        Description = "Apple iPhone 13 with 128GB storage, A15 Bionic chip, and dual-camera system.",
        ImageFile = "iphone13.jpg",
        Price = 799.99m,
        Category = new List<string> { "Electronics", "Mobile Phones" }
},
            new Product
    {
        Id = Guid.NewGuid(),
        Name = "Samsung Galaxy S21",
        Description = "Samsung Galaxy S21 with 256GB storage, Snapdragon 888, and 64MP camera.",
        ImageFile = "galaxys21.jpg",
        Price = 699.99m,
        Category = new List<string> { "Electronics", "Mobile Phones" }
    },
            new Product
    {
        Id = Guid.NewGuid(),
        Name = "Sony WH-1000XM4 Headphones",
        Description = "Sony wireless noise-canceling headphones with up to 30 hours of battery life.",
        ImageFile = "sony_wh1000xm4.jpg",
        Price = 349.99m,
        Category = new List<string> { "Electronics", "Audio" }
    },
            new Product
    {
        Id = Guid.NewGuid(),
        Name = "MacBook Air M1",
        Description = "Apple MacBook Air with M1 chip, 13-inch Retina display, and 256GB SSD.",
        ImageFile = "macbook_air_m1.jpg",
        Price = 999.99m,
        Category = new List<string> { "Electronics", "Laptops" }
    },
            new Product
    {
        Id = Guid.NewGuid(),
        Name = "Nike Air Max 270",
        Description = "Nike Air Max 270 running shoes with a comfortable fit and stylish design.",
        ImageFile = "nike_air_max_270.jpg",
        Price = 149.99m,
        Category = new List<string> { "Apparel", "Footwear" }
    },
            new Product
    {
        Id = Guid.NewGuid(),
        Name = "Canon EOS Rebel T7",
        Description = "Canon EOS Rebel T7 DSLR camera with 18-55mm lens and built-in Wi-Fi.",
        ImageFile = "canon_rebel_t7.jpg",
        Price = 479.99m,
        Category = new List<string> { "Electronics", "Cameras" }
    },
            new Product
    {
        Id = Guid.NewGuid(),
        Name = "Instant Pot Duo 7-in-1",
        Description = "Instant Pot Duo 7-in-1 electric pressure cooker with 6-quart capacity.",
        ImageFile = "instant_pot_duo.jpg",
        Price = 89.99m,
        Category = new List<string> { "Home Appliances", "Kitchen" }
    },
            new Product
    {
        Id = Guid.NewGuid(),
        Name = "Fitbit Charge 5",
        Description = "Fitbit Charge 5 fitness and health tracker with built-in GPS and sleep tracking.",
        ImageFile = "fitbit_charge_5.jpg",
        Price = 129.99m,
        Category = new List<string> { "Electronics", "Wearables" }
    },
            new Product
    {
        Id = Guid.NewGuid(),
        Name = "Echo Dot (4th Gen)",
        Description = "Amazon Echo Dot smart speaker with Alexa and improved sound quality.",
        ImageFile = "echo_dot_4th_gen.jpg",
        Price = 49.99m,
        Category = new List<string> { "Electronics", "Smart Home" }
    },
            new Product
    {
        Id = Guid.NewGuid(),
        Name = "Kindle Paperwhite",
        Description = "Amazon Kindle Paperwhite with a 6.8-inch display and adjustable warm light.",
        ImageFile = "kindle_paperwhite.jpg",
        Price = 139.99m,
        Category = new List<string> { "Electronics", "e-Readers" }
    }
        };

    }
}
