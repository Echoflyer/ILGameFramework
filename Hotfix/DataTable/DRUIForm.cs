using GameFramework.DataTable;
using System.Collections.Generic;

namespace Hotfix
{
    /// <summary>
    /// 界面配置表。
    /// </summary>
    public class DRUIForm : IDataRow
    {
        /// <summary>
        /// 界面编号。
        /// </summary>
        public int Id
        {
            get;
            protected set;
        }

        /// <summary>
        /// 资源名称。
        /// </summary>
        public string AssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// 界面组名称。
        /// </summary>
        public string UIGroupName
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否允许多个界面实例。
        /// </summary>
        public bool AllowMultiInstance
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否暂停被其覆盖的界面。
        /// </summary>
        public bool PauseCoveredUIForm
        {
            get;
            private set;
        }

        /// <summary>
        /// 热更新重写的UIFormLogic
        /// </summary>
        public string HotfixUIFormLogic
        {
            get;
            private set;
        }

        public void ParseDataRow(string dataRowText)
        {
            string[] text = dataRowText.Split('\t');
         int index = 0;
            index++; // 跳过注释列
            Id = int.Parse(text[index++]);
            index++; // 跳过备注列
            AssetName = text[index++];
            UIGroupName = text[index++];
            AllowMultiInstance = bool.Parse(text[index++]);
            PauseCoveredUIForm = bool.Parse(text[index++]);
            HotfixUIFormLogic=text[index++];
        }

        private void AvoidJIT()
        {
            new Dictionary<int, DRUIForm>();
        }
    }
}
