namespace com.halo.framework
{
    namespace runtime
    {
        /// <summary>
        /// 管理器基类
        /// </summary>
        public abstract class BaseManager : IManager
        {
            /// <summary>
            /// 核心框架实例
            /// </summary>
            //public CoreFramework mFramework { get; private set; }

            /// <summary>
            /// 管理器名字
            /// </summary>
            public virtual string mName => this.GetType().Name;

            /// <summary>
            /// 管理器是否释放完成
            /// </summary>
            protected bool isReleaseDone { get; set; }

            /// <summary>
            /// 生命周期 - 初始化时
            /// </summary>
            protected abstract void onInitialization();

            /// <summary>
            /// 生命周期 - 每帧更新时
            /// </summary>
            /// <param name="frameCount"></param>
            /// <param name="time"></param>
            /// <param name="deltaTime"></param>
            /// <param name="unscaledTime"></param>
            /// <param name="realElapseSeconds"></param>
            protected virtual void onTick(int frameCount, float time, float deltaTime, float unscaledTime, float realElapseSeconds) { }

            /// <summary>
            /// 生命周期 - 开始预加载时
            /// </summary>
            protected virtual void onPreload() { }

            /// <summary>
            /// 生命周期 - 预加载完成时
            /// </summary>
            protected virtual void onPreloadCompleted() { }

            /// <summary>
            /// 生命周期 - 开始释放时
            /// </summary>
            protected virtual void onRelease() { }

            /// <summary>
            /// 生命周期 - 释放完成时
            /// </summary>
            protected virtual void onReleaseCompleted() { }

            /// <summary>
            /// 预加载
            /// </summary>
            public void Preload()
            {
                this.onPreload();
            }

            /// <summary>
            /// 获取预加载进度(0.0-1.0)
            /// </summary>
            /// <returns></returns>
            public virtual float GetPreloadProgress() { return 1.0f; }

            /// <summary>
            /// 获取预加载是否完成
            /// </summary>
            /// <returns></returns>
            public virtual bool IsPreloadDone() { return true; }

            /// <summary>
            /// 初始化
            /// </summary>
            /// <param name="framework">框架实例对象</param>
            public void Initialization()
            //public void Initialization(CoreFramework framework)
            {
                //this.mFramework = framework;
                this.onInitialization();
            }

            /// <summary>
            /// 每帧更新（Update方法的包装）
            /// </summary>
            /// <param name="frameCount"></param>
            /// <param name="time"></param>
            /// <param name="deltaTime"></param>
            /// <param name="unscaledTime"></param>
            /// <param name="realElapseSeconds"></param>
            public void Tick(int frameCount, float time, float deltaTime, float unscaledTime, float realElapseSeconds)
            {
                this.onTick(frameCount, time, deltaTime, unscaledTime, realElapseSeconds);
            }

            /// <summary>
            /// 预加载完成
            /// </summary>
            public void PreloadCompleted()
            {
                this.onPreloadCompleted();
            }

            /// <summary>
            /// 释放
            /// </summary>
            public void Release()
            {
                this.onRelease();
            }

            /// <summary>
            /// 获取释放进度
            /// </summary>
            /// <returns></returns>
            public virtual float GetReleaseProgress()
            {
                this.isReleaseDone = true;
                return 1.0f;
            }

            /// <summary>
            /// 是否释放完成
            /// </summary>
            /// <returns></returns>
            public bool IsReleaseDone()
            {
                return this.isReleaseDone;
            }

            /// <summary>
            /// 释放完成
            /// </summary>
            public void ReleaseCompleted()
            {
                this.onReleaseCompleted();
            }
        }
    }
}
