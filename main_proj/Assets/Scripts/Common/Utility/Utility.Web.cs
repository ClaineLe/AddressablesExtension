using com.halo.framework.runtime;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace com.halo.framework
{
    namespace common
    {
        public static partial class Utility
        {

            public class Web
            {
                /// <summary>
                /// 错误处理
                /// </summary>
                /// <param name="pUWR"></param>
                /// <returns></returns>
                private static FrameworkStateCode ErrorHandle(UnityWebRequest pUWR)
                {
                    var code = FrameworkStateCode.Succeed;
                    if (pUWR.isNetworkError || pUWR.isHttpError)
                    {
                        FrameworkLog.Error(
                            $"HttpGet网络错误\nErrorMsg:{pUWR.error} Type:{pUWR} isNetworkError:{pUWR.isNetworkError} isHttpError:{pUWR.isHttpError}\nurl:{pUWR}");
                        code = FrameworkStateCode.Error;
                        return code;
                    }
                    if (!pUWR.isDone)
                    {
                        FrameworkLog.Error($"HttpGet访问超时\nErrorMsg:{pUWR.error} Type:{pUWR} isDone:{pUWR.isDone} \nurl:{pUWR.url}");
                        code = FrameworkStateCode.TimeOut;
                        return code;
                    }
                    return code;
                }

                /// <summary>
                /// Get方式访问http
                /// </summary>
                /// <param name="pUrl"></param>
                /// <param name="pTimeout"></param>
                /// <param name="pCallback"></param>
                public static async Task<FrameworkStateCode> Get(string pUrl, int pTimeout, Action<FrameworkStateCode> pCallback)
                {
                    var uwr = UnityWebRequest.Get(pUrl);
                    uwr.timeout = pTimeout;
                    await uwr.SendWebRequest();
                    var code = ErrorHandle(uwr);
                    pCallback?.Invoke(code);
                    uwr.Dispose();
                    return code;
                }

                /// <summary>
                /// Get方式访问http
                /// </summary>
                /// <param name="pUrl"></param>
                /// <param name="pType">传类型的时候会序列化成该类型的对象 传byte[]或者string会返回对应对象 传其他则会走json反序列化</param>
                /// <param name="pCallback"></param>
                public static async Task<FrameworkStateCode> Get(string pUrl, int pTimeout, Type pType, Action<FrameworkStateCode, object> pCallback)
                {
                    var uwr = UnityWebRequest.Get(pUrl);
                    uwr.downloadHandler = new DownloadHandlerBuffer();
                    uwr.timeout = pTimeout;
                    await uwr.SendWebRequest();
                    var code = ErrorHandle(uwr);
                    if (code != FrameworkStateCode.Succeed)
                    {
                        pCallback?.Invoke(code, null);
                        uwr.Dispose();
                        return code;
                    }
                    if (pType == typeof(byte[]))
                    {
                        pCallback?.Invoke(code, uwr.downloadHandler.data);
                        uwr.Dispose();
                        return code;
                    }
                    if (pType == typeof(string))
                    {
                        pCallback?.Invoke(code, uwr.downloadHandler.text);
                        uwr.Dispose();
                        return code;
                    }
                    if (pType != null)
                    {
                        var content = uwr.downloadHandler.text;
                        var jsonObj = JsonUtility.FromJson(content, pType);
                        pCallback?.Invoke(code, jsonObj);
                        uwr.Dispose();
                        return code;
                    }
                    pCallback?.Invoke(code, uwr.downloadHandler.data);
                    uwr.Dispose();
                    return code;
                }

                /// <summary>
                /// Post方式访问http
                /// </summary>
                /// <param name="pUrl"></param>
                /// <param name="pTimeout"></param>
                /// <param name="pCallback"></param>
                public static async Task<FrameworkStateCode> Post(string pUrl, Dictionary<string, string> pFormDataDict, int pTimeout, Action<FrameworkStateCode> pCallback)
                {
                    var uwr = UnityWebRequest.Post(pUrl, pFormDataDict);
                    uwr.timeout = pTimeout;
                    await uwr.SendWebRequest();
                    var code = ErrorHandle(uwr);
                    pCallback?.Invoke(code);
                    uwr.Dispose();
                    return code;
                }

                /// <summary>
                /// Post方式访问
                /// </summary>
                /// <param name="pUrl"></param>
                /// <param name="pFormDataDict"></param>
                /// <param name="pTimeout"></param>
                /// <param name="type">传类型的时候会序列化成该类型的对象 传byte[]或者string会返回对应对象 传其他则会走json反序列化</param>
                /// <param name="pCallback"></param>
                public static async Task<FrameworkStateCode> Post(string pUrl, Dictionary<string, string> pFormDataDict, int pTimeout, Type type, Action<FrameworkStateCode, object> pCallback)
                {
                    var uwr = UnityWebRequest.Post(pUrl, pFormDataDict);
                    uwr.timeout = pTimeout;
                    await uwr.SendWebRequest();
                    var code = ErrorHandle(uwr);
                    if (code != FrameworkStateCode.Succeed)
                    {
                        pCallback?.Invoke(code, null);
                        uwr.Dispose();
                        return code;
                    }
                    if (type == typeof(byte[]))
                    {
                        pCallback?.Invoke(code, uwr.downloadHandler.data);
                        uwr.Dispose();
                        return code;
                    }
                    if (type == typeof(string))
                    {
                        pCallback?.Invoke(code, uwr.downloadHandler.text);
                        uwr.Dispose();
                        return code;
                    }
                    var content = uwr.downloadHandler.text;
                    var jsonObj = JsonUtility.FromJson(content, type);
                    pCallback?.Invoke(code, jsonObj);
                    uwr.Dispose();
                    return code;
                }
            }
        }
    }
}