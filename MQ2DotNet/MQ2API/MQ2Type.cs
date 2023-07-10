using JetBrains.Annotations;
using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;

namespace MQ2DotNet.MQ2API
{
    [PublicAPI]
    public class MQ2Type
    {
        #region Cache members, not sure if this is a good idea... So far not a good idea from testing, strange behaviour with StringType, but might be just strings dont like lazy loading, because of shared temp var in MQ2 client.
        public static readonly bool IsMemberCachingEnabled = false;

        private struct MemberKey
        {
            public IntPtr IntPtr;
            public string Name;
            public string Index;

            public MemberKey(IntPtr intPtr, string name, string index)
            {
                IntPtr = intPtr;
                Name = name;
                Index = index;
            }

            public override bool Equals(object obj)
            {
                MemberKey memberKey = (MemberKey)obj;

                return IntPtr == memberKey.IntPtr &&
                    string.Compare(Name, memberKey.Name) == 0 &&
                    string.Compare(Index, memberKey.Index) == 0;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        private readonly ConcurrentDictionary<MemberKey, MQ2TypeVar> _members;
        #endregion

        public IntPtr IntPtr { get; }

        public MQ2Type(IntPtr intPtr)
        {
            if (intPtr == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(intPtr));
            }

            IntPtr = intPtr;

            _members = new ConcurrentDictionary<MemberKey, MQ2TypeVar>();
        }

        public bool TryGetMember(MQ2VarPtr varPtr, string memberName, string index, out MQ2TypeVar typeVar)
        {
            if (IntPtr == IntPtr.Zero)
            {
                typeVar = null;

                return false;
            }

            MemberKey memberKey = new MemberKey(IntPtr, memberName, index);

            if (!_members.TryGetValue(memberKey, out typeVar))
            {
                if (!NativeMethods.MQ2Type__GetMember(IntPtr, varPtr.VarPtr, memberName, index, out var nativeTypeVar))
                {
                    typeVar = null;

                    return false;
                }

                typeVar = new MQ2TypeVar(nativeTypeVar);

                if (IsMemberCachingEnabled)
                {
                    _members.TryAdd(memberKey, typeVar);
                }
            }

            return true;
        }

        public string ToString(MQ2VarPtr varPtr)
        {
            if (IntPtr == IntPtr.Zero)
            {
                return ToString();
            }

            var stringBuilder = new StringBuilder(2048);

            if (!NativeMethods.MQ2Type__ToString(IntPtr, varPtr.VarPtr, stringBuilder))
            {
                return ToString();
            }

            return stringBuilder.ToString();
        }

        private static class NativeMethods
        {
            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQ2Type__FromData(IntPtr type, out NativeMQ2VarPtr varPtr, ref NativeMQ2TypeVar typeVar);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQ2Type__FromString(IntPtr type, out NativeMQ2VarPtr varPtr, [MarshalAs(UnmanagedType.LPStr)] string source);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQ2Type__InitVariable(IntPtr type, out NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQ2Type__FreeVariable(IntPtr type, in NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQ2Type__GetMember(IntPtr type, in NativeMQ2VarPtr varPtr, [MarshalAs(UnmanagedType.LPStr)] string memberName, [MarshalAs(UnmanagedType.LPStr)] string index, out NativeMQ2TypeVar typeVar);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQ2Type__ToString(IntPtr type, in NativeMQ2VarPtr varPtr, [MarshalAs(UnmanagedType.LPStr)] StringBuilder destination);
        }
    }
}
