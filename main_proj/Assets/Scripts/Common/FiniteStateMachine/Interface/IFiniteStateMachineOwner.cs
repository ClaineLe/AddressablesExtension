namespace com.halo.framework
{
    namespace common
    {
        /// <summary>
        /// 有限状态机持有者接口
        /// </summary>
        public interface IFiniteStateMachineOwner
        {
            /// <summary>
            /// 每帧更新
            /// </summary>
            /// <param name="frameCount"></param>
            /// <param name="time"></param>
            /// <param name="deltaTime"></param>
            /// <param name="unscaledTime"></param>
            /// <param name="realElapseSeconds"></param>
            void Tick(int frameCount, float time, float deltaTime, float unscaledTime, float realElapseSeconds);
        }
    }
}