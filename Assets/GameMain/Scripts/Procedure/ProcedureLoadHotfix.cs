//-----------------------------------------------------------------------
// <copyright file="ProcedureLoadHotfix.cs" company="Codingworks Game Development">
//     Copyright (c) codingworks. All rights reserved.
// </copyright>
// <author> codingworks </author>
// <time> #CREATETIME# </time>
//-----------------------------------------------------------------------

using UnityEngine;
using GameFramework.Procedure;
using GameFramework.Fsm;
using System.IO;
using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using GameFramework.Resource;

namespace ILFramework
{
    public class ProcedureLoadHotfix : ProcedureBase
    {
        #region 属性
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
            
            //加载热更新资源
            LoadHatfixAsset();
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
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }
        #endregion

#region 加载热更新
        private void LoadHatfixAsset()
        {
#if UNITY_EDITOR
            TextAsset _dllAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(AssetUtility.GetILRuntimeDllAsset("Hotfix"));
            TextAsset _pdbAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(AssetUtility.GetILRuntimePdbAsset("Hotfix"));
            GameEntry._ILRuntime.LoadHotFixAssembly(_dllAsset.bytes, _pdbAsset.bytes);
            LoadHatfixProcedure();
//#elif DEVELOPMENT_BUILD
#else
             GameEntry.Resource.LoadAsset(AssetUtility.GetILRuntimeDllAsset("Hotfix"),new LoadAssetCallbacks(LoadAssetSuccessCallback, LoadAssetFailureCallback));
#endif
        }
        private void LoadAssetSuccessCallback(string assetName, object asset, float duration, object userData)
        {
            if (asset == null)
                return;
            TextAsset _dllAsset = asset as TextAsset;
            GameEntry._ILRuntime.LoadHotFixAssembly(_dllAsset.bytes,null);
            Log.Debug("热更新脚本加载完毕");
            LoadHatfixProcedure();
        }
        private void LoadAssetFailureCallback(string assetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            Log.Error("热更新资源加载失败:" + assetName);
        }
#endregion

#region 增加流程
        private void LoadHatfixProcedure()
        {
            ProcedureBase _hotfixProcedure = GameEntry._ILRuntime._AppDomain.Instantiate<ProcedureBase>("Hotfix.ProcedureHotfixStart", null);
            GameEntry.Procedure.HotfixProcedure(new ProcedureBase[] { _hotfixProcedure }, _hotfixProcedure);
        }
#endregion

    }
}
