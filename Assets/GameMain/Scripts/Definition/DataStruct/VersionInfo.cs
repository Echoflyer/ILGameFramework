//-----------------------------------------------------------------------
// <copyright file="VersionInfo.cs" company="Codingworks Game Development">
//     Copyright (c) codingworks. All rights reserved.
// </copyright>
// <author> codingworks </author>
// <time> #CREATETIME# </time>
//-----------------------------------------------------------------------

namespace ILFramework
{
    public class VersionInfo
    {
        /// <summary>
        /// 最新的游戏版本号
        /// </summary>
        public string LatestGameVersion{ get; set; }
        /// <summary>
        /// 最新的资源内部版本号
        /// </summary>
        public int LatestInternalResourceVersion { get; set; }
        /// <summary>
        /// 版本资源列表大小
        /// </summary>
        public int VersionListLength { get; set; }
        /// <summary>
        /// 版本资源列表哈希值
        /// </summary>
        public int VersionListHashCode { get; set; }
        /// <summary>
        /// 版本资源列表压缩后大小
        /// </summary>
        public int VersionListZipLength { get; set; }
        /// <summary>
        /// 版本资源列表压缩后哈希值
        /// </summary>
        public int VersionListZipHashCode { get; set; }
    }
}
