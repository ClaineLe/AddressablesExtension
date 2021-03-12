namespace com.halo.framework
{
    namespace common
    {
        /// <summary>
        /// 引用池对象接口
        /// </summary>
        public interface IReference
        {
            /// <summary>
            /// 生命周期 - 清理引用（Return时触发）。
            /// </summary>
            void OnReferenceClear();
        }
    }
}
