namespace com.halo.framework
{
    namespace runtime
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public enum FrameworkStateCode
        {
            /// <summary>
            /// 成功
            /// </summary>
            Succeed = 0,

            /// <summary>
            /// 完成
            /// </summary>
            Finished = 1,

            /// <summary>
            /// 无需更新
            /// </summary>
            NoUpdate = 65511,

            /// <summary>
            /// 需要更新
            /// </summary>
            NeedUpdate = 65512,

            /// <summary>
            /// 当前版本库为空
            /// </summary>
            SelfRepositoryInfoNull = 65513,

            /// <summary>
            /// 版本库信息列表为空
            /// </summary>
            RepositoryInfoListNull = 65514,

            /// <summary>
            /// 底包版本低
            /// </summary>
            BaseVersionIsLow = 65517,

            /// <summary>
            /// 文件未找到
            /// </summary>
            FileNotFound = 65518,

            /// <summary>
            /// 加密错误
            /// </summary>
            EncryptionError = 65519,

            /// <summary>
            /// 超时
            /// </summary>
            TimeOut = 65520,

            /// <summary>
            /// 非法路径
            /// </summary>
            IllegalPath = 65521,

            /// <summary>
            /// 网络错误
            /// </summary>
            NetworkError = 65522,

            /// <summary>
            /// 平台错误
            /// </summary>
            PlatformError = 65523,

            /// <summary>
            /// 上传报错
            /// </summary>
            UploadError = 65524,

            /// <summary>
            /// 复制最新版本的Bundle至StreamingAsset错误
            /// </summary>
            CopyBundleToStreamingAssetError = 65526,

            /// <summary>
            /// 存在相同的版本库
            /// </summary>
            SameRepositoryExits = 65527,

            /// <summary>
            /// 移动文件夹时出错
            /// </summary>
            MoveDirError = 65528,

            /// <summary>
            /// 文件夹不存在
            /// </summary>
            DirectoryNotFound = 65529,

            /// <summary>
            /// 文件读取错误
            /// </summary>
            ReadFileError = 65530,

            /// <summary>
            /// 匹配包体信息报错
            /// </summary>
            MatchPackageInfoByChannel = 65531,

            /// <summary>
            /// 解析报错
            /// </summary>
            ParseJsonError = 65532,

            /// <summary>
            /// 下载报错
            /// </summary>
            DownloadError = 65533,

            /// <summary>
            /// 无任务
            /// </summary>
            NoneTask = 65534,

            /// <summary>
            /// 错误
            /// </summary>
            Error = 65535,
        }
    }
}
