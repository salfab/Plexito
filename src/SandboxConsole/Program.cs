using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandboxConsole
{
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    using JimBobBennett.JimLib.Network;
    using JimBobBennett.JimLib.Xamarin.Net45.Images;
    using JimBobBennett.JimLib.Xamarin.Network;
    using JimBobBennett.JimLib.Xamarin.Timers;
    using JimBobBennett.RestAndRelaxForPlex.Caches;
    using JimBobBennett.RestAndRelaxForPlex.Connection;
    using JimBobBennett.RestAndRelaxForPlex.PlexObjects;

    using Jint;

    using SandboxConsole.Model;
    using SandboxConsole.Services;

    class Program
    {
        static void Main(string[] args)
        {
            var api = new PlexBinding();
            var device = api.GetDevices()["Hubert"];
            var status = api.PlayBack.GetStatus(device, api.GetDevices().Values.Where(d => d.Provides.Contains("server")));
            api.PlayBack.Pause(device);
            api.PlayBack.Resume(device);
            api.PlayBack.SkipPrevious(device);
            api.PlayBack.SkipNext(device);

            //TryJint();
            //var restConnection = new RestConnection();
            //var tvdbConnection = new TvdbConnection(restConnection);
            //var tmdbConnection = new TmdbConnection(restConnection, TestConstants.TMDbApiKey);
            //var imageHelper = new ImageHelper(restConnection);
            //var connectionManager = new ConnectionManager(new Timer(), new LocalServerDiscovery(),
            //    restConnection, new MyPlexConnection(restConnection),
            //    new TvdbCache(tvdbConnection), new TmdbCache(tmdbConnection), new NowPlaying(imageHelper));
            

            //Task.Factory.StartNew(async () =>
            //{
            //    ((INotifyCollectionChanged)connectionManager.NowPlaying.VideosNowPlaying).CollectionChanged += async (s, e) =>
            //    {
            //        foreach (var video in connectionManager.NowPlaying.VideosNowPlaying.ToList())
            //        {
            //            Console.WriteLine("Loading external data for: " + video.Title);
            //            await connectionManager.PopulateFromExternalSourcesAsync(video, false);

            //            WriteVideo(video);

            //            var role = video.Roles.FirstOrDefault();
            //            if (role != null)
            //            {
            //                await connectionManager.PopulateFromExternalSourcesAsync(role, false);
            //                Console.WriteLine(role.Tag + " starred in " + role.CastCredits.Count);
            //                if (role.CastCredits.Any())
            //                    Console.WriteLine("  " + role.CastCredits[0].Title + " (" + role.CastCredits[0].Role + ") " + role.CastCredits[0].ReleaseDate.Year);
            //            }
            //        }
            //    };

            //    Console.WriteLine("Connecting to MyPlex...");
            //    await connectionManager.ConnectToMyPlexAsync(TestConstants.MyPlexUserName, TestConstants.MyPlexPassword);

            //    Console.WriteLine("Connecting...");
            //    await connectionManager.ConnectAsync();
                

            //    Console.WriteLine("Waiting for videos...");
            //});

            //Console.WriteLine("Press any key to exit");

            Console.ReadKey();
        }

//        private static void TryJint()
//        {
//            var scripts = new Engine()
//                .Execute(@"function add(a, b) { return a + b; } 
//                           function sub(a, b) { return a - b; }");
            
//            var add = scripts.GetValue("add").Invoke(1, 2);// --> 3
//            var sub = scripts.GetValue("sub").Invoke(5, 3); // -> 2
//        }

//        private static void WriteVideo(Video video)
//        {
//            Console.WriteLine();
//            Console.WriteLine();
//            Console.WriteLine(video.Title);

//            if (video.VideoType == VideoType.Episode)
//            {
//                Console.WriteLine("Show name: " + video.Show);
//                Console.WriteLine("Season: " + video.SeasonNumber + ", Episode: " + video.EpisodeNumber);
//            }
//            else
//                Console.WriteLine("Type: " + video.Type);

//            Console.WriteLine("Playing on " + video.Player.Title);
//            Console.WriteLine("Thumb: " + video.VideoImageSource);

//            Console.WriteLine();
//            Console.WriteLine("User:" + video.UserName);
//            Console.WriteLine("User Thumb:" + video.UserThumb);
//            Console.WriteLine();
//            Console.WriteLine("External ids:");
//            Console.WriteLine(video.ExternalIds);
//            Console.WriteLine("Episode External ids:");
//            Console.WriteLine(video.EpisodeExternalIds);
//            Console.WriteLine(video.Player.State);

//            Console.WriteLine();
//            Console.WriteLine("Links:");
//            Console.WriteLine(video.Uri);
//            Console.WriteLine(video.SchemeUri);
//            Console.WriteLine();

//            Console.WriteLine();
//            if (video.Player.State == PlayerState.Playing)
//                Console.WriteLine("Position: " + video.Progress);

//            Console.WriteLine();
//            Console.WriteLine("Cast:");

//            foreach (var role in video.Roles)
//            {
//                Console.WriteLine(role.RoleName + ": " + role.Tag);
//                Console.WriteLine("   Image: " + role.Thumb);
//            }

//            //Console.WriteLine("Directors");
//            //foreach (var director in video.Directors)
//            //    Console.WriteLine(director.tag);

//            Console.WriteLine();
//        }
    }
}