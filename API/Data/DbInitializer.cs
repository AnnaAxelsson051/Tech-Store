using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(StoreContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    UserName = "bob",
                    Email = "bob@test.com"
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");

                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };

                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] { "Member", "Admin" });
            }


            if (context.Products.Any()) return;

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Apple Watch SE black",
                    Description =
                        "It keeps track of health, motivates to exercise and movement, and has innovative safety features such as crash and fall detection. With eSIM for your Apple Watch SE (2022) you also stay connected without the mobile phone.",
                    Price = 329,
                    PictureUrl = "/images/products/watch-black.png",
                    Brand = "Apple",
                    Type = "Watches",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Apple Watch SE white",
                    Description = "The aluminum housing is light and made from 100 percent recycled aluminum in an alloy used in the aerospace industry. The sports band is made of a particularly durable fluoroelastomer. It's durable yet surprisingly soft and has an innovative pin buckle.",
                    Price = 335,
                    PictureUrl = "/images/products/watch-white.jpeg",
                    Brand = "Apple",
                    Type = "Watches",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Apple Watch SE orange",
                    Description =
                        "The aluminum housing is light and made from 100 percent recycled aluminum in an alloy used in the aerospace industry. The sports band is made of a particularly durable fluoroelastomer. It's durable yet surprisingly soft and has an innovative pin buckle.",
                    Price = 335,
                    PictureUrl = "/images/products/watch-orange.jpeg",
                    Brand = "Apple",
                    Type = "Watches",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Apple Watch SE olive",
                    Description =
                        "The aluminum housing is light and made from 100 percent recycled aluminum in an alloy used in the aerospace industry. The sports band is made of a particularly durable fluoroelastomer. It's durable yet surprisingly soft and has an innovative pin buckle.",
                    Price = 30000,
                    PictureUrl = "/images/products/watch-olive.jpeg",
                    Brand = "Apple",
                    Type = "Watches",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Apple Watch SE pink, refurbished",
                    Description =
                        "This refurbished™ Apple Watch SE is the perfect option for any bargain hunter. It offers sensational features at a sensational refurbished™ price.",
                    Price = 249,
                    PictureUrl = "/images/products/watch-pink.jpeg",
                    Brand = "Apple",
                    Type = "Watches",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Apple Watch SE blue, refurbished",
                    Description =
                        "This refurbished™ Apple Watch SE is the perfect option for any bargain hunter. It offers sensational features at a sensational refurbished™ price.",
                    Price = 249,
                    PictureUrl = "/images/products/watch-blue.jpeg",
                    Brand = "Apple",
                    Type = "Watches",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Core Blue Hat",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 249,
                    PictureUrl = "/images/products/hat-core1.png",
                    Brand = "NetCore",
                    Type = "Hats",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Samsung Galaxy A54 violet",
                    Description =
                        "Experience a smooth user experience thanks to the Samsung Galaxy A54 5G smartphone that offers a 6.4” FHD+ Super AMOLED screen with a 120Hz refresh rate, a 50MPX main camera, an extra-large 5000mAh battery and 25W fast charging.",
                    Price = 464,
                    PictureUrl = "/images/products/samsung-galaxy-a54-violet.png",
                    Brand = "Samsung",
                    Type = "Phones",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Samsung Galaxy A54 black",
                    Description =
                        "Experience a smooth user experience thanks to the Samsung Galaxy A54 5G smartphone that offers a 6.4” FHD+ Super AMOLED screen with a 120Hz refresh rate, a 50MPX main camera, an extra-large 5000mAh battery and 25W fast charging.",
                    Price = 464,
                    PictureUrl = "/images/products/samsung-galaxy-a54-black.png",
                    Brand = "Samsung",
                    Type = "Phones",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Samsung Galaxy S21 pink, refurbished",
                    Description =
                        "Our refurbed™ Galaxy S21 5G is a top-of-the-range smartphone from Samsung that offers a host of premium features and an extraordinary design.",
                    Price = 1800,
                    PictureUrl = "/images/products/samsung-galaxy-s21-pink.png",
                    Brand = "Samsung",
                    Type = "Phones",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Samsung Galaxy S21 grey, refurbished",
                    Description =
                        "Our refurbed™ Galaxy S21 5G is a top-of-the-range smartphone from Samsung that offers a host of premium features and an extraordinary design.",
                    Price = 1500,
                    PictureUrl = "/images/products/samsung-galaxy-s21-grey.png",
                    Brand = "Samsung",
                    Type = "Phones",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Purple React Gloves",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1600,
                    PictureUrl = "/images/products/glove-react1.png",
                    Brand = "React",
                    Type = "Gloves",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Green React Gloves",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1400,
                    PictureUrl = "/images/products/glove-react2.png",
                    Brand = "React",
                    Type = "Gloves",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Redis Red Boots",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 25000,
                    PictureUrl = "/images/products/boot-redis1.png",
                    Brand = "Redis",
                    Type = "Boots",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Core Red Boots",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 18999,
                    PictureUrl = "/images/products/boot-core2.png",
                    Brand = "NetCore",
                    Type = "Boots",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Core Purple Boots",
                    Description =
                        "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                    Price = 19999,
                    PictureUrl = "/images/products/boot-core1.png",
                    Brand = "NetCore",
                    Type = "Boots",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Angular Purple Boots",
                    Description = "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.",
                    Price = 15000,
                    PictureUrl = "/images/products/boot-ang2.png",
                    Brand = "Angular",
                    Type = "Boots",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Angular Blue Boots",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 18000,
                    PictureUrl = "/images/products/boot-ang1.png",
                    Brand = "Angular",
                    Type = "Boots",
                    QuantityInStock = 100
                },
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
        }
    }
}

