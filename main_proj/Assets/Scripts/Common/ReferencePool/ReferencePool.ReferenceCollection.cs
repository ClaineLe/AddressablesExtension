using System.Collections.Generic;

namespace com.halo.framework
{
    namespace common
    {
        /// <summary>
        /// 引用池
        /// </summary>
        public static partial class ReferencePool
        {
            /// <summary>
            /// 引用池合集
            /// </summary>
            private sealed class ReferenceCollection : IPoolInfo
            {
                private readonly Stack<IReference> _freeStack;

                /// <summary>
                /// 名字
                /// </summary>
                public string Name { get; private set; }

                /// <summary>
                /// 当前没有使用的对象总数
                /// </summary>
                public int UnusedCount => _freeStack.Count;
                
                /// <summary>
                 /// 当前使用中的对象总数
                 /// </summary>
                public int UsingCount { get; private set; }

                /// <summary>
                /// 获取对象总次数
                /// </summary>
                public int TakeCount { get; private set; }

                /// <summary>
                /// 归还对象总次数
                /// </summary>
                public int ReturnCount { get; private set; }

                /// <summary>
                /// 创建对象总次数
                /// </summary>
                public int CreateCount { get; private set; }

                /// <summary>
                /// 释放对象总次数
                /// </summary>
                public int ReleaseCount { get; private set; }

                /// <summary>
                /// 构造方法
                /// </summary>
                /// <param name="name"></param>
                public ReferenceCollection(string name)
                {
                    _freeStack = new Stack<IReference>();
                    Name = name;
                    UsingCount = 0;
                    TakeCount = 0;
                    ReturnCount = 0;
                    CreateCount = 0;
                    ReleaseCount = 0;
                }

                /// <summary>
                /// 从引用池获取对象
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <returns></returns>
                public T Take<T>()
                    where T : class, IReference, new()
                {
                    UsingCount++;
                    TakeCount++;
                    lock (_freeStack)
                    {
                        if (_freeStack.Count > 0)
                        {
                            return _freeStack.Pop() as T;
                        }
                    }

                    CreateCount++;
                    return new T();
                }

                /// <summary>
                /// 向对象池归还对象
                /// </summary>
                /// <param name="reference"></param>
                public void Return(IReference reference)
                {
                    reference.OnReferenceClear();
                    lock (_freeStack)
                    {
                        _freeStack.Push(reference);
                    }

                    ReturnCount++;
                    UsingCount--;
                }

                /// <summary>
                /// 清理引用池内对象
                /// </summary>
                public void Clear()
                {
                    lock (_freeStack)
                    {
                        ReleaseCount += _freeStack.Count;
                        _freeStack.Clear();
                    }
                }

                /// <summary>
                /// 释放引用对象
                /// </summary>
                /// <param name="force"></param>
                /// <returns></returns>
                public bool Release(bool force = false)
                {
                    if (this.UsingCount > 0 && force == false)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
    }
}
