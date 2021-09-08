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

using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Grpc.Core;
using NetExchange;

namespace NetService
{
    public class ExchangeService : ExProto.ExProtoBase
    {
        private readonly ILogger _logger;
        private static IServerStreamWriter<ExMessage> responseStream = null;

        public ExchangeService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExchangeService>();
            _logger.LogInformation($"Exchange Service Protocol Starts");
        }

        public override async Task ExLink(IAsyncStreamReader<ExMessage> requestStream, IServerStreamWriter<ExMessage> responseStream, ServerCallContext context)
        {
            ExchangeService.responseStream = responseStream;

            while (await requestStream.MoveNext())
            {
                ExMessage request = requestStream.Current;
                TxLink(ref request);
            }

            ExchangeService.responseStream = null;
        }

        internal static void TxLink(ref ExMessage request)
        {
            Program.exlink.RequestStream.WriteAsync(request);
        }

        internal static void RxLink(ref ExMessage response)
        {
            IServerStreamWriter<ExMessage> responseStream = ExchangeService.responseStream;

            if (responseStream != null)
            {
                responseStream.WriteAsync(response);
            }
        }
    }
}
