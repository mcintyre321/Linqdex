using System;
using System.Runtime.CompilerServices;

namespace Linqdex
{
    public static class KeyExtension
    {
        static ConditionalWeakTable<object, string> KeyTable = new ConditionalWeakTable<object, string>();
        internal static string Key(this object obj)
        {
            return KeyTable.GetValue(obj, o => Guid.NewGuid().ToString());
        }
        internal static string Key(this object obj, string key)
        {
            return KeyTable.GetValue(obj, o => key);
        } 
    }
}