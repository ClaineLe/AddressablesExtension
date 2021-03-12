namespace com.halo.framework
{
    namespace common
    {
        /// <summary>
        /// 状态机状态基类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public abstract class BaseState<T> : IState<T> where T : class, IFiniteStateMachineOwner
        {
            /// <summary>
            /// 名字
            /// </summary>
            public string Name => this.GetType().Name;

            /// <summary>
            /// 生命周期 - 初始化时
            /// </summary>
            /// <param name="owner"></param>
            protected virtual void onInit(T owner) { }

            /// <summary>
            /// 生命周期 - 进入状态时
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="fromState"></param>
            /// <param name="userData"></param>
            protected virtual void onEnter(T owner, IState<T> fromState, object userData) { }

            /// <summary>
            /// 生命周期 - 每帧更新时
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="frameCount"></param>
            /// <param name="time"></param>
            /// <param name="deltaTime"></param>
            /// <param name="unscaledTime"></param>
            /// <param name="realElapseSeconds"></param>
            protected abstract void onTick(T owner, int frameCount, float time, float deltaTime, float unscaledTime, float realElapseSeconds);
            
            /// <summary>
            /// 生命周期 - 退出状态时
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="toState"></param>
            protected virtual void onExit(T owner, IState<T> toState) { }

            /// <summary>
            /// 初始化
            /// </summary>
            /// <param name="owner"></param>
            public void Init(T owner)
            {
                onInit(owner);
            }

            /// <summary>
            /// 进入状态
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="fromState"></param>
            /// <param name="userData"></param>
            public void Enter(T owner, IState<T> fromState, object userData)
            {
                onEnter(owner, fromState, userData);
            }

            /// <summary>
            /// 每帧更新
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="frameCount"></param>
            /// <param name="time"></param>
            /// <param name="deltaTime"></param>
            /// <param name="unscaledTime"></param>
            /// <param name="realElapseSeconds"></param>
            public void Tick(T owner, int frameCount, float time, float deltaTime, float unscaledTime, float realElapseSeconds)
            {
                onTick(owner, frameCount, time, deltaTime, unscaledTime, realElapseSeconds);
            }

            /// <summary>
            /// 退出状态
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="toState"></param>
            public void Exit(T owner, IState<T> toState)
            {
                onExit(owner, toState);
            }
        }
    }
}
