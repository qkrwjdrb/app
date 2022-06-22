// Copyright 2015 gRPC authors.
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

using System;
using Grpc.Core;
using NetExchange;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;

namespace NetworkClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1:5044", ChannelCredentials.Insecure);
            var client = new ExProto.ExProtoClient(channel);
           // string user = "you";

            CancellationToken cancellationToken = new CancellationToken(false);
#if false
            var response = client.SayHello(new CallOptions(null, null, cancellationToken));

            for (int i = 0; i < 100; i++)
            {
                var timestamp = Timestamp.FromDateTime(DateTime.UtcNow);
                await response.RequestStream.WriteAsync(new HelloRequest
                {
                    Name = "½Å¿í¼±",
                    Timestamp = timestamp
                });
                Console.WriteLine(String.Format("Request : {0}", timestamp.ToString()));
                await response.ResponseStream.MoveNext();
                Console.WriteLine(String.Format("Response : {0}, Timestamp : {1}",
                    response.ResponseStream.Current.Message,
                    response.ResponseStream.Current.Timestamp));
                await Task.Delay(1000);
            }
#else
            var response = client.MessageRtu(new CallOptions(null, null, cancellationToken));

            for (int i = 0; i < 100; i++)
            {               
                await response.RequestStream.WriteAsync(new RtuMessage
                {
                    Channel = 0,
                    SequenceNumber = (uint)i,
                    GwId = 1,
                    DeviceId = 12,
                    DataUnit = ByteString.CopyFrom(new byte[] { 0x01, 0x03, 0x00, 0x01, 0x00, 0x08, 0x15, 0xCC })
                    
                });
               
                Console.WriteLine(String.Format("Request : DeviceId {0}", 123));
                await response.ResponseStream.MoveNext();
                Console.WriteLine(String.Format("Response : Channel {0}, SequenceNumber : {1}", 
                    response.ResponseStream.Current.Channel,
                    response.ResponseStream.Current.SequenceNumber));
                await Task.Delay(1000);
            }
#endif
        }
    }
}
