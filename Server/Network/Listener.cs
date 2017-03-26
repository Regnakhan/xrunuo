using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using Server;
using Server.Events;

namespace Server.Network
{
	public class Listener : IDisposable
	{
		private static readonly ILog log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		private Socket m_Listener;
		private bool m_Disposed;
		private int m_ThisPort;

		private Queue m_Accepted;

		private AsyncCallback m_OnAccept;

		private static Socket[] m_EmptySockets = new Socket[0];

		public int UsedPort
		{
			get { return m_ThisPort; }
		}

		private static int m_Port = 2593;

		public static int Port
		{
			get
			{
				return m_Port;
			}
			set
			{
				m_Port = value;
			}
		}

		public Listener( int port )
		{
			m_ThisPort = port;
			m_Disposed = false;
			m_Accepted = new Queue();
			m_OnAccept = new AsyncCallback( OnAccept );

			m_Listener = Bind( IPAddress.Any, port );

			var ipep = m_Listener.LocalEndPoint as IPEndPoint;

			if ( ipep == null )
				return;

			foreach ( var adapter in NetworkInterface.GetAllNetworkInterfaces() )
			{
				var properties = adapter.GetIPProperties();
				foreach ( var unicast in properties.UnicastAddresses )
					if ( ipep.AddressFamily == unicast.Address.AddressFamily )
						log.Info( "Address: {0}:{1}", unicast.Address, ipep.Port );
			}
		}

		private Socket Bind( IPAddress ip, int port )
		{
			IPEndPoint ipep = new IPEndPoint( ip, port );

			Socket s = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

			try
			{
				s.Bind( ipep );
				s.Listen( 300 );

				s.BeginAccept( m_OnAccept, s );

				return s;
			}
			catch ( Exception e )
			{
				log.Error( "Bind exception: ", e );

				try { s.Shutdown( SocketShutdown.Both ); }
				catch { }

				try { s.Close(); }
				catch { }

				return null;
			}
		}

		private void OnAccept( IAsyncResult asyncResult )
		{
			Socket accepted = null;

			try
			{
				accepted = m_Listener.EndAccept( asyncResult );
			}
			catch ( SocketException ex )
			{
				UOSocket.TraceException( ex );
			}
			catch ( ObjectDisposedException )
			{
				return;
			}

			if ( accepted != null )
			{
				if ( VerifySocket( accepted ) )
				{
					Enqueue( accepted );
				}
				else
				{
					Release( accepted );
				}
			}

			try
			{
				m_Listener.BeginAccept( m_OnAccept, m_Listener );
			}
			catch ( SocketException ex )
			{
				UOSocket.TraceException( ex );
			}
			catch ( ObjectDisposedException )
			{
			}
		}

		private bool VerifySocket( Socket socket )
		{
			try
			{
				SocketConnectEventArgs args = new SocketConnectEventArgs( socket );

				EventSink.InvokeSocketConnect( args );

				return args.AllowConnection;
			}
			catch ( Exception ex )
			{
				UOSocket.TraceException( ex );

				return false;
			}
		}

		private void Enqueue( Socket socket )
		{
			lock ( m_Accepted.SyncRoot )
			{
				m_Accepted.Enqueue( socket );
			}

			//Core.WakeUp();
		}

		private void Release( Socket socket )
		{
			try
			{
				socket.Shutdown( SocketShutdown.Both );
			}
			catch ( SocketException ex )
			{
				UOSocket.TraceException( ex );
			}

			try
			{
				socket.Close();
			}
			catch ( SocketException ex )
			{
				UOSocket.TraceException( ex );
			}
		}

		public Socket[] Slice()
		{
			lock ( m_Accepted.SyncRoot )
			{
				if ( m_Accepted.Count == 0 )
					return m_EmptySockets;

				object[] array = m_Accepted.ToArray();
				m_Accepted.Clear();

				Socket[] sockets = new Socket[array.Length];

				Array.Copy( array, sockets, array.Length );

				return sockets;
			}
		}

		public void Dispose()
		{
			if ( !m_Disposed )
			{
				m_Disposed = true;

				if ( m_Listener != null )
				{
					try { m_Listener.Shutdown( SocketShutdown.Both ); }
					catch { }

					try { m_Listener.Close(); }
					catch { }

					m_Listener = null;
				}
			}
		}
	}
}
