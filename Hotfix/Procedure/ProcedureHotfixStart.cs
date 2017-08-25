using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework;
using ILFramework;

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
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.WebRequestStart, OnWebRequestStart);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.WebRequestSuccess, OnWebRequestSuccess);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.WebRequestFailure, OnWebRequestFailure);

            GameEntry.WebRequest.AddWebRequest("https://www.baidu.com");
            // UnityEngine.GameObject _go = new UnityEngine.GameObject();
            // _go.AddComponent(System.Type.GetType(""));
            
            Log.Debug("Class1 OnEnter");
          //     ChangeState<ILFramework.ProcedureTest>(procedureOwner);
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
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.WebRequestStart, OnWebRequestStart);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.WebRequestSuccess, OnWebRequestSuccess);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.WebRequestFailure, OnWebRequestFailure);

            Log.Debug("OnLeave Base Front!!");
            base.OnLeave(procedureOwner,isShutdown);
            Log.Debug("OnLeave Base Back!!");
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
           // Log.Debug("OnUpdate:" + elapseSeconds + "**" + realElapseSeconds);
        }

        private void OnWebRequestStart(object sender, GameFramework.Event.GameEventArgs e)
        {
            Log.Debug("ProcedureHotfixStart--OnWebRequestStart");
        }
        private void OnWebRequestSuccess(object sender, GameFramework.Event.GameEventArgs e)
        {
            Log.Debug("ProcedureHotfixStart--OnWebRequestSuccess");
            UnityGameFramework.Runtime.WebRequestSuccessEventArgs _ne = (UnityGameFramework.Runtime.WebRequestSuccessEventArgs)e;
            string _content = Utility.Converter.GetString(_ne.GetWebResponseBytes());
            Log.Debug("ProcedureHotfixStart--OnWebRequestSuccess:"+ _content);
        }
        private void OnWebRequestFailure(object sender, GameFramework.Event.GameEventArgs e)
        {
            Log.Debug("ProcedureHotfixStart--OnWebRequestFailure");
        }
    }
}
