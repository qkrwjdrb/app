// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: helloworld.proto
// </auto-generated>
// Original file comments:
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
//
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace NetExchange {
  /// <summary>
  /// The greeting service definition.
  ///service Greeter {
  /// </summary>
  public static partial class ExProto
  {
    static readonly string __ServiceName = "NetExchange.ExProto";

    static readonly grpc::Marshaller<global::NetExchange.HelloRequest> __Marshaller_NetExchange_HelloRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NetExchange.HelloRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NetExchange.HelloReply> __Marshaller_NetExchange_HelloReply = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NetExchange.HelloReply.Parser.ParseFrom);

    static readonly grpc::Method<global::NetExchange.HelloRequest, global::NetExchange.HelloReply> __Method_SayHello = new grpc::Method<global::NetExchange.HelloRequest, global::NetExchange.HelloReply>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "SayHello",
        __Marshaller_NetExchange_HelloRequest,
        __Marshaller_NetExchange_HelloReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::NetExchange.HelloworldReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ExProto</summary>
    [grpc::BindServiceMethod(typeof(ExProto), "BindService")]
    public abstract partial class ExProtoBase
    {
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="requestStream">Used for reading requests from the client.</param>
      /// <param name="responseStream">Used for sending responses back to the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>A task indicating completion of the handler.</returns>
      public virtual global::System.Threading.Tasks.Task SayHello(grpc::IAsyncStreamReader<global::NetExchange.HelloRequest> requestStream, grpc::IServerStreamWriter<global::NetExchange.HelloReply> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for ExProto</summary>
    public partial class ExProtoClient : grpc::ClientBase<ExProtoClient>
    {
      /// <summary>Creates a new client for ExProto</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ExProtoClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ExProto that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ExProtoClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ExProtoClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ExProtoClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncDuplexStreamingCall<global::NetExchange.HelloRequest, global::NetExchange.HelloReply> SayHello(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SayHello(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncDuplexStreamingCall<global::NetExchange.HelloRequest, global::NetExchange.HelloReply> SayHello(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_SayHello, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ExProtoClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ExProtoClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ExProtoBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_SayHello, serviceImpl.SayHello).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ExProtoBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_SayHello, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::NetExchange.HelloRequest, global::NetExchange.HelloReply>(serviceImpl.SayHello));
    }

  }
}
#endregion
