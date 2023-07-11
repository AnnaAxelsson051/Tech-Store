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
                    Name = "Advantech AIMB-787 LGA1200 Motherboard",
                    Description =
                        "Advantech AIMB-787 LGA1200 Motherboard features support for 7x expansion slots and a PCIe x4 M.2 socket. The AIMB-787 includes the latest Intel® Core™ processors and offers high-performance computing for industrial applications that require seamless upgrades, long-term support, proven reliability, and strict revision control. The ASMB-787 series are server-grade motherboards with Gen. Intel® Core™/Xeon® W-1200 processors. They have up to 128GB of DDR4 memory and offer DVI-D, VGA, and HDMI 2.0 connectors to support three displays.",
                    Price = 429,
                    PictureUrl = "/images/products/motherboard-advantech.png",
                    Brand = "Advantech",
                    Type = "Motherboards",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "ProArt B650-Creator Motherboard",
                    Description =
                        "ProArt B650-Creator is an advanced motherboard designed for intensive content-creation workloads like virtual production, 3D rendering and 4K/8K video editing. It unleashes the performance of AMD Ryzen™ 7000 Series processors through robust power delivery and cooling design, and its comprehensive connectivity includes onboard 2.5 Gb and 1 Gb Ethernet, a USB 3.2 Gen 2x2 Type-C® front-panel connector with Quick Charge 4+, and DisplayPort™ via USB-C.",
                    Price = 456,
                    PictureUrl = "/images/products/motherboard-proart.png",
                    Brand = "ProArt",
                    Type = "Motherboards",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "ASUS Dual GeForce RTX 3060 12GB OC V2",
                    Description =
                        "ASUS Dual GeForce RTX™ 3060 V2 combines dynamic thermal performance with wide compatibility thanks to the latest NVIDIA® Ampere architecture. Advanced cooling solutions taken from top-of-the-line graphics cards - including two Axial-tech fans to maximize airflow to the heatsink - are fitted into the 20cm long, two bay card - providing more power in less space.",
                    Price = 365,
                    PictureUrl = "/images/products/gpu-asus-dual-3060.png",
                    Brand = "Asus",
                    Type = "Graphics card",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "ASUS STRIX GeForce RTX 4090 24GB Gaming OC",
                    Description =
                        "ROG Strix GeForce RTX® 4090 gives a whole new meaning to the word opulence. Inside and out, every part of the card gives the monstrous GPU free space to breathe and deliver ultimate performance. The Nvidia Ada Lovelace architecture is here to dominate.",
                    Price = 978,
                    PictureUrl = "/images/products/gpu-asus-strix-4090.png",
                    Brand = "Asus",
                    Type = "Graphics card",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Apple MacBook Air 13 inch M2 8-core",
                    Description =
                        "MacBook Air has received a completely new design with an unimaginably thin aluminum casing. Supercharged with the new M2 chip, it offers superb performance and up to 18 hours of battery life.",
                    Price = 999,
                    PictureUrl = "/images/products/macbook-air-13.png",
                    Brand = "Apple",
                    Type = "Laptops",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Apple MacBook Pro 14 inch M2 Pro",
                    Description = "The 14-inch MacBook Pro with M2 Pro or M2 Max is extremely fast and powerful, both when running on battery power and when connected to the mains. With an impressive Liquid Retina XDR display, all the ports you need and an all-day battery1, this professional laptop can take you where you want to go.",
                    Price = 1200,
                    PictureUrl = "/images/products/macbook-pro-14.png",
                    Brand = "Apple",
                    Type = "Laptops",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Deltaco Smart Home Plug / 10A - 3-pack",
                    Description = "The H-P01 is a wireless power switch that can be easily turned on or off via your phone, both when you're at home or remotely. SH-P01 can be used for all devices that have the correct plug (max 10A). Perfect for e.g. TV, computer, lamps or other lighting.",
                    Price = 39,
                    PictureUrl = "/images/products/smart-plug.png",
                    Brand = "Google Home",
                    Type = "Smart Homes",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Deltaco Smart Home LED-lamp / Color / 3-pack - E27",
                    Description = "The SH-LE27RGB is a smart RGB LED lamp that can be placed in any E27 lamp holder. The lamp is dimmable via the app so you can set the desired brightness yourself and if warm or cold white is not enough, you can also choose a special color from among 16 million colors to get the right feeling in the room. The power of the SH-LE27RGB is 9W, which corresponds to approximately 60W from a traditional light bulb.",
                    Price = 39,
                    PictureUrl = "/images/products/smart-lamp.png",
                    Brand = "Google Home",
                    Type = "Smart Homes",
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

