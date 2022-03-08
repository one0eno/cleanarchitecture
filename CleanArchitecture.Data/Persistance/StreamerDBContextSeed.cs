using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistance
{
    //alimentamos de datos la base de datos
    public class StreamerDBContextSeed
    {

        
        public static async Task SeedAsync(StreamerDBContext context, ILogger<StreamerDBContextSeed> logger)
        {
            if (!context.Streamers!.Any())
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos insertando nuevos records", typeof(StreamerDBContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {

            return new List<Streamer>() {

                new Streamer(){
                     CreatedBy = "jorgearranz", Name="Jorge HBP", Url="http://www.hbp.com"
                },
                 new Streamer(){
                     CreatedBy = "jorgearranz", Name="Amazon VIP", Url="http://www.amazonvip.com"
                }

            };
            
        }
    }
}
