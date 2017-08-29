using GameFramework.Procedure;
using ILFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotfix
{
    class ProcedureForm
    {
        //流程列表
        private string[] _ProcedureForms=new string[] { "Hotfix.ProcedureHotfixStart", "Hotfix.ProcedureLoadUI", "Hotfix.ProcedureSetUILogic" };
        public string[] ProcedureForms { get { return _ProcedureForms; } }

        //流程开始
        private string _ProcedureStart = "Hotfix.ProcedureHotfixStart";
        public string ProcedureStart { get { return _ProcedureStart; } }

        public void LoadProcedure()
        {
            List<ProcedureBase> _hotfixProcedure = new List<ProcedureBase>();
            _hotfixProcedure.Add(new ProcedureHotfixStart());
            _hotfixProcedure.Add(new ProcedureLoadUI());
            _hotfixProcedure.Add(new ProcedureSetUILogic());
            ProcedureBase _hotfixProcedureStart = _hotfixProcedure[0];
            //  GameEntry.Procedure.HotfixProcedure(_hotfixProcedure.ToArray(), _hotfixProcedureStart);
         //   GameEntry.Fsm.CreateFsm<>

        }

    }
}
