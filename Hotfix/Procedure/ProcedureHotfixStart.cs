using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using GameFramework;

namespace Hotfix
{
    public class ProcedureHotfixStart: ProcedureBase
    {
        //
        // 摘要:
        //     /// 进入状态时调用。 ///
        //
        // 参数:
        //   procedureOwner:
        //     流程持有者。
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            Debug.Log("游戏版本:" + Application.version);
            Debug.Log("Class 1 OnEnter");
            GameObject _go = new GameObject("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            _go.AddComponent<UnityEngine.UI.Button>();
            _go.AddComponent<LineRenderer>();
            Log.Debug("Class1 OnEnter");
               ChangeState<ILFramework.ProcedureTest>(procedureOwner);
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
            Log.Debug("OnLeave Base Front!!");
            base.OnLeave(procedureOwner,isShutdown);
            Log.Debug("OnLeave Base Back!!");
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            Log.Debug("OnUpdate:" + elapseSeconds + "**" + realElapseSeconds);
        }

    }
}
