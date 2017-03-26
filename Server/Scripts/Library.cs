using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Server
{
	public class Library
	{
		private string m_Name;
		private Assembly m_Assembly;
		private Type[] m_Types;
		private TypeTable m_TypesByName, m_TypesByFullName;
		private bool m_Configured, m_Initialized;
		private TypeCache m_TypeCache;

		public Library( Configuration.Library libConfig, Assembly assembly )
		{
			m_Name = libConfig.Name;
			m_Assembly = assembly;

			m_Assembly.GetTypes();

			m_Types = m_Assembly.GetTypes().Where( type => !libConfig.GetIgnoreType( type ) ).ToArray();

			m_TypesByName = new TypeTable( m_Types.Length );
			m_TypesByFullName = new TypeTable( m_Types.Length );

			foreach ( var type in m_Types )
			{
				m_TypesByName.Add( type.Name, type );
				m_TypesByFullName.Add( type.FullName, type );

				if ( type.IsDefined( typeof( TypeAliasAttribute ), false ) )
				{
					var attrs = type.GetCustomAttributes( typeof( TypeAliasAttribute ), false );

					if ( attrs.Length > 0 && attrs[0] != null )
					{
						var attr = attrs[0] as TypeAliasAttribute;

						foreach ( var alias in attr.Aliases )
							m_TypesByFullName.Add( alias, type );
					}
				}
			}

			m_TypeCache = new TypeCache( m_Types, m_TypesByName, m_TypesByFullName );
		}

		public string Name
		{
			get { return m_Name; }
		}

		public Assembly Assembly
		{
			get { return m_Assembly; }
		}

		public TypeCache TypeCache
		{
			get { return m_TypeCache; }
		}

		public Type[] Types
		{
			get { return m_Types; }
		}

		public TypeTable TypesByName
		{
			get { return m_TypesByName; }
		}

		public TypeTable TypesByFullName
		{
			get { return m_TypesByFullName; }
		}

		public void Verify( ref int itemCount, ref int mobileCount )
		{
			Type[] ctorTypes = new Type[] { typeof( Serial ) };

			foreach ( Type t in m_Types )
			{
				bool isItem = t.IsSubclassOf( typeof( Item ) );
				bool isMobile = t.IsSubclassOf( typeof( Mobile ) );

				if ( isItem || isMobile )
				{
					if ( isItem )
						++itemCount;
					else
						++mobileCount;

					try
					{
						if ( t.GetConstructor( ctorTypes ) == null )
							Console.WriteLine( "Warning: {0} has no serialization constructor", t );

						if ( t.GetMethod( "Serialize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly ) == null )
							Console.WriteLine( "Warning: {0} has no Serialize() method", t );

						if ( t.GetMethod( "Deserialize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly ) == null )
							Console.WriteLine( "Warning: {0} has no Deserialize() method", t );
					}
					catch
					{
					}
				}
			}
		}

		private void InvokeAll( string methodName )
		{
			List<MethodInfo> invoke = new List<MethodInfo>();

			foreach ( Type type in m_Types )
			{
				MethodInfo m = type.GetMethod( methodName, BindingFlags.Static | BindingFlags.Public );

				if ( m != null )
					invoke.Add( m );
			}

			invoke.Sort( CallPriorityComparer.Instance );

			foreach ( MethodInfo m in invoke )
				m.Invoke( null, null );
		}

		public void Configure()
		{
			if ( m_Name == "Core" )
			{
				m_Configured = true;
				return;
			}

			if ( m_Configured )
				throw new ApplicationException( "already configured" );

			InvokeAll( "Configure" );

			m_Configured = true;
		}

		public void Initialize()
		{
			if ( m_Name == "Core" )
			{
				m_Initialized = true;
				return;
			}

			if ( !m_Configured )
				throw new ApplicationException( "not configured yet" );
			if ( m_Initialized )
				throw new ApplicationException( "already initialized" );

			InvokeAll( "Initialize" );

			m_Initialized = true;
		}

		public Type GetTypeByName( string name, bool ignoreCase )
		{
			return m_TypesByName.Get( name, ignoreCase );
		}

		public Type GetTypeByFullName( string fullName, bool ignoreCase )
		{
			return m_TypesByFullName.Get( fullName, ignoreCase );
		}
	}
}