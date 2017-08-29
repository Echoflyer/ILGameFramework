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
using System.Reflection;
using System;

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
            //object _procedureForm = GameEntry._ILRuntime._AppDomain.Instantiate("Hotfix.ProcedureForm");
            //Type _ttt = _procedureForm.GetType();
            //GameEntry._ILRuntime._AppDomain.Invoke("Hotfix.ProcedureForm", "LoadProcedure", _procedureForm,null);
            Type _type = GameEntry._ILRuntime._AppDomain.GetType("Hotfix.ProcedureForm").ReflectionType;
            object _procedureForm = GameEntry._ILRuntime._AppDomain.Instantiate("Hotfix.ProcedureForm");
            PropertyInfo _fiForms = _type.GetProperty("ProcedureForms");
            PropertyInfo _fiStart = _type.GetProperty("ProcedureStart");
            string[] _fromValue = (string[])_fiForms.GetValue(_procedureForm, null);
            string _startValue = (string)_fiStart.GetValue(_procedureForm, null);

            if (_fromValue == null || _startValue == null
                || _fromValue.Length <= 0 || string.IsNullOrEmpty(_startValue))
            {
                Log.Error("无法增加热更新流程--热更新ProcedureForm参数不对");
                return;
            }

            Type[] _hotfixProcedureTypes = new Type[_fromValue.Length];
            ProcedureBase[] _hotfixProcedure = new ProcedureBase[_fromValue.Length];
            Type _hotfixProcedureStart = null;
            for (int i = 0; i < _fromValue.Length; i++)
            {
                Type _typeValue = GameEntry._ILRuntime._AppDomain.GetType(_fromValue[i]).ReflectionType;
                _hotfixProcedure[i] = GameEntry._ILRuntime._AppDomain.Instantiate<ProcedureBase>(_fromValue[i], null);
                _hotfixProcedureTypes[i] = _typeValue;

                if (_startValue == _fromValue[i])
                    _hotfixProcedureStart = _hotfixProcedureTypes[i];
            }
            GameEntry.Procedure.HotfixProcedure(_hotfixProcedureTypes,_hotfixProcedure, _hotfixProcedureStart);
        }
#endregion

    }
}
