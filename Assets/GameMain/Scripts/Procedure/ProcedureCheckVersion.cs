//-----------------------------------------------------------------------
// <copyright file="ProcedureCheckVersion.cs" company="Codingworks Game Development">
//     Copyright (c) codingworks. All rights reserved.
// </copyright>
// <author> codingworks </author>
// <time> #CREATETIME# </time>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameFramework.Fsm;
using System.IO;
using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using System.Xml;

namespace ILFramework
{
    public class ProcedureCheckVersion : ProcedureBase
    {
        #region 属性
        private readonly string _VersionFileName = "version.dat";
        private readonly string _VersionFileUrl = "GameResourceVersion.xml";
        #endregion

        #region 重写函数
        //
        // 摘要:
        //     /// 状态初始化时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
        }
        //
        // 摘要:
        //     /// 状态销毁时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
        //
        // 摘要:
        //     /// 进入状态时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.WebRequestSuccess, OnWebRequestSuccess);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.WebRequestFailure, OnWebRequestFailure);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.VersionListUpdateSuccess, OnVersionListUpdateSuccess);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.VersionListUpdateFailure, OnVersionListUpdateFailure);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.ResourceCheckComplete, OnResourceCheckComplete);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.ResourceUpdateStart, OnResourceUpdateStart);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.ResourceUpdateFailure, OnResourceUpdateFailure);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.ResourceUpdateSuccess, OnResourceUpdateSuccess);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.ResourceUpdateAllComplete, OnResourceUpdateAllComplete);
            
            CheckLocalFiles();
            CheckRemoteVersion();
        }
        
        //
        // 摘要:
        //     /// 离开状态时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        //
        //   isShutdown:
        //     是否是关闭状态机时触发。
        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.WebRequestSuccess, OnWebRequestSuccess);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.WebRequestFailure, OnWebRequestFailure);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.VersionListUpdateSuccess, OnVersionListUpdateSuccess);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.VersionListUpdateFailure, OnVersionListUpdateFailure);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.ResourceCheckComplete, OnResourceCheckComplete);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.ResourceUpdateStart, OnResourceUpdateStart);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.ResourceUpdateFailure, OnResourceUpdateFailure);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.ResourceUpdateSuccess, OnResourceUpdateSuccess);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.ResourceUpdateAllComplete, OnResourceUpdateAllComplete);
            
            base.OnLeave(procedureOwner, isShutdown);
        }
        //
        // 摘要:
        //     /// 状态轮询时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        //
        //   elapseSeconds:
        //     逻辑流逝时间，以秒为单位。
        //
        //   realElapseSeconds:
        //     真实流逝时间，以秒为单位。
        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
        }
        #endregion

        #region 事件回调
        private void OnWebRequestSuccess(object sender, GameEventArgs e)
        {
            WebRequestSuccessEventArgs _ne = (WebRequestSuccessEventArgs)e;
            if (_ne.UserData != this)
            {
                return;
            }
            //解析xml
            string _responseXml = Utility.Converter.GetString(_ne.GetWebResponseBytes());
            VersionInfo _versionInfo= GetVersionInfoFromXml(_responseXml);
            if (_versionInfo == null)
                return;
            //检查版本
            if (GameEntry.Resource.CheckVersionList(_versionInfo.LatestInternalResourceVersion)
            == GameFramework.Resource.CheckVersionListResult.NeedUpdate)
            {
                //不同版本 资源不同
                GameEntry.Resource.UpdatePrefixUri += "/" + _versionInfo.Path;
                GameEntry.Resource.UpdateVersionList(_versionInfo.VersionListLength, _versionInfo.VersionListHashCode, _versionInfo.VersionListZipLength, _versionInfo.VersionListZipHashCode);
            }
            else
            {
                Log.Debug("已是最新版本");
                GameEntry.Resource.CheckResources();
            }
        }
        private void OnWebRequestFailure(object sender, GameEventArgs e)
        {
            WebRequestFailureEventArgs ne = (WebRequestFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            Log.Error("web请求失败");
        }
        private void OnVersionListUpdateSuccess(object sender, GameEventArgs e)
        {
            //检查资源
            GameEntry.Resource.CheckResources();
        }
        private void OnVersionListUpdateFailure(object sender, GameEventArgs e)
        {
            Log.Error("版本检查失败，请检查网络");
        }
        private void OnResourceCheckComplete(object sender,GameEventArgs e)
        {
            Log.Debug("资源检查完毕--更新资源");
            ResourceCheckCompleteEventArgs _ne = (ResourceCheckCompleteEventArgs)e;
            //更新资源
            if (_ne.UpdateCount > 0)
                GameEntry.Resource.UpdateResources();
            else
            {
                //切换下一个状态
            }
        }
        private void OnResourceUpdateStart(object sender,GameEventArgs e)
        {    
          //  Log.Debug("开始--更新资源");
        }
        private void OnResourceUpdateFailure(object sender,GameEventArgs e)
        {    
          //  Log.Debug("OnResourceUpdateFailure--更新资源");    
        }
        private void OnResourceUpdateSuccess(object sender,GameEventArgs e)
        {
        //    Log.Debug("OnResourceUpdateSuccess--更新资源");    
        }
        private void OnResourceUpdateAllComplete(object sender,GameEventArgs e)
        {
            Log.Debug("所有资源更新完毕-->再检查资源");
            GameEntry.Resource.CheckResources();
        }
        #endregion

        #region 检查本地文件
        void CheckLocalFiles()
        {
            if (!Directory.Exists(GameEntry.Resource.ReadOnlyPath))
            {
                Log.Warning("不存在可读路径:"+GameEntry.Resource.ReadOnlyPath);
                return;
            }
            //检查本地读写文件是否有文件
            if (File.Exists(GameEntry.Resource.ReadWritePath + "/" + _VersionFileName))
                return;
          
            //复制所有文件从可读路径到读写路径
            DirectoryInfo _readFolder = new DirectoryInfo(GameEntry.Resource.ReadOnlyPath);
            FileInfo[] _files = _readFolder.GetFiles("*", SearchOption.AllDirectories);
            foreach (var item in _files)
            {
                if (item.FullName.EndsWith(".meta"))
                    continue;
                     Log.Debug("开始复制文件："+item.Name);
                string _fullName = item.FullName.Replace("\\", "/");
                string _destFileName = _fullName.Replace(GameEntry.Resource.ReadOnlyPath,GameEntry.Resource.ReadWritePath);
                string _destFolder = _destFileName.Replace(item.Name, "");
                if (!Directory.Exists(_destFolder))
                    Directory.CreateDirectory(_destFolder);
                File.Copy(_fullName, _destFileName);
            }
        }
        #endregion

#region 检查远程版本
        void CheckRemoteVersion()
        {
            GameEntry.WebRequest.AddWebRequest(GameEntry.Resource.UpdatePrefixUri+"/"+_VersionFileUrl,this);
         //   GameEntry.Resource.url
        }
#endregion

#region 解析xml获取最新的版本资源信息
       VersionInfo GetVersionInfoFromXml(string _xml)
        {
            VersionInfo _info = new VersionInfo();
            _info.Path="windows";

            string _nodeName="StandaloneWindows";
            switch(Application.platform)
            {
                case RuntimePlatform.Android:
                _nodeName="Android";
                _info.Path="android";
                break;
                case RuntimePlatform.IPhonePlayer:
                 _nodeName="IOS";
                 _info.Path="ios";
                break;
                default:
                break;
            }
            try
            {
                XmlDocument _doc = new XmlDocument();
                _doc.LoadXml(_xml);
                XmlElement _versionNode = _doc.DocumentElement;
                //游戏版本号
                _info.LatestGameVersion = _versionNode.GetAttribute("ApplicableGameVersion");
                //资源版本号
                _info.LatestInternalResourceVersion = int.Parse(_versionNode.GetAttribute("LatestInternalResourceVersion"));
                XmlElement _nodePlatform = (XmlElement)_versionNode.SelectSingleNode(_nodeName);
                _info.VersionListHashCode = int.Parse(_nodePlatform.GetAttribute("HashCode"));
                _info.VersionListLength = int.Parse(_nodePlatform.GetAttribute("Length"));
                _info.VersionListZipHashCode = int.Parse(_nodePlatform.GetAttribute("ZipHashCode"));
                _info.VersionListZipLength = int.Parse(_nodePlatform.GetAttribute("ZipLength"));
                return _info;
            }
            catch (System.Xml.XmlException e)
            {
                Log.Error("xml解析失败:" + e.ToString());
                return null;
            }
        }
#endregion

    }
}
