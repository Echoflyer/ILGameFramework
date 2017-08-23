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
        private string[] _ProcedureForms=new string[] { "Hotfix.ProcedureHotfixStart" };
        public string[] ProcedureForms { get { return _ProcedureForms; } }

        //流程开始
        private string _ProcedureStart = "Hotfix.ProcedureHotfixStart";
        public string ProcedureStart { get { return _ProcedureStart; } }
    }
}
