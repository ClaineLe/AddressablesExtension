namespace com.halo.framework
{
    namespace runtime
    {
        /// <summary>
        /// 管理器接口
        /// </summary>
        public interface IManager
        {
            /// <summary>
            /// 管理器名字
            /// </summary>
            string mName { get; }

            /// <summary>
            /// 初始化
            /// </summary>
            /// <param name="framework">框架实例对象</param>
            void Initialization();
            //void Initialization(CoreFramework framework);

            /// <summary>
            /// 预加载
            /// </summary>
            void Preload();

            /// <summary>
            /// 获取预加载进度(0.0-1.0)
            /// </summary>
            /// <returns></returns>
            /// 
            float GetPreloadProgress();

            /// <summary>
            /// 获取预加载是否完成
            /// </summary>
            /// <returns></returns>
            bool IsPreloadDone();

            /// <summary>
            /// 预加载完成
            /// </summary>
            void PreloadCompleted();

            /// <summary>
            /// 每帧更新（Update方法的包装）
            /// </summary>
            /// <param name="frameCount"></param>
            /// <param name="time"></param>
            /// <param name="deltaTime"></param>
            /// <param name="unscaledTime"></param>
            /// <param name="realElapseSeconds"></param>
            void Tick(int frameCount, float time, float deltaTime, float unscaledTime, float realElapseSeconds);

            /// <summary>
            /// 释放
            /// </summary>
            void Release();

            /// <summary>
            /// 是否释放完成
            /// </summary>
            /// <returns></returns>
            bool IsReleaseDone();

            /// <summary>
            /// 获取释放进度
            /// </summary>
            /// <returns></returns>
            float GetReleaseProgress();

            /// <summary>
            /// 释放完成
            /// </summary>
            void ReleaseCompleted();
        }
    }
}