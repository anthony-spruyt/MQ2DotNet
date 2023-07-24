using MQ2DotNet.MQ2API.DataTypes;
using System;
using System.Collections.Concurrent;

/* To create the member properties, grab everything in the switch statement from the cpp MQ2xxxType::GetMember function
 * Then in notepad++, find all:
 *      (case (\w+):|Dest\.Type)
 * Select all and copy, then find/replace:
 *      case (\w+):\s+Dest.Type = p(\w+);
 *      public \2 \1 => GetMember<\2>\("\1"\);
 * Note this doesn't handle index members
 */
namespace MQ2DotNet.MQ2API
{
    /// <summary>
    /// Base class from which all wrapped MQ2 data types derive
    /// </summary>
    public class MQ2DataType
    {
        public static readonly ConcurrentDictionary<string, Exception> DataTypeErrors = new ConcurrentDictionary<string, Exception>();

        private readonly MQ2TypeFactory _typeFactory;
        private readonly MQ2TypeVar _typeVar;

        /// <summary>
        /// Create a new MQ2DataType from an MQ2TypeVar
        /// </summary>
        /// <param name="typeFactory">MQ2TypeFactory to use with GetMember calls</param>
        /// <param name="typeVar"></param>
        public MQ2DataType(MQ2TypeFactory typeFactory, MQ2TypeVar typeVar)
        {
            _typeFactory = typeFactory;
            _typeVar = typeVar;
        }
        
        /// <summary>
        /// Underlying data storage. Exposed for use in basic types e.g. int, double, etc
        /// </summary>
        protected MQ2VarPtr VarPtr => _typeVar.VarPtr;

        /// <inheritdoc />
        public override string ToString()
        {
            return _typeVar.ToString();
        }

        protected string OriginalToString()
        {
            return base.ToString();
        }

        #region Helpers for derived classes
        /// <summary>
        /// Get a member from the variable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns>The member if the call succeeded and was able to be cast to the <typeparamref name="T"/>, otherwise null</returns>
        /// <exception cref="InvalidCastException" />
        public T GetMember<T>(string name, string index = "") where T : MQ2DataType
        {
            if (!_typeVar.TryGetMember(name, index, out var memberTypeVar))
                return null;

            T member;

            try
            {
                member = (T)_typeFactory.Create(memberTypeVar);
            }
            catch (Exception ex)
            {
                var thisType = GetType();
                var thisDeclaringType = GetType().DeclaringType ?? GetType();
                var memberType = typeof(T);
                var memberDeclaringType = typeof(T).DeclaringType ?? typeof(T);
                var key = string.IsNullOrWhiteSpace(index) ?
                    $"Data Type: '{thisType}' Data Type Declaring Type: '{thisDeclaringType}' Member Name: '{name}' Member Type: '{memberType}' Member Declaring Type: '{memberDeclaringType}'" :
                    $"Data Type: '{thisType}' Data Type Declaring Type: '{thisDeclaringType}' Member Name: '{name}' Member Type: '{memberType}' Member Declaring Type: '{memberDeclaringType}' Member Index: '{index}'";

                DataTypeErrors.TryAdd(key, ex);

                member = null;
            }

            return member;
        }

        /// <summary>
        /// Helper class to access members with an indexer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TIndex"></typeparam>
        public class IndexedMember<T, TIndex> where T : MQ2DataType
        {
            private readonly MQ2DataType _owner;
            private readonly string _name;

            /// <summary>
            /// Create a new IndexedMember that accesses the specified member in the specified MQ2DataType
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="name"></param>
            /// <remarks>
            /// Should be used in the constructor, e.g. <code>MyIndexedMember = new MyIndexedMember(this, "MyIndexedMember"</code>
            /// Users can then do <code>myVar.MyIndexedMember["index"]</code> just like in macros
            /// </remarks>
            public IndexedMember(MQ2DataType owner, string name)
            {
                _owner = owner;
                _name = name;
            }

            /// <summary>
            /// Get the member using an index
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public T this[TIndex index] => _owner.GetMember<T>(_name, index.ToString());
        }

        /// <summary>
        /// Helper class to access members with an indexer that return a different type for a different index type, e.g. spell given an int, or int given a spell name
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TIndex1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TIndex2"></typeparam>
        public class IndexedMember<T1, TIndex1, T2, TIndex2> where T1 : MQ2DataType where T2 : MQ2DataType
        {
            private readonly MQ2DataType _owner;
            private readonly string _name;

            /// <summary>
            /// See <see cref="IndexedMember{T}.IndexedMember(MQ2DataType, string)"/>
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="name"></param>
            public IndexedMember(MQ2DataType owner, string name)
            {
                _owner = owner;
                _name = name;
            }

            /// <summary>
            /// Get the member using an index of type <typeparamref name="TIndex1"/>
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public T1 this[TIndex1 index] => _owner.GetMember<T1>(_name, index.ToString());

            /// <summary>
            /// Get the member using an index of the <typeparamref name="TIndex2"/>
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public T2 this[TIndex2 index] => _owner.GetMember<T2>(_name, index.ToString());
        }
        
        /// <summary>
        /// Helper class to access members with an indexer that return a string type
        /// </summary>
        /// <typeparam name="TIndex"></typeparam>
        public class IndexedStringMember<TIndex>
        {
            // This class is "needed" because IndexedMember<string, int> isn't valid because of the where TIndex : MQ2Type constraint
            private readonly MQ2DataType _owner;
            private readonly string _name;

            /// <summary>
            /// See <see cref="IndexedMember{T}.IndexedMember(MQ2DataType, string)"/>
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="name"></param>
            public IndexedStringMember(MQ2DataType owner, string name)
            {
                _owner = owner;
                _name = name;
            }

            /// <summary>
            /// Get the member using an index
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public string this[TIndex index] => _owner.GetMember<StringType>(_name, index.ToString());
        }

        /// <summary>
        /// Helper class to access members with an indexer that returns a string for one index type and something else for another, e.g. string given an int, or int given a string
        /// </summary>
        /// <typeparam name="TIndex"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TIndex2"></typeparam>
        public class IndexedStringMember<TIndex, T2, TIndex2> where T2 : MQ2DataType
        {
            private readonly MQ2DataType _owner;
            private readonly string _name;

            /// <summary>
            /// See <see cref="IndexedMember{T}.IndexedMember(MQ2DataType, string)"/>
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="name"></param>
            public IndexedStringMember(MQ2DataType owner, string name)
            {
                _owner = owner;
                _name = name;
            }

            /// <summary>
            /// Get the member using an index of type <typeparamref name="TIndex"/>
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public string this[TIndex index] => _owner.GetMember<StringType>(_name, index.ToString());

            /// <summary>
            /// Get the member using an index of the <typeparamref name="TIndex2"/>
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public T2 this[TIndex2 index] => _owner.GetMember<T2>(_name, index.ToString());
        }

        /// <summary>
        /// Helper class to access a member with an indexer, where the indexer is a string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class IndexedMember<T> : IndexedMember<T, string> where T : MQ2DataType
        {
            /// <summary>
            /// See <see cref="IndexedMember{T}.IndexedMember(MQ2DataType, string)"/>
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="name"></param>
            public IndexedMember(MQ2DataType owner, string name) : base(owner, name)
            {
            }
        }
        #endregion
    }
}