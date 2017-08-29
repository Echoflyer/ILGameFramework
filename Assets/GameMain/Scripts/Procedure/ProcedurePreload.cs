//-----------------------------------------------------------------------
// <copyright file="ProcedureStart.cs" company="Codingworks Game Development">
//     Copyright (c) codingworks. All rights reserved.
// </copyright>
// <author> codingworks </author>
// <time> #CREATETIME# </time>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Procedure;
using GameFramework.Fsm;
using GameFramework;

namespace ILFramework
{
    public class ProcedurePreload : ProcedureBase
    {
        #region 属性

        #endregion

        #region 重写函数
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
        }
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
            base.OnUpdate(procedureOwner,elapseSeconds,realElapseSeconds);

            ChangeState<ProcedureLoadHotfix>(procedureOwner);
        }
#endregion

        #region 回调函数
        #endregion

    }
}
