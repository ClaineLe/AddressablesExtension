namespace com.halo.framework
{
    namespace common
    {
        /// <summary>
        /// 有限状态机的状态对象接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface IState<T>
            where T : class, IFiniteStateMachineOwner
        {
            /// <summary>
            /// 名字
            /// </summary>
            string Name { get; }

            /// <summary>
            /// 初始化
            /// </summary>
            /// <param name="onwer"></param>
            void Init(T onwer);

            /// <summary>
            /// 进入状态
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="fromState"></param>
            /// <param name="userData"></param>
            void Enter(T owner, IState<T> fromState, object userData);

            /// <summary>
            /// 每帧更新
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="frameCount"></param>
            /// <param name="time"></param>
            /// <param name="deltaTime"></param>
            /// <param name="unscaledTime"></param>
            /// <param name="realElapseSeconds"></param>
            void Tick(T owner, int frameCount, float time, float deltaTime, float unscaledTime, float realElapseSeconds);

            /// <summary>
            /// 退出状态
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="toState"></param>
            void Exit(T owner, IState<T> toState);
        }
    }
}