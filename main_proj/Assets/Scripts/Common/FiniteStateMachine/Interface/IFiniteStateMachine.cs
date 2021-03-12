namespace com.halo.framework
{
    namespace common
    {
        /// <summary>
        /// 有限状态机接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface IFiniteStateMachine<T>
            where T : class, IFiniteStateMachineOwner
        {
            /// <summary>
            /// 状态机持有对象
            /// </summary>
            T mOwner { get; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="frameCount"></param>
            /// <param name="time"></param>
            /// <param name="deltaTime"></param>
            /// <param name="unscaledTime"></param>
            /// <param name="realElapseSeconds"></param>
            void Tick(int frameCount, float time, float deltaTime, float unscaledTime, float realElapseSeconds);
            
            /// <summary>
            /// 是否能切换状态
            /// </summary>
            /// <returns></returns>
            bool CanSwitch();
            
            /// <summary>
            /// 切换状态
            /// </summary>
            /// <typeparam name="IS"></typeparam>
            /// <param name="userData"></param>
            /// <returns></returns>
            bool Switch<IS>(object userData) where IS : class, IState<T>;
            
            /// <summary>
            /// 切换状态
            /// </summary>
            /// <param name="stateName"></param>
            /// <param name="userData"></param>
            /// <returns></returns>
            bool Switch(string stateName, object userData);
            
            /// <summary>
            /// 是否存在状态
            /// </summary>
            /// <typeparam name="IS"></typeparam>
            /// <returns></returns>
            bool HasState<IS>() where IS : class, IState<T>;

            /// <summary>
            /// 是否存在状态
            /// </summary>
            /// <param name="stateName"></param>
            /// <returns></returns>
            bool HasState(string stateName);
            
            /// <summary>
            /// 添加状态
            /// </summary>
            /// <typeparam name="IS"></typeparam>
            /// <returns></returns>
            IS AddState<IS>() where IS : class, IState<T>, new();
        }
    }
}
