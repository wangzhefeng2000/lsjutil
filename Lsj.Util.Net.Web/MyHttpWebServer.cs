﻿using Lsj.Util.IO;
using Lsj.Util.Net.Sockets;
using Lsj.Util.Net.Web.Modules;
using Lsj.Util.Net.Web.Response;
using Lsj.Util.Net.Web.Website;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lsj.Util.Net.Web
{
    public class HttpWebServer : DisposableClass, IDisposable
    {
        public const string ServerVersion = "HttpWebServer/lsj(1.0)";
        ConcurrentBag<HttpClient> clients;
        Socket m_socket;
        Socket manage_socket;
        List<HttpWebsite> sites = new List<HttpWebsite>();

        public HttpWebServer(IPAddress ip, int port,int manageport,int maxclient)
        {
            try
            {
                this.m_socket = new TcpSocket();
                m_socket.Bind(ip, port);
                this.manage_socket = new TcpSocket();
                manage_socket.Bind(ip, manageport);
                clients = new ConcurrentBag<HttpClient>();
               
            }
            catch (Exception e)
            {
                Log.Log.Default.Error("Bind Error" + e.ToString());
            }
        }
        public void Start()
        {
            try
            {
                m_socket.Listen();
                m_socket.BeginAccept(OnAccept);   
            }
            catch (Exception e)
            {
                Log.Log.Default.Error("Start Error" + e.ToString());
                if (m_socket != null)
                {
                    m_socket.Close();
                }
            }
        }

        private void OnAccept(IAsyncResult ar)
        {
            m_socket.BeginAccept(OnAccept);
            var handle = m_socket.EndAccept(ar);
            var client = new HttpClient(handle, this);
            client.Receive();
        }

 

        public void Stop()
        {
            try
            {
                m_socket.Close();
            }
            catch (Exception e)
            {
                Log.Log.Default.Error("Stop Error" + e.ToString());

            }
        }
        
        public void AddWebsite(HttpWebsite website)
        {
            if (!sites.Contains(website))
            {
                sites.Insert(0, website);
            }
        }
        public void RemoveWebsite(HttpWebsite website)
        {
            if (!sites.Contains(website))
            {
                sites.Remove(website);
            }
        }
        public HttpWebsite GetWebSite(string host)
        {
            foreach (var a in sites)
            {
                if (host.IsMatchIgnoreCase(a.Config.Host))
                {
                    return a;
                }
            }
            return null;
        }
        internal void RemoveClient(HttpClient client)
        {
            clients.r
        }
    }
}
