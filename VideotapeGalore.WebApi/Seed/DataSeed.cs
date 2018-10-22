using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.WebApi.Seed.Entities;

namespace VideotapeGalore.WebApi.Seed
{
    public class DataSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetService<IConfiguration>();
            var tapeFile = config.GetSection("SeedFiles").GetValue<string>("Tape");
            var friendFile = config.GetSection("SeedFiles").GetValue<string>("Friend");
            
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>()))
            {
                if (context.Friends.Any())
                {
                    // Db already seeded
                    return;
                }

                // Read tapes from files
                var tapes = JsonConvert.DeserializeObject<IEnumerable<TapeSeedDto>>(
                    File.ReadAllText(tapeFile), JsonSerializerSettings);
                // Add tapes to the db
                foreach (var tape in tapes)
                {
                    context.Add(new Tape
                    {
                        Id = tape.Id,
                        Title = tape.Title,
                        Type = Enum.Parse<Tape.TapeType>(tape.Type),
                        ReleaseDate = tape.ReleaseDate,
                        Eidr = tape.Eidr,
                        DirectorFirstName =  tape.DirectorFirstName,
                        DirectorLastName = tape.DirectorLastName,
                    });
                }

                // Read friends from file
                var friends = JsonConvert.DeserializeObject<IEnumerable<FriendSeedDto>>(
                    File.ReadAllText(friendFile), JsonSerializerSettings);
                // Add friends and their borrow history to the db
                foreach (var friend in friends)
                {
                    context.Add(new Friend
                    {
                        Id = friend.Id,
                        FirstName = friend.FirstName,
                        LastName = friend.LastName,
                        Email = friend.Email,
                        Phone = friend.Phone,
                        Address = friend.Address,
                    });
                    
                    if (friend.Tapes == null)
                        continue;
                    foreach (var bi in friend.Tapes)
                    {
                        context.Add(new BorrowInfo
                        {
                            BorrowDate = bi.BorrowDate,
                            ReturnDate = bi.ReturnDate,
                            FriendId = friend.Id,
                            TapeId = bi.Id,
                        });
                    }
                }
                // Commit db changes
                context.SaveChanges();
            }
        }

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
    }
}