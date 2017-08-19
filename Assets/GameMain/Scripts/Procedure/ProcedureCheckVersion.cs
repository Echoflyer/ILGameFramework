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

namespace ILFramework
{
    public class ProcedureCheckVersion : ProcedureBase
    {
        #region 属性
        private readonly string _VersionFileName = "version.dat"; 
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
                string _fullName = item.FullName.Replace("\\", "/");
                string _destFileName = _fullName.Replace(GameEntry.Resource.ReadOnlyPath,GameEntry.Resource.ReadWritePath);
                if (!File.Exists(_destFileName))
                    Directory.CreateDirectory(_destFileName.Replace(item.Name,""));
                File.Copy(_fullName, _destFileName);
            }
        }
#endregion

    }
}
