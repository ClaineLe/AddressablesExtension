namespace com.halo.framework 
{
    namespace common
    {
        /// <summary>
        /// 框架日志
        /// </summary>
        public class FrameworkLog
        {
            /// <summary>
            /// 输出日志信息
            /// </summary>
            /// <param name="message"></param>
            static public void Info(object message)
            {
                UnityEngine.Debug.Log(message);
            }
            
            /// <summary>
            /// 输出日志信息（带格式）
            /// </summary>
            /// <param name="message"></param>
            /// <param name="args"></param>
            static public void InfoFormat(string message, params object[] args)
            {
                Info(Utility.Text.Format(message, args));
            }

            /// <summary>
            /// 输出调试信息
            /// </summary>
            /// <param name="message"></param>
            static public void Debug(object message)
            {
                UnityEngine.Debug.Log(message);
            }
            /// <summary>
            /// 输出调试信息（带格式）
            /// </summary>
            /// <param name="message"></param>
            /// <param name="args"></param>
            static public void DebugFormat(string message, params object[] args)
            {
                Debug(Utility.Text.Format(message, args));
            }

            /// <summary>
            /// 输出警告信息
            /// </summary>
            /// <param name="message"></param>
            static public void Warn(object message)
            {
                UnityEngine.Debug.LogWarning(message);

            }
            
            /// <summary>
            /// 输出警告信息（带格式）
            /// </summary>
            /// <param name="message"></param>
            /// <param name="args"></param>
            static public void WarnFormat(string message, params object[] args)
            {
                Warn(Utility.Text.Format(message, args));
            }

            /// <summary>
            /// 输出错误信息
            /// </summary>
            /// <param name="message"></param>
            static public void Error(object message)
            {
                UnityEngine.Debug.LogError(message);
            }

            /// <summary>
            /// 输出错误信息（带格式）
            /// </summary>
            /// <param name="message"></param>
            /// <param name="args"></param>
            static public void ErrorFormat(string message, params object[] args)
            {
                Error(Utility.Text.Format(message, args));
            }
        }
    }
}