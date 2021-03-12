using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace com.halo.framework
{
    namespace common
    {
        static public class AsyncOperationExtension
        {
            /// <summary>
            /// 获取异步等待器
            /// </summary>
            /// <param name="asyncOp"></param>
            /// <returns></returns>
            public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
            {
                var tcs = new TaskCompletionSource<object>();
                asyncOp.completed += obj => { tcs.SetResult(null); };
                return ((Task)tcs.Task).GetAwaiter();
            }
        }
    }
}