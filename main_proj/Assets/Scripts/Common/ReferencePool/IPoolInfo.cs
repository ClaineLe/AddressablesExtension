namespace com.halo.framework
{
    namespace common
    {
        /// <summary>
        /// 对象池信息接口
        /// </summary>
        public interface IPoolInfo
        {
            /// <summary>
            /// 名字
            /// </summary>
            string Name { get; }

            /// <summary>
            /// 当前没有使用的对象总数
            /// </summary>
            int UnusedCount { get; }

            /// <summary>
            /// 当前使用中的对象总数
            /// </summary>
            int UsingCount { get; }

            /// <summary>
            /// 获取对象总次数
            /// </summary>
            int TakeCount { get; }

            /// <summary>
            /// 归还对象总次数
            /// </summary>
            int ReturnCount { get; }

            /// <summary>
            /// 创建对象总次数
            /// </summary>
            int CreateCount { get; }

            /// <summary>
            /// 释放对象总次数
            /// </summary>
            int ReleaseCount { get; }
        }
    }
}