using System;
using System.Collections.Generic;
using System.Text;

namespace com.halo.framework
{
    namespace common
    {
        public static partial class Utility
        {
            /// <summary>
            /// 字符相关的实用函数。
            /// </summary>
            public static class Text
            {
                [ThreadStatic]
                private static StringBuilder s_CachedStringBuilder = null;

                /// <summary>
                /// 获取格式化字符串。
                /// </summary>
                /// <param name="format">字符串格式。</param>
                /// <param name="arg0">字符串参数 0。</param>
                /// <returns>格式化后的字符串。</returns>
                public static string Format(string format, object arg0)
                {
                    if (format == null)
                    {
                        throw new Exception("Format is invalid.");
                    }

                    CheckCachedStringBuilder();
                    s_CachedStringBuilder.Length = 0;
                    s_CachedStringBuilder.AppendFormat(format, arg0);
                    return s_CachedStringBuilder.ToString();
                }

                /// <summary>
                /// 获取格式化字符串。
                /// </summary>
                /// <param name="format">字符串格式。</param>
                /// <param name="arg0">字符串参数 0。</param>
                /// <param name="arg1">字符串参数 1。</param>
                /// <returns>格式化后的字符串。</returns>
                public static string Format(string format, object arg0, object arg1)
                {
                    if (format == null)
                    {
                        throw new Exception("Format is invalid.");
                    }

                    CheckCachedStringBuilder();
                    s_CachedStringBuilder.Length = 0;
                    s_CachedStringBuilder.AppendFormat(format, arg0, arg1);
                    return s_CachedStringBuilder.ToString();
                }

                /// <summary>
                /// 获取格式化字符串。
                /// </summary>
                /// <param name="format">字符串格式。</param>
                /// <param name="arg0">字符串参数 0。</param>
                /// <param name="arg1">字符串参数 1。</param>
                /// <param name="arg2">字符串参数 2。</param>
                /// <returns>格式化后的字符串。</returns>
                public static string Format(string format, object arg0, object arg1, object arg2)
                {
                    if (format == null)
                    {
                        throw new Exception("Format is invalid.");
                    }

                    CheckCachedStringBuilder();
                    s_CachedStringBuilder.Length = 0;
                    s_CachedStringBuilder.AppendFormat(format, arg0, arg1, arg2);
                    return s_CachedStringBuilder.ToString();
                }

                /// <summary>
                /// 获取格式化字符串。
                /// </summary>
                /// <param name="format">字符串格式。</param>
                /// <param name="args">字符串参数。</param>
                /// <returns>格式化后的字符串。</returns>
                public static string Format(string format, params object[] args)
                {
                    if (format == null)
                    {
                        throw new Exception("Format is invalid.");
                    }

                    if (args == null)
                    {
                        throw new Exception("Args is invalid.");
                    }

                    CheckCachedStringBuilder();
                    s_CachedStringBuilder.Length = 0;
                    s_CachedStringBuilder.AppendFormat(format, args);
                    return s_CachedStringBuilder.ToString();
                }

                private static void CheckCachedStringBuilder()
                {
                    if (s_CachedStringBuilder == null)
                    {
                        s_CachedStringBuilder = new StringBuilder(1024);
                    }
                }

                /// <summary>
                /// 屏蔽系统emoji转为?
                /// </summary>
                /// <param name="pStr"></param>

                static public string FilterEmoji(string pStr)
                {
                    List<string> patten = new List<string>() { @"\p{Cs}", @"\p{Co}", @"\p{Cn}", @"[\u2702-\u27B0]" };
                    for (int i = 0; i < patten.Count; i++)
                    {
                        pStr = System.Text.RegularExpressions.Regex.Replace(pStr, patten[i], "{}");//屏蔽emoji   
                    }
                    pStr = System.Text.RegularExpressions.Regex.Replace(pStr, "{}{}", "?");
                    return pStr;
                }

                /// <summary>
                /// 下载大小转成容量显示
                /// </summary>
                /// <returns></returns>
                static public string DownloadSize2String(long size)
                {
                    const float average = 1024.0f;
                    float k = size;
                    k /= average;
                    if (k < average)
                    {
                        k = ((int)(k * 100)) / 100.0f;
                        return $"{k}KB";
                    }
                    k /= average;
                    if (k < average)
                    {
                        k = ((int)(k * 100)) / 100.0f;
                        return $"{k}MB";
                    }
                    k /= average;
                    if (k < average)
                    {
                        k = ((int)(k * 100)) / 100.0f;
                        return $"{k}GB";
                    }
                    return size.ToString();
                }
            }
        }
    }
}