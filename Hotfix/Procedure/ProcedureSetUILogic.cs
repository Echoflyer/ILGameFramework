using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework;
using ILFramework;
using GameFramework.Event;
using GameFramework.DataTable;

namespace Hotfix
{
    class ProcedureSetUILogic: ProcedureBase
    {
#region 重写函数
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Info("切换状态成功--ProcedureSetUILogic");
            //GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.LoadDataTableSuccess, OnLoadDataTableSuccess);
            //GameEntry.Event.Subscribe(UnityGameFramework.Runtime.EventId.LoadDataTableSuccess, OnLoadDataTableFailure);

            //GameEntry.DataTable.LoadDataTable<DRUIForm>("UIForm",AssetUtility.GetDataTableAsset("UIForm"));
        //    GameEntry.UI.SetHotfixUIFormLogic();
        }
        
        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            //GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.LoadDataTableSuccess, OnLoadDataTableSuccess);
            //GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.EventId.LoadDataTableSuccess, OnLoadDataTableFailure);

            base.OnLeave(procedureOwner, isShutdown);
        }
        #endregion

        #region 回调函数
        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            // 数据表加载成功事件
            UnityGameFramework.Runtime.LoadDataTableSuccessEventArgs ne = e as UnityGameFramework.Runtime.LoadDataTableSuccessEventArgs;
            Log.Info("Load data table '{0}' success.", ne.DataTableName);
            
            // 获得数据表
            IDataTable<DRUIForm> _dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            // 获得所有行
            DRUIForm[] _drUIForms = _dtUIForm.GetAllDataRows();
            if (_drUIForms.Length == 0)
                return;
            System.Collections.Generic.Dictionary<int, System.Type> _value = new System.Collections.Generic.Dictionary<int, System.Type>();
            foreach (var item in _drUIForms)
            {
                if (item != null)
                {
                    // 此行存在，可以获取内容了
                    if (GameEntry.UI.GetUIGroup(item.UIGroupName) == null)
                        GameEntry.UI.AddUIGroup(item.UIGroupName);
                    System.Type _type=  GameEntry._ILRuntime._AppDomain.GetType(item.HotfixUIFormLogic).ReflectionType;
                    if(_type.BaseType== typeof(UnityGameFramework.Runtime.UIFormLogic))
                        _value.Add(item.Id, _type);
                }
            }
            if (_value == null || _value.Count <= 0)
                return;
            GameEntry.UI.SetHotfixUIFormLogic(_value);
        }
        private void OnLoadDataTableFailure(object sender, GameEventArgs e)
        {
            // 数据表加载失败事件
            UnityGameFramework.Runtime.LoadDataTableFailureEventArgs ne = e as UnityGameFramework.Runtime.LoadDataTableFailureEventArgs;
            Log.Warning("Load data table '{0}' failure.", ne.DataTableName);
        }
        #endregion

        //public new System.Type GetType()
        //{
        //   return  typeof(ProcedureSetUILogic);
        //}
    }
}
