using System;
using System.Collections.Generic;

namespace com.halo.framework
{
    namespace common
    {
        /// <summary>
        /// 引用对象池
        /// </summary>
        public static partial class ReferencePool
        {
            /// <summary>
            /// 引用对象池字典缓存
            /// </summary>
            private static readonly Dictionary<Type, ReferenceCollection> _referenceCollections = new Dictionary<Type, ReferenceCollection>();

            /// <summary>
            /// 获取引用池的数量。
            /// </summary>
            public static int mCount
            {
                get
                {
                    return _referenceCollections.Count;
                }
            }

            /// <summary>
            /// 获取所有引用池的信息
            /// </summary>
            /// <returns>所有引用池的信息。</returns>
            public static IPoolInfo[] GetAllReferencePoolInfos()
            {
                int index = 0;
                IPoolInfo[] results = null;

                lock (_referenceCollections)
                {
                    results = new IPoolInfo[_referenceCollections.Count];
                    foreach (KeyValuePair<Type, ReferenceCollection> referenceCollection in _referenceCollections)
                    {
                        results[index++] = referenceCollection.Value;
                    }
                }
                return results;
            }

            /// <summary>
            /// 清除所有引用池
            /// </summary>
            public static void Clear<T>()
            {
                GetReferenceCollection(typeof(T))?.Clear();
            }

            /// <summary>
            /// 从引用池获取引用
            /// </summary>
            /// <typeparam name="T">引用类型。</typeparam>
            /// <returns>引用。</returns>
            public static T Take<T>()
                where T : class, IReference, new()
            {
                System.Type referenceType = typeof(T);
                ReferenceCollection referenceCollection = GetReferenceCollection(referenceType);
                if (referenceCollection == null)
                {
                    CreateReferenceCollection(referenceType);
                    referenceCollection = GetReferenceCollection(referenceType);
                }
                return referenceCollection.Take<T>();
            }

            /// <summary>
            /// 将引用归还引用池
            /// </summary>
            /// <param name="reference">引用。</param>
            public static void Return<T>(T reference)
                where T : class, IReference
            {
                if (reference == null)
                {
                    throw new Exception("Reference is invalid.");
                }

                Type referenceType = reference.GetType();
                GetReferenceCollection(referenceType).Return(reference);
            }

            /// <summary>
            /// 释放对象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="force"></param>
            public static void Release<T>(bool force = false)
            {
                System.Type referenceType = typeof(T);
                ReferenceCollection referenceCollection = GetReferenceCollection(referenceType);
                if (referenceCollection == null)
                    return;
                if (referenceCollection.Release(force))
                {
                    _referenceCollections.Remove(referenceType);
                }
            }

            /// <summary>
            /// 获取对象池合集
            /// </summary>
            /// <param name="referenceType"></param>
            /// <returns></returns>
            private static ReferenceCollection GetReferenceCollection(Type referenceType)
            {
                ReferenceCollection referenceCollection = null;
                lock (_referenceCollections)
                {
                    _referenceCollections.TryGetValue(referenceType, out referenceCollection);
                }
                return referenceCollection;
            }

            /// <summary>
            /// 创建对象池合集
            /// </summary>
            /// <param name="referenceType"></param>
            /// <returns></returns>
            private static bool CreateReferenceCollection(Type referenceType)
            {
                ReferenceCollection referenceCollection = null;
                if (_referenceCollections.TryGetValue(referenceType, out referenceCollection) == true)
                {
                    FrameworkLog.ErrorFormat("对象池已存在. referenceType:" + referenceType);
                    return false;
                }
                referenceCollection = new ReferenceCollection(referenceType.Name);
                _referenceCollections.Add(referenceType, referenceCollection);
                return true;
            }
        }
    }
}
