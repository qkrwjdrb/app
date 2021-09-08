#region Copyright notice and license

// Copyright 2019 The gRPC Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using NetExchange;

namespace NetService
{
    public class Program
    {
        private static GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5052");
        internal static ExProto.ExProtoClient exchange = new ExProto.ExProtoClient(channel);
        internal static AsyncDuplexStreamingCall<ExMessage, ExMessage> exlink = exchange.ExLink();

        public static void Main(string[] args)
        {
            _ = Task.Run(async () =>
            {
                while (await exlink.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
                {
                    ExMessage response = exlink.ResponseStream.Current;
                    UInt32 route = response.Route;

                    switch ((UInt16)(route >> 16))
                    {
                        case ((UInt16)'M' << 8) | 'B':
                        case ((UInt16)'E' << 8) | 'M':
                            ExchangeService.RxLink(ref response);
                            break;
                        default:
                            byte[] cmd = new byte[2] { (byte)(route >> 24), (byte)(route >> 16) };
                            Console.WriteLine($"Unknown cmd = {System.Text.Encoding.UTF8.GetString(cmd)}");
                            Console.WriteLine();
                            break;
                    }
                }
            });

            Console.WriteLine("Network Service Host Starts");
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenLocalhost(5054, o => o.Protocols = HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
