//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace Server.Engines.Reports
{
	using System;
	using System.Collections;


	/// <summary>
	/// Strongly typed collection of Server.Engines.Reports.ReportColumn.
	/// </summary>
	public class ReportColumnCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ReportColumnCollection()
			: base()
		{
		}

		/// <summary>
		/// Gets or sets the value of the Server.Engines.Reports.ReportColumn at a specific position in the ReportColumnCollection.
		/// </summary>
		public Server.Engines.Reports.ReportColumn this[int index] { get { return ( (Server.Engines.Reports.ReportColumn) ( this.List[index] ) ); } set { this.List[index] = value; } }

		public int Add( string width, string align )
		{
			return Add( new ReportColumn( width, align ) );
		}

		public int Add( string width, string align, string name )
		{
			return Add( new ReportColumn( width, align, name ) );
		}

		/// <summary>
		/// Append a Server.Engines.Reports.ReportColumn entry to this collection.
		/// </summary>
		/// <param name="value">Server.Engines.Reports.ReportColumn instance.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		public int Add( Server.Engines.Reports.ReportColumn value )
		{
			return this.List.Add( value );
		}

		/// <summary>
		/// Determines whether a specified Server.Engines.Reports.ReportColumn instance is in this collection.
		/// </summary>
		/// <param name="value">Server.Engines.Reports.ReportColumn instance to search for.</param>
		/// <returns>True if the Server.Engines.Reports.ReportColumn instance is in the collection; otherwise false.</returns>
		public bool Contains( Server.Engines.Reports.ReportColumn value )
		{
			return this.List.Contains( value );
		}

		/// <summary>
		/// Retrieve the index a specified Server.Engines.Reports.ReportColumn instance is in this collection.
		/// </summary>
		/// <param name="value">Server.Engines.Reports.ReportColumn instance to find.</param>
		/// <returns>The zero-based index of the specified Server.Engines.Reports.ReportColumn instance. If the object is not found, the return value is -1.</returns>
		public int IndexOf( Server.Engines.Reports.ReportColumn value )
		{
			return this.List.IndexOf( value );
		}

		/// <summary>
		/// Removes a specified Server.Engines.Reports.ReportColumn instance from this collection.
		/// </summary>
		/// <param name="value">The Server.Engines.Reports.ReportColumn instance to remove.</param>
		public void Remove( Server.Engines.Reports.ReportColumn value )
		{
			this.List.Remove( value );
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the Server.Engines.Reports.ReportColumn instance.
		/// </summary>
		/// <returns>An Server.Engines.Reports.ReportColumn's enumerator.</returns>
		new public ReportColumnCollectionEnumerator GetEnumerator()
		{
			return new ReportColumnCollectionEnumerator( this );
		}

		/// <summary>
		/// Insert a Server.Engines.Reports.ReportColumn instance into this collection at a specified index.
		/// </summary>
		/// <param name="index">Zero-based index.</param>
		/// <param name="value">The Server.Engines.Reports.ReportColumn instance to insert.</param>
		public void Insert( int index, Server.Engines.Reports.ReportColumn value )
		{
			this.List.Insert( index, value );
		}

		/// <summary>
		/// Strongly typed enumerator of Server.Engines.Reports.ReportColumn.
		/// </summary>
		public class ReportColumnCollectionEnumerator : System.Collections.IEnumerator
		{
			/// <summary>
			/// Current index
			/// </summary>
			private int _index;

			/// <summary>
			/// Current element pointed to.
			/// </summary>
			private Server.Engines.Reports.ReportColumn _currentElement;

			/// <summary>
			/// Collection to enumerate.
			/// </summary>
			private ReportColumnCollection _collection;

			/// <summary>
			/// Default constructor for enumerator.
			/// </summary>
			/// <param name="collection">Instance of the collection to enumerate.</param>
			internal ReportColumnCollectionEnumerator( ReportColumnCollection collection )
			{
				_index = -1;
				_collection = collection;
			}

			/// <summary>
			/// Gets the Server.Engines.Reports.ReportColumn object in the enumerated ReportColumnCollection currently indexed by this instance.
			/// </summary>
			public Server.Engines.Reports.ReportColumn Current
			{
				get
				{
					if ( ( ( _index == -1 ) || ( _index >= _collection.Count ) ) )
					{
						throw new System.IndexOutOfRangeException( "Enumerator not started." );
					}
					else
					{
						return _currentElement;
					}
				}
			}

			/// <summary>
			/// Gets the current element in the collection.
			/// </summary>
			object IEnumerator.Current
			{
				get
				{
					if ( ( ( _index == -1 ) || ( _index >= _collection.Count ) ) )
					{
						throw new System.IndexOutOfRangeException( "Enumerator not started." );
					}
					else
					{
						return _currentElement;
					}
				}
			}

			/// <summary>
			/// Reset the cursor, so it points to the beginning of the enumerator.
			/// </summary>
			public void Reset()
			{
				_index = -1;
				_currentElement = null;
			}

			/// <summary>
			/// Advances the enumerator to the next queue of the enumeration, if one is currently available.
			/// </summary>
			/// <returns>true, if the enumerator was succesfully advanced to the next queue; false, if the enumerator has reached the end of the enumeration.</returns>
			public bool MoveNext()
			{
				if ( ( _index < ( _collection.Count - 1 ) ) )
				{
					_index = ( _index + 1 );
					_currentElement = this._collection[_index];
					return true;
				}
				_index = _collection.Count;
				return false;
			}
		}
	}
}