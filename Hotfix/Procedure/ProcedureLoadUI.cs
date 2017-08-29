using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework;
using ILFramework;
using GameFramework.Event;
using GameFramework.DataTable;

namespace Hotfix
{
    class ProcedureLoadUI: ProcedureBase
    {
#region 重写函数
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.UI.OpenUIForm(AssetUtility.GetUIFormAsset("TestUIForm"), "UI");
        }
        
        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }
        #endregion

        #region 回调函数

        #endregion

        //public new System.Type GetType()
        //{
        //   return typeof(ProcedureLoadUI);
        //}
    }
}
