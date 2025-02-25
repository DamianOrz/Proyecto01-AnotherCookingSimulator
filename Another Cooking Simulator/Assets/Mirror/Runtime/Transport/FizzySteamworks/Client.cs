﻿using Steamworks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Mirror.FizzySteam
{
    public class Client : Common
    {
        public bool Connected { get; private set; }
        public bool Error { get; private set; }

        private event Action<byte[], int> OnReceivedData;
        private event Action OnConnected;
        private event Action OnDisconnected;

        private TimeSpan ConnectionTimeout;

        private CSteamID hostSteamID = CSteamID.Nil;
        private TaskCompletionSource<Task> connectedComplete;
        private CancellationTokenSource cancelToken;

        private Client(FizzySteamworks transport) : base(transport)
        {
            ConnectionTimeout = TimeSpan.FromSeconds(Math.Max(1, transport.Timeout));
        }

        public static Client CreateClient(FizzySteamworks transport, string host)
        {
            Client c = new Client(transport);

            c.OnConnected += () => transport.OnClientConnected.Invoke();
            c.OnDisconnected += () => transport.OnClientDisconnected.Invoke();
            c.OnReceivedData += (data, channel) => transport.OnClientDataReceived.Invoke(new ArraySegment<byte>(data), channel);

            if (SteamManager.Initialized)
            {
                c.Connect(host);
            }
            else
            {
                Debug.LogError("SteamWorks not initialized");
                c.OnConnectionFailed(CSteamID.Nil);
            }

            return c;
        }

        private async void Connect(string host)
        {
            cancelToken = new CancellationTokenSource();

            try
            {
                hostSteamID = new CSteamID(UInt64.Parse(host));
                connectedComplete = new TaskCompletionSource<Task>();

                OnConnected += SetConnectedComplete;
                CloseP2PSessionWithUser(hostSteamID);

                SendInternal(hostSteamID, InternalMessages.CONNECT);

                Task connectedCompleteTask = connectedComplete.Task;

                if (await Task.WhenAny(connectedCompleteTask, Task.Delay(ConnectionTimeout, cancelToken.Token)) != connectedCompleteTask)
                {
                    Debug.LogError($"Connection to {host} timed out.");
                    OnConnected -= SetConnectedComplete;
                    OnConnectionFailed(hostSteamID);
                }

                OnConnected -= SetConnectedComplete;
            }
            catch (FormatException)
            {
                Debug.LogError($"Connection string was not in the right format. Did you enter a SteamId?");
                Error = true;
                OnConnectionFailed(hostSteamID);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                Error = true;
                OnConnectionFailed(hostSteamID);
            }
            finally
            {
                if (Error)
                {
                    OnConnectionFailed(CSteamID.Nil);
                }
            }

        }

        public void Disconnect()
        {
            SendInternal(hostSteamID, InternalMessages.DISCONNECT);
            Dispose();
            cancelToken.Cancel();

            transport.StartCoroutine(WaitDisconnect(hostSteamID));
        }

        private void SetConnectedComplete() => connectedComplete.SetResult(connectedComplete.Task);

        protected override void OnReceiveData(byte[] data, CSteamID clientSteamID, int channel)
        {
            if (clientSteamID != hostSteamID)
            {
                Debug.LogError("Received a message from an unknown");
                return;
            }

            OnReceivedData.Invoke(data, channel);
        }

        protected override void OnNewConnection(P2PSessionRequest_t result)
        {
            if (hostSteamID == result.m_steamIDRemote)
            {
                SteamNetworking.AcceptP2PSessionWithUser(result.m_steamIDRemote);
            }
            else
            {
                Debug.LogError("P2P Acceptance Request from unknown host ID.");
            }
        }

        protected override void OnReceiveInternalData(InternalMessages type, CSteamID clientSteamID)
        {
            switch (type)
            {
                case InternalMessages.ACCEPT_CONNECT:
                    Connected = true;
                    OnConnected.Invoke();
                    Debug.Log("Connection established.");
                    break;
                case InternalMessages.DISCONNECT:
                    Connected = false;
                    Debug.Log("Disconnected.");
                    OnDisconnected.Invoke();
                    break;
                default:
                    Debug.Log("Received unknown message type");
                    break;
            }
        }

        public bool Send(byte[] data, int channelId) => Send(hostSteamID, data, channelId);

        protected override void OnConnectionFailed(CSteamID remoteId) => OnDisconnected.Invoke();
    }
}