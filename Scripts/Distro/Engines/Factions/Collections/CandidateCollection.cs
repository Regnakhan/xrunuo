//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace Server.Factions
{
	using System;
	using System.Collections;


	/// <summary>
	/// Strongly typed collection of Server.Factions.Candidate.
	/// </summary>
	public class CandidateCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CandidateCollection()
		{
		}

		/// <summary>
		/// Gets or sets the value of the Server.Factions.Candidate at a specific position in the CandidateCollection.
		/// </summary>
		public Server.Factions.Candidate this[int index] { get { return ( (Server.Factions.Candidate) ( this.List[index] ) ); } set { this.List[index] = value; } }

		/// <summary>
		/// Append a Server.Factions.Candidate entry to this collection.
		/// </summary>
		/// <param name="value">Server.Factions.Candidate instance.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		public int Add( Server.Factions.Candidate value )
		{
			return this.List.Add( value );
		}

		/// <summary>
		/// Determines whether a specified Server.Factions.Candidate instance is in this collection.
		/// </summary>
		/// <param name="value">Server.Factions.Candidate instance to search for.</param>
		/// <returns>True if the Server.Factions.Candidate instance is in the collection; otherwise false.</returns>
		public bool Contains( Server.Factions.Candidate value )
		{
			return this.List.Contains( value );
		}

		/// <summary>
		/// Retrieve the index a specified Server.Factions.Candidate instance is in this collection.
		/// </summary>
		/// <param name="value">Server.Factions.Candidate instance to find.</param>
		/// <returns>The zero-based index of the specified Server.Factions.Candidate instance. If the object is not found, the return value is -1.</returns>
		public int IndexOf( Server.Factions.Candidate value )
		{
			return this.List.IndexOf( value );
		}

		/// <summary>
		/// Removes a specified Server.Factions.Candidate instance from this collection.
		/// </summary>
		/// <param name="value">The Server.Factions.Candidate instance to remove.</param>
		public void Remove( Server.Factions.Candidate value )
		{
			this.List.Remove( value );
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the Server.Factions.Candidate instance.
		/// </summary>
		/// <returns>An Server.Factions.Candidate's enumerator.</returns>
		new public CandidateCollectionEnumerator GetEnumerator()
		{
			return new CandidateCollectionEnumerator( this );
		}

		/// <summary>
		/// Insert a Server.Factions.Candidate instance into this collection at a specified index.
		/// </summary>
		/// <param name="index">Zero-based index.</param>
		/// <param name="value">The Server.Factions.Candidate instance to insert.</param>
		public void Insert( int index, Server.Factions.Candidate value )
		{
			this.List.Insert( index, value );
		}

		/// <summary>
		/// Strongly typed enumerator of Server.Factions.Candidate.
		/// </summary>
		public class CandidateCollectionEnumerator : System.Collections.IEnumerator
		{
			/// <summary>
			/// Current index
			/// </summary>
			private int _index;

			/// <summary>
			/// Current element pointed to.
			/// </summary>
			private Server.Factions.Candidate _currentElement;

			/// <summary>
			/// Collection to enumerate.
			/// </summary>
			private CandidateCollection _collection;

			/// <summary>
			/// Default constructor for enumerator.
			/// </summary>
			/// <param name="collection">Instance of the collection to enumerate.</param>
			internal CandidateCollectionEnumerator( CandidateCollection collection )
			{
				_index = -1;
				_collection = collection;
			}

			/// <summary>
			/// Gets the Server.Factions.Candidate object in the enumerated CandidateCollection currently indexed by this instance.
			/// </summary>
			public Server.Factions.Candidate Current
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