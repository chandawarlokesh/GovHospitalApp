//using System;
//using GovHospitalApp.Core.Infrastructure.Persistance;
//using GovHospitalApp.Core.Infrastructure.Persistance.Models;
//using Microsoft.EntityFrameworkCore;

//namespace GovHospitalApp.Tests.Common
//{
//    public class DbContextFactory
//    {
//        public static AppDbContext Create()
//        {
//            var options = new DbContextOptionsBuilder<AppDbContext>()
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;

//            var context = new AppDbContext(options);

//            context.Database.EnsureCreated();

//            context.Patients.AddRange(new[] {
//                new SqlPatient {
//                    Id = new Guid("2dd48967-b233-44b9-a29f-8a1dedc72440"),
//                    Name = "Lokesh Chandawar",
//                    DateOfBirth = DateTime.Now,
//                    Gender = 0,
//                    HospitalId = new Guid("6719146c-b3b5-434f-a9f6-34cdb6112928"),
//                    MobileNumber = "9021433312",
//                    Address = new SqlAddress() {
//                        Street = "Vanaz corner, Kothrud",
//                        City = "Pune",
//                        State = "Maharashtra",
//                        ZipCode = "410038"
//                    }
//                },
//                new SqlPatient {
//                    Id = new Guid("afaae873-6d2f-4290-be2c-9f86481e6d96"),
//                    Name = "Shivaji",
//                    DateOfBirth = DateTime.Now,
//                    Gender = 0,
//                    HospitalId = null,
//                    MobileNumber = "8888008888",
//                    Address = new SqlAddress() {
//                        Street = "Bhosari",
//                        City = "Pune",
//                        State = "Maharashtra",
//                        ZipCode = "410050"
//                    }
//                },
//            });
//            context.Hospitals.AddRange(new[] {
//                new SqlHospital {
//                    Id = new Guid("6719146c-b3b5-434f-a9f6-34cdb6112928"),
//                    Name = "Nobel Hospital",
//                    MobileNumber = "9090901800",
//                    Address = new SqlAddress() {
//                        Street = "Vanaz corner, Kothrud",
//                        City = "Pune",
//                        State = "Maharashtra",
//                        ZipCode = "410038"
//                    }
//                },
//                new SqlHospital {
//                    Id = new Guid("0944b6da-4c0c-4ae9-9310-4ff0d11b735f"),
//                    Name = "Apollo Hospital",
//                    MobileNumber = "9090901899",
//                    Address = new SqlAddress() {
//                        Street = "Bhosari",
//                        City = "Pune",
//                        State = "Maharashtra",
//                        ZipCode = "410040"
//                    }
//                },
//            });

//            context.SaveChanges();

//            return context;
//        }

//        public static void Destroy(AppDbContext context)
//        {
//            context.Database.EnsureDeleted();

//            context.Dispose();
//        }
//    }
//}
