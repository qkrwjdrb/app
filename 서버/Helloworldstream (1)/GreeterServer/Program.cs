using System;
using System.Threading.Tasks;
using System.Threading;
using Grpc.Core;
using NetExchange;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;

namespace NetworkService
{
    class ExProtoImpl : ExProto.ExProtoBase
    {
        // Server side handler of the SayHello RPC
#if true
        public override async Task SayHello( IAsyncStreamReader<HelloRequest> requestStream, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)            
        {
            while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Request : {0}, Timestamp : {1}", requestStream.Current.Name,
                    requestStream.Current.Timestamp);
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = $"Hello {requestStream.Current.Name}", Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
                });

                Console.WriteLine("Response : {0}, Timestamp : {1}", requestStream.Current.Name,
                    requestStream.Current.Timestamp);
            }
        }
#endif

        public override async Task MessageRtu(IAsyncStreamReader<RtuMessage> requestStream, IServerStreamWriter<RtuMessage> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
            {
                RtuMessage request = requestStream.Current; // From Client
                await Program.rtuLink.RequestStream.WriteAsync(request);     // Network(게이트웨이) Server로 Request 전달

                Console.WriteLine("Request: ch-{0}, sq-{1}, sid-{2} did-{3}", 
                    requestStream.Current.Channel, requestStream.Current.SequenceNumber, requestStream.Current.GwId, requestStream.Current.DeviceId);

                await Program.rtuLink.ResponseStream.MoveNext();     // 응답이 있는지 확인
                Console.WriteLine(String.Format("1 :Response : Channel {0}, SequenceNumber : {1}",
                     Program.rtuLink.ResponseStream.Current.Channel,
                     Program.rtuLink.ResponseStream.Current.SequenceNumber));
                await Task.Delay(1000);


                await responseStream.WriteAsync(Program.rtuLink.ResponseStream.Current    // 서버로 부터 받은 응답을 클라이언트로 전달
                /*    
                    new RtuMessage
                {
                    //  Message = $"Hello {requestStream.Current.Channel}",
                    //  Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
                    Channel = Program.rtuLink.ResponseStream.Current.Channel,
                    SequenceNumber = (uint)Program.rtuLink.ResponseStream.Current.SequenceNumber,
                    GwId = Program.rtuLink.ResponseStream.Current.GwId,
                    DeviceId = Program.rtuLink.ResponseStream.Current.DeviceId,
                    DataUnit = Program.rtuLink.ResponseStream.Current.DataUnit
                }*/
                );

                Console.WriteLine("2 : Response -> Channel: {0}, SequenceNumber: {1}", requestStream.Current.Channel, requestStream.Current.SequenceNumber);
            }
        }
    }

    public class Program
    {
        const int serverPort = 5044;
        //const int clientPort = 5042;

        private static Channel channel = new Channel("127.0.0.1:5042", ChannelCredentials.Insecure);
        private static ExProto.ExProtoClient client = new ExProto.ExProtoClient(channel);
        private static CancellationToken cancellationToken = new CancellationToken(false);
        public static AsyncDuplexStreamingCall<RtuMessage, RtuMessage> rtuLink = client.MessageRtu(new CallOptions(null, null, cancellationToken));

        public static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { ExProto.BindService(new ExProtoImpl()) },
                Ports = { new ServerPort("127.0.0.1", serverPort, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("ExProto server listening on port " + serverPort);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
