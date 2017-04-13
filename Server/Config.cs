﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Server
{
	public static class Config
	{
		private static readonly ILog log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		public sealed class Entry : IEquatable<Entry>, IComparable<Entry>
		{
			public int FileIndex { get; }
			public string File { get; }
			public string Scope { get; }
			public string Desc { get; set; }
			public string Key { get; set; }
			public string Value { get; set; }

			public bool UseDefault { get; set; }

			public Entry( string file, int fileIndex, string scope, string desc, string key, string value, bool useDefault )
			{
				File = file;
				FileIndex = fileIndex;

				Scope = scope;
				Desc = desc;

				Key = key;
				Value = value;

				UseDefault = useDefault;
			}

			public override string ToString()
			{
				return $"{Scope}.{( UseDefault ? "@" : "" )}{Key}={Value}";
			}

			public override int GetHashCode()
			{
				unchecked
				{
					var hash = -1;

					hash = ( hash * 397 ) ^ File.GetHashCode();
					hash = ( hash * 397 ) ^ Scope.GetHashCode();
					hash = ( hash * 397 ) ^ Key.GetHashCode();

					return hash;
				}
			}

			public override bool Equals( object obj )
			{
				return obj is Entry && Equals( (Entry)obj );
			}

			public bool Equals( Entry other )
			{
				if ( ReferenceEquals( other, null ) )
				{
					return false;
				}

				if ( ReferenceEquals( other, this ) )
				{
					return true;
				}

				return Insensitive.Equals( File, other.File ) &&
				       Insensitive.Equals( Scope, other.Scope ) &&
				       Insensitive.Equals( Key, other.Key );
			}

			public int CompareTo( Entry other )
			{
				if ( other == null )
					return -1;

				if ( !Insensitive.Equals( File, other.File ) )
					return Insensitive.Compare( File, other.File );

				return FileIndex.CompareTo( other.FileIndex );
			}
		}

		private static bool m_Initialized;

		private static readonly string m_Path = Path.Combine( Core.BaseDirectory, "Config" );

		private static readonly IFormatProvider m_NumFormatter = CultureInfo.InvariantCulture.NumberFormat;

		private static readonly Dictionary<string, Entry> m_Entries =
			new Dictionary<string, Entry>( StringComparer.OrdinalIgnoreCase );

		public static IEnumerable<Entry> Entries => m_Entries.Values;

		public static void Load()
		{
			if ( m_Initialized )
				return;

			m_Initialized = true;

			if ( !Directory.Exists( m_Path ) )
				Directory.CreateDirectory( m_Path );

			IEnumerable<string> files;

			try
			{
				files = Directory.EnumerateFiles( m_Path, "*.cfg", SearchOption.AllDirectories );
			}
			catch ( DirectoryNotFoundException )
			{
				log.Warning( "No configuration files found!" );
				return;
			}

			foreach ( var path in files )
			{
				try
				{
					LoadFile( path );
				}
				catch ( Exception e )
				{
					log.Warning( "Failed to load configuration file {0}: {1}", path, e.Message );

					if ( Core.Service )
					{
						ConsoleKeyInfo key;

						do
						{
							Console.WriteLine( "Ignore this warning? (y/n)" );
							key = Console.ReadKey( true );
						} while ( key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N && key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape );

						switch ( key.Key )
						{
							case ConsoleKey.Escape:
							case ConsoleKey.N:
							{
								Console.WriteLine( "Press any key to exit..." );
								Console.ReadKey();

								Core.Kill( false );
							}
								return;
						}
					}
				}
			}
		}

		private static void LoadFile( string path )
		{
			var info = new FileInfo( path );

			if ( !info.Exists )
				throw new FileNotFoundException();

			path = info.Directory?.FullName ?? string.Empty;

			var io = path.IndexOf( m_Path, StringComparison.OrdinalIgnoreCase );

			if ( io > -1 )
			{
				path = path.Substring( io + m_Path.Length );
			}

			var parts = path.Split( Path.DirectorySeparatorChar );

			var scope = string.Join( ".", parts.Where( p => !string.IsNullOrWhiteSpace( p ) ) );

			if ( scope.Length > 0 )
				scope += ".";

			scope += Path.GetFileNameWithoutExtension( info.Name );

			var lines = File.ReadAllLines( info.FullName );

			var desc = new List<string>( 0x10 );

			for ( int i = 0, idx = 0; i < lines.Length; i++ )
			{
				var line = lines[i].Trim();

				if ( string.IsNullOrWhiteSpace( line ) )
				{
					desc.Clear();
					continue;
				}

				if ( line.StartsWith( "#" ) )
				{
					desc.Add( line.TrimStart( '#' ).Trim() );
					continue;
				}

				var useDef = false;

				if ( line.StartsWith( "@" ) )
				{
					useDef = true;
					line = line.TrimStart( '@' ).Trim();
				}

				io = line.IndexOf( '=' );

				if ( io < 0 )
				{
					throw new FormatException( $"Bad format at line {i + 1}" );
				}

				var key = line.Substring( 0, io );
				var val = line.Substring( io + 1 );

				if ( string.IsNullOrWhiteSpace( key ) )
				{
					throw new NullReferenceException( $"Key can not be null at line {i + 1}" );
				}

				key = key.Trim();

				if ( string.IsNullOrEmpty( val ) )
				{
					val = null;
				}

				var e = new Entry( info.FullName, idx++, scope, string.Join( string.Empty, desc ), key, val, useDef );

				m_Entries[$"{e.Scope}.{e.Key}"] = e;

				desc.Clear();
			}
		}

		public static void Save()
		{
			if ( !m_Initialized )
			{
				Load();
			}

			if ( !Directory.Exists( m_Path ) )
			{
				Directory.CreateDirectory( m_Path );
			}

			foreach ( var g in m_Entries.Values.ToLookup( e => e.File ) )
			{
				try
				{
					SaveFile( g.Key, g.OrderBy( e => e.FileIndex ) );
				}
				catch ( Exception e )
				{
					log.Warning( "Failed to save configuration file {0}: {1}", g.Key, e.Message );
				}
			}
		}

		private static void SaveFile( string path, IEnumerable<Entry> entries )
		{
			var content = new StringBuilder( 0x1000 );
			var line = new StringBuilder( 0x80 );

			foreach ( var e in entries )
			{
				content.AppendLine();

				if ( !string.IsNullOrWhiteSpace( e.Desc ) )
				{
					line.Clear();

					foreach ( var word in e.Desc.Split( ' ' ) )
					{
						if ( ( line + word ).Length > 100 )
						{
							content.AppendLine( $"# {line}" );
							line.Clear();
						}

						line.AppendFormat( "{0} ", word );
					}

					if ( line.Length > 0 )
					{
						content.AppendLine( $"# {line}" );
						line.Clear();
					}
				}

				content.AppendLine( $"{( e.UseDefault ? "@" : string.Empty )}{e.Key}={e.Value}" );
			}

			File.WriteAllText( path, content.ToString() );
		}

		public static Entry Find( string key )
		{
			Entry e;
			m_Entries.TryGetValue( key, out e );
			return e;
		}

		private static void InternalSet( string key, string value )
		{
			var e = Find( key );

			if ( e != null )
			{
				e.Value = value;
				e.UseDefault = false;
				return;
			}

			var parts = key.Split( '.' );
			var realKey = parts.Last();

			parts = parts.Take( parts.Length - 1 ).ToArray();

			var file = new FileInfo( Path.Combine( m_Path, Path.Combine( parts ) + ".cfg" ) );
			var idx = m_Entries.Values.Where( o => Insensitive.Equals( o.File, file.FullName ) ).Select( o => o.FileIndex ).DefaultIfEmpty().Max();

			m_Entries[key] = new Entry( file.FullName, idx, string.Join( ".", parts ), string.Empty, realKey, value, false );
		}

		public static void Set( string key, string value )
		{
			InternalSet( key, value );
		}

		public static void Set( string key, char value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, sbyte value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, byte value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, short value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, ushort value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, int value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, uint value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, long value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, ulong value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, float value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, double value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, decimal value )
		{
			InternalSet( key, value.ToString( m_NumFormatter ) );
		}

		public static void Set( string key, bool value )
		{
			InternalSet( key, value ? "true" : "false" );
		}

		public static void Set( string key, TimeSpan value )
		{
			InternalSet( key, value.ToString() );
		}

		public static void Set( string key, DateTime value )
		{
			InternalSet( key, value.ToString( CultureInfo.InvariantCulture ) );
		}

		public static void SetEnum<T>( string key, T value ) where T : struct, IConvertible
		{
			var t = typeof( T );

			if ( !t.IsEnum )
			{
				throw new ArgumentException( "T must be an enumerated type" );
			}

			var vals = Enum.GetValues( t ).Cast<T>();

			foreach ( T o in vals.Where( o => o.Equals( value ) ) )
			{
				InternalSet( key, o.ToString( CultureInfo.InvariantCulture ) );
				return;
			}

			throw new ArgumentException( "Enumerated value not found" );
		}

		private static void Warn<T>( string key )
		{
			log.Warning( "'{0}' invalid value for '{1}'", typeof( T ), key );
		}

		private static string InternalGet( string key )
		{
			if ( !m_Initialized )
			{
				Load();
			}

			Entry e;

			if ( m_Entries.TryGetValue( key, out e ) && e != null )
			{
				return e.UseDefault ? null : e.Value;
			}

			log.Warning( "Using default value for {0}", key );

			return null;
		}

		public static string Get( string key, string defaultValue )
		{
			return InternalGet( key ) ?? defaultValue;
		}

		public static sbyte Get( string key, sbyte defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || sbyte.TryParse( value, NumberStyles.Any, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<sbyte>( key );

			return defaultValue;
		}

		public static byte Get( string key, byte defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || byte.TryParse( value, NumberStyles.Any & ~NumberStyles.AllowLeadingSign, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<byte>( key );

			return defaultValue;
		}

		public static short Get( string key, short defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || short.TryParse( value, NumberStyles.Any, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<short>( key );

			return defaultValue;
		}

		public static ushort Get( string key, ushort defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || ushort.TryParse( value, NumberStyles.Any & ~NumberStyles.AllowLeadingSign, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<ushort>( key );

			return defaultValue;
		}

		public static int Get( string key, int defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || int.TryParse( value, NumberStyles.Any, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<int>( key );

			return defaultValue;
		}

		public static uint Get( string key, uint defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || uint.TryParse( value, NumberStyles.Any & ~NumberStyles.AllowLeadingSign, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<uint>( key );

			return defaultValue;
		}

		public static long Get( string key, long defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || long.TryParse( value, NumberStyles.Any, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<long>( key );

			return defaultValue;
		}

		public static ulong Get( string key, ulong defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || ulong.TryParse( value, NumberStyles.Any & ~NumberStyles.AllowLeadingSign, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<ulong>( key );

			return defaultValue;
		}

		public static float Get( string key, float defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || float.TryParse( value, NumberStyles.Any, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<float>( key );

			return defaultValue;
		}

		public static double Get( string key, double defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || double.TryParse( value, NumberStyles.Any, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<double>( key );

			return defaultValue;
		}

		public static decimal Get( string key, decimal defaultValue )
		{
			var ret = defaultValue;
			var value = InternalGet( key );

			if ( value == null || decimal.TryParse( value, NumberStyles.Any, m_NumFormatter, out ret ) )
			{
				return ret;
			}

			Warn<decimal>( key );

			return defaultValue;
		}

		public static bool Get( string key, bool defaultValue )
		{
			var value = InternalGet( key );

			if ( value == null )
				return defaultValue;

			if ( Regex.IsMatch( value, @"(true|yes|on|1|enabled)", RegexOptions.IgnoreCase ) )
				return true;

			if ( Regex.IsMatch( value, @"(false|no|off|0|disabled)", RegexOptions.IgnoreCase ) )
				return false;

			Warn<bool>( key );

			return defaultValue;
		}

		public static TimeSpan Get( string key, TimeSpan defaultValue )
		{
			var value = InternalGet( key );

			TimeSpan ts;

			if ( TimeSpan.TryParse( value, out ts ) )
				return ts;

			Warn<TimeSpan>( key );

			return defaultValue;
		}

		public static DateTime Get( string key, DateTime defaultValue )
		{
			var value = InternalGet( key );

			DateTime dt;

			if ( DateTime.TryParse( value, out dt ) )
				return dt;

			Warn<DateTime>( key );

			return defaultValue;
		}

		public static T GetEnum<T>( string key, T defaultValue ) where T : struct, IConvertible
		{
			if ( !typeof( T ).IsEnum )
				throw new ArgumentException( "T must be an enumerated type" );

			var value = InternalGet( key );

			if ( value == null )
				return defaultValue;

			value = value.Trim();

			var vals = Enum.GetValues( typeof( T ) ).Cast<T>();

			foreach ( var o in vals.Where( o => Insensitive.Equals( value, o.ToString( CultureInfo.InvariantCulture ) ) ) )
			{
				return o;
			}

			Warn<T>( key );

			return defaultValue;
		}
	}
}
