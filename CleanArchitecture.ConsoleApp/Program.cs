// See https://aka.ms/new-console-template for more information
using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
StreamerDBContext context = new ();


await QueryMethods();

async Task  QueryMethods()
{
    var streamer = context.Streamers!;

    var FirstAsync = await streamer.Where(x => x.Name!.Contains("a")).FirstAsync();

    var FirstOrDefaultAsync  = await streamer!.Where(x => x.Name!.Contains("a")).FirstOrDefaultAsync();


    var FirstOrDefaultAsync_v2 = await streamer!.FirstOrDefaultAsync(x => x.Name!.Contains("a"));

    var singleAsync = await streamer.Where(x => x.Id == 1).SingleAsync();





}


await QueryFilter();

async Task QueryFilter() {

    var streamer = await context!.Streamers!.Where(x => EF.Functions.Like(x.Name!,"%Netflix%")).ToListAsync();

    foreach (var item in streamer)
    {
        Console.WriteLine(item.Id);
    }
    

}



//Streamer streamer = new Streamer() { Name = "Amazon Prime", Url = "https://www.amazonprime.com" };

//await context!.Streamers!.AddAsync(streamer);

// await context.SaveChangesAsync();

//var movies = new List<Video>
//{
//    new Video
//    {
//         Name = "Start Wras",
//         StreamerId = streamer.Id

//    },
//    new Video
//    {
//        Name = "La Señal",
//        StreamerId = streamer.Id

//    },
//     new Video
//    {
//        Name = "KING KONG",
//        StreamerId = streamer.Id

//    },
//      new Video
//    {
//        Name = "PREDATOR 2",
//        StreamerId = streamer.Id

//    }
//};

//await context.AddRangeAsync(movies);
//await context!.SaveChangesAsync();