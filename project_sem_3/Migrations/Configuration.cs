namespace project_sem_3.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<project_sem_3.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "project_sem_3.Models.ApplicationDbContext";
        }

        protected override void Seed(project_sem_3.Models.ApplicationDbContext context)
        {
            // role
            context.Roles.AddOrUpdate(x => x.Id,
                new ApplicationRole() { Id = "1", Name = "Super" },
                new ApplicationRole() { Id = "2", Name = "Admin" }
                //new ApplicationRole() { Id = "3", Name = "User" }
            );

            // user
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("@Ad123456");
            var mapUser = new Dictionary<string, ApplicationUser>(); // role + user
            List<SeedUserRole> userList = new List<SeedUserRole>();
            // admin
            var admin01 = new ApplicationUser()
            {
                Id = "1",
                UserName = "Super",
                PasswordHash = password,
                Email = "tungvtth1809003@fpt.edut.vn",
                EmailConfirmed = true,
                PhoneNumber = "+84356120129",
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
            };
            userList.Add(new SeedUserRole() { User = admin01, RoleId = "1" });

            var admin02 = new ApplicationUser()
            {
                Id = "2",
                UserName = "Thu",
                PasswordHash = password,
                Email = "thunqth1807052@fpt.edu.vn",
                EmailConfirmed = true,
                PhoneNumber = "+84979650208",
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
            };
            userList.Add(new SeedUserRole() { User = admin02, RoleId = "2" });

            var admin04 = new ApplicationUser()
            {
                Id = "4",
                UserName = "Linh",
                PasswordHash = password,
                Email = "linhnkth1809007@fpt.edu.vn",
                EmailConfirmed = true,
                PhoneNumber = "+84347561935",
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
            };
            userList.Add(new SeedUserRole() { User = admin04, RoleId = "2" });
            //// user
            //var user05 = new ApplicationUser()
            //{
            //    Id = "5",
            //    UserName = "Nga",
            //    PasswordHash = password,
            //    Email = "nganguyen@fpt.edu.vn",
            //    EmailConfirmed = true,
            //    PhoneNumber = "+8434657896",
            //    PhoneNumberConfirmed = true,
            //    LockoutEnabled = true,
            //};
            //userList.Add(new SeedUserRole() { User = user05, RoleId = "3" });

            //var user06 = new ApplicationUser()
            //{
            //    Id = "6",
            //    UserName = "Dung",
            //    PasswordHash = password,
            //    Email = "dungchi2000@fpt.edu.vn",
            //    EmailConfirmed = true,
            //    PhoneNumber = "+8434657652",
            //    PhoneNumberConfirmed = true,
            //    LockoutEnabled = true,
            //};
            //userList.Add(new SeedUserRole() { User = user06, RoleId = "3" });

            //var user07 = new ApplicationUser()
            //{
            //    Id = "7",
            //    UserName = "Tinh",
            //    PasswordHash = password,
            //    Email = "chutinh@fpt.edu.vn",
            //    EmailConfirmed = true,
            //    PhoneNumber = "+8434362214",
            //    PhoneNumberConfirmed = true,
            //    LockoutEnabled = true,
            //};
            //userList.Add(new SeedUserRole() { User = user07, RoleId = "3" });

            //var user08 = new ApplicationUser()
            //{
            //    Id = "8",
            //    UserName = "Thanh",
            //    PasswordHash = password,
            //    Email = "thanhnguyen@fpt.edu.vn",
            //    EmailConfirmed = true,
            //    PhoneNumber = "+8434236956",
            //    PhoneNumberConfirmed = true,
            //    LockoutEnabled = true,
            //};
            //userList.Add(new SeedUserRole() { User = user08, RoleId = "3" });

            //var user09 = new ApplicationUser()
            //{
            //    Id = "9",
            //    UserName = "Loi",
            //    PasswordHash = password,
            //    Email = "loichuvan@fpt.edu.vn",
            //    EmailConfirmed = true,
            //    PhoneNumber = "+8434698569",
            //    PhoneNumberConfirmed = true,
            //    LockoutEnabled = true,
            //};
            //userList.Add(new SeedUserRole() { User = user09, RoleId = "3" });

            //var user10 = new ApplicationUser()
            //{
            //    Id = "10",
            //    UserName = "Long",
            //    PasswordHash = password,
            //    Email = "tranlong@fpt.edu.vn",
            //    EmailConfirmed = true,
            //    PhoneNumber = "+8434698236",
            //    PhoneNumberConfirmed = true,
            //    LockoutEnabled = true,
            //};
            //userList.Add(new SeedUserRole() { User = user10, RoleId = "3" });

            //var user11 = new ApplicationUser()
            //{
            //    Id = "11",
            //    UserName = "Thang",
            //    PasswordHash = password,
            //    Email = "Thang@gmail.com",
            //    EmailConfirmed = true,
            //    PhoneNumber = "+84347561955",
            //    PhoneNumberConfirmed = true,
            //    LockoutEnabled = true,
            //};
            //userList.Add(new SeedUserRole() { User = user11, RoleId = "3" });

            //var user12 = new ApplicationUser()
            //{
            //    Id = "12",
            //    UserName = "Manh",
            //    PasswordHash = password,
            //    Email = "manh@gmail.com",
            //    EmailConfirmed = true,
            //    PhoneNumber = "+84347561875",
            //    PhoneNumberConfirmed = true,
            //    LockoutEnabled = true,
            //};
            //userList.Add(new SeedUserRole() { User = user12, RoleId = "3" });

            // seed user + userRole
            foreach (var seedUserRole in userList)
            {
                var roleIdTemp = seedUserRole.RoleId;
                var userTemp = seedUserRole.User;
                var userRoleTemp = new IdentityUserRole();
                userRoleTemp.RoleId = roleIdTemp;
                userRoleTemp.UserId = userTemp.Id;
                userTemp.Roles.Add(userRoleTemp);
                context.Users.AddOrUpdate(x => x.Id, userTemp);
            }

            // Category
            context.Categories.AddOrUpdate(x => x.Id,
                new Category()
                {
                    Id = 1,
                    Name = "T-Shirt",
                    Status = ECategoryStatus.Active,
                    CreatedAt = DateTime.Parse("2019-04-24")
                },

                new Category()
                {
                    Id = 2,
                    Name = "Shirt",
                    Status = ECategoryStatus.Active,
                    CreatedAt = DateTime.Parse("2019-04-24")
                },

                new Category()
                {
                    Id = 3,
                    Name = "Short",
                    Status = ECategoryStatus.Active,
                    CreatedAt = DateTime.Parse("2019-04-24")
                },

                new Category()
                {
                    Id = 4,
                    Name = "Jeans",
                    Status = ECategoryStatus.Active,
                    CreatedAt = DateTime.Parse("2019-04-24")
                }
            );

            //Product
            context.Products.AddOrUpdate(x => x.Id,
                //T-shirt 1
                new Product()
                {
                    Id = 1,
                    Name = "Áo Jean đuôi tôm",
                    Price = 20,
                    Discount = 9.9,
                    Description = "100% Supima® cotton for a high-quality feel. A basic item that goes with any look.",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-jean-duoi-tom-ca-tinh-cho-be-tu-1-8-tuoi-mau-xanh-p22178-300x300.jpg",
                    CategoryId = 1,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2020-04-24")
                },
                //T-shirt 2
                new Product()
                {
                    Id = 2,
                    Name = "Áo khoác Jean Mickey",
                    Price = 7.9,
                    Description = "Simple and versatile. Stays dry, giving it a comfortable and dry feeling against the skin.",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-khoac-jean-mickey-cho-be-gai-p2529173ff46b2-300x300.jpg",
                    CategoryId = 1,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2020-05-24")
                },
                //Shirt 3
                new Product()
                {
                    Id = 3,
                    Name = "Áo hoodie crop top",
                    Price = 29.9,
                    Discount = 19.9,
                    Description = "Made from fine cotton for a soft feel. The surface is soft for a more comfortable feel.",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-dai-cach-tan-hoa-mai-trang-dang-vintage-xinh-xan-cho-be-gai-p25456391ca9f1-300x300.jpg",
                    CategoryId = 2,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2019-10-24")
                },
                //Shirt 4
                new Product()
                {
                    Id = 4,
                    Name = "Áo Hoodie",
                    Price = 29.9,
                    Description = "Made with fine cotton to reduce wrinkles after washing.",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-hoodie-dang-croptop-gau-pooh-xinh-xan-cho-be-gai-p2551787d17b5b-300x300.jpg",
                    CategoryId = 2,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2020-05-10")
                },
                //Shirt 5
                new Product()
                {
                    Id = 10,
                    Name = "Dotilo FLOWER",
                    Price = 29.9,
                    Description = "Made with fine cotton to reduce wrinkles after washing.",
                    Thumbnails = "https://salt.tikicdn.com/cache/w1200/ts/product/93/22/26/20757b535bad32d6fecc1508e33f8521.jpg",
                    CategoryId = 2,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2020-05-10")
                },
                 //Shirt 6
                 new Product()
                 {
                     Id = 11,
                     Name = "Áo T-Shirt Trẻ em cổ tròn",
                     Price = 29.9,
                     Description = "Made with fine cotton to reduce wrinkles after washing.",
                     Thumbnails = "https://salt.tikicdn.com/cache/w1200/ts/product/65/06/98/a02adf76107294b1d26f2d17ae16f1f2.jpg",
                     CategoryId = 2,
                     Status = EProductStatus.Active,
                     CreatedAt = DateTime.Parse("2020-05-10")
                 },
                  //Shirt 7
                  new Product()
                  {
                      Id = 12,
                      Name = "Set 5 áo sơ sinh cotton",
                      Price = 100,
                      Description = "Made with fine cotton to reduce wrinkles after washing.",
                      Thumbnails = "https://salt.tikicdn.com/cache/280x280/ts/product/70/e8/1e/267c28e62c0e53fbd2cd4f77e755c02c.jpg",
                      CategoryId = 2,
                      Status = EProductStatus.Active,
                      CreatedAt = DateTime.Parse("2020-05-10")
                  },
                  //Shirt 8
                  new Product()
                  {
                      Id = 13,
                      Name = "Áo T-shirt Dành Cho Bé Dotilo CAKE",
                      Price = 100,
                      Description = "Made with fine cotton to reduce wrinkles after washing.",
                      Thumbnails = "https://salt.tikicdn.com/cache/280x280/ts/product/cb/4a/b3/f5f3fa7b507147437ed92273f4734fdf.jpg",
                      CategoryId = 2,
                      Status = EProductStatus.Active,
                      CreatedAt = DateTime.Parse("2020-05-10")
                  },
                  //Shirt 9
                  new Product()
                  {
                      Id = 14,
                      Name = "Áo T-shirt Dành Cho Bé Dotilo CHIARA",
                      Price = 10,
                      Description = "Made with fine cotton to reduce wrinkles after washing.",
                      Thumbnails = "https://salt.tikicdn.com/cache/280x280/ts/product/77/51/19/460f6b87b9301736e64686e95bcafcbd.jpg",
                      CategoryId = 2,
                      Status = EProductStatus.Active,
                      CreatedAt = DateTime.Parse("2020-05-10")
                  },
                  //Shirt 9
                  new Product()
                  {
                      Id = 15,
                      Name = "Áo T-Shirt Unisex Cho Bé Dotilo See My Soul",
                      Price = 10,
                      Description = "Made with fine cotton to reduce wrinkles after washing.",
                      Thumbnails = "https://salt.tikicdn.com/cache/280x280/ts/product/82/f0/8b/8b8b1f02abaeb3b413c38ec532427d43.jpg",
                      CategoryId = 2,
                      Status = EProductStatus.Active,
                      CreatedAt = DateTime.Parse("2020-05-10")
                  },
                //Short 5
                new Product()
                {
                    Id = 5,
                    Name = "NYLON ACTIVE SHORTS",
                    Price = 29.9,
                    Description = "Both materials and details have a sporty finish. A different take on a stylish piece.",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-dai-cach-tan-hoa-mai-trang-dang-vintage-xinh-xan-cho-be-gai-p25456391ca9f1-300x300.jpg",
                    CategoryId = 3,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2020-03-24")
                },
                //Short 6
                new Product()
                {
                    Id = 6,
                    Name = "Áo dài cách tân",
                    Price = 10,
                    Description = "These relaxed pants are soft and comfortable. Short and easy to wear.",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-dai-cach-tan-gam-nhung-tuyet-va-chan-vay-tutu-cho-be-gai-p25495297aa8e4-300x300.jpg",
                    CategoryId = 3,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2020-04-15")
                },
                //Short 7
                new Product()
                {
                    Id = 16,
                    Name = "Áo Short cho bé",
                    Price = 10,
                    Description = "These relaxed pants are soft and comfortable. Short and easy to wear.",
                    Thumbnails = "https://cf.shopee.vn/file/c9fa62d1aafd51e88ffec6ce2c48710a",
                    CategoryId = 3,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2020-04-15")
                },
                //Jeans 7
                new Product()
                {
                    Id = 7,
                    Name = "EZY ULTRA STRETCH COLOR JEANS",
                    Price = 39.9,
                    Discount = 29.9,
                    Description = "Ultra stretch fabric makes it incredibly comfortable. Refined design and texture.",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-dai-cach-tan-gam-nhung-tuyet-va-chan-vay-tutu-cho-be-gai-p25495297aa8e4-300x300.jpg",
                    CategoryId = 4,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2020-01-24")
                },
                //Jeans 8
                new Product()
                {
                    Id = 8,
                    Name = "ULTRA STRETCH SKINNY FIT JEANS",
                    Price = 49.9,
                    Description = "The slimmest skinny fit of all UNIQLO jeans. It's surprisingly flexible and still comfortable.",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-thun-phoi-non-2-mau-hoa-tiet-sua-bo-xinh-yeu-cho-be-gai-p25486fcb40e52-300x300.jpg",
                    CategoryId = 4,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2021-12-24")
                },
                //Jeans 9
                new Product
                {
                    Id = 9,
                    Name = "ULTRA STRETCH SKINNY FIT JEANS",
                    Price = 9.9,
                    Description = "The slimmest skinny fit of all UNIQLO jeans. It's surprisingly flexible and still comfortable.",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-thun-phoi-non-2-mau-hoa-tiet-sua-bo-xinh-yeu-cho-be-gai-p25486fcb40e52-300x300.jpg",
                    CategoryId = 4,
                    Status = EProductStatus.Active,
                    CreatedAt = DateTime.Parse("2021-1-24")
                }
            );
                //

            //Product Details
            context.ProductDetails.AddOrUpdate(x => x.Id,
                //T-shirt 1 
                new ProductDetail()
                {
                    Id = 1,
                    ProductId = 1,
                    Color = "Navy",
                    Size = "S",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-dai-cach-tan-hoa-mai-trang-dang-vintage-xinh-xan-cho-be-gai-p25456391ca9f1-300x300.jpg",
                    Quantity = 18,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 2,
                    ProductId = 1,
                    Color = "Navy",
                    Size = "M",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-dai-cach-tan-chim-hac-ngam-la-sen-cho-be-gai-p25455cb8fd8d-300x300.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 3,
                    ProductId = 1,
                    Color = "Navy",
                    Size = "L",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-khoac-kaki-dang-dai-phong-cach-han-quoc-cho-be-trai-va-be-gai-p253296aae1d87-300x300.jpg",
                    Quantity = 45,
                    Status = EProductStatus.Deleted,
                },
                new ProductDetail()
                {
                    Id = 4,
                    ProductId = 1,
                    Color = "Beige",
                    Size = "S",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-khoac-be-gai-freeship-ao-khoac-cho-be-tu-7-15kg-pba3ca30d-300x300.jpg",
                    Quantity = 31,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 5,
                    ProductId = 1,
                    Color = "Beige",
                    Size = "M",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-khoac-be-gai-freeship-ao-khoac-phoi-no-cho-be-tu-7-15kg-p756d9ff2-300x300.jpg",
                    Quantity = 21,
                    Status = EProductStatus.Deactive,
                },
                new ProductDetail()
                {
                    Id = 6,
                    ProductId = 1,
                    Color = "Beige",
                    Size = "L",
                    Thumbnails = "https://cdn.becungshop.vn/images/500x500/ao-khoac-be-gai-freeship-ao-khoac-cho-be-tu-7-15kg-pbc938325-500x500.jpg",
                    Quantity = 11,
                    Status = EProductStatus.Active,
                },
                //T-shirt 2
                new ProductDetail()
                {
                    Id = 7,
                    ProductId = 2,
                    Color = "Gray",
                    Size = "S",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-khoac-kaki-dang-dai-phong-cach-han-quoc-cho-be-trai-va-be-gai-p253296aae1d87-300x300.jpg",
                    Quantity = 22,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 8,
                    ProductId = 2,
                    Color = "Gray",
                    Size = "M",
                    Thumbnails = "https://cdn.becungshop.vn/images/300x300/ao-khoac-kaki-dang-dai-phong-cach-han-quoc-cho-be-trai-va-be-gai-p253296aae1d87-300x300.jpg",
                    Quantity = 32,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 9,
                    ProductId = 2,
                    Color = "Gray",
                    Size = "L",
                    Thumbnails = "https://cdn.becungshop.vn/images/500x500/ao-phao-long-am-sieu-dep-cho-be-trai-be-gai-pc97417c2-500x500.jpg",
                    Quantity = 22,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 10,
                    ProductId = 2,
                    Color = "Dark Gray",
                    Size = "S",
                    Thumbnails = "https://cdn.becungshop.vn/images/500x500/ao-phao-long-am-sieu-dep-cho-be-trai-be-gai-pcb7fd8a0-500x500.jpg",
                    Quantity = 12,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 11,
                    ProductId = 2,
                    Color = "Dark Gray",
                    Size = "M",
                    Thumbnails = "https://cdn.becungshop.vn/images/500x500/ao-phao-long-am-sieu-dep-cho-be-trai-be-gai-pcb94d400-500x500.jpg",
                    Quantity = 34,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 12,
                    ProductId = 2,
                    Color = "Dark Gray",
                    Size = "L",
                    Thumbnails = "https://cdn.becungshop.vn/images/500x500/ao-phao-long-am-sieu-dep-cho-be-trai-be-gai-pca3520f5-500x500.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Active,
                },
                //Shirt 3
                new ProductDetail()
                {
                    Id = 13,
                    ProductId = 3,
                    Color = "Yellow",
                    Size = "S",
                    Thumbnails = "https://cdn.becungshop.vn/images/500x500/ao-dai-cach-tan-hoa-mai-trang-dang-vintage-xinh-xan-cho-be-gai-p25456391ca9f1-500x500.jpg",
                    Quantity = 14,
                    Status = EProductStatus.Deactive,
                },
                new ProductDetail()
                {
                    Id = 14,
                    ProductId = 3,
                    Color = "Yellow",
                    Size = "M",
                    Thumbnails = "https://cdn.becungshop.vn/images/500x500/ao-dai-cach-tan-hoa-mai-trang-dang-vintage-xinh-xan-cho-be-gai-p25456391ca9f1-500x500.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 15,
                    ProductId = 3,
                    Color = "Yellow",
                    Size = "L",
                    Thumbnails = "https://cdn.becungshop.vn/images/500x500/ao-dai-cach-tan-hoa-mai-trang-dang-vintage-xinh-xan-cho-be-gai-p25456391ca9f1-500x500.jpg",
                    Quantity = 42,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 16,
                    ProductId = 3,
                    Color = "White",
                    Size = "S",
                    Thumbnails = "https://cdn.becungshop.vn/images/500x500/ao-hoodie-mickey-mouse-de-thuong-cho-be-p25131787ba350-500x500.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 17,
                    ProductId = 3,
                    Color = "White",
                    Size = "M",
                    Thumbnails = "https://thoitrangbaby.vn/datafiles/15007/upload/members/Bi/29.JPG",
                    Quantity = 10,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 18,
                    ProductId = 3,
                    Color = "White",
                    Size = "L",
                    Thumbnails = "https://babi.vn/images/companies/1/Up%20hinh%20san%20pham/361351/bo-cham-bi-sanh-dieu-dao-pho-cho-be-gai%20(3).jpg?1527476036584",
                    Quantity = 35,
                    Status = EProductStatus.Active,
                },
                //Shirt 4
                new ProductDetail()
                {
                    Id = 19,
                    ProductId = 4,
                    Color = "Blue",
                    Size = "S",
                    Thumbnails = "https://shopkhoinghiep.com/wp-content/uploads/2019/06/shop-ban-qua-ao-tre-em-dep.jpg",
                    Quantity = 25,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 20,
                    ProductId = 4,
                    Color = "Blue",
                    Size = "M",
                    Thumbnails = "https://nguyentuanhung.vn/wp-content/uploads/2019/07/thoi-trang-tre-em.jpg",
                    Quantity = 15,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 21,
                    ProductId = 4,
                    Color = "Blue",
                    Size = "L",
                    Thumbnails = "https://top1review.vn/wp-content/uploads/2020/01/shop-quan-ao-tre-em.jpg",
                    Quantity = 20,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 22,
                    ProductId = 4,
                    Color = "Yellow",
                    Size = "S",
                    Thumbnails = "https://media3.scdn.vn/img3/2019/9_21/0Mgx12_simg_de2fe0_500x500_maxb.jpg",
                    Quantity = 30,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 23,
                    ProductId = 4,
                    Color = "Yellow",
                    Size = "M",
                    Thumbnails = "https://miro.medium.com/max/1200/0*stWPEbHHxy_8zwNA.png",
                    Quantity = 40,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 24,
                    ProductId = 4,
                    Color = "Yellow",
                    Size = "L",
                    Thumbnails = "https://bizweb.dktcdn.net/100/060/622/files/ban-buon-quan-ao-tre-em-3.jpg?v=1479223417138",
                    Quantity = 8,
                    Status = EProductStatus.Deleted,
                },
                 //Shirt5
                 new ProductDetail()
                 {
                     Id = 54,
                     ProductId = 10,
                     Color = "Yellow",
                     Size = "L",
                     Thumbnails = "https://salt.tikicdn.com/cache/280x280/ts/product/93/22/26/20757b535bad32d6fecc1508e33f8521.jpg",
                     Quantity = 8,
                     Status = EProductStatus.Deleted,
                 },
                  new ProductDetail()
                  {
                      Id = 54,
                      ProductId = 10,
                      Color = "Gray",
                      Size = "M",
                      Thumbnails = "https://salt.tikicdn.com/cache/280x280/ts/product/93/22/26/20757b535bad32d6fecc1508e33f8521.jpg",
                      Quantity = 8,
                      Status = EProductStatus.Deleted,
                  },
                   new ProductDetail()
                   {
                       Id = 54,
                       ProductId = 10,
                       Color = "White",
                       Size = "S",
                       Thumbnails = "https://salt.tikicdn.com/cache/280x280/ts/product/93/22/26/20757b535bad32d6fecc1508e33f8521.jpg",
                       Quantity = 8,
                       Status = EProductStatus.Deleted,
                   },
                //Short 5
                new ProductDetail()
                {
                    Id = 25,
                    ProductId = 5,
                    Color = "Gray",
                    Size = "S",
                    Thumbnails = "https://cdn.becungshop.vn/images/500x500/bo-thun-the-thao-hoa-cuc-hottrend-cho-be-trai-va-be-gai-p2511645865aaf-500x500.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 26,
                    ProductId = 5,
                    Color = "Gray",
                    Size = "M",
                    Thumbnails = "https://honikids.com/wp-content/uploads/2019/01/tim-dai-ly-quan-ao-tre-em-thai-lan.jpg",
                    Quantity = 15,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 27,
                    ProductId = 5,
                    Color = "Gray",
                    Size = "L",
                    Thumbnails = "https://cdn.jadiny.vn/shop-quan-ao-tre-em-dep-416120877-d637095023336968431.png",
                    Quantity = 15,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 28,
                    ProductId = 5,
                    Color = "Green",
                    Size = "S",
                    Thumbnails = "https://cdn.sudospaces.com/website/topz.vn/home/topz/public_html/2019/12/be-xinh-shop-143320.jpg",
                    Quantity = 25,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 29,
                    ProductId = 5,
                    Color = "Green",
                    Size = "M",
                    Thumbnails = "https://sakurafashion.vn/upload/images_294/49001724_2082110805182787_3587064352412270592_n(1).jpg",
                    Quantity = 30,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 30,
                    ProductId = 5,
                    Color = "Green",
                    Size = "L",
                    Thumbnails = "https://sakurafashion.vn/upload/images_294/42382627_2135406740014564_3485148145732550656_n.jpg",
                    Quantity = 8,
                    Status = EProductStatus.Active,
                },
                //Short 6
                new ProductDetail()
                {
                    Id = 31,
                    ProductId = 6,
                    Color = "Green",
                    Size = "S",
                    Thumbnails = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR9PTx9hEKKH4y_XollTfyO0tYfqd-vtdtvpw&usqp=CAU",
                    Quantity = 2,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 32,
                    ProductId = 6,
                    Color = "Green",
                    Size = "M",
                    Thumbnails = "https://cf.shopee.vn/file/9d11be160129a500721f68ed193207a3",
                    Quantity = 30,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 33,
                    ProductId = 6,
                    Color = "Green",
                    Size = "L",
                    Thumbnails = "https://quanaongoclinh.com/upload/hinhanh/00-ban-quan-ao-tre-em-ha-noi-1.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 34,
                    ProductId = 6,
                    Color = "Red",
                    Size = "S",
                    Thumbnails = "https://toplist.vn/images/800px/maruko-baby-462653.jpg",
                    Quantity = 19,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 35,
                    ProductId = 6,
                    Color = "Red",
                    Size = "M",
                    Thumbnails = "https://www.topkinhnghiem.com/wp-content/uploads/2020/08/ao-thun-tre-em-dep-gia-re-300x300.jpg",
                    Quantity = 22,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 36,
                    ProductId = 6,
                    Color = "Red",
                    Size = "L",
                    Thumbnails = "https://miro.medium.com/max/1200/0*stWPEbHHxy_8zwNA.png",
                    Quantity = 15,
                    Status = EProductStatus.Active,
                },
                //Short 7
                new ProductDetail()
                {
                    Id = 49,
                    ProductId = 16,
                    Color = "Green",
                    Size = "M",
                    Thumbnails = "https://cf.shopee.vn/file/c9fa62d1aafd51e88ffec6ce2c48710a",
                    Quantity = 8,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 55,
                    ProductId = 16,
                    Color = "Green",
                    Size = "L",
                    Thumbnails = "https://cf.shopee.vn/file/c9fa62d1aafd51e88ffec6ce2c48710a",
                    Quantity = 8,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 56,
                    ProductId = 16,
                    Color = "Green",
                    Size = "S",
                    Thumbnails = "https://cf.shopee.vn/file/c9fa62d1aafd51e88ffec6ce2c48710a",
                    Quantity = 8,
                    Status = EProductStatus.Active,
                },
                  new ProductDetail()
                  {
                      Id = 50,
                      ProductId = 16,
                      Color = "Blu",
                      Size = "M",
                      Thumbnails = "https://cf.shopee.vn/file/c9fa62d1aafd51e88ffec6ce2c48710a",
                      Quantity = 8,
                      Status = EProductStatus.Active,
                  },
                  new ProductDetail()
                     {
                         Id = 57,
                         ProductId = 16,
                         Color = "Blu",
                         Size = "L",
                         Thumbnails = "https://cf.shopee.vn/file/c9fa62d1aafd51e88ffec6ce2c48710a",
                         Quantity = 8,
                         Status = EProductStatus.Active,
                     },
                  new ProductDetail()
                        {
                            Id = 58,
                            ProductId = 16,
                            Color = "Blu",
                            Size = "S",
                            Thumbnails = "https://cf.shopee.vn/file/c9fa62d1aafd51e88ffec6ce2c48710a",
                            Quantity = 8,
                            Status = EProductStatus.Active,
                        },
                  new ProductDetail()
                  {
                      Id = 51,
                      ProductId = 16,
                      Color = "Gray",
                      Size = "S",
                      Thumbnails = "https://cf.shopee.vn/file/661456c2045cc25f6d4a235ec1730d88",
                      Quantity = 8,
                      Status = EProductStatus.Active,
                  },
                   new ProductDetail()
                   {
                       Id = 52,
                       ProductId = 16,
                       Color = "Gray",
                       Size = "L",
                       Thumbnails = "https://cf.shopee.vn/file/661456c2045cc25f6d4a235ec1730d88",
                       Quantity = 8,
                       Status = EProductStatus.Active,
                   },
                    new ProductDetail()
                    {
                        Id = 53,
                        ProductId = 16,
                        Color = "Gray",
                        Size = "S",
                        Thumbnails = "https://cf.shopee.vn/file/661456c2045cc25f6d4a235ec1730d88",
                        Quantity = 8,
                        Status = EProductStatus.Active,
                    },

                //Jeans 7
                new ProductDetail()
                {
                    Id = 37,
                    ProductId = 7,
                    Color = "Blue",
                    Size = "S",
                    Thumbnails = "https://www.topkinhnghiem.com/wp-content/uploads/2020/08/thoi-trang-tre-em-1.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 38,
                    ProductId = 7,
                    Color = "Blue",
                    Size = "M",
                    Thumbnails = "https://my-test-11.slatic.net/p/b4e74248d90b76d7d0879c417f236f86.jpg_720x720q80.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 39,
                    ProductId = 7,
                    Color = "Blue",
                    Size = "L",
                    Thumbnails = "https://ninistore.vn/wp-content/uploads/2019/10/th5-e1571621001632.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Deleted,
                },
                new ProductDetail()
                {
                    Id = 40,
                    ProductId = 7,
                    Color = "Olive",
                    Size = "S",
                    Thumbnails = "https://i.imgur.com/nsOo9K9.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Deactive,
                },
                new ProductDetail()
                {
                    Id = 41,
                    ProductId = 7,
                    Color = "Olive",
                    Size = "M",
                    Thumbnails = "https://img.alicdn.com/i2/675527807/O1CN01ZVkOVO27XecN0kvt4_!!675527807.jpg",
                    Quantity = 18,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 42,
                    ProductId = 7,
                    Color = "Olive",
                    Size = "L",
                    Thumbnails = "https://my-test-11.slatic.net/p/8ad7f146e66f1adc925e1df0a8cb8332.jpg_720x720q80.jpg",
                    Status = EProductStatus.Active,
                },
                //Jeans 8
                new ProductDetail()
                {
                    Id = 43,
                    ProductId = 8,
                    Color = "Blue",
                    Size = "S",
                    Thumbnails = "https://bucket.nhanh.vn/store/11055/ps/20170923/bo_quan_ao_tre_em_bo_quan_ao_be_gai_GLSET004__1__800x800.jpg",
                    Quantity = 0,
                    Status = EProductStatus.Deactive,
                },
                new ProductDetail()
                {
                    Id = 44,
                    ProductId = 8,
                    Color = "Blue",
                    Size = "M",
                    Thumbnails = "https://vn-test-11.slatic.net/p/81a256369ed93a5c324bc8f312a58568.jpg_720x720q80.jpg",
                    Quantity = 5,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 45,
                    ProductId = 8,
                    Color = "Blue",
                    Size = "L",
                    Thumbnails = "https://gd1.alicdn.com/imgextra/i1/380855681/TB2OdgHgrsrBKNjSZFpXXcXhFXa_!!380855681.jpg",
                    Quantity = 20,
                    Status = EProductStatus.Active,
                },
                new ProductDetail()
                {
                    Id = 46,
                    ProductId = 8,
                    Color = "Dark Gray",
                    Size = "S",
                    Thumbnails = "https://product.hstatic.net/1000069666/product/upload_d427215b35ef4ecabbd9cf1a8716043c_large.jpg",
                    Quantity = 0,
                    Status = EProductStatus.Deleted,
                },
                new ProductDetail()
                {
                    Id = 47,
                    ProductId = 8,
                    Color = "Dark Gray",
                    Size = "M",
                    Thumbnails = "https://pedrovietnam.com/wp-content/uploads/2020/08/1596492229_616_Danh-gia-Top-10-shop-ban-quan-ao-tre-em.jpg",
                    Quantity = 8,
                    Status = EProductStatus.Deactive,
                },
                new ProductDetail()
                {
                    Id = 48,
                    ProductId = 8,
                    Color = "Dark Gray",
                    Size = "L",
                    Thumbnails = "https://pedrovietnam.com/wp-content/uploads/2020/08/1596492229_616_Danh-gia-Top-10-shop-ban-quan-ao-tre-em.jpg",
                    Quantity = 10,
                    Status = EProductStatus.Active,
                }
            );

            //Discount
            context.Discounts.AddOrUpdate(x => x.Id,
                new Discount()
                {
                    Id = 1,
                    Code = "ALISALE001",
                    Value = 0.2,
                    ExprirationDate = DateTime.Parse("2021-10-24"),
                    MinTotal = 100,
                    UseTime = 1,
                    Status = EDiscountStatus.Active,
                    CreatedAt = DateTime.Parse("2020-04-24")
                },
                new Discount()
                {
                    Id = 2,
                    Code = "ALISALE002",
                    Value = 0.1,
                    ExprirationDate = DateTime.Parse("2021-10-24"),
                    MinTotal = 50,
                    UseTime = 10,
                    Status = EDiscountStatus.Active,
                    CreatedAt = DateTime.Parse("2020-04-24")
                }
            );

            //Order01
            var order01 = new Order()
            {
                Id = 1,
                CustomerName = "Matt Addie",
                CustomerPhone = "(423) 610-3161",
                CustomerEmail = "letter.japan@gmail.com",
                CustomerAddress = "777 Brockton Avenue, Abington MA 2351",
                PaymentMethod = EPaymentMethod.Cash,
                DiscountId = 1,
                TotalPrice = 25.7,
                Status = EOrderStatus.Delivered,
                CreatedAt = DateTime.Parse("2020-01-24")
            };

            var orderDetail01 = new OrderDetail()
            {
                Id = 1,
                OrderId = 1,
                ProductDetailId = 1,
                UnitPrice = 14.9,
                Discount = 9.9,
                Quantity = 1,
            };

            var orderDetail02 = new OrderDetail()
            {
                Id = 2,
                OrderId = 1,
                ProductDetailId = 7,
                UnitPrice = 7.9,
                //Discount = 9.9,
                Quantity = 2,
            };

            order01.AddOrderDetail(orderDetail01);
            order01.AddOrderDetail(orderDetail02);
            context.Orders.AddOrUpdate(order01);

            //Order02
            var order02 = new Order()
            {
                Id = 2,
                CustomerName = "Allen Quimby",
                CustomerPhone = "(739) 261-3110",
                CustomerEmail = "allen@gmail.com",
                CustomerAddress = "30 Memorial Drive, Avon MA 2322",
                PaymentMethod = EPaymentMethod.Paypal,
                DiscountId = 2,
                TotalPrice = 209.3,
                Status = EOrderStatus.Delivered,
                CreatedAt = DateTime.Parse("2020-01-15")
            };

            var orderDetail03 = new OrderDetail()
            {
                Id = 3,
                OrderId = 2,
                ProductDetailId = 30,
                UnitPrice = 29.9,
                //Discount = 9.9,
                Quantity = 3,
            };

            var orderDetail04 = new OrderDetail()
            {
                Id = 4,
                OrderId = 2,
                ProductDetailId = 25,
                UnitPrice = 29.9,
                //Discount = 9.9,
                Quantity = 4,
            };

            order02.AddOrderDetail(orderDetail03);
            order02.AddOrderDetail(orderDetail04);
            context.Orders.AddOrUpdate(order02);

            //Order03
            var order03 = new Order()
            {
                Id = 3,
                CustomerName = "Weldon Ries",
                CustomerPhone = "(314) 345-9898",
                CustomerEmail = "weldon@gmail.com",
                CustomerAddress = "250 Hartford Avenue, Bellingham MA 2019",
                PaymentMethod = EPaymentMethod.Cash,
                TotalPrice = 139.6,
                Status = EOrderStatus.Delivered,
                CreatedAt = DateTime.Parse("2020-02-24")
            };

            var orderDetail05 = new OrderDetail()
            {
                Id = 5,
                OrderId = 3,
                ProductDetailId = 42,
                UnitPrice = 39.9,
                Discount = 29.9,
                Quantity = 3,
            };

            var orderDetail06 = new OrderDetail()
            {
                Id = 6,
                OrderId = 3,
                ProductDetailId = 45,
                UnitPrice = 49.9,
                //Discount = 9.9,
                Quantity = 1,
            };

            order03.AddOrderDetail(orderDetail05);
            order03.AddOrderDetail(orderDetail06);
            context.Orders.AddOrUpdate(order03);

            //Order04
            var order04 = new Order()
            {
                Id = 4,
                CustomerName = "Cary Mcmurtrie",
                CustomerPhone = "(797) 740-3175",
                CustomerEmail = "cary@gmail.com",
                CustomerAddress = "66-4 Parkhurst Rd, Chelmsford MA 1824",
                PaymentMethod = EPaymentMethod.Cash,
                TotalPrice = 45.7,
                Status = EOrderStatus.Delivered,
                CreatedAt = DateTime.Parse("2020-02-02")
            };

            var orderDetail07 = new OrderDetail()
            {
                Id = 7,
                OrderId = 4,
                ProductDetailId = 12,
                UnitPrice = 7.9,
                //Discount = 29.9,
                Quantity = 2,
            };

            var orderDetail08 = new OrderDetail()
            {
                Id = 8,
                OrderId = 4,
                ProductDetailId = 20,
                UnitPrice = 29.9,
                //Discount = 9.9,
                Quantity = 1,
            };

            order04.AddOrderDetail(orderDetail07);
            order04.AddOrderDetail(orderDetail08);
            context.Orders.AddOrUpdate(order04);

            //Order05
            var order05 = new Order()
            {
                Id = 5,
                CustomerName = "Wade Coy",
                CustomerPhone = "(696) 658-5886",
                CustomerEmail = "wade@gmail.com",
                CustomerAddress = "591 Memorial Dr, Chicopee MA 1020",
                PaymentMethod = EPaymentMethod.Cash,
                DiscountId = 1,
                TotalPrice = 79.6,
                Status = EOrderStatus.Delivered,
                CreatedAt = DateTime.Parse("2020-02-19")
            };

            var orderDetail09 = new OrderDetail()
            {
                Id = 9,
                OrderId = 5,
                ProductDetailId = 35,
                UnitPrice = 19.9,
                //Discount = 29.9,
                Quantity = 2,
            };

            var orderDetail10 = new OrderDetail()
            {
                Id = 10,
                OrderId = 5,
                ProductDetailId = 15,
                UnitPrice = 29.9,
                Discount = 19.9,
                Quantity = 2,
            };

            order05.AddOrderDetail(orderDetail09);
            order05.AddOrderDetail(orderDetail10);
            context.Orders.AddOrUpdate(order05);

            //Order06
            var order06 = new Order()
            {
                Id = 6,
                CustomerName = "Winfred Kitamura",
                CustomerPhone = "(544) 926-3679",
                CustomerEmail = "winfred@gmail.com",
                CustomerAddress = "55 Brooksby Village Way, Danvers MA 1923",
                PaymentMethod = EPaymentMethod.Cash,
                TotalPrice = 169.5,
                Status = EOrderStatus.Delivered,
                CreatedAt = DateTime.Parse("2020-03-24")
            };

            var orderDetail11 = new OrderDetail()
            {
                Id = 11,
                OrderId = 6,
                ProductDetailId = 24,
                UnitPrice = 29.9,
                //Discount = 29.9,
                Quantity = 4,
            };

            var orderDetail12 = new OrderDetail()
            {
                Id = 12,
                OrderId = 6,
                ProductDetailId = 43,
                UnitPrice = 49.9,
                //Discount = 19.9,
                Quantity = 1,
            };

            order06.AddOrderDetail(orderDetail11);
            order06.AddOrderDetail(orderDetail12);
            context.Orders.AddOrUpdate(order06);

            //Order07
            var order07 = new Order()
            {
                Id = 7,
                CustomerName = "Winfred Kitamura",
                CustomerPhone = "(544) 926-3679",
                CustomerEmail = "winfred@gmail.com",
                CustomerAddress = "55 Brooksby Village Way, Danvers MA 1923",
                PaymentMethod = EPaymentMethod.Cash,
                DiscountId = 2,
                TotalPrice = 169.5,
                Status = EOrderStatus.Delivered,
                CreatedAt = DateTime.Parse("2020-03-16")
            };

            var orderDetail13 = new OrderDetail()
            {
                Id = 13,
                OrderId = 7,
                ProductDetailId = 24,
                UnitPrice = 29.9,
                //Discount = 29.9,
                Quantity = 4,
            };

            var orderDetail14 = new OrderDetail()
            {
                Id = 14,
                OrderId = 7,
                ProductDetailId = 43,
                UnitPrice = 49.9,
                //Discount = 19.9,
                Quantity = 1,
            };

            order07.AddOrderDetail(orderDetail13);
            order07.AddOrderDetail(orderDetail14);
            context.Orders.AddOrUpdate(order07);

            //Order08
            var order08 = new Order()
            {
                Id = 8,
                CustomerName = "Winfred Kitamura",
                CustomerPhone = "(544) 926-3679",
                CustomerEmail = "winfred@gmail.com",
                CustomerAddress = "55 Brooksby Village Way, Danvers MA 1923",
                PaymentMethod = EPaymentMethod.Cash,
                TotalPrice = 169.5,
                Status = EOrderStatus.Delivered,
                CreatedAt = DateTime.Parse("2020-04-24")
            };

            var orderDetail15 = new OrderDetail()
            {
                Id = 15,
                OrderId = 8,
                ProductDetailId = 24,
                UnitPrice = 29.9,
                //Discount = 29.9,
                Quantity = 4,
            };

            var orderDetail16 = new OrderDetail()
            {
                Id = 16,
                OrderId = 8,
                ProductDetailId = 43,
                UnitPrice = 49.9,
                //Discount = 19.9,
                Quantity = 1,
            };

            order08.AddOrderDetail(orderDetail15);
            order08.AddOrderDetail(orderDetail16);
            context.Orders.AddOrUpdate(order08);

            //Order09
            var order09 = new Order()
            {
                Id = 9,
                CustomerName = "Winfred Kitamura",
                CustomerPhone = "(544) 926-3679",
                CustomerEmail = "winfred@gmail.com",
                CustomerAddress = "55 Brooksby Village Way, Danvers MA 1923",
                PaymentMethod = EPaymentMethod.Cash,
                TotalPrice = 169.5,
                Status = EOrderStatus.Delivered,
                CreatedAt = DateTime.Parse("2020-04-04")
            };

            var orderDetail17 = new OrderDetail()
            {
                Id = 17,
                OrderId = 9,
                ProductDetailId = 24,
                UnitPrice = 29.9,
                //Discount = 29.9,
                Quantity = 4,
            };

            var orderDetail18 = new OrderDetail()
            {
                Id = 18,
                OrderId = 9,
                ProductDetailId = 43,
                UnitPrice = 49.9,
                //Discount = 19.9,
                Quantity = 1,
            };

            order08.AddOrderDetail(orderDetail15);
            order08.AddOrderDetail(orderDetail16);
            context.Orders.AddOrUpdate(order08);

            //Order10
            var order10 = new Order()
            {
                Id = 10,
                CustomerName = "Winfred Kitamura",
                CustomerPhone = "(544) 926-3679",
                CustomerEmail = "winfred@gmail.com",
                CustomerAddress = "55 Brooksby Village Way, Danvers MA 1923",
                PaymentMethod = EPaymentMethod.Cash,
                TotalPrice = 169.5,
                Status = EOrderStatus.Delivered,
                CreatedAt = DateTime.Parse("2020-05-19")
            };

            var orderDetail19 = new OrderDetail()
            {
                Id = 19,
                OrderId = 10,
                ProductDetailId = 24,
                UnitPrice = 29.9,
                //Discount = 29.9,
                Quantity = 4,
            };

            var orderDetail20 = new OrderDetail()
            {
                Id = 20,
                OrderId = 10,
                ProductDetailId = 43,
                UnitPrice = 49.9,
                //Discount = 19.9,
                Quantity = 1,
            };

            order10.AddOrderDetail(orderDetail19);
            order10.AddOrderDetail(orderDetail20);
            context.Orders.AddOrUpdate(order10);
        }
    }
}
